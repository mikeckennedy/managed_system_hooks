Managed System Hooks in .NET
====================

For now, please see http://www.codeproject.com/Articles/6362/Global-System-Hooks-in-NET for details.

The class library can be used to create any type of system hook. There are two that come pre-built: MouseHook and KeyboardHook. We have also included specialized versions of these classes called MouseHookExt and KeyboardHookExt respectively. Following the model set by those classes, you can easily build system hooks for any of the 15 hook event types in the Win32 API. Also, the entire class library comes with a compiled HTML help file which documents the classes. Be sure to look at this help file if you decide to use this library in your applications.

![screenshot](https://raw.github.com/mikeckennedy/managed_system_hooks/master/screenshots/SystemHookSampleApp.jpg)