using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Tween alpha của CanvasGroup khi show/hide UI.
    /// </summary>
    public class UITweenFade : UITweenBase
    {
        [Title("Fade")]
        [SerializeField] private float _inactiveAlpha = 0f;
        [SerializeField] private float _activeAlpha = 1f;

        protected override string DefaultPresetResourcePath => "Tween/TweenPreset_Fade";

        /// <remarks>
        /// OnComplete gắn vào tween; nếu UniTask bị cancel thì Active có thể không được gọi.
        /// </remarks>
        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);
            await UITweenShortcuts.Fade(CanvasGroup, _activeAlpha, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: token);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            Interactable = false;

            await base.Hide(token);
            await UITweenShortcuts.Fade(CanvasGroup, _inactiveAlpha, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: token);
        }

        public override void Active()
        {
            base.Active();
            CanvasGroup.alpha = _activeAlpha;
            Interactable = true;
        }

        public override void Inactive()
        {
            base.Inactive();
            CanvasGroup.alpha = _inactiveAlpha;
            Interactable = false;
        }
    }
}
