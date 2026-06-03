using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Gán delay show tăng dần cho từng TweenDelayByIndex con theo thứ tự hierarchy.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class TweenDelayByIndexControl : MonoBehaviour
    {
        [SerializeField] private float _delayInterval = 0.05f;

        [SerializeField] private float _startDelay;

        private void Awake()
        {
            ApplyStaggerDelays();
        }

        /// <summary>
        /// Tính lại delay cho mọi slot con (gọi lại khi spawn runtime).
        /// </summary>
        public void ApplyStaggerDelays()
        {
            float delay = _startDelay;
            TweenDelayByIndex[] slots = GetComponentsInChildren<TweenDelayByIndex>(true);

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].OverrideDelay(true, delay, 0f);
                delay += _delayInterval;
            }
        }
    }
}
