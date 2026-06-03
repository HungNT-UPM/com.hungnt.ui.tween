using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    public class UITweenSize : UITweenBase
    {
        [Title("Size")]
        [InlineButton(nameof(FetchInactiveSize), "Fetch")]
        [SerializeField]
        private Vector2 _inactiveSize;

        [InlineButton(nameof(FetchActiveSize), "Fetch")]
        [SerializeField]
        private Vector2 _activeSize;

        protected override string ConfigTypeName => "Size";

        protected override void Reset()
        {
            base.Reset();
            FetchInactiveSize();
            FetchActiveSize();
        }

        private void FetchInactiveSize()
        {
            _inactiveSize = RectTransform.sizeDelta;
        }

        private void FetchActiveSize()
        {
            _activeSize = RectTransform.sizeDelta;
        }

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);
            await UITweenShortcuts.SizeDelta(RectTransform, _activeSize, ShowDuration)
                .SetEase(ShowEase).SetDelay(DelayShow)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
            if (!HasHideTweenConfig)
                return;

            await UITweenShortcuts.SizeDelta(RectTransform, _inactiveSize, HideDuration)
                .SetEase(HideEase).SetDelay(DelayHide)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override void Active()
        {
            base.Active();
            RectTransform.sizeDelta = _activeSize;
        }

        public override void Inactive()
        {
            base.Inactive();
            RectTransform.sizeDelta = _inactiveSize;
        }
    }
}
