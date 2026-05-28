# HungNT UI Tween (`com.hungnt.ui.tween`)

Component UI show/hide dùng **DOTween** + **UniTask**, cấu hình qua **TweenPreset** ScriptableObject.

Phụ thuộc **`com.hungnt.ui`** (`UIViewBase`).

Package tự thêm scripting define **`UNITASK_DOTWEEN_SUPPORT`** cho mọi build target khi mở Editor (script `UITweenDefineSetup`). Không cần cấu hình tay.

## Components

| Component | Mô tả |
|-----------|--------|
| `UITweenFade` | Alpha qua `CanvasGroup` |
| `UITweenMove` | `RectTransform` anchor position |
| `UITweenScale` | Local scale |
| `UITweenRotate` | Xoay liên tục (loading spinner) |
| `UITweenMoveFade` | Move + fade song song |
| `UITweenScaleFade` | Scale + fade song song |

## TweenPreset

Tạo asset: **Create → HungNT/UI/Tween Preset**.

Preset mặc định load từ `Resources/Tween/TweenPreset_*` khi `Reset()` hoặc khi `_preset` null:

- `Tween/TweenPreset_Default`
- `Tween/TweenPreset_Fade`
- `Tween/TweenPreset_Move`
- `Tween/TweenPreset_Scale`
- `Tween/TweenPreset_Rotate`

## Stagger delay (sequence show)

Gắn **`TweenDelayByIndexControl`** lên holder, mỗi phần tử con có **`TweenDelayByIndex`** + `UITween*`.

- Control tính delay tăng dần theo thứ tự hierarchy (`ApplyStaggerDelays`).
- Spawn runtime: mỗi con mới `Awake` → gọi lại `ApplyStaggerDelays()` trên control cha.
- Show lần đầu chạy ở **`Start`** của `UITweenBase` (sau khi delay đã gán trong `Awake`).

## Ví dụ

```csharp
using Cysharp.Threading.Tasks;
using HungNT.UI.Tween;

await GetComponent<UITweenFade>().Hide(this.GetCancellationTokenOnDestroy());
```

## Lifecycle (`UITweenBase`)

- `OnEnable` → `Inactive()`; re-enable sau `Start` → `Show()`.
- `Start` (lần đầu) → `Show()` (delay đã được `TweenDelayByIndex` gán ở `Awake`).
- `OverrideDelay()` — ghi đè `_useDelay` / `_delayIn` / `_delayOut` từ bên ngoài.
- `OnDisable` → hủy tween đang chạy.
