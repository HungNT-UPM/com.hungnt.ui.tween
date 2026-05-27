using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Chạy scale và fade song song khi show/hide.
    /// </summary>
    public class UITweenScaleFade : UITweenBase
    {
        [Title("Scale")]
        [SerializeField] private Vector2 _inactiveScale = new Vector2(0f, 0f);
        [SerializeField] private Vector2 _activeScale = new Vector2(1f, 1f);

        [Title("Fade")]
        [SerializeField] private float _inactiveAlpha = 0f;
        [SerializeField] private float _activeAlpha = 1f;

        protected override string DefaultPresetResourcePath => "Tween/TweenPreset_Scale";

        /// <summary>
        /// DOScale Vector2 có thể crash Unity khi popup chứa UIParticle — dùng Vector3 tạm thời.
        /// </summary>
        private Vector3 InactiveScale => new Vector3(_inactiveScale.x, _inactiveScale.y, 1f);
        private Vector3 ActiveScale => new Vector3(_activeScale.x, _activeScale.y, 1f);

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);

            var sequence = DOTween.Sequence();
            sequence.Join(RectTransform.DOScale(ActiveScale, DurationIn).SetEase(EaseIn))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _activeAlpha, DurationIn).SetEase(EaseIn))
                .SetDelay(DelayIn)
                .OnComplete(Active);

            await sequence.ToUniTask(cancellationToken: token);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);

            var sequence = DOTween.Sequence();
            sequence.Join(RectTransform.DOScale(InactiveScale, DurationOut).SetEase(EaseOut))
                .Join(UITweenShortcuts.Fade(CanvasGroup, _inactiveAlpha, DurationOut).SetEase(EaseOut))
                .SetDelay(DelayOut)
                .OnComplete(Inactive);

            await sequence.ToUniTask(cancellationToken: token);
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
