using System;
using System.Collections.Generic;
using FishNet.Managing.Scened;
using UnityEngine;

namespace EMullen.Core 
{
    public class SceneSingletons 
    {
        private static Dictionary<(SceneLookupData, Type), Data> _singletons;
        private static Dictionary<(SceneLookupData, Type), Data> singletons => _singletons ??= new();

        /// TODO: This class could use an advanced OOP pass, specifically relating to arguments.
        /// Why not use a IS3Args struct? this way arguments can be easily added/removed

        public static bool Contains(SceneLookupData lookupData, Type type) => singletons.ContainsKey((lookupData, type)) && singletons[(lookupData, type)].Singleton != null;
        public static object Get(SceneLookupData lookupData, Type type) 
        {
            if(!Contains(lookupData, type))
                throw new InvalidOperationException($"Can't get scene singleton object for scene \"{lookupData}\" and type \"{type}\". It doesn't exist.");

            return singletons[(lookupData, type)].Singleton;
        }

        public static void SubscribeToSingleton(IS3 subscriber, SceneLookupData lookupData, Type singletonType) 
        {
            if(IsSubscribed(subscriber, lookupData, singletonType)) {
                Debug.LogWarning("Can't subscribe to singleton, the subscriber is already subscribed.");
                return;
            }

            // We're assured this pair exists in the dictionary because GetCreate is called in SubscribeToSingleton
            singletons[(lookupData, singletonType)].subscribers.Add(subscriber);

            // If the singleton is already instantiated, call the registered method for the subscriber.
            subscriber.SingletonRegistered(singletonType, singletons[(lookupData, singletonType)].Singleton);
        }

        public static bool IsSubscribed(IS3 subscriber, SceneLookupData lookupData, Type singletonType) => GetCreate(lookupData, singletonType).subscribers.Contains(subscriber);
        
        public static bool Register(object singleton) 
        {
            if(singleton is not MonoBehaviour) {
                Debug.LogError("Can't register singleton object, it's not an instance of MonoBehaviour");
                return false;
            }

            MonoBehaviour obj = singleton as MonoBehaviour;
            Type type = singleton.GetType();
            SceneLookupData lookupData = new(obj.gameObject.scene.handle, obj.gameObject.scene.name);

            Data data = GetCreate(lookupData, type);

            if(data.Singleton != null) {
                Debug.LogError("Can't register scene singleton, it's already instantiated.");
                return false;
            }

            data.Singleton = singleton;
            data.subscribers.ForEach(sub => sub.SingletonRegistered(type, obj));
            return true;
        }

        private static Data GetCreate(SceneLookupData lookupData, Type singletonType) 
        {
            if(!singletons.TryGetValue((lookupData, singletonType), out Data data)) {
                data = new();
                singletons[(lookupData, singletonType)] = data;
            }
            return data;
        }

        class Data {
            private object singleton;
            public object Singleton {
                get {
                    // TODO: Catch if the singleton doesn't exist here and delete it before it throws an error.
                    return singleton;
                }
                set {
                    singleton = value;
                }
            }
            internal List<IS3> subscribers;
            
            public Data() 
            {
                subscribers = new();
            }
        }
    }

    /// <summary>
    /// IS3 stands for ISceneSingletonSubscriber, used for calling back when a singleton has loaded
    ///   will be called from the SceneSingletons class once the target singleton loads.
    /// </summary>
    public interface IS3
    {
        public void SingletonRegistered(Type type, object singleton);
        public void SingletonDeregistered(Type type, object singleton);
    }
}
