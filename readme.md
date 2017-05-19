# SignalR typings creator

A simple command line tool for creating TypeScript definition files from Hubs.

```
SignalRTypingsCreator.exe "MyAssembly" "MyProjectRootDir"
```

msbuild:

```
$(MSBuildProjectDirectory)\$(OutDir)SignalRTypingsCreator.exe "$(AssemblyName)" "$(MSBuildProjectDirectory)"
```

## Features

* Searches through the assembly for all Hub implementations and creates a definition file in the "Scripts/Typings" directory of the project.
* Respects the HubName and HubMethodName attributes

## Requirements

* SignalR 2.2 (nuget package Microsoft.AspNet.SignalR.Core)
* jQuery typings (nuget package jquery.TypeScript.DefinitelyTyped)
* SignalR typings (nuget package signalr.TypeScript.DefinitelyTyped)

## Example

ChatHub example

```csharp
public class ChatHub : Hub
{
    public void SendName(string name) { }

    public void Send(string name, string message) { }
}
```

Will result in the following typings file

```csharp
interface ChatHub {
     server:ChatHubServer
     client:any
}

interface ChatHubServer {
     sendName(name:string):void
     send(name:string, message:string):void
}

interface SignalR
{
     chatHub:ChatHub
}
```

## Known issues

* Client is defined as any.
* Complex return types and arguments are defined as any
* Generated Definition file is not added to solution