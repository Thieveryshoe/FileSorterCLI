using FileSorterCLI;
using FileSorterCLILib;

var container = Bootstrapper.Bootstrap();

var app = container.GetInstance<IApp>();

app.Execute();