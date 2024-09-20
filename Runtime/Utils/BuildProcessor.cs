using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif
using System.IO;

namespace EMullen.Core {
    public class BuildProcessor 
#if UNITY_EDITOR
    : IPreprocessBuildWithReport, IPostprocessBuildWithReport
#endif
    {
        public int callbackOrder => 0;
        public static readonly string JSONDirectory = Application.streamingAssetsPath;
        public static readonly string JSONPath = Path.Combine(JSONDirectory, "SceneList.json");

#region Data retrieving
        private static BuildSettingsScene[] scenes;

        public static BuildSettingsScene[] Scenes { get {
            if(scenes == null) {
#if UNITY_EDITOR  
                EditorBuildSettingsScene[] editorScenes = EditorBuildSettings.scenes;
                scenes = new BuildSettingsScene[editorScenes.Length];
                for(int i = 0; i < editorScenes.Length; i++) {
                    scenes[i] = new() {
                        index = i,
                        path = editorScenes[i].path,
                        enabled = editorScenes[i].enabled
                    };
                }
                Debug.Log($"BuildProcessor: Loaded {scenes.Length} scenes");
#else
                if(!File.Exists(JSONPath)) {
                    Debug.LogError("Failed to get Build Scene data. Program can't continue.");
                    Application.Quit();
                }
                string json = File.ReadAllText(JSONPath);
                scenes = JsonUtility.FromJson<BuildSettings>(json).scenes;
                Debug.Log($"BuildProcessor: Retrieved {scenes.Length} scenes");
#endif
            }
            return scenes;
        } }
#endregion

#region Data writing

#if UNITY_EDITOR
        public void OnPreprocessBuild(BuildReport report) 
        {
            BLog.Highlight("Scenes len:" + Scenes.Length);
            BuildSettings settings = new() {
                scenes = Scenes
            };

            string json = JsonUtility.ToJson(settings);
            if(!Directory.Exists(JSONDirectory))
                Directory.CreateDirectory(JSONDirectory);

            File.WriteAllText(JSONPath, json);
            BLog.Highlight(json);
            BLog.Highlight("Saved to " + JSONPath);
        }

        public void OnPostprocessBuild(BuildReport report)
        {
        }
#endif

    }
#endregion

    [Serializable]
    public struct BuildSettingsScene 
    {
        public int index;
        public bool enabled;
        public string path;
    }

    [Serializable]
    public struct BuildSettings 
    {
        public BuildSettingsScene[] scenes;
    }

}