using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDownloader;
using DataModel;
using System.IO;
using System;

namespace FootballPredictor.Tests
{
    [TestClass]
    public class ApiServiceTests
    {
        ApiDownloader _api = new ApiDownloader();

        [TestMethod]
        public void GetCsv()
        {
            var path = _api.GetScoresCsv();
            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void GetFixture()
        {
            var s = _api.GetMatchdayJson(1);
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void GetAllFixtures()
        {
            var s = _api.GetAllFixturesJson();
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void GetTable()
        {
            var t = _api.GetTableJson();
            Assert.IsNotNull(t);
        }

    }
}
