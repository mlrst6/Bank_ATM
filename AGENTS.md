# Bank_ATM Agent Rules

These rules apply to this repository.

## UI/UX Work

- Do UI/UX layout and style changes in `*.Designer.cs` files only, for example `Bank_ATM/LanguageForm1.Designer.cs`, `Bank_ATM/MainForm.Designer.cs`, or `Bank_ATM/User/UserCardEntryForm.Designer.cs`.
- Do not add UI/UX styling, layout, positioning, sizing, visibility, docking, anchoring, or control-order changes in runtime form `.cs` files.
- Do not add runtime UI hooks in constructors, `Load`, `Shown`, `Application.Idle`, global theme initializers, or helper methods.
- Do not create or expand runtime methods such as `ApplyTheme()`, `StyleButton()`, or layout helpers for UI/UX changes unless the user explicitly asks for runtime UI behavior.
- Keep form `.cs` files focused on event handling, navigation, validation, data loading, and backend behavior.
- Do not edit backend behavior while doing UI-only work unless the user asks.
- Do not remove existing buttons, labels, text boxes, panels, event handlers, or form controls unless the user explicitly asks to remove them.
- If a control must be hidden, explain why first. Prefer keeping all existing actions visible and usable.
- Avoid global UI restyling hooks that can make Visual Studio designer and runtime layouts differ.
- Runtime behavior must match the Visual Studio designer layout.
- When using a UI library such as Krypton, add library-based control/style changes through the relevant `.Designer.cs` file, not through runtime code.

## WinForms Safety

- Preserve designer-generated event wiring.
- Do not mix designer layout changes and runtime layout changes for the same screen in the same task.
- Prefer changing one screen at a time, then building.
- If a form looks correct in Visual Studio but wrong at runtime, check the form `.cs` constructor, `Load` event, and helper methods for code that changes `Location`, `Size`, `Visible`, `Dock`, `Anchor`, or `Controls` order, then remove that runtime UI change unless the user explicitly wants it.
- After changing UI code, build with:

```powershell
msbuild Bank_ATM.slnx /t:Build /p:Configuration=Debug
```

## Project Rules From The User

- The user wants the project UI improved with a library, but not by breaking existing screens.
- The user wants UI/UX work to be manageable from the Designer files.
- The user does not want UI/UX changes added in runtime code.
- The user does not want buttons disappearing or appearing in unexpected places after build/run.
- The user expects runtime behavior to match the intended designer layout.

## General Development

- Keep changes scoped.
- Build before reporting completion.
- Mention any remaining screens that still need visual cleanup.
