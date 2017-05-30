# SignalR typings creator

A simple command line tool for creating TypeScript definition files from Hubs.

## NuGet

[SignalRTypingsCreator](https://www.nuget.org/packages/SignalRTypingsCreator)

```
Install-Package SignalRTypingsCreator
```

**Note:** 

Installing SignalR typings will also install signalr-1.0.d.ts. 
Please delete this file, this will cause subsequent variable declarations errors.

## Command line

```
SignalRTypingsCreator.exe "MyAssembly" "MyProjectRootDir" "ProjectFileFullPath"
```

## MsBuild

```
$(MSBuildProjectDirectory)\$(OutDir)SignalRTypingsCreator.exe "$(AssemblyName)" "$(MSBuildProjectDirectory)" "$(MSBuildProjectFullPath)"
```

## c#

```csharp
var typingsCreator = new SignalRTypingsCreator.Core.SignalRTypingsCreator();
typingsCreator.Generate(new SignalRTypingsCreatorConfig
{
    AssemblyName = "MyAssembly",
    ProjectRootDir = "MyProjectRootDir",
    ProjectFileFullPath = "FullPathToProjectFile.csproj"
});
```
## Features

* Searches through the assembly for all Hub implementations and creates a definition file in the "Scripts/Typings" directory of the project.
* Respects the HubName and HubMethodName attributes
* Generates definition files for all models used in the hub (arguments and return types)
* Supports Array and IEnumerable types

## Requirements

* [SignalR 2.2](https://www.nuget.org/packages/Microsoft.AspNet.SignalR/)
* [jQuery typings](https://www.nuget.org/packages/jquery.TypeScript.DefinitelyTyped/)
* [SignalR typings](https://www.nuget.org/packages/signalr.TypeScript.DefinitelyTyped/)
* [Microsoft.Build](https://www.nuget.org/packages/Microsoft.Build)
* [Microsoft.Build.Utilities.Core](https://www.nuget.org/packages/Microsoft.Build.Utilities.Core)

## Example

ChatHub example

```csharp
public class ChatHub : Hub
{
    public void SendName(string name) { }

    public void Send(Messge message) { }
}

public class Message
{
    public string Name { get; set; }
    public string Text { get; set; }
}
```

Will result in the following typings files 

\Scripts\typings\signalrhubs\Chathub.d.ts

```csharp
interface ChatHub {
     server:ChatHubServer
     client:any
}

interface ChatHubServer {
     sendName(name:string):void
     send(message:Message):void
}

interface SignalR
{
     chatHub:ChatHub
}
```

\Scripts\typings\signalrhubs\Message.d.ts

```csharp
interface Message {
     Name:string
     Text:string
}
```

## Known issues

* Client is defined as any.
* Circular references in models result in exceptions