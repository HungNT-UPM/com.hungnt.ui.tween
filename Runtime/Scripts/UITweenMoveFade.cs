using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Chạy move và fade song song trên cùng một RectTransform.
    /// </summary>
    public class UITweenMoveFade : UITweenBase
    {
        [Title("Move")]
        [SerializeField] private Vector2 _offset;

        [Title("Fade")]
        [SerializeField] private float _inactiveAlpha = 0f;
        [SerializeField] private float _activeAlpha = 1f;

        private Vector2 _activeAnchorPos;
        private Vector2 _inactiveAnchorPos;

        protected override string DefaultPresetResourcePath => "Tween/TweenPreset_Move";

        public override void Init()
        {
            base.Init();

            _activeAnchorPos = RectTransform.anchoredPosition;
            _inactiveAnchorPos = _activeAnchorPos + _offset;
        }

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);

            var sequence = DOTween.Sequence();
            sequence.Join(UITweenShortcuts.AnchorPos(RectTransform, _activeAnchorPos, DurationIn).SetEase(EaseIn))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _activeAlpha, DurationIn).SetEase(EaseIn))
                .SetDelay(DelayIn)
                .OnComplete(Active);

            await sequence.ToUniTask(cancellationToken: token);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);

            var sequence = DOTween.Sequence();
            sequence.Join(UITweenShortcuts.AnchorPos(RectTransform, _inactiveAnchorPos, DurationOut).SetEase(EaseOut))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _inactiveAlpha, DurationOut).SetEase(EaseOut))
                .SetDelay(DelayOut)
                .OnComplete(Inactive);

            await sequence.ToUniTask(cancellationToken: token);
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
