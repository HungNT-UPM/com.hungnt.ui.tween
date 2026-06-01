using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HungNT.UI.UITween
{
    public abstract class UITweenBase : UIViewBase
    {
        #region Show
        [TabGroup("Tween", "Show")]
        [SerializeField]
        private bool _hasShowTween = true;

        [TabGroup("Tween", "Show")]
        [ShowIf(nameof(_hasShowTween))]
        [InlineEditor, InlineButton(nameof(EnsureShowConfig), "Ensure")]
        [SerializeField]
        private TweenConfig _showTweenConfig;

        [TabGroup("Tween", "Show")]
        [ShowIf(nameof(_hasShowTween))]
        [SerializeField]
        private bool _overrideShowDuration;

        [TabGroup("Tween", "Show")]
        [ShowIf(nameof(_hasShowTween))]
        [ShowIf(nameof(_overrideShowDuration))]
        [SerializeField]
        private float _overrideShowDurationValue = 0.25f;

        [TabGroup("Tween", "Show")]
        [ShowIf(nameof(_hasShowTween))]
        [SerializeField]
        private bool _overrideShowEase;

        [TabGroup("Tween", "Show")]
        [ShowIf(nameof(_hasShowTween))]
        [ShowIf(nameof(_overrideShowEase))]
        [SerializeField]
        private Ease _overrideShowEaseValue = Ease.Linear;

        [TabGroup("Tween", "Show")]
        [ShowIf(nameof(_hasShowTween))]
        [SerializeField]
        private float _delayShow;
        #endregion

        #region Hide
        [TabGroup("Tween", "Hide")]
        [SerializeField]
        private bool _hasHideTween = true;

        [TabGroup("Tween", "Hide")]
        [ShowIf(nameof(_hasHideTween))]
        [InlineEditor, InlineButton(nameof(EnsureHideConfig), "Ensure")]
        [SerializeField]
        private TweenConfig _hideTweenConfig;

        [TabGroup("Tween", "Hide")]
        [ShowIf(nameof(_hasHideTween))]
        [SerializeField]
        private bool _overrideHideDuration;

        [TabGroup("Tween", "Hide")]
        [ShowIf(nameof(_hasHideTween))]
        [ShowIf(nameof(_overrideHideDuration))]
        [SerializeField]
        private float _overrideHideDurationValue = 0.25f;

        [TabGroup("Tween", "Hide")]
        [ShowIf(nameof(_hasHideTween))]
        [SerializeField]
        private bool _overrideHideEase;

        [TabGroup("Tween", "Hide")]
        [ShowIf(nameof(_hasHideTween))]
        [ShowIf(nameof(_overrideHideEase))]
        [SerializeField]
        private Ease _overrideHideEaseValue = Ease.Linear;

        [TabGroup("Tween", "Hide")]
        [ShowIf(nameof(_hasHideTween))]
        [SerializeField]
        private float _delayHide;
        #endregion

        #region Event
        [TabGroup("Tween", "Event")]
        [SerializeField]
        private UnityEvent _onShowCompleted;

        [TabGroup("Tween", "Event")]
        [SerializeField]
        private UnityEvent _onHideCompleted;
        #endregion

        private CancellationTokenSource _tweenCts;

        public bool HasHideTween => _hasHideTween && _hideTweenConfig != null;

        public float ShowDuration => _overrideShowDuration ? _overrideShowDurationValue : _showTweenConfig != null ? _showTweenConfig.Duration : 0.25f;

        public float HideDuration => _overrideHideDuration ? _overrideHideDurationValue : _hideTweenConfig != null ? _hideTweenConfig.Duration : 0.25f;

        public Ease ShowEase => _overrideShowEase ? _overrideShowEaseValue : _showTweenConfig != null ? _showTweenConfig.Ease : Ease.Linear;

        public Ease HideEase => _overrideHideEase ? _overrideHideEaseValue : _hideTweenConfig != null ? _hideTweenConfig.Ease : Ease.Linear;

        public float DelayShow => _delayShow;

        public float DelayHide => _delayHide;

        protected bool HasHideTweenConfig => _hideTweenConfig != null;

        protected CancellationToken TweenToken => _tweenCts?.Token ?? this.GetCancellationTokenOnDestroy();

        protected abstract string ConfigTypeName { get; }

        protected virtual void Reset()
        {
            EnsureShowConfig();
            EnsureHideConfig();
        }

        protected virtual void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            if (_hasShowTween)
            {
                Inactive();
                Show(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        private void OnDisable()
        {
            CancelActiveTween();
        }

        public virtual void Init()
        {
            if (_hasShowTween)
                EnsureShowConfig();

            if (_hasHideTween)
                EnsureHideConfig();
        }

        protected void EnsureShowConfig()
        {
            if (_showTweenConfig != null)
                return;

            _showTweenConfig = TweenConfigLoader.LoadOrCreateShow(ConfigTypeName);
        }

        protected void EnsureHideConfig()
        {
            if (_hideTweenConfig != null)
                return;

            _hideTweenConfig = TweenConfigLoader.LoadOrCreateHide(ConfigTypeName);
        }

        protected void CancelActiveTween()
        {
            if (_tweenCts != null)
            {
                _tweenCts.Cancel();
                _tweenCts.Dispose();
                _tweenCts = null;
            }

            DOTween.Kill(this);
        }

        protected void BeginTweenOperation(CancellationToken externalToken)
        {
            CancelActiveTween();
            _tweenCts = CancellationTokenSource.CreateLinkedTokenSource(externalToken, this.GetCancellationTokenOnDestroy());
        }

        public virtual UniTask Show(CancellationToken token = default)
        {
            BeginTweenOperation(token);
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide(CancellationToken token = default)
        {
            BeginTweenOperation(token);
            return UniTask.CompletedTask;
        }

        public virtual void Active()
        {
            _onShowCompleted?.Invoke();
        }

        public virtual void Inactive()
        {
            _onHideCompleted?.Invoke();
        }

        public void OverrideDelay(bool useDelay, float delayShow, float delayHide)
        {
            _delayShow = useDelay ? delayShow : 0f;
            _delayHide = useDelay ? delayHide : 0f;
        }
    }
}