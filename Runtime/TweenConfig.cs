using DG.Tweening;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Cấu hình duration và ease một hướng tween. Delay đặt trên UITweenBase.
    /// </summary>
    [CreateAssetMenu(menuName = TweenDefine.ConfigMenuName)]
    public class TweenConfig : ScriptableObject
    {
        [SerializeField]
        private float _duration = 0.25f;

        [SerializeField]
        private Ease _ease = Ease.Linear;

        public float Duration => _duration;

        public Ease Ease => _ease;
    }
}