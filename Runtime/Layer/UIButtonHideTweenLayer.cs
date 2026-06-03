using UnityEngine;
using UnityEngine.UI;

namespace HungNT.UI.Tween
{
    /// <summary>
    /// Nút đóng: gọi <see cref="UITweenLayer.HideTween"/> theo hành vi đã cấu hình trên layer.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UIButtonHideTweenLayer : MonoBehaviour
    {
        [SerializeField]
        private UITweenLayer _layerTween;

        private Button _button;

        private void Reset()
        {
            _layerTween = GetComponentInParent<UITweenLayer>();
        }

        private void Awake()
        {
            _button = GetComponent<Button>();

            if (_button != null)
            {
                _button.onClick.AddListener(OnClick);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
            }
        }

        private void OnClick()
        {
            ResolveLayerTween()?.HideTween();
        }

        private UITweenLayer ResolveLayerTween()
        {
            if (_layerTween != null)
                return _layerTween;

            _layerTween = GetComponentInParent<UITweenLayer>();
            return _layerTween;
        }
    }
}
