# GHUnitTest
A Unit Test project for grasshopper

## Instructions
1. Build GH_UnitTest. The `.gha` file should automatically be copied into the right grasshopper component folder
2. Follow the example test `TestGrasshopper.cs` in `UnitTest`. This uses the gh definition `sum.gh` as a simple use case

## Improvements to-do list
* Open Rhino once at the beginning of the Unit test and close it at the end using
```
    [TestInitialize]  
    public void TestGrasshopper()  
    {  
        ...
    }
```
and
```
    [TestCleanup]  
    public void testClean()  
    {  
        ...
    }  
```
* Use TCP/IP connection to communicate with the unit test server rather than serializing to file