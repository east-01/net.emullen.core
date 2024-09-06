using System;

namespace EMullen.Core 
{
    public interface IPlayerData {
        public string Serialize();
        public Object Deserialize(String data);
    }
}