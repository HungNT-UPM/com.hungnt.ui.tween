using UnityEditor;
using UnityEngine;

namespace HungNT.UI.UITween.Editor
{
    public static class TweenConfigMenu
    {
        static readonly string[] ConfigClassNames =
        {
            nameof(UITweenFade),
            nameof(UITweenMove),
            nameof(UITweenScale),
            nameof(UITweenScaleFade),
            nameof(UITweenMoveFade),
            nameof(UITweenRotate),
        };

        [MenuItem("HungNT/UI/Create Default Tween Configs (Show)")]
        public static void CreateShowConfigs()
        {
            for (int i = 0; i < ConfigClassNames.Length; i++)
                TweenConfigLoader.LoadOrCreateShow(ConfigClassNames[i]);

            AssetDatabase.Refresh();
            Debug.Log($"Đã tạo TweenConfig show tại {TweenDefine.ProjectConfigFolder}");
        }

        [MenuItem("HungNT/UI/Create Default Tween Configs (Hide)")]
        public static void CreateHideConfigs()
        {
            for (int i = 0; i < ConfigClassNames.Length; i++)
                TweenConfigLoader.LoadOrCreateHide(ConfigClassNames[i]);

            AssetDatabase.Refresh();
            Debug.Log($"Đã tạo TweenConfig hide tại {TweenDefine.ProjectConfigFolder}");
        }
    }
}
