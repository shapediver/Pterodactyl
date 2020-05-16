﻿using System;
using System.Collections.Generic;
using PterodactylEngine;
using Xunit;

namespace UnitTestEngine
{
    public class TestTableHelper : TheoryData<List<string>, List<int>, string[,], List<string>>
    {
        public TestTableHelper()
        {
            Add(new List<string> { "Head 1", "Head 2", "Head 3" },
                new List<int> { 0, 0, 0 },
                new string[,] { { "2", "5" }, { "lol", "hrhr" }, { "rrrr", "hohoho" } },
                new List<string> { "" });
        }

    }

    public class TestMaxStringLengthHelper : TheoryData<List<string>, List<int>, string[,], List<int>>
    {
        public TestMaxStringLengthHelper()
        {
            Add(new List<string> { "Head 1", "Head 2", "A" },
                new List<int> { 0, 0, 0 },
                new string[,] 
                { 
                    { "2", "5" },
                    { "lol", "hrhrhrh" },
                    { "r", "h" } 
                },
                new List<int> { 6, 7, 1 });

            Add(new List<string> { "", "a"},
                new List<int> { 0, 2 },
                new string[,]
                {
                    { "", "", "" },
                    { "a", "a", "a" },
                },
                new List<int> { 0, 1 });
        }
    }

    public class TestPrepareColumnSize
    {
        [Theory]
        [InlineData(0, 4)]
        [InlineData(1, 4)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(1000, 1000)]
        public void Data(int maxStringLength, int expected)
        {
            Table testObject = new Table(new List<string> {""}, new List<int> {0}, new string[,] { {"0","0"} });
            int actual = testObject.PrepareColumnSize(maxStringLength);
            Assert.Equal(expected, actual);
        }
    }

    public class TestTable
    {
        [Theory]
        [ClassData(typeof(TestTableHelper))]
        public void CorrectData(List<string> headings, List<int> alignment, string[,] dataTree, List<string> expected)
        {
            Table testObject = new Table(headings, alignment, dataTree);
            Assert.Equal(headings, testObject.Headings);
            Assert.Equal(alignment, testObject.Alignment);
            Assert.Equal(dataTree, testObject.DataTree);
        }
    }

    public class TestMaxStringLength
    {
        [Theory]
        [ClassData(typeof(TestMaxStringLengthHelper))]
        public void CorrectData(List<string> headings, List<int> alignment, string[,] dataTree, List<int> expected)
        {
            Table testObject = new Table(headings, alignment, dataTree);
            List<int> actual = testObject.MaxStringLength;

            Assert.Equal(expected, actual);
        }
    }
}
