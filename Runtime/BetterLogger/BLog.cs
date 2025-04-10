using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace EMullen.Core {
    public class BLog : MonoBehaviour
    {

        public static readonly string FILE_PATH = Application.streamingAssetsPath + "/BetterLoggerSettings.json";
        public static int MAX_VERBOSITY = 5;

        private static BLogChannel defaultChannel;
        public static BLogChannel DefaultChannel { get {
            if(defaultChannel == null) {
                defaultChannel = ScriptableObject.CreateInstance<BLogChannel>();
                defaultChannel.color = new Color(0.796f, 0.804f, 0.812f);
                defaultChannel.logName = "Default";
                defaultChannel.enable = true;
                defaultChannel.verbosity = 5;
                defaultChannel.showPrefix = false;
            }
            return defaultChannel;
        } }

        private static BLogChannel highlightChannel;
        public static BLogChannel HighlightChannel { get {
            if(highlightChannel == null) {
                highlightChannel = ScriptableObject.CreateInstance<BLogChannel>();
                highlightChannel.color = new Color(1f, 0.843f, 0f);
                highlightChannel.enable = true;
                highlightChannel.verbosity = 5;
                highlightChannel.isBold = true;
                highlightChannel.showPrefix = false;
            }
            return highlightChannel;
        } }

        /// <summary>
        /// A list of type names that we're already issued "no BLog channel" warnings for, warnings
        ///   will no longer be issued for types in this list.
        /// </summary>
        private static List<string> warnedTypeNames = new();

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
                UnityEngine.Debug.LogError($"Can't BLog. Provided verbosity is out of range. Provided {verbosity}, range [0, {MAX_VERBOSITY}]");
            }

            // Create a StackTrace that skips the first frame (this method)
            StackTrace stackTrace = new(1, true);

            if(channel == null) {
                channel = DefaultChannel;
                string typeName = stackTrace.GetFrame(0).GetMethod().GetType().Name;
                if(!warnedTypeNames.Contains(typeName)) {
                    UnityEngine.Debug.LogWarning($"No channel provided for log message coming from type \"{typeName}\" either add a BLog channel or ensure it's reference is established.");
                    warnedTypeNames.Add(typeName);
                }
            }

            if(!channel.enable)
                return;

            // If this messages verbosity is greater than the limit, don't print
            if(verbosity > channel.verbosity)
                return;

            string color = ColorUtility.ToHtmlStringRGB(channel.color);
            string prefix = "";
            if(channel.showPrefix)
                prefix = $"[{channel.logName}@{verbosity}] ";

            if(channel.isBold)
                message = $"<b>{message}</b>";

            UnityEngine.Debug.Log($"<color=#{color}>{prefix}{message}</color>\n{stackTrace}");
        }
        
        // TODO: Implement string channels
        public static void Log(string message, string channelID, int verbosity = 0) 
        {
            Log(message, (BLogChannel)null, verbosity);
        }

        public static void Highlight(string message) => Log(message, HighlightChannel);
    }
}