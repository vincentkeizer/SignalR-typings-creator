# SignalR typings creator

A simple command line tool for creating TypeScript definition files from SignalR Hubs.

## NuGet

[SignalRTypingsCreator](https://www.nuget.org/packages/SignalRTypingsCreator)

```
Install-Package SignalRTypingsCreator
```

The nuget package contains a post build target which triggers the command line tool for the current project.

As of 0.4.0, the files aren't automatically added to the project anymore.
To add files to dotnet 4.6 projects, please install the following package:

[SignalRTypingsCreator.DotNet46ProjectFileUpdater](https://www.nuget.org/packages/SignalRTypingsCreator.DotNet46ProjectFileUpdater)


## Features

* Searches through the assembly for all Hub implementations and creates a definition file in the "Scripts/Typings/signalrhubs" directory of the project.
* Supports Hub and Hub\<T> implementations
* Respects the HubName and HubMethodName attributes
* Generates definition files for all models used in the hub (arguments and return types)
* Supports Array and IEnumerable types

## Implementation

### Server

All Hubs (implementations of Microsoft.AspNet.SignalR.Hub) in the current assembly are automatically discovered. 
All public methods are added to the server object of the hub.

### Client

For the client of the hub, there are two possibilities:
* Implement the Hub\<T> class. A client interface will be created based on type T.
* Create an interface which inherits from **SignalRTypingsCreator.Core.Hubs.IHubClient\<T>** where T is the hub implementation class.
All methods defined in the interface are added to the client object of the hub.

When no interface for the hub is provided, the client is generated as type "any".

## Example

ChatHub example

C#:

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

public interface IChatHubClient : IHubClient<ChatHub>
{
    void BroadcastMessage(string message, string name);
}

```

Will result in the following typings files 

\Scripts\typings\signalrhubs\Chathub.d.ts

```csharp
interface ChatHub {
     server:ChatHubServer,
     client:ChatHubClient
}

interface SignalR
{
     chatHub:ChatHub
}

interface ChatHubServer {
     sendName(name:string):void
     send(message:Message):void
}

interface ChatHubClient {
     broadcastMessage(message:string, name:string):void
}
```

\Scripts\typings\signalrhubs\Message.d.ts

```csharp
interface Message {
     Name:string
     Text:string
}
```

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

## Dependencies

* [Microsoft.AspNet.SignalR.Core 2.2](https://www.nuget.org/packages/Microsoft.AspNet.SignalR.Core/)
* [jQuery typings](https://www.nuget.org/packages/jquery.TypeScript.DefinitelyTyped/)
* [SignalR typings](https://www.nuget.org/packages/signalr.TypeScript.DefinitelyTyped/)
* [Microsoft.Build](https://www.nuget.org/packages/Microsoft.Build)
* [Microsoft.Build.Utilities.Core](https://www.nuget.org/packages/Microsoft.Build.Utilities.Core)
* [System.Collections.Immutable](https://www.nuget.org/packages/System.Collections.Immutable)

## Common issues

* Installing SignalR typings nuget package will also install signalr-1.0.d.ts. 
Please delete this file, this will cause subsequent variable declarations errors.
* When the error "Could not load file or assembly 'System.Collections.Immutable, Version=1.2.1.0" occurs, please update the NuGet package "Microsoft.Net.Compilers" to at least 1.1.0.
Version 1.0.0 forces an old version of the "System.Collections.Immutable" assembly to be used, which can override the "System.Collections.Immutable" nuget package version. 