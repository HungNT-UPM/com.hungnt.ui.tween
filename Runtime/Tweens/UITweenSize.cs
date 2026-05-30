using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.UITween
{
    public class UITweenSize : UITweenBase
    {
        [Title("Size")]
        [InlineButton(nameof(FetchInactiveSize), "Fetch")]
        [SerializeField]
        private Vector2 _inactiveSize;

        [InlineButton(nameof(FetchActiveSize), "Fetch")]
        [SerializeField/*, DisableIf(nameof(_followRectTransformSize))*/]
        private Vector2 _activeSize;

        // [SerializeField]
        // private bool _followRectTransformSize = true;

        protected override string ConfigTypeName => "Size";

        protected override void Reset()
        {
            base.Reset();
            // _followRectTransformSize = true;
            FetchInactiveSize();
            FetchActiveSize();
        }

        public override void Init()
        {
            base.Init();
            // if (_followRectTransformSize)
            //     _activeSize = RectTransform.sizeDelta;
        }

//         protected override void OnValidate()
//         {
//             base.OnValidate();
// #if UNITY_EDITOR
//             if (!Application.isPlaying)
//                 SyncActiveSizeFromRectTransform();
// #endif
//         }
//
//         private void OnRectTransformDimensionsChange()
//         {
// #if UNITY_EDITOR
//             if (!Application.isPlaying)
//                 SyncActiveSizeFromRectTransform();
// #endif
//         }

        // private void SyncActiveSizeFromRectTransform()
        // {
        //     if (!_followRectTransformSize || RectTransform == null)
        //         return;
        //
        //     ResetSize();
        // }

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
