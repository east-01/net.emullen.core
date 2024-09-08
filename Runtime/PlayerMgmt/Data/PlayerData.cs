using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace EMullen.Core {
    /// <summary>
    /// This is data relating to the in game player.
    /// </summary>
    [Serializable]
    public class PlayerData : ISerializationCallbackReceiver {

        private readonly Dictionary<Type, PlayerDataClass> data;
        
        [SerializeField]
        private List<PlayerDataClass> serializedClasses;

        public List<Type> Types => data.Keys.ToList();
        public List<string> TypeNames => Types.Select(type => type.Name).ToList();
        public List<PlayerDataClass> Datas => data.Values.ToList();

        public PlayerData() {
            data = new();
            UnityEngine.Debug.Log("PlayerData was initialiezd via default constructor, ensure it gets IdentifierData");
        }

        public PlayerData(int playerIndex) 
        {
            data = new();
            SetData(new IdentifierData(playerIndex));
        }

        public PlayerData(IdentifierData identifierData) {
            data = new();
            SetData(identifierData);
        }

#region Data Management
        public bool HasData<T>() where T : PlayerDataClass => data.ContainsKey(typeof(T));
        public T GetData<T>() where T : PlayerDataClass
        {
            if(!HasData<T>()) {
                UnityEngine.Debug.LogError($"Failed to retrieve data of type \"{typeof(T)}\" Returned default values. Use PlayerData#HasData<{typeof(T)}> to ensure this player has the data before retrieving it.");
                return default(T);
            }
            return (T) data[typeof(T)];
        }

        public void SetData<T>(T data) where T : PlayerDataClass
        {
            if(HasData<T>()) {
                this.data[typeof(T)] = data;
            } else {
                this.data.Add(typeof(T), data);
            }
        }

        public void ClearData<T>() where T : PlayerDataClass 
        {
            if(!HasData<T>()) {
                UnityEngine.Debug.LogError($"Can't clear data of type \"{typeof(T)}\"");
                return;
            }
            data.Remove(typeof(T));
        }
#endregion

#region Serializers
        public void OnBeforeSerialize()
        {
            serializedClasses.Concat(data.Values);         
        }

        public void OnAfterDeserialize()
        {
            foreach(PlayerDataClass cls in serializedClasses) {
                data.Add(cls.GetType(), cls);
            }
        }
#endregion

    }

    /// <summary>
    /// This class exists to enforce child classes to be Serializable, that way we can network
    ///   the information if necessary.
    /// </summary>
    [Serializable]
    public abstract class PlayerDataClass {}
}
