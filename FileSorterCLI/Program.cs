using FileSorterCLI;
using FileSorterCLILib;

var container = Bootstrapper.Bootstrap();

var app = container.GetInstance<IApp>();

// TODO args to Options class
app.Execute(args[0], args[1]);