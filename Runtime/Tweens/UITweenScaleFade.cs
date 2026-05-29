using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.UITween
{
    public class UITweenScaleFade : UITweenBase
    {
        [Title("Scale")]
        [SerializeField] private Vector2 _inactiveScale;

        [SerializeField] private Vector2 _activeScale = Vector2.one;

        [Title("Fade")]
        [SerializeField] private float _inactiveAlpha;

        [SerializeField] private float _activeAlpha = 1f;

        protected override string ConfigTypeName => "Scale";

        private Vector3 InactiveScale => new Vector3(_inactiveScale.x, _inactiveScale.y, 1f);

        private Vector3 ActiveScale => new Vector3(_activeScale.x, _activeScale.y, 1f);

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);

            await DOTween.Sequence()
                .Join(RectTransform.DOScale(ActiveScale, ShowDuration).SetEase(ShowEase))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _activeAlpha, ShowDuration).SetEase(ShowEase))
                .SetDelay(DelayShow).OnComplete(Active)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
            if (!HasHideTweenConfig)
                return;

            await DOTween.Sequence()
                .Join(RectTransform.DOScale(InactiveScale, HideDuration).SetEase(HideEase))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _inactiveAlpha, HideDuration).SetEase(HideEase))
                .SetDelay(DelayHide).OnComplete(Inactive)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override void Active()
        {
            base.Active();
            CanvasGroup.alpha = _activeAlpha;
            Interactable = true;
            RectTransform.localScale = ActiveScale;
        }

        public override void Inactive()
        {
            base.Inactive();
            CanvasGroup.alpha = _inactiveAlpha;
            Interactable = false;
            RectTransform.localScale = InactiveScale;
        }
    }
}
