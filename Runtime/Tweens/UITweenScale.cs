using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    public class UITweenScale : UITweenBase
    {
        [Title("Scale")]
        [SerializeField] private Vector2 _inactiveScale;

        [SerializeField] private Vector2 _activeScale = Vector2.one;

        protected override string ConfigTypeName => "Scale";

        private Vector3 InactiveScale => new Vector3(_inactiveScale.x, _inactiveScale.y, 1f);

        private Vector3 ActiveScale => new Vector3(_activeScale.x, _activeScale.y, 1f);

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);
            await RectTransform.DOScale(ActiveScale, ShowDuration)
                .SetEase(ShowEase).SetDelay(DelayShow)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
            if (!HasHideTweenConfig)
                return;

            await RectTransform.DOScale(InactiveScale, HideDuration)
                .SetEase(HideEase).SetDelay(DelayHide)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: TweenToken);
        }

        public override void Active()
        {
            base.Active();
            RectTransform.localScale = ActiveScale;
        }

        public override void Inactive()
        {
            base.Inactive();
            RectTransform.localScale = InactiveScale;
        }
    }
}
