
This PoC uses advanced OOP concepts to achieve the two major goals in software design: Low coupling and high cohesion. In order to get it, 
the following key features/techniques are used:

. Dependency Inversion Principle: Done through the use of interfaces.<br>
. Dependency Injection: Instances of other classes are passed as arguments to the dependent classes.<br>
. IoC Container: It uses the built-in .NET Core framework support for that.<br>
. Outer layers are only allowed to depend on more inner layers. It means that it is only allowed to start from the presentation layer<br>
towards the application layer (based on Onion Architecture).

<b>The great point of this PoC is: ease of implementing new user interfaces without affect other parts of the software</b>.

![](https://github.com/prog-lessons/csharp/blob/master/ProgLessons.IoC-example/example%20ioc.png)

To understand how all this works, you may start analyzing the entry points in the presentation layer:

[Console](https://github.com/prog-lessons/csharp/blob/master/ProgLessons.IoC-example/Presentation/ProgLessons.IoC-example.Presentation.UI.Console/Program.cs)
<br>
[Avalonia UI](https://github.com/prog-lessons/csharp/blob/master/ProgLessons.IoC-example/Presentation/ProgLessons.IoC-example.Presentation.UI.Avalonia/App.xaml.cs)

If you wish to switch the user interface on running the program, modify launch.json ("configurations":{"program", "cwd"}) in .vscode folder.

### Demo
<b>Console</b>

![](https://user-images.githubusercontent.com/24251894/103371692-e1eb6f00-4aae-11eb-96e9-7706bb39281f.gif)
<br><br>
<b>GUI - Avalonia</b>

![](https://user-images.githubusercontent.com/24251894/103363368-b9a54580-4a99-11eb-87ab-b94662d060fd.gif)

### Development Environment

Linux Debian Buster<br>
VS Code<br>
.NET Core 3.1
