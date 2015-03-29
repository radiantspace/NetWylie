using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using org.rigpa.wylie;
using System.Reflection;
using System.Diagnostics;

namespace NetWylie.Tests
{
    [TestClass]
    public class WylieTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var overallpass = true;
            var w = new Wylie(true, false, false, true);
            var w2 = new Wylie(true, true, false, true);
            int rownum = 1;
            foreach(var row in GetTestData())
            {
                rownum++;
                if (rownum == 209 || rownum == 211)
                    rownum.ToString();
                try
                {
                    var wylie = row[0];
                    var uni = row[1];
                    var warns = Int32.Parse(row[2]);
                    var wylie2 = row[3];
                    var wyliewarns = Int32.Parse(row[4]);
                    var uni_diff = Int32.Parse(row[5]);

                    var e = new List<string>();
                    var s = w.fromWylie(wylie, e);
                    var e2 = new List<string>();
                    var s2 = w2.fromWylie(wylie, e2);
                    var e3 = new List<string>();
                    var rewylie = w.toWylie(s, e3, true);
                    var reuni = w.fromWylie(rewylie);

                    if (!s.Equals(s2))
                        throw new Exception("got diff unicode w/ and w/o strict checking.");

                    if (!s.Equals(uni))
                        throw new Exception(String.Format("Expected {0}, but got {1}", uni, s));

                    if (e.size() != 0 && e2.size() == 0)
                        throw new Exception("Got warnings in easy mode but not in strict!");

                    if(warns == 0)
                    {
                        if (e.size() > 0 || e2.size() > 0)
                            throw new Exception("No warnings expected.");
                    }
                    else if (warns == 1)
                    {
                        if (e.size() != 0)
                        {
                            throw new Exception("No non-strict warnings expected.");
                        }
                        if(e2.size() == 0)
                        {
                            throw new Exception("Expected strict warnings.");
                        }
                    }
                    else if (warns == 2)
                    {
                        if (e.size() == 0)
                            throw new Exception("Expected non-strict warnings.");
                    }

                    if(!wylie2.Equals(rewylie))
                        throw new Exception(String.Format("to_wylie expected ({0}), got ({1})", wylie2, rewylie));

                    if(wyliewarns == 0)
                    {
                        if(e3.size()!= 0)
                            throw new Exception("unexpected to_wylie warnings.");
                    }
                    else
                    {
                        if(e3.size() == 0)
                            throw new Exception("missing expected to_wylie warnings.");
                    }

                    if(uni_diff > 0) {
                        if(reuni.Equals(s))
                            throw new Exception("should not round-trip to Unicode.");
                    }
                    else
                    {
                        if(!reuni.Equals(s))
                            throw new Exception("should round-trip to Unicode.");
                    }

                    Debug.WriteLine("Row {0}: PASS", rownum);
                    WriteTestResult("Row {0}: PASS", rownum);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Row {0}: FAIL: {1}", rownum, ex.toString());
                    overallpass = false;
                }
            }
            Assert.IsTrue(overallpass);
        }

        private void WriteTestResult(string message, params object[] args)
        {
            File.AppendAllText("wylie_test_results.txt",String.Format(message, args));
        }

        private List<string[]> GetTestData()
        {
            var list = new List<string[]>();
            using(var file = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("NetWylie.Tests.test.txt")))
            {
                while(!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    if(!line.StartsWith("#"))
                        list.Add(line.Split("\t".ToCharArray()));
                }
            }
            return list;
        }
    }
}
