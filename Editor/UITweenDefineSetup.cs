using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace HungNT.UI.Tween.Editor
{
    [InitializeOnLoad]
    static class UITweenDefineSetup
    {
        const string Symbol = "UNITASK_DOTWEEN_SUPPORT";

        static UITweenDefineSetup()
        {
            foreach (BuildTargetGroup buildTargetGroup in Enum.GetValues(typeof(BuildTargetGroup)))
            {
                if (buildTargetGroup == BuildTargetGroup.Unknown)
                    continue;

                try
                {
                    var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
                    EnsureSymbol(namedBuildTarget);
                }
                catch (ArgumentException)
                {
                }
            }
        }

        static void EnsureSymbol(NamedBuildTarget namedBuildTarget)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget)
                .Split(';')
                .Where(d => !string.IsNullOrEmpty(d))
                .ToList();

            if (defines.Contains(Symbol))
                return;

            defines.Add(Symbol);
            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, string.Join(";", defines));
        }
    }
}
