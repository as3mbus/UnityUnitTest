# Sample Play Test Project For Unity

This Git Repository provide information on how to do Play Test with **"Test First"** approach.

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