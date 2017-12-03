using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDownloader;
using DataModel;
using System.Linq;

namespace FootballPredictor.Tests
{
    [TestClass]
    public class MatchdayTests
    {
        private readonly MatchdayService _ds = new MatchdayService();

        [TestMethod]
        public void GetMatches()
        {
            var res = _ds.GetMatches(1);

            Assert.AreEqual(res[0].HomeTeam, "Arsenal FC");
        }

        [TestMethod]
        public void CheckMatchesCount()
        {
            var res = _ds.GetMatches(1);
            Assert.AreEqual(res.Count*2, 20);
        }

        [TestMethod]
        public void InsertAllMatches()
        {
            var res = _ds.InsertAllMatches();
            Assert.IsTrue(res > 0);
            using (var ctx = new FootballEntities())
            {
                Assert.AreEqual(ctx.Matches.Max(m => m.Matchweek), 38);
                // TODO
            }
        }
        
    }
}
