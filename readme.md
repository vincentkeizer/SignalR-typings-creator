# SignalR typings creator

A simple command line tool for creating TypeScript definition files from Hubs.

```
SignalRTypingsCreator.exe "MyAssembly" "MyProjectRootDir"
```

msbuild:

```
$(MSBuildProjectDirectory)\$(OutDir)SignalRTypingsCreator.exe "$(AssemblyName)" "$(MSBuildProjectDirectory)"
```

c#

```csharp
var typingsCreator = new SignalRTypingsCreator.Core.SignalRTypingsCreator();
typingsCreator.Generate(new SignalRTypingsCreatorConfig
{
    AssemblyName = "MyAssembly",
    ProjectRootDir = "MyProjectRootDir"
});
```
## Features

* Searches through the assembly for all Hub implementations and creates a definition file in the "Scripts/Typings" directory of the project.
* Respects the HubName and HubMethodName attributes
* Generates definition files for all models used in the hub (arguments and return types)

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

    public void Send(Messge message) { }
}

public class Messge
{
    public string Name { get; set; }
    public string Message { get; set; }
}
```

Will result in the following typings files 

\Scripts\typings\Chathub.d.ts

```csharp
interface ChatHub {
     server:ChatHubServer
     client:any
}

interface ChatHubServer {
     sendName(name:string):void
     send(message:Messge):void
}

interface SignalR
{
     chatHub:ChatHub
}
```

\Scripts\typings\Message.d.ts

```csharp
interface Messge {
     Name:string
     Message:string
}
```

## Known issues

* Client is defined as any.
* Circular references in models result in exceptions
* Generated Definition file is not added to solution