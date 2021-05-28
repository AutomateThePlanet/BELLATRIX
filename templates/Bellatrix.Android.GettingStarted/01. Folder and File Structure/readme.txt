Find detailed information about what each empty project contains or should contain if you wish to create it manually.

1. Each new BELLATRIX tests project targets .NET Core 5.0

2. Contains the following NuGet dependencies:
<PackageReference Include="Bellatrix.Mobile.NUnit" Version="1.1.0.16" />

<PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.3.0" />
<PackageReference Include="NUnit" Version="3.13.2" />
<PackageReference Include="NUnit3TestAdapter" Version="3.17.0"/>
<PackageReference Include="StyleCop.Analyzers" Version="1.1.0-beta004"/>

Note: the version may vary if you install the template now.

In short:
 - we reference Microsoft configuration packages so that we can work with configuration files where the different framework settings are placed.
 - Microsoft.NET.Test.Sdk, NUnit.TestAdapter, NUnit.TestFramework are prerequisites so that you can execute NUnit framework tests.
 - also, we use Unity inversion of control container inside BELLATRIX for many things. 
 You will not be able to use it directly, but there are a couple of ways that you will use it in your code for some more complex scenarios.
 - Lastly, we install StyleCop.Analyzers, we use it to enforce coding standards in the tests code.

3. .editorconfig You can read more about it here- https://automatetheplanet.com/coding-styles-editorconfig/
In short: EditorConfig helps developers define and maintain consistent coding styles between different editors and IDEs. 
The EditorConfig project consists of a file format for defining coding styles and a collection of text editor plugins that enable editors to read the file format and adhere to defined styles. 
EditorConfig files are easily readable, and they work nicely with version control systems.
You can override the global Visual Studio settings through a .editorconfig file placed on solution level.
All projects come with a predefined set of this rules that we advise you to use. You can always change them to follow your company's global coding standards.

4. StyleCop files
StyleCop is an open source static code analysis tool from Microsoft that checks C# code for conformance to StyleCop's recommended coding styles and a subset of Microsoft's .NET Framework Design Guidelines.
The StyleCopAnalyzers open source project is similar to EditorConfig. It integrates with all versions of Visual Studio. 
It contains set of style and consistency rules. The code is checked on a build. If some of the rules are violated warning messaged are displayed. 
This way you can quickly locate the problems and fix them.
You can find more detailed information here: https://automatetheplanet.com/style-consistency-rules-stylecop/.

All projects come with predefined StyleCop rules:
- stylecop.json
- StyleCopeRules.ruleset

Note: You can reuse both .editorconfig and StyleCop files. Place them in a folder inside your solution and change their paths inside your projects' MSBuild files.

As with .editorconfig, you can change the predefined rules to fit your company's standards.

5. Test framework settings files

There are three files testFrameworkSettings. They are JSON files. These are the main settings files for .NET Core and .NET Standard libraries.
Depending on your build configuration the different files are used. For example, if you run your tests in Debug the testFrameworkSettings.Debug.json file is used.

Note: There isn't a way as in .NET Framework to reuse the content, so if you want to make changes you need to do it in each file separately. 

There is a separate more detailed section in the guide describing how to use the configuration files.

6. TestInitialize file
This is the entry point for all tests. The methods here are executed only once per tests execution. You need it to start and stop some BELLATRIX services that you can use in your tests.

Note: There are separate sections describing in more details the AndroidTest base class and the App class.

7. Categories

Contains constants that we use to mark our tests for easier filtering.