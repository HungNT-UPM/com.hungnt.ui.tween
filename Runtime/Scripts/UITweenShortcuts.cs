using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Wrapper tween UI dùng DOTween core (DOTween.To).
    /// DOTweenModuleUI (DOFade, DOAnchorPos) nằm ở Assembly-CSharp-firstpass — package UPM không reference được.
    /// </summary>
    internal static class UITweenShortcuts
    {
        public static TweenerCore<float, float, FloatOptions> Fade(CanvasGroup target, float endValue, float duration)
        {
            return DOTween.To(() => target.alpha, x => target.alpha = x, endValue, duration)
                .SetTarget(target);
        }

        public static TweenerCore<Vector2, Vector2, VectorOptions> AnchorPos(RectTransform target, Vector2 endValue, float duration)
        {
            return DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, endValue, duration)
                .SetTarget(target);
        }
    }
}
