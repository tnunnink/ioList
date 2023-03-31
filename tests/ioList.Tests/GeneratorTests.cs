using System;
using System.Diagnostics;
using ioList.Generation;
using ioList.Model;
using NUnit.Framework;

namespace ioList.Tests;

[TestFixture]
public class GeneratorTests
{
    private const string TestFile = @"C:\Users\tnunnink\Local\Tests\L5X\Template.L5X";
    private const string TestDestination = @"C:\Users\tnunnink\Local\Tests\L5X\Output\IOList.csv";

    [Test]
    public void GenerateShouldWork()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var generator = new Generator(TestFile, TestDestination, new GeneratorConfig());
        
        generator.Generate();
        
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
}