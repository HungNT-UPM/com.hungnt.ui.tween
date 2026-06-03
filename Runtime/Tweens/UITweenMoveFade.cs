using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    public class UITweenMoveFade : UITweenBase
    {
        [Title("Move")]
        [SerializeField] private Vector2 _offset;

        [Title("Fade")]
        [SerializeField] private float _inactiveAlpha;

        [SerializeField] private float _activeAlpha = 1f;

        private Vector2 _activeAnchorPos;
        private Vector2 _inactiveAnchorPos;

        protected override string ConfigTypeName => "Move";

        public override void Init()
        {
            base.Init();
            _activeAnchorPos = RectTransform.anchoredPosition;
            _inactiveAnchorPos = _activeAnchorPos + _offset;
        }

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);

            await DOTween.Sequence()
                .Join(UITweenShortcuts.AnchorPos(RectTransform, _activeAnchorPos, ShowDuration).SetEase(ShowEase))
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
                .Join(UITweenShortcuts.AnchorPos(RectTransform, _inactiveAnchorPos, HideDuration).SetEase(HideEase))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _inactiveAlpha, HideDuration).SetEase(HideEase))
                .SetDelay(DelayHide).OnComplete(Inactive)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override void Active()
        {
            base.Active();
            RectTransform.anchoredPosition = _activeAnchorPos;
            CanvasGroup.alpha = _activeAlpha;
            Interactable = true;
        }

        public override void Inactive()
        {
            base.Inactive();
            RectTransform.anchoredPosition = _inactiveAnchorPos;
            CanvasGroup.alpha = _inactiveAlpha;
            Interactable = false;
        }
    }
}
