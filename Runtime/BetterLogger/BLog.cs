using UnityEngine;

namespace EMullen.Core {
    public class BLog : MonoBehaviour
    {

        public static readonly string FILE_PATH = Application.streamingAssetsPath + "/BetterLoggerSettings.json";
        public static int MAX_VERBOSITY = 5;

        /// <summary>
        /// Log a message to unitys Debug#Log. Provide a channel argument to send a log through
        ///   that channel. Provide a verbosity level to add different levels of detail to log
        ///   messages; if the messages verbosity is greater than the limit it will not be logged.
        /// 
        /// Recommended verbosity levels:
        /// 0. Messages that will always be printed
        /// 1. Startup/shutdown messages
        /// 2. Initialization messages
        /// 3. 
        /// 4. 
        /// 5. 
        /// </summary>
        /// <param name="message">The log message to send.</param>
        /// <param name="channel">The channel that the message is sent through</param>
        /// <param name="verbosity">The verbosity of the message</param>
        public static void Log(string message, BLogChannel channel = null, int verbosity = 0) 
        {            
            if(verbosity < 0 || verbosity > MAX_VERBOSITY) {
                Debug.LogError($"Can't BLog. Provided verbosity is out of range. Provided {verbosity}, range [0, {MAX_VERBOSITY}]");
            }
            // If this messages verbosity is greater than the limit, don't print
            if(verbosity > channel.verbosity)
                return;

            string color = "#c9c9c9";
            string prefix = "";
            if(channel != null) {
                if(!channel.enable)
                    return;
                color = ColorUtility.ToHtmlStringRGB(channel.color);
                prefix = $"[{channel.logName}@{verbosity}] ";
            }

            Debug.Log($"<color=#{color}>{prefix}{message}</color>");
        }

        public static void Highlight(string message) 
        {
            Debug.Log($"<color=#FFD700><b>{message}</b></color>");        
        }
    }
}