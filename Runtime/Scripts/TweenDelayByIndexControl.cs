using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Gán delay show tăng dần cho từng <see cref="TweenDelayByIndex"/> con theo thứ tự hierarchy.
    /// Gọi lại khi spawn thêm con lúc runtime (qua <see cref="TweenDelayByIndex.Awake"/>).
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class TweenDelayByIndexControl : MonoBehaviour
    {
        [SerializeField] private float delayInterval = 0.05f;
        [SerializeField] private float startDelay = 0f;

        private void Awake()
        {
            ApplyStaggerDelays();
        }

        /// <summary>
        /// Tính lại delay cho mọi <see cref="TweenDelayByIndex"/> con (theo thứ tự hierarchy).
        /// </summary>
        public void ApplyStaggerDelays()
        {
            var delay = startDelay;
            var slots = GetComponentsInChildren<TweenDelayByIndex>(true);
            for (var i = 0; i < slots.Length; i++)
            {
                slots[i].OverrideDelay(true, delay, 0f);
                delay += delayInterval;
            }
        }
    }
}
