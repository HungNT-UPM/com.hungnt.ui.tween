using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.UITween
{
    public class UITweenFade : UITweenBase
    {
        [Title("Fade")]
        [SerializeField] private float _inactiveAlpha;

        [SerializeField] private float _activeAlpha = 1f;

        protected override string ConfigTypeName => "Fade";

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);
            await UITweenShortcuts.Fade(CanvasGroup, _activeAlpha, ShowDuration)
                .SetEase(ShowEase).SetDelay(DelayShow)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
            if (!HasHideTweenConfig)
                return;

            Interactable = false;
            await UITweenShortcuts.Fade(CanvasGroup, _inactiveAlpha, HideDuration)
                .SetEase(HideEase).SetDelay(DelayHide)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: TweenToken);
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
