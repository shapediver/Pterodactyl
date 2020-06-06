using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PterodactylCharts;
using Xunit;

namespace UnitTestEngine
{
    public class TestPointGraphEngineHelper : TheoryData<bool, string, List<double>, List<double>, string, string, Color, string, string>
    {
        public TestPointGraphEngineHelper()
        {
            Add(false, "Test title", new List<double>{0.0, 1.0}, new List<double>{0.0, 1.0}, "x", "y", Color.Black, "", "");
            Add(false, "Test title", new List<double>{0.0}, new List<double>{5.0}, "x", "y", Color.Black, "", "");
            Add(false, "", new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 1.0 }, "x", "y", Color.Black, "", "");
            Add(false, "Test title", new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 1.0 }, "", "y", Color.Black, "", "");
            Add(false, "Test title", new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 1.0 }, "many words", "y", Color.Black, "", "");
            Add(true, "Test title", new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 1.0 }, "x", "y", Color.Black, "", "");
            Add(false, "Test title", new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 1.0 }, "x", "y", Color.Black, @"SampleFolder\Test.png",
                @"![Test title](SampleFolder\Test.png)");
            Add(false, "Test title", new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 1.0 }, "x", "y", Color.FromArgb(red:10, blue:11, green:12, alpha: 14), "", "");
        }
    }
    public class TestPointGraphEngineExceptionHelper : TheoryData<bool, string, List<double>, List<double>, string, string, Color, string, string>
    {
        public TestPointGraphEngineExceptionHelper()
        {
            Add(false, "Test title", new List<double> { 0.0 }, new List<double> { 0.0, 1.0 }, "x", "y", Color.Black, "", 
                "X Values should match Y Values - check if both lists have the same number of elements.");
            Add(false, "Test title", new List<double> { 0.0, 1.0 }, new List<double> { 0.0 }, "x", "y", Color.Black, "",
                "X Values should match Y Values - check if both lists have the same number of elements.");
        }
    }

    public class TestPointGraphEngine
    {
        [Theory]
        [ClassData(typeof(TestPointGraphEngineHelper))]
        public void CorrectData(bool showGraph, string title,
            List<double> xValues, List<double> yValues, string xName,
            string yName, Color color, string path, string expected)
        {
            PointGraphEngine testObject = new PointGraphEngine(showGraph, title, xValues, yValues, xName, yName, color, path);
            Assert.Equal(showGraph, testObject.ShowGraph);
            Assert.Equal(title, testObject.Title);
            Assert.Equal(xValues, testObject.XValues);
            Assert.Equal(yValues, testObject.YValues);
            Assert.Equal(xName, testObject.XName);
            Assert.Equal(yName, testObject.YName);
            Assert.Equal(color, testObject.ColorData);
            Assert.Equal(path, testObject.Path);
        }

        [Theory]
        [ClassData(typeof(TestPointGraphEngineHelper))]
        public void CheckReportCreation(bool showGraph, string title,
            List<double> xValues, List<double> yValues, string xName,
            string yName, Color color, string path, string expected)
        {
            PointGraphEngine testObject = new PointGraphEngine(showGraph, title, xValues, yValues, xName, yName, color, path);
            string actual = testObject.Create();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestPointGraphEngineExceptionHelper))]
        public void CheckExceptions(bool showGraph, string title,
            List<double> xValues, List<double> yValues, string xName,
            string yName, Color color, string path, string message)
        {
            var exception = Assert.Throws<ArgumentException>(() => new PointGraphEngine(showGraph, title, xValues, yValues, xName, yName, color, path));
            Assert.Equal(message, exception.Message);
        }
    }
}