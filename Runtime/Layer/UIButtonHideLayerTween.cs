using UnityEngine;
using UnityEngine.UI;

namespace HungNT.UI.UITween
{
    /// <summary>
    /// Nút đóng: gọi <see cref="UILayerTween.HideTween"/> theo hành vi đã cấu hình trên layer.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UIButtonHideLayerTween : MonoBehaviour
    {
        [SerializeField]
        private UILayerTween _layerTween;

        private Button _button;

        private void Reset()
        {
            _layerTween = GetComponentInParent<UILayerTween>();
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

        private UILayerTween ResolveLayerTween()
        {
            if (_layerTween != null)
                return _layerTween;

            _layerTween = GetComponentInParent<UILayerTween>();
            return _layerTween;
        }
    }
}
