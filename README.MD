# Sample Play Test Project For Unity

This Git Repository provide information on how to do Play Test with **"Test First"** approach.

## ToDo

- [ ] add additional info regarding additional use (code coverage experimentation)
- [ ] add edit mode test for single test case

## How To Use

you should try to checkout to first commit and move forward 1 by 1 to understand the process of creating it. but if you just want to know one of many way to do play test just see the code yourselves

## Run Unity Test Without Too Many OverHead

below is the command to execute unity Test from command line without much overhead.
edit it to your needs

``` powershell
.\Unity.exe `
-runTests `
-ProjectPath D:\Projects\Unity\TestProjects\ `
-testResults D:\UnityTestResult\result.xml `
-testPlatform playmode `
-logFile D:\UnityTestResult\Logfiles.txt `
-batchMode -nographics -noUpm `
| Out-Null
```

.\Unity.exe -runTests -ProjectPath D:\Projects\Unity\onboard-crossword\ -testResults D:\UnityTestResult\resultcover.xml -testPlatform editmode -logFileD:\UnityTestResult\Logfiles2.txt -batchMode -nographics -noUpm | Out-Null

## what i've tried so far in experimenting code coverage in unity

### running test in console outside of unity environment / toolkit

since coverlet inject the process of test in console test runner the first thing that come to mind is getting unity test to be able to run in `dotnet test` / `vstest.console` / `nunit3-console`

all these test doesnt have big difference and mostly have same output on mentioned tool

#### normal test

```powershell
dotnet build .\EditModeTest.csproj
dotnet test .\EditModeTest.csproj --no-build -v d
```

> normal build and test. test not detected

#### add adapter path

```powershell
dotnet test .\EditModeTest.csproj --no-build -v d --test-adapter-path <path/to/adapter>
```

> same result

#### add nunit package to existing csproj

```powershell
dotnet add .\EditModeTest.csproj package nunit
```

> Unity might disable registering additional package in generated csproj through external tool

```output
info : Adding PackageReference for package 'nunit' into project '.\EditModeTest.csproj'.
error: Error while adding package 'nunit' to project '.\EditModeTest.csproj'. The project does not support adding package references through the add package command.
```

#### forcefully inject package reference in `.csproj` file

when you cant insert package dependency through dotnet commands inject it directly through the file itself

![img1][forceInjectCsprojImg]

##### output

```output
Build started 8/1/2019 2:39:15 PM.
Test run for D:\Projects\Unity\onboard-crossword\Temp\bin\Debug\EditModeTest.dll(.NETFramework,Version=v4.7.1)
Microsoft (R) Test Execution Command Line Tool Version 16.0.1
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
Multiple versions of same extension found. Selecting the highest version.
  NUnit3.TestAdapter : 3.13.0.0
NUnit Adapter 3.13.0.0: Test execution started
Running all tests in D:\Projects\Unity\onboard-crossword\Temp\bin\Debug\EditModeTest.dll
Exception NUnit.Engine.NUnitEngineException, Exception thrown executing tests
An exception occurred in the driver while loading tests.
   at NUnit.Engine.Runners.DirectTestRunner.LoadDriver(IFrameworkDriver driver, String testFile, TestPackage subPackage)
   at NUnit.Engine.Runners.DirectTestRunner.LoadPackage()
   at NUnit.Engine.Runners.TestDomainRunner.LoadPackage()
   at NUnit.Engine.Runners.DirectTestRunner.EnsurePackageIsLoaded()
   at NUnit.Engine.Runners.DirectTestRunner.Explore(TestFilter filter)
   at NUnit.Engine.Runners.MasterTestRunner.Explore(TestFilter filter)
   at NUnit.VisualStudio.TestAdapter.NUnit3TestExecutor.RunAssembly(String assemblyPath, TestFilter filter) in D:\repos\NUnit\nunit3-vs-adapter\src\NUnitTestAdapter\NUnit3TestExecutor.cs:line 310
   at NUnit.VisualStudio.TestAdapter.NUnit3TestExecutor.RunTests(IEnumerable`1 sources, IRunContext runContext, IFrameworkHandle frameworkHandle) in D:\repos\NUnit\nunit3-vs-adapter\src\NUnitTestAdapter\NUnit3TestExecutor.cs:line 103
Innerexception: System.IO.FileLoadException: Could not load file or assembly 'nunit.framework, Version=3.11.0.0, Culture=neutral, PublicKeyToken=2638cd05610744eb' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
File name: 'nunit.framework, Version=3.11.0.0, Culture=neutral, PublicKeyToken=2638cd05610744eb' ---> System.IO.FileLoadException: Could not load file or assembly 'nunit.framework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=2638cd05610744eb' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
File name: 'nunit.framework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=2638cd05610744eb'


WRN: Assembly binding logging is turned OFF.
To enable assembly bind failure logging, set the registry value [HKLM\Software\Microsoft\Fusion!EnableLog] (DWORD) to 1.
Note: There is some performance penalty associated with assembly bind failure logging.
To turn this feature off, remove the registry value [HKLM\Software\Microsoft\Fusion!EnableLog].

   at System.Reflection.RuntimeAssembly._nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)
   at System.Reflection.RuntimeAssembly.nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)
   at System.Reflection.RuntimeAssembly.InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)
   at System.Activator.CreateInstance(String assemblyString, String typeName, Boolean ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes, Evidence securityInfo, StackCrawlMark& stackMark)
   at System.Activator.CreateInstance(String assemblyName, String typeName, Boolean ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes, Evidence securityInfo)
   at System.AppDomain.CreateInstance(String assemblyName, String typeName, Boolean ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes, Evidence securityAttributes)
   at System.AppDomain.CreateInstanceAndUnwrap(String assemblyName, String typeName, Boolean ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes, Evidence securityAttributes)
   at System.AppDomain.CreateInstanceAndUnwrap(String assemblyName, String typeName, Boolean ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes, Evidence securityAttributes)
   at NUnit.Engine.Drivers.NUnit3FrameworkDriver.CreateObject(String typeName, Object[] args)
   at NUnit.Engine.Drivers.NUnit3FrameworkDriver.Load(String testAssemblyPath, IDictionary`2 settings)
   at NUnit.Engine.Runners.DirectTestRunner.LoadDriver(IFrameworkDriver driver, String testFile, TestPackage subPackage)


NUnit Adapter 3.13.0.0: Test execution complete
No test is available in D:\Projects\Unity\onboard-crossword\Temp\bin\Debug\EditModeTest.dll. Make sure that test discoverer & executors are registered and platform & framework version settings are appropriate and try again.
```

also tried to mix match removing one of each package plugin and by doing so showing different result,but most of em still shows similar exception

#### Conclusion

> Unity run nunit test using their own test runner to the point where nunit developer doesn't know how they run their script in unity. [source](https://github.com/nunit/nunit-console/issues/538)

### Using Coverlet with Unity Command Line Interface

as the previous option fails it leaves using coverlet global tool as last option of using unity generated .csproj file, the idea is using previously built dll then crosscheck it with command running unity test process

from that in mind i try to use coverlet global tool by using command below

```powershell
coverlet .\Temp\bin\Debug\EditModeTest.dll --target unity_headless_test.bat
```

or

```powershell
coverlet .\obj\Debug\EditModeTest.dll --target unity_headless_test.bat
```

i tried many possible condition including:

- through dotnet and msbuild then run unity test
- using duplicated project to run test as running test clean temp folder once finished
- using duplicated project obj folder

below are closest result it output

```output
Calculating coverage result...
  Generating report 'D:\Projects\Unity\onboard-crossword-Copy\coverage.json'
+-------------------+------+--------+--------+
| Module            | Line | Branch | Method |
+-------------------+------+--------+--------+
| Assembly-CSharp   | 0%   | 0%     | 0%     |
+-------------------+------+--------+--------+
| GameScripts       | 0%   | 0%     | 0%     |
+-------------------+------+--------+--------+
| JSONChecker       | 0%   | 0%     | 0%     |
+-------------------+------+--------+--------+
| PlayModeTest      | 0%   | 0%     | 0%     |
+-------------------+------+--------+--------+
| ThirdPartyPlugins | 0%   | 0%     | 0%     |
+-------------------+------+--------+--------+

+---------+------+--------+--------+
|         | Line | Branch | Method |
+---------+------+--------+--------+
| Total   | 0%   | 0%     | 0%     |
+---------+------+--------+--------+
| Average | 0%   | 0%     | 0%     |
+---------+------+--------+--------+
```

or

```output
  Generating report 'D:\Projects\Unity\onboard-crossword\coverage.json'
+--------+------+--------+--------+
| Module | Line | Branch | Method |
+--------+------+--------+--------+

+---------+------+--------+--------+
|         | Line | Branch | Method |
+---------+------+--------+--------+
| Total   | 100% | 100%   | 100%   |
+---------+------+--------+--------+
| Average | ∞%   | ∞%     | ∞%     |
+---------+------+--------+--------+
```

### I'm running out of option

After several hour researching in internet exhaust me out of any option possible to expand any more way to enable nunit test into unity (especially if we are going to use unity test runner). here's the reason:

#### Unity Test Runner doesn't use NUnit Test Framework

instead they made custom NUnit framework called [Unity Test Framework][UnityTestFrameworkManualPage]. This framework behave differently from NUnit Framework thus wont make any NUnit test adapter working in any way possible (unless unity publish how their test framework are working). By making NUnit test adapter not able to recognize their test it render all code coverage tool that work with NUnit useless. The reason behind it is most code coverage tool depends on their ability to inject NUnit Test process, when unity test aren't recognized with NUnit Framework resulting adapter wont run any Unity Test and any code coverage tool can't inject unity with any calculation metric. using [coverlet][coverletGithubPage] global tool also not possible since with different nunit framework, makes test run by unity doesn't recognized as any NUnit test.

#### Possible chance using Hard Option

##### compiling script one by one referencing their needs and dividing their usage

this is one tedious process which probably hard to be done, especially without implemented assembly definition that group script and their reference. even with it does we still need to compile it outside unity and not using provided csproj referencing only some of unity component. later we compile the test outside of unity environment using nunit framework and run it with it as well

##### creating library (plugin) based development system

Unity is able to read dll file as plugin including c#, and by doing so unity give an option to use provided `unityengine.dll` to build plugin with their dll containing custom api. this way we can make [unity related library][unityDocManagedPluginPage] outside of unity environment and test it externally. This option have big flaw as developer need to develop plugin/ library first outside of unity implementing their component before moving it to unity environment as dll. it enable nunit testing and a possibility that nunit test adapter able to discover it.

### what next

next experiment that are going to be done is probably compiling file by file slowly

[forceInjectCsprojImg]: ./img/force_inject_csproj.png
[UnityTestFrameworkManualPage]: (https://docs.unity3d.com/Packages/com.unity.test-framework@1.0/manual/index.html)
[coverletGithubPage]: https://github.com/tonerdo/coverlet
[unityDocManagedPluginPage]: https://docs.unity3d.com/Manual/UsingDLL.html
