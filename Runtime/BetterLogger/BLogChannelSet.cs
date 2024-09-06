using UnityEngine;

namespace EMullen.Core 
{
    [CreateAssetMenu(fileName = "BLogChannelSet", menuName = "Better Logger/Create BLog Channel Set")]
    public class BLogChannelSet : ScriptableObject 
    {
        public BLogChannel[] channels;

        public BLogChannel Decode(int channelIndex) => channels[channelIndex];
    }
}