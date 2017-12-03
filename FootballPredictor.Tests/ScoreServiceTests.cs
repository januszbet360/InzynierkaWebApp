using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDownloader;

namespace FootballPredictor.Tests
{
    /// <summary>
    /// Summary description for ScoreServiceTests
    /// </summary>
    [TestClass]
    public class ScoreServiceTests
    {
        CsvScoreService _ds = new CsvScoreService();

        [TestMethod]
        public void CheckSeasonForDate()
        {

            var jan = SeasonHelper.GetCurrentSeason(new DateTime(2017,1,10));
            var july = SeasonHelper.GetCurrentSeason(new DateTime(2017, 7, 10));
            var nov = SeasonHelper.GetCurrentSeason(new DateTime(2017, 11, 10));

            Assert.AreEqual(jan, "2016/2017");
            Assert.AreEqual(july, "2017/2018");
            Assert.AreEqual(nov, "2017/2018");
        }

        [TestMethod]
        public void CheckParseCsv()
        {
            var res = _ds.ParseCsvScores(new DateTime(2017, 10, 1));
            Assert.AreEqual(res.Count, 3);

            res = _ds.ParseCsvScores(new DateTime(2017, 1, 1));
            Assert.AreEqual(res.Count, 70);
        }

        [TestMethod]
        public void InsertScores()
        {
            var counter = _ds.InsertScores(new DateTime(2017, 1, 1));
            Assert.IsTrue(counter > 0);
        }

    }
}
