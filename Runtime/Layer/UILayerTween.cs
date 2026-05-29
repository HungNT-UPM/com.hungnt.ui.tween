using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HungNT.UI.UITween
{
    /// <summary>
    /// Điều phối hide tween trên các <see cref="UITweenBase"/> con có <see cref="UITweenBase.HasHideTween"/>.
    /// </summary>
    public class UILayerTween : UIViewBase
    {
        [SerializeField]
        private bool _includeInactiveTweens;

        [SerializeField]
        private GameObject _hideTarget;

        [SerializeField]
        private UILayerTweenHideBehaviour _hideBehaviour;

        // [SerializeField]
        // private UnityEvent _onHideTweenCompleted;

        public bool IsHideTweening { get; private set; }

        private void Reset()
        {
            _hideTarget = gameObject;
        }

        private void OnEnable()
        {
            IsHideTweening = false;
        }

        /// <summary>
        /// Đóng layer: hide tween các con → callback → disable/destroy theo cấu hình inspector.
        /// </summary>
        public void HideTween(Action extra = null)
        {
            HideTweenAsync(extra, this.GetCancellationTokenOnDestroy()).Forget();
        }

        /// <summary>
        /// Phiên bản async của <see cref="HideTween"/>.
        /// </summary>
        public async UniTask HideTweenAsync(Action extra = null, CancellationToken token = default)
        {
            if (IsHideTweening)
                return;

            if (!gameObject.activeInHierarchy)
            {
                extra?.Invoke();
                ApplyHideTweenBehaviour();
                return;
            }

            IsHideTweening = true;

            try
            {
                await HideChildTweensAsync(token);
                // _onHideTweenCompleted?.Invoke();
                extra?.Invoke();
                ApplyHideTweenBehaviour();
            }
            finally
            {
                IsHideTweening = false;
            }
        }

        /// <summary>
        /// Chạy Hide trên mỗi UITweenBase con đang active và có HasHideTween.
        /// </summary>
        public async UniTask HideChildTweensAsync(CancellationToken token = default)
        {
            IReadOnlyList<UITweenBase> tweens = CollectActiveTweens();
            if (tweens.Count == 0)
                return;

            Interactable = false;

            var hideTasks = new List<UniTask>();

            for (int i = 0; i < tweens.Count; i++)
            {
                UITweenBase tween = tweens[i];
                if (tween != null && tween.HasHideTween)
                    hideTasks.Add(tween.Hide(token));
            }

            if (hideTasks.Count > 0)
                await UniTask.WhenAll(hideTasks);
        }

        public GameObject ResolveHideTarget()
        {
            return _hideTarget != null ? _hideTarget : gameObject;
        }

        protected virtual IReadOnlyList<UITweenBase> CollectActiveTweens()
        {
            UITweenBase[] tweens = GetComponentsInChildren<UITweenBase>(_includeInactiveTweens);
            var activeTweens = new List<UITweenBase>(tweens.Length);

            for (int i = 0; i < tweens.Length; i++)
            {
                UITweenBase tween = tweens[i];
                if (tween != null && tween.gameObject.activeInHierarchy)
                    activeTweens.Add(tween);
            }

            return activeTweens;
        }

        private void ApplyHideTweenBehaviour()
        {
            switch (_hideBehaviour)
            {
                case UILayerTweenHideBehaviour.DisableTarget:
                    ResolveHideTarget()?.SetActive(false);
                    break;
                case UILayerTweenHideBehaviour.DestroyTarget:
                    GameObject target = ResolveHideTarget();
                    if (target != null)
                        Destroy(target);
                    break;
            }
        }
    }
}
