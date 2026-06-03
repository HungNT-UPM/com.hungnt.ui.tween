using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    public class UITweenRotate : UITweenBase
    {
        [Title("Rotate")]
        [SerializeField]
        private float _inactiveAngle = -30f;

        [SerializeField]
        private float _activeAngle = 0f;

        [SerializeField]
        private RotateMode _rotateMode = RotateMode.Fast;

        [SerializeField]
        private int _loopCount = 0;

        [ShowIf(nameof(_hasLoop))]
        [SerializeField]
        private LoopType _loopType = LoopType.Yoyo;

        private bool _hasLoop => _loopCount != 0;

        private float _originalAngle;

        protected override string ConfigTypeName => "Rotate";

        public override void Init()
        {
            base.Init();
            _originalAngle = RectTransform.localEulerAngles.z;
        }

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);
            var tween = RectTransform
                .DORotate(Vector3.forward * (_originalAngle + _activeAngle), ShowDuration, _rotateMode)
                .SetEase(ShowEase)
                .SetDelay(DelayShow);

            if (_hasLoop)
                tween.SetLoops(_loopCount, _loopType);
            else
                tween.OnComplete(Active);

            await tween.ToUniTask(cancellationToken: TweenToken);

            if (!_hasLoop)
                Active();
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);

            await RectTransform
                .DORotate(Vector3.forward * (_originalAngle + _inactiveAngle), HideDuration, _rotateMode)
                .SetEase(HideEase)
                .SetDelay(DelayHide)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override void Active()
        {
            base.Active();
            RectTransform.localEulerAngles = new Vector3(0f, 0f, _originalAngle + _activeAngle);
        }

        public override void Inactive()
        {
            base.Inactive();
            RectTransform.localEulerAngles = new Vector3(0f, 0f, _originalAngle + _inactiveAngle);
        }
    }
}
