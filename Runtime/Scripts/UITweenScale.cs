using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Tween local scale giữa giá trị inactive và active (Vector2 → Vector3 với z = 1).
    /// </summary>
    public class UITweenScale : UITweenBase
    {
        [Title("Scale")]
        [SerializeField] private Vector2 _inactiveScale = new Vector2(0f, 0f);
        [SerializeField] private Vector2 _activeScale = new Vector2(1f, 1f);

        protected override string DefaultPresetResourcePath => "Tween/TweenPreset_Scale";

        /// <summary>
        /// DOScale Vector2 có thể crash Unity khi popup chứa UIParticle — dùng Vector3 tạm thời.
        /// </summary>
        private Vector3 InactiveScale => new Vector3(_inactiveScale.x, _inactiveScale.y, 1f);
        private Vector3 ActiveScale => new Vector3(_activeScale.x, _activeScale.y, 1f);

        public override async UniTask Show(CancellationToken token = default)
        {
            await base.Show(token);
            await RectTransform.DOScale(ActiveScale, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: token);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
            await RectTransform.DOScale(InactiveScale, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: token);
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
