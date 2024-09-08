using UnityEngine;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;

namespace EMullen.Core 
{
    public class PlayerDataNetworkedRegistry : NetworkBehaviour
    {
        public static PlayerDataNetworkedRegistry Instance { get; private set;}
        
        internal SyncVar<Dictionary<string, PlayerData>> PlayerDatas { get; } = new();

        private void Awake() 
        {
            if(Instance != null) {
                Debug.LogWarning($"The PlayerDataNetworkedRegistry is a singleton and is already instantiated, destroying owner GameObject \"{gameObject.name}\"");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

    }
}