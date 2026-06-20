namespace HungNT.UI.Tween
{
    /// <summary>
    /// Đường dẫn và quy ước tên asset <see cref="TweenConfig"/> trong project.
    /// </summary>
    public static class TweenDefine
    {
        public const string ProjectConfigFolder = "Assets/Resources/Tween";
        public const string ResourcesLoadRoot = "Tween";
        public const string ConfigMenuName = "HungNT/UI/Tween Config";

        public static string ConfigResourcePath(string name) => $"{ResourcesLoadRoot}/{name}";

        public static string ConfigAssetPath(string name) => $"{ProjectConfigFolder}/{name}.asset";

        public static string ShowConfigName(string className) => $"{className}_Show";

        public static string HideConfigName(string className) => $"{className}_Hide";
    }
}