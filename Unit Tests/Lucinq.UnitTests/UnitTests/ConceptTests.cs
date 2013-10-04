﻿using System;
using System.Diagnostics;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucinq.Enums;
using Lucinq.Interfaces;
using Lucinq.Querying;
using NUnit.Framework;

namespace Lucinq.UnitTests.UnitTests
{
    [TestFixture]
    public class ConceptTests
    {
        /// <summary>
        /// Test to show the speed of opening / closing ram directory
        /// </summary>
        [Test]
        public void OpeningClosingAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Opening FS Dir");
            FSDirectory fileSystemDirectory = FSDirectory.Open(new DirectoryInfo(GeneralConstants.Paths.CarDataIndex));
            WriteTime(stopwatch);
            Console.WriteLine("Opening Ram Dir");
            RAMDirectory ramDirectory = new RAMDirectory(fileSystemDirectory);
            WriteTime(stopwatch);
            ramDirectory.Dispose();
            WriteTime(stopwatch);
            Console.WriteLine("Disposed Ram Dir");
            fileSystemDirectory.Dispose();
            WriteTime(stopwatch);
            Console.WriteLine("Disposed FS Dir");
            stopwatch.Stop();
        }

        /// <summary>
        /// Test to show the speed of opening / closing fs directory
        /// </summary>
        [Test]
        public void OpeningClosingFsOnlyObjects()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Opening FS Dir");
            FSDirectory fileSystemDirectory = FSDirectory.Open(new DirectoryInfo(GeneralConstants.Paths.CarDataIndex));
            WriteTime(stopwatch);
            fileSystemDirectory.Dispose();
            WriteTime(stopwatch);
            Console.WriteLine("Disposed FS Dir");
            stopwatch.Stop();
        }

        [Test]
        public void OpenCloseOpenClose()
        {
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
            OpeningClosingAll();
        }

        [Test]
        public void OpenCloseFsOnly()
        {
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
            OpeningClosingFsOnlyObjects();
        }

        [Test]
        public void LuceneObjectsIntoLucinq()
        {
            LuceneSearch search = new LuceneSearch(GeneralConstants.Paths.BBCIndex);
            // raw lucene object
            TermQuery query = new TermQuery(new Term(BBCFields.Title, "africa"));
            
            // executed directly by the search
            LuceneSearchResult result = search.Execute(query);
            Assert.AreEqual(8, result.TotalHits);

            // or by through a querybuilder
            IQueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.Add(query, Matches.Always);
            LuceneSearchResult result2 = search.Execute(queryBuilder);
            Assert.AreEqual(8, result2.TotalHits);
        }

        private void WriteTime(Stopwatch stopwatch)
        {
            Console.Write(stopwatch.ElapsedTicks + " - ");
            Console.Write(stopwatch.ElapsedMilliseconds + "\r\n");
        }
    }
}