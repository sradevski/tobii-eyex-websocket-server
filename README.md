## Synopsis

**Tobii Web Socket Server** is a Web Socket Server that wraps the Tobii EyeX SDK and transmits the data through web sockets. It is primarily used for [EyeNav](https://github.com/sradevski/eyenav) although you may use it in your application.

## Usage

You can download an executable if you don't want to compile the project by yourself. To do that, simply download the (Release Folder)[Release/] to a location of your choice.
In order to run the server, open the terminal, navigate to the location where the Release folder is located, and run `TobiiSocketServer.exe`, which will start the server on the default port (8887). You can optionally pass a custom port number like `TobiiSocketServer.exe 8886`.

If you wish, you could also build the project by yourself. For my convenience I just uploaded the whole Visual Studio project, so you can just open it in Visual Studio and build the project.

## License

This project is under the [MIT Licence](LICENSE)
