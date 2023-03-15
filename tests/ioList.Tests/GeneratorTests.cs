using System;
using System.Diagnostics;
using ioList.Shared;
using NUnit.Framework;

namespace ioList.Tests;

[TestFixture]
public class GeneratorTests
{
    private const string TestFile = @"C:\Users\tnunnink\Local\Transfer\Site.L5X";
    private const string TestDestination = @"C:\Users\tnunnink\Local\Transfer\Site-IO-List.csv";

    [Test]
    public void GenerateShouldWork()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Generator.Generate(TestFile, TestDestination);
        
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
}