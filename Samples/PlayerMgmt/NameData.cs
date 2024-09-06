using UnityEngine;
using EMullen.Core;

namespace EMullen.Core.Samples 
{
    public class NameData : IPlayerData
    {

        public string Name = "PlayerName";

        public object Deserialize(string data)
        {
            Name = data;
            return data;
        }

        public string Serialize()
        {
            return Name;
        }
    }
}