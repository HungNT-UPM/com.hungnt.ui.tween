using UnityEngine;

namespace HungNT.UI.UITween
{
    /// <summary>
    /// Load hoặc tạo asset <see cref="TweenConfig"/> trong Assets/Resources/Tween.
    /// </summary>
    public static class TweenConfigLoader
    {
        /// <summary>
        /// Load từ Resources hoặc tạo asset mới trên disk (Editor).
        /// </summary>
        public static TweenConfig LoadOrCreate(string configName)
        {
            TweenConfig config = Resources.Load<TweenConfig>(TweenDefine.ConfigResourcePath(configName));
            if (config != null)
                return config;

#if UNITY_EDITOR
            string assetPath = TweenDefine.ConfigAssetPath(configName);
            config = UnityEditor.AssetDatabase.LoadAssetAtPath<TweenConfig>(assetPath);
            if (config != null)
                return config;

            if (!System.IO.Directory.Exists(TweenDefine.ProjectConfigFolder))
                System.IO.Directory.CreateDirectory(TweenDefine.ProjectConfigFolder);

            config = ScriptableObject.CreateInstance<TweenConfig>();
            UnityEditor.AssetDatabase.CreateAsset(config, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            return config;
#else
            return ScriptableObject.CreateInstance<TweenConfig>();
#endif
        }

        public static TweenConfig LoadOrCreateShow(string className) => LoadOrCreate(TweenDefine.ShowConfigName(className));

        public static TweenConfig LoadOrCreateHide(string className) => LoadOrCreate(TweenDefine.HideConfigName(className));
    }
}