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
    public class PlayerData {
        private Dictionary<Type, IPlayerData> allPlayerData;
        public List<Type> Types => allPlayerData.Keys.ToList();
        public List<IPlayerData> Datas => allPlayerData.Values.ToList();

        public PlayerData(int playerIndex) 
        {
            allPlayerData = new();
            SetData(new IdentifierData(playerIndex));
        }

        public PlayerData(IdentifierData identifierData) {
            allPlayerData = new();
            SetData(identifierData);
        }

        public bool HasData<T>() where T : IPlayerData => allPlayerData.ContainsKey(typeof(T));
        public T GetData<T>() where T : IPlayerData
        {
            if(!HasData<T>()) {
                UnityEngine.Debug.LogError($"Failed to retrieve data of type \"{typeof(T)}\" Returned default values. Use PlayerData#HasData<{typeof(T)}> to ensure this player has the data before retrieving it.");
                return default(T);
            }
            return (T) allPlayerData[typeof(T)];
        }

        public void SetData<T>(T data) where T : IPlayerData
        {
            if(HasData<T>()) {
                allPlayerData[typeof(T)] = data;
            } else {
                allPlayerData.Add(typeof(T), data);
            }
        }

        public void ClearData<T>() where T : IPlayerData 
        {
            if(!HasData<T>()) {
                UnityEngine.Debug.LogError($"Can't clear data of type \"{typeof(T)}\"");
                return;
            }
            allPlayerData.Remove(typeof(T));
        }

    }
}
