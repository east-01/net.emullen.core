using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EMullen.Core.Editor
{
    public class BetterLoggerWindow : EditorWindow
    {

        private static Vector2 scrollPos;

        [MenuItem("Window/Better Logger")]
        public static void ShowWindow() 
        {
            GetWindow<BetterLoggerWindow>("Better Logger");
        }

        private void OnGUI() 
        {
            // Dictionary<LogChannel, LogChannelData> data = BLog.Settings.channelDatas;
            // int verbosity = BLog.Settings.verbosity;

            // GUILayout.Space(5);

            // bool save = false;
            // int initialVerbosity = verbosity;
            // verbosity = EditorGUILayout.IntSlider("Verbosity:", verbosity, 0, BLog.MAX_VERBOSITY);
            // if(verbosity != initialVerbosity)
            //     save = true;

            // GUILayout.Space(5);

            // EditorUtilities.CreateHeader("Channels:");

            // scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            // foreach(LogChannel channel in Enum.GetValues(typeof(LogChannel))) 
            // {
            //     if(channel == LogChannel.Default)
            //         continue;

            //     EditorGUILayout.BeginHorizontal();

            //     GUILayout.Label(FormatEnum(channel.ToString()), new GUILayoutOption[] {GUILayout.Width(125)});

            //     LogChannelData channelData = data.ContainsKey(channel) ? data[channel] : LogChannelData.DefaultData;

            //     bool initialEnable = channelData.enable;
            //     channelData.enable = EditorGUILayout.Toggle(channelData.enable);
            //     if(channelData.enable != initialEnable)
            //         save = true;

            //     Color initialColor = channelData.color;
            //     channelData.color = EditorGUILayout.ColorField(channelData.color, new GUILayoutOption[] {GUILayout.Width(100)});
            //     if(channelData.color != initialColor)
            //         save = true;

            //     data[channel] = channelData;

            //     EditorGUILayout.EndHorizontal();            
            // }

            // EditorGUILayout.EndScrollView();


            // if(save) {
            //     BetterLoggerSettings newSettings = new() {
            //         channelDatas = data,
            //         verbosity = verbosity
            //     };
                
            //     BLog.Settings = newSettings;
            //     BLog.SaveSettings();
            // }
        }

        
    }
}