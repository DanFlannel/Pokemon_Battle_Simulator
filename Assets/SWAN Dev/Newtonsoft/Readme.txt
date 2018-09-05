—————————————————
Newtonsoft Json.NET for Unity3D
—————————————————

Newtonsoft Json.NET is a de facto standard JSON library in .NET ecosystem. But it doesn't support Unity3D, so it's a little bit hard to use JSON.NET just after getting Json.NET package. This package is for Unity3D programmers that need to use latest Json.NET in Unity3D.

Where can I get it?
Visit Release page to get latest Json.NET unity-package.
To use this library in IL2CPP build settings, you need to add link.xml to your project's asset folder. For detailed information about link.xml, read unity manual about this.

What's the deal?
Unity3D has old-fashioned and bizarre .NET Framework like these :)
* Basically based on .NET Framework 3.5 (forked Mono 2.6)
* Runtime lacks some types in .NET Framework 3.5 (like System.ComponentModel.AddingNewEventHandler)
* For iOS, dynamic code emission is forbidden by Apple AppStore.
Because Newtonsoft Json.NET doesn't handle these limitations, errors will welcome you when you use official Json.NET dll targetting .NET 3.5 Framework.

What's done?
Following works are done to make Json.NET support Unity3D.
* Based on Newtonsoft Json.NET 9.
* Disable IL generation to work well under AOT environment like iOS.
* Remove code related with System.ComponentModel.
* Remove System.Data and EntityKey support.
* Remove XML support.
* Remove DiagnosticsTraceWriter support.
* Workaround for differences between Microsoft.NET & Unity3D-Mono.NET
For Unity.Lite version, additional works are done to make more lite.
* Remove JsonLinq, JPath (JToken, ...)
* Remove Bson

Unity Compatibility
This library is tested on Unity 4.7, 5.2 and 5.3. For AOT environment like iOS, you need to use IL2CPP instead of obsolute Mono-AOT because IL2CPP handles generic code better than Mono-AOT. With Mono-AOT configuration, AOT related exception would be thrown.
For windows store build, there is a compatibility issue related with UWP. If you have a problem, please read workaround for UWP.

Special Thanks
SaladLab Git: https://github.com/SaladLab/Json.Net.Unity3D 