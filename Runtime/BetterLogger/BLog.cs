using UnityEngine;

namespace EMullen.Core {
    public class BLog : MonoBehaviour
    {

        public static readonly string FILE_PATH = Application.streamingAssetsPath + "/BetterLoggerSettings.json";
        public static int MAX_VERBOSITY = 5;

        public static void Log(string message, BLogChannel channel = null, int verbosity = 0) 
        {            
            if(verbosity < 0 || verbosity > MAX_VERBOSITY) {
                Debug.LogError($"Can't BLog. Provided verbosity is out of range. Provided {verbosity}, range [0, {MAX_VERBOSITY}]");
            }
            // If this messages verbosity is greater than the limit, don't print
            if(verbosity > channel.verbosity)
                return;

            string color = "#c9c9c9";
            if(channel != null) {
                if(!channel.enable)
                    return;
                color = ColorUtility.ToHtmlStringRGB(channel.color);
            }

            Debug.Log($"<color={color}>{message}</color>");
        }

        public static void Highlight(string message) 
        {
            Debug.Log($"<color=#FFD700><b>{message}</b></color>");        
        }
    }
}