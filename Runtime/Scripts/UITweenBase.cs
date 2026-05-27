using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Base class cho component tween show/hide UI, dùng chung <see cref="TweenPreset"/>.
    /// Lần đầu <c>Show()</c> ở <c>Start</c> (sau khi delay helper gán xong ở <c>Awake</c>); re-enable gọi lại ở <c>OnEnable</c>.
    /// </summary>
    public abstract class UITweenBase : UIViewBase
    {
        #region ===== Fields =====
        [Title("Base Settings")]
        [InlineEditor, InlineButton(nameof(LoadPreset))]
        [SerializeField] protected TweenPreset _preset;

        [FoldoutGroup("Override"), HorizontalGroup("Override/Settings"), VerticalGroup("Override/Settings/Ease"), LabelWidth(100)]
        [SerializeField] protected bool _overrideEase;

        [VerticalGroup("Override/Settings/Ease"), LabelWidth(100)]
        [EnableIf(nameof(_overrideEase))]
        [SerializeField] protected Ease _easeIn = Ease.Linear;

        [VerticalGroup("Override/Settings/Ease"), LabelWidth(100)]
        [EnableIf(nameof(_overrideEase))]
        [SerializeField] protected Ease _easeOut = Ease.Linear;

        [HorizontalGroup("Override/Settings"), VerticalGroup("Override/Settings/Duration"), LabelWidth(100)]
        [SerializeField] protected bool _overrideDuration;

        [VerticalGroup("Override/Settings/Duration"), LabelWidth(100)]
        [EnableIf(nameof(_overrideDuration))]
        [SerializeField] protected float _durationIn = 0.25f;

        [VerticalGroup("Override/Settings/Duration"), LabelWidth(100)]
        [EnableIf(nameof(_overrideDuration))]
        [SerializeField] protected float _durationOut = 0.25f;

        [FoldoutGroup("Event")] [SerializeField] protected UnityEvent _onShowCompleted;
        [FoldoutGroup("Event")] [SerializeField] protected UnityEvent _onHideCompleted;

        [SerializeField] [Space] protected bool _useDelay;
        [SerializeField, ShowIf(nameof(_useDelay))] protected float _delayIn;
        [SerializeField, ShowIf(nameof(_useDelay))] protected float _delayOut;
        #endregion

        #region ===== Properties =====
        public Ease EaseIn => _overrideEase ? _easeIn : _preset.easeIn;
        public Ease EaseOut => _overrideEase ? _easeOut : _preset.easeOut;
        public float DurationIn => _overrideDuration ? _durationIn : _preset.durationIn;
        public float DurationOut => _overrideDuration ? _durationOut : _preset.durationOut;
        public float DelayIn => _useDelay ? _delayIn : 0f;
        public float DelayOut => _useDelay ? _delayOut : 0f;
        #endregion

        protected virtual string DefaultPresetResourcePath => "Tween/TweenPreset_Default";
        private CancellationTokenSource _cts;
        private bool _started;

        protected virtual void Reset()
        {
            LoadPreset();
        }

        protected virtual void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            Inactive();
            Show(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private void OnDisable()
        {
            CancelActiveTween();
        }

        private void LoadPreset()
        {
            _preset = Resources.Load<TweenPreset>(DefaultPresetResourcePath);
        }

        public virtual void Init()
        {
            if (_preset == null)
            {
                LoadPreset();
            }
        }

        private void CancelActiveTween()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public virtual UniTask Show(CancellationToken token = default)
        {
            CancelActiveTween();
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide(CancellationToken token = default)
        {
            CancelActiveTween();
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// Hiện ngay lập tức, không chạy tween.
        /// </summary>
        public virtual void Active()
        {
            _onShowCompleted?.Invoke();
        }

        /// <summary>
        /// Ẩn ngay lập tức, không chạy tween.
        /// </summary>
        public virtual void Inactive()
        {
            _onHideCompleted?.Invoke();
        }

        /// <summary>
        /// Ghi đè delay show/hide — dùng bởi <see cref="TweenDelayByIndex"/>.
        /// </summary>
        public void OverrideDelay(bool useDelay, float delayIn, float delayOut)
        {
            _useDelay = useDelay;
            _delayIn = delayIn;
            _delayOut = delayOut;
        }
    }
}