using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// ScriptableObject chứa ease và duration mặc định cho show/hide của <see cref="UITweenBase"/>.
    /// </summary>
    [CreateAssetMenu(menuName = TweenConstant.TweenPresetMenuName, fileName = TweenConstant.TweenPresetFileName)]
    public class TweenPreset : ScriptableObject
    {
        [HorizontalGroup("In"), LabelWidth(100)]
        public Ease easeIn = Ease.Linear;

        [HorizontalGroup("Out"), LabelWidth(100)]
        public Ease easeOut = Ease.Linear;

        [HorizontalGroup("In"), LabelWidth(100)]
        public float durationIn = 0.25f;

        [HorizontalGroup("Out"), LabelWidth(100)]
        public float durationOut = 0.25f;
    }
}
