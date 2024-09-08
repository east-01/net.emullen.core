using UnityEngine;
using EMullen.Core;

namespace EMullen.Core.Samples 
{
    public class NameData : PlayerDataClass
    {
        public string Name = "PlayerName";
        public override string ToString() => Name;
    }
}