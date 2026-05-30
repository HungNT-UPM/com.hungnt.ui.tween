using UnityEngine;

namespace HungNT.UI.UITween
{
    /// <summary>
    /// Slot trong chuỗi stagger — Awake báo control cha tính lại delay.
    /// </summary>
    public class TweenDelayByIndex : MonoBehaviour
    {
        private void Awake()
        {
            GetComponentInParent<TweenDelayByIndexControl>()?.ApplyStaggerDelays();
        }

        /// <summary>
        /// Gán delay show cho mọi UITweenBase trên object/con.
        /// </summary>
        public void OverrideDelay(bool useDelay, float delayIn, float delayOut)
        {
            UITweenBase[] tweens = GetComponentsInChildren<UITweenBase>(true);
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].OverrideDelay(useDelay, delayIn, delayOut);
        }
    }
}
