using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EMullen.Core 
{
    /// <summary>
    /// The PlayerDataRegistry stores all PlayerData for the local instance. If using a networked
    ///   setup, the data will sync with the server.
    /// </summary>
    public class PlayerDataRegistry 
    {
        /// <summary>
        /// All tracked PlayerData objects, mapped using their IdentifierData#uid value.
        /// </summary>
        private readonly Dictionary<string, PlayerData> playerDatas = new();

        public bool IsPlayerRegistered(string uid) => playerDatas.ContainsKey(uid);

        public void RegisterPlayer(PlayerData newData) 
        {
            if(!newData.HasData<IdentifierData>()) {
                Debug.LogError("Can't register new PlayerData. Id doesn't have any IdentifierData.");
                return;
            }
            string uid = newData.GetData<IdentifierData>().uid;
            if(playerDatas.ContainsKey(uid)) {
                Debug.LogError("Can't register new PlayerData: The uid provided is already registered with this PlayerDataRegistry.");
                return;
            }
            playerDatas.Add(uid, newData);
        }

        public PlayerData GetPlayerData(string uid) 
        {
            if(!playerDatas.ContainsKey(uid)) {
                Debug.LogError("Cant get PlayerData, it is not registered with this PlayerDataRegistry.");
                return null;
            }
            return playerDatas[uid];
        }

        public PlayerData[] GetAllData() 
        {
            if(playerDatas.Values.Count == 0)
                return new PlayerData[0];

            return playerDatas.Values.ToArray();
        }

    }
}