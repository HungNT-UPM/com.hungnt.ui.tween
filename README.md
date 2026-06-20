# com.hungnt.ui.tween

Tween show/hide cho UI dựng trên **DOTween + UniTask**. Mỗi `UITweenBase` con tự play khi GameObject bật; `TweenGroup` gom toàn bộ hide tween con để **chờ animation xong** trước khi ẩn/destroy. Duration/ease tách ra `TweenConfig` (ScriptableObject) tự sinh trong `Resources/Tween`.

Namespace: **`HungNT.UI.Tween`**.

---

## Cài đặt

`Packages/manifest.json`:

```json
"com.hungnt.ui.tween": "https://github.com/HungNT-UPM/com.hungnt.ui.tween.git#2.3.0"
```

### Yêu cầu
- Unity 2022.3+
- [`com.hungnt.ui`](https://github.com/HungNT-UPM/com.hungnt.ui) ≥ 1.0.0
- [UniTask](https://github.com/Cysharp/UniTask) ≥ 2.5.11 (cần cả asmdef `UniTask.DOTween`)
- DOTween — define `UNITASK_DOTWEEN_SUPPORT` được set tự động qua `UITweenDefineSetup` (Editor)
- Odin Inspector (Inspector tabs)

---

## UITweenBase + các tween cụ thể

Base abstract cho một loại tween, có 2 hướng **Show** / **Hide** độc lập. **Show** tự play qua `OnEnable` khi object bật; **Hide** chạy qua `TweenGroup` (hoặc gọi trực tiếp `Hide(token)`).

| Tween | Hiệu ứng |
|-------|----------|
| `UITweenFade` | alpha (CanvasGroup) |
| `UITweenMove` | anchoredPosition theo offset |
| `UITweenScale` | localScale |
| `UITweenSize` | sizeDelta |
| `UITweenRotate` | rotate Z (hỗ trợ loop) |
| `UITweenMoveFade` | move + fade |
| `UITweenScaleFade` | scale + fade |

Gắn các tween khác nhau lên từng child để kết hợp animation:

```
HomePanel
├── Title      → UITweenMoveFade
├── PlayButton → UITweenScale
└── Footer     → UITweenFade
```

Mỗi component có tab **Show / Hide / Event** trong Inspector:
- `_hasShowTween` / `_hasHideTween` — bật/tắt từng hướng.
- `_showTweenConfig` / `_hideTweenConfig` — asset config (nút **Ensure** tự tạo nếu thiếu).
- override duration/ease riêng, `_delayShow` / `_delayHide`.
- `OnShowCompleted` / `OnHideCompleted` (UnityEvent).

---

## TweenConfig

`ScriptableObject` giữ `Duration` + `Ease` cho một hướng tween. Tạo qua menu **Create → HungNT/UI/Tween Config**, hoặc để `TweenConfigLoader` tự sinh:

```
Assets/Resources/Tween/<Type>_Show.asset
Assets/Resources/Tween/<Type>_Hide.asset
```

Tween gọi `EnsureShowConfig` / `EnsureHideConfig` (qua `Reset` hoặc nút **Ensure**) → load từ Resources, hoặc tạo asset mới trên disk khi ở Editor.

---

## TweenGroup

Component điều phối hide tween. Thu thập mọi `UITweenBase` con đang active có `HasHideTween`, chạy hide đồng thời rồi **chờ tất cả hoàn tất**.

```csharp
using HungNT.UI.Tween;

[RequireComponent(typeof(TweenGroup))]
public class MyWindow : MonoBehaviour
{
    private TweenGroup _tweenGroup;
    private void Awake() => _tweenGroup = GetComponent<TweenGroup>();

    public async UniTask CloseAsync(CancellationToken token)
    {
        await _tweenGroup.PlayHideAsync(token);   // chờ toàn bộ child hide xong
        gameObject.SetActive(false);
    }
}
```

`IsHiding` = `true` khi đang chạy — dùng để chặn gọi lồng nhau. Package `com.hungnt.ui.panel` dùng `TweenGroup` trong `UIPanelTween` để có hide animation cho panel.

---

## TweenDelayByIndexControl + TweenDelayByIndex

Stagger: gán delay show **tăng dần** cho các slot con theo thứ tự hierarchy (hiệu ứng "đổ" lần lượt).

```
List
├── TweenDelayByIndexControl   (_startDelay = 0, _delayInterval = 0.05)
├── Item 0 → TweenDelayByIndex  → delay 0.00
├── Item 1 → TweenDelayByIndex  → delay 0.05
└── Item 2 → TweenDelayByIndex  → delay 0.10
```

`ApplyStaggerDelays()` tính lại delay cho mọi slot — gọi lại được sau khi spawn item runtime.
