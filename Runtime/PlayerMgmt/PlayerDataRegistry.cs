using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using FishNet.Managing;
using Codice.Client.BaseCommands.BranchExplorer;

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
        /// TODO: When networked, why don't we just reference the synced version if we need to
        private Dictionary<string, PlayerData> playerDatas = new();

        private Dictionary<string, PlayerData> PlayerDatas { get {
#if FISHNET
            Debug.LogWarning("TODO: Check if connected to a server using the NetworkController");      
            // TODO: Check if connected to a server using the NetworkController
            return PlayerDataNetworkedRegistry.Instance.PlayerDatas.Value; 
#else
            return playerDatas;
#endif
        } }

        public bool IsPlayerRegistered(string uid) => PlayerDatas.ContainsKey(uid);

        public void RegisterPlayer(PlayerData newData) 
        {
            if(!newData.HasData<IdentifierData>()) {
                Debug.LogError("Can't register new PlayerData. Id doesn't have any IdentifierData.");
                return;
            }
            string uid = newData.GetData<IdentifierData>().uid;
            if(PlayerDatas.ContainsKey(uid)) {
                Debug.LogError("Can't register new PlayerData: The uid provided is already registered with this PlayerDataRegistry.");
                return;
            }
            PlayerDatas.Add(uid, newData);
        }

        public PlayerData GetPlayerData(string uid) 
        {
            if(!PlayerDatas.ContainsKey(uid)) {
                Debug.LogError("Cant get PlayerData, it is not registered with this PlayerDataRegistry.");
                return null;
            }
            return PlayerDatas[uid];
        }

        public PlayerData[] GetAllData() 
        {
            if(PlayerDatas.Values.Count == 0)
                return new PlayerData[0];

            return PlayerDatas.Values.ToArray();
        }

    }
}