using UnityEditor;

namespace EMullen.Core 
{
    /// <summary>
    /// This IdentifierData is used by the PlayerManagement to keep track of the PlayerData.
    /// The uid WILL NOT CHANGE once instantiated, this ensures you can safely reference the
    ///   same PlayerData each time.
    /// </summary>
    public class IdentifierData : IPlayerData
    {
        public string uid { get; private set; } 
        public int? localPlayerIndex { get; private set; }

        public IdentifierData(string uid, int? localPlayerIndex = null) 
        {
            this.uid = uid;
            this.localPlayerIndex = localPlayerIndex;
        }

        public IdentifierData(int? localPlayerIndex = null) 
        {
            this.uid = GUID.Generate().ToString();
            this.localPlayerIndex = localPlayerIndex;
        }

        public object Deserialize(string data)
        {
            throw new System.NotImplementedException();
        }

        public string Serialize()
        {
            return $"uid: {uid} idx: {localPlayerIndex}";
        }
    }
}