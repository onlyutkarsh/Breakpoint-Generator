# Breakpoint Generator

<!-- Replace this badge with your own-->
[![Build status](https://ci.appveyor.com/api/projects/status/hv6uyc059rqbc6fj?svg=true)](https://ci.appveyor.com/project/madskristensen/extensibilitytools)

<!-- Update the VS Gallery link after you upload the VSIX-->
Download this extension from the [VS Marketplace](https://marketplace.visualstudio.com/items?itemName=AndrewBHall-MSFT.BreakpointGenerator)
or get the [CI build](http://vsixgallery.com/extension/d26b7824-0b3f-4a14-aaa0-0ae9853d272c/).

---------------------------------------

This extension enables you to generate breakpoints (and TracePoints) for any public method in your application.  This quickly allows you to learn the execution flow of new code bases and add debug time logging to your applications without the need to modify the source code.

> The tool only works with C# projects

See the [change log](CHANGELOG.md) for changes and road map.

## Using the extension

Once you download and install the tool, a new menu item "Generate Breakpoints" will appear under the Debug menu

![Menu](Screenshots/menu.png)

You can then choose which projects, files, and methods to create breakpoints for

![Toolwindow](Screenshots/toolwindow.png)

The tool will by default generate TracePoints but can be configured to use a different default message or create breakpoints instead. 

![Breakpoint Config](Screenshots/breakpoint-config.png)

More information can be found in the [blog post announcing the tool](http://blogs.msdn.com/b/visualstudioalm/archive/2015/11/19/breakpoint-generator-extension.aspx).

## Contribute
Check out the [contribution guidelines](CONTRIBUTING.md)
if you want to contribute to this project.

## License
???