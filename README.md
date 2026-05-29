# com.hungnt.ui.tween

Namespace: **HungNT.UI.UITween**. Package UPM: **com.hungnt.ui.tween**.

## UILayerTween

Điều phối hide tween trên các `UITweenBase` con có `HasHideTween`. Cấu hình `_hideBehaviour` (Disable/Destroy), `_hideTarget`, `HideTween()` / `HideTweenAsync()`.

## UIButtonHideLayerTween

`[RequireComponent(Button)]` — click gọi `UILayerTween.HideTween()` trên parent (hoặc field gán sẵn).

## UITweenBase

Tab Show / Hide / Event — `_showTweenConfig`, override duration/ease, `_hasHideTween`, `_hideTweenConfig`.
