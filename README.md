# Biggie Piggie Farm

## Reccomended editor settings

- `Text Editor` > `External` > `Exec Path` = `C:/Users/[USER]/AppData/Local/Programs/Microsoft VS Code/Code.exe`
- `Text Editor` > `External` > `Exec Flags` = `{project} --goto {file}:{line}:{col}`
- `Text Editor` > `External` > `Use External Editor` = `On`
- `Text Editor` > `Behavior` > `Auto Reload Scripts on External Change` = `On`
- `Network` > `Language Server` > `Show Native Symbols in Editor` = `On`
- `Network` > `Language Server` > `Remote Port` = `6005`

## VS Code configuration

### Recommeded settings

- `godotTools.lsp.serverPort` = `6005`

### Recommended extensions


| Name | Description | Link |
| --- | --- | --- |
| **C# Tools for Godot** | Helps when using C# with Godot and with debugging. | <https://marketplace.visualstudio.com/items?itemName=neikeq.godot-csharp-vscode> |
| **Godot Files** | Helps with file paths resolve. | <https://marketplace.visualstudio.com/items?itemName=alfish.godot-files> |
| **godot-tools** | This extension will connect with Godot Engine and make the editor work. | <https://marketplace.visualstudio.com/items?itemName=geequlim.godot-tools> |

### .vscode/launch.json

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Launch",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "C:/Program Files/Godot_v4.4.1-stable_mono_win64/Godot_v4.4.1-stable_mono_win64.exe",
            "console": "internalConsole",
            "stopAtEntry": false,
        }
    ]
}
```

### .vscode/tasks.json

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "C:/Program Files/Godot_v4.4.1-stable_mono_win64/Godot_v4.4.1-stable_mono_win64.exe",
            "type": "shell",
            "args": [
                "--build-solutions",
                "--path",
                "${workspaceRoot}",
                "--no-window",
                "-q"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
```