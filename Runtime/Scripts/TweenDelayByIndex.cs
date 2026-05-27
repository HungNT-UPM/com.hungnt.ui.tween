using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Một phần tử trong sequence show lần lượt. Awake nhờ <see cref="TweenDelayByIndexControl"/> cha tính lại delay (kể cả spawn runtime).
    /// </summary>
    public class TweenDelayByIndex : MonoBehaviour
    {
        private void Awake()
        {
            var control = GetComponentInParent<TweenDelayByIndexControl>();
            control?.ApplyStaggerDelays();
        }

        /// <summary>
        /// Gán delay cho các <see cref="UITweenBase"/> trên cùng GameObject / con.
        /// </summary>
        public void OverrideDelay(bool useDelay, float delayIn, float delayOut)
        {
            var tweens = GetComponentsInChildren<UITweenBase>(true);
            for (var i = 0; i < tweens.Length; i++)
                tweens[i].OverrideDelay(useDelay, delayIn, delayOut);
        }
    }
}
