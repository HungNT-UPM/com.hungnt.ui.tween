using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Điều phối hide tween của các <see cref="UITweenBase"/> con: thu thập tween đang active,
    /// chạy hide đồng thời rồi chờ tất cả hoàn tất.
    /// </summary>
    public class TweenGroup : MonoBehaviour
    {
        /// <summary><c>true</c> khi hide tween đang chạy — để consumer chặn gọi lồng nhau.</summary>
        public bool IsHiding { get; private set; }

        /// <summary>
        /// Chạy hide tween trên mọi <see cref="UITweenBase"/> con đang active và chờ hoàn tất.
        /// Không có tween nào cần hide thì trả về ngay.
        /// </summary>
        public async UniTask PlayHideAsync(CancellationToken token)
        {
            IsHiding = true;

            try
            {
                IReadOnlyList<UITweenBase> tweens = CollectActiveTweens();

                List<UniTask> hideTasks = null;
                for (int i = 0; i < tweens.Count; i++)
                {
                    UITweenBase tween = tweens[i];
                    if (tween != null && tween.HasHideTween)
                        (hideTasks ??= new List<UniTask>(tweens.Count)).Add(tween.Hide(token));
                }

                if (hideTasks != null)
                    await UniTask.WhenAll(hideTasks);
            }
            finally
            {
                IsHiding = false;
            }
        }

        private IReadOnlyList<UITweenBase> CollectActiveTweens()
        {
            UITweenBase[] tweens = GetComponentsInChildren<UITweenBase>(includeInactive: false);
            var activeTweens = new List<UITweenBase>(tweens.Length);

            for (int i = 0; i < tweens.Length; i++)
            {
                UITweenBase tween = tweens[i];
                if (tween != null && tween.gameObject.activeInHierarchy && tween.enabled)
                    activeTweens.Add(tween);
            }

            return activeTweens;
        }
    }
}
