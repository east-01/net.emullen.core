using UnityEngine;

namespace EMullen.Core 
{
    [CreateAssetMenu(fileName = "BLogChannel", menuName = "Better Logger/Create BLog Channel")]
    public class BLogChannel : ScriptableObject
    {
        public bool enable;
        public string logName;
        public int verbosity;
        public Color color;
    }
}