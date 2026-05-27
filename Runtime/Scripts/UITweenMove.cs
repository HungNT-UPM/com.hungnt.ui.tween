using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Tween vị trí anchor của RectTransform giữa trạng thái active và offset inactive.
    /// </summary>
    public class UITweenMove : UITweenBase
    {
        [Title("Move")]
        [SerializeField] private Vector2 _offset;

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
            await UITweenShortcuts.AnchorPos(RectTransform, _activeAnchorPos, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .OnComplete(Active)
                .ToUniTask(cancellationToken: token);
        }

        public override async UniTask Hide(CancellationToken token = default)
        {
            await base.Hide(token);
            await UITweenShortcuts.AnchorPos(RectTransform, _inactiveAnchorPos, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .OnComplete(Inactive)
                .ToUniTask(cancellationToken: token);
        }

        public override void Active()
        {
            base.Active();
            RectTransform.anchoredPosition = _activeAnchorPos;
        }

        public override void Inactive()
        {
            base.Inactive();
            RectTransform.anchoredPosition = _inactiveAnchorPos;
        }
    }
}
