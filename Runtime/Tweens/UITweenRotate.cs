using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace HungNT.UI.UITween
{
    public class UITweenRotate : UITweenBase
    {
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
            await RectTransform.DORotate(Vector3.forward * -360f, ShowDuration, RotateMode.FastBeyond360)
                .SetEase(ShowEase).SetDelay(DelayShow)
                .SetLoops(-1, LoopType.Restart)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
        }

        public override void Active()
        {
            base.Active();
            RectTransform.eulerAngles = new Vector3(0f, 0f, _originalAngle);
        }

        public override void Inactive()
        {
            base.Inactive();
            RectTransform.eulerAngles = new Vector3(0f, 0f, _originalAngle);
        }
    }
}
