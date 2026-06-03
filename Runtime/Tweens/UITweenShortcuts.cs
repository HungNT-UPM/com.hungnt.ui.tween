using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Wrapper DOTween UI - package UPM không reference DOTweenModuleUI.
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

        public static TweenerCore<Vector2, Vector2, VectorOptions> SizeDelta(RectTransform target, Vector2 endValue, float duration)
        {
            return DOTween.To(() => target.sizeDelta, x => target.sizeDelta = x, endValue, duration)
                .SetTarget(target);
        }
    }
}
