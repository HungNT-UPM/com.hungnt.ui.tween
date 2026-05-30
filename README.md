# HungNT UI Tween (`com.hungnt.ui.tween`)

Component UI show/hide dùng **DOTween** + **UniTask**, cấu hình qua **TweenConfig** ScriptableObject.

Phụ thuộc **`com.hungnt.ui`** (`UIViewBase`).

Package tự thêm scripting define **`UNITASK_DOTWEEN_SUPPORT`** cho mọi build target khi mở Editor (`UITweenDefineSetup`).

## Components

| Component | Mô tả |
|-----------|--------|
| `UITweenFade` | Alpha qua `CanvasGroup` |
| `UITweenMove` | `RectTransform` anchored position |
| `UITweenScale` | Local scale |
| `UITweenSize` | `RectTransform.sizeDelta` (hỗ trợ follow size trong Editor) |
| `UITweenRotate` | Xoay liên tục (loading spinner) |
| `UITweenMoveFade` | Move + fade song song |
| `UITweenScaleFade` | Scale + fade song song |
| `UITweenLayer` | Điều phối hide tween các `UITweenBase` con |
| `UIButtonHideTweenLayer` | Button click → `UITweenLayer.HideTween()` |

## TweenConfig

Asset cấu hình duration/ease cho show và hide. Tạo qua menu **HungNT/UI/Tween Config** hoặc nút **Ensure** trên inspector.

Config mặc định load/tạo tại `Assets/Resources/Tween/{Type}_Show` và `{Type}_Hide` (vd. `Fade_Show`, `Size_Hide`).

## Stagger delay

Gắn **`TweenDelayByIndexControl`** lên holder, mỗi phần tử con có **`TweenDelayByIndex`** + `UITween*`.

## Ví dụ

```csharp
using Cysharp.Threading.Tasks;
using HungNT.UI.UITween;

await GetComponent<UITweenFade>().Hide(this.GetCancellationTokenOnDestroy());
```

## Lifecycle (`UITweenBase`)

- `OnEnable` → `Inactive()` rồi `Show()`.
- `OverrideDelay()` — ghi đè delay show/hide từ bên ngoài.
- `OnDisable` → hủy tween đang chạy.
