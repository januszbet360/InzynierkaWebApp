using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataDownloader;
using DataModel;
using System.Linq;
using DataModel.Models;

namespace FootballPredictor.Tests
{
    [TestClass]
    public class TeamDataServiceTests
    {
        TeamDataService _ds = new TeamDataService();

        [TestMethod]
        public void GetTeamsFromFileListCount()
        {
            var teams = _ds.GetTeamsFromFile();
            Assert.AreEqual(teams.Count, 20 + 24 + 24 + 24);
        }

        [TestMethod]
        public void GetTeamsFromFileTeamName()
        {
            var team = _ds.GetTeamsFromFile()[0];

            Assert.AreEqual(team.Name, "Arsenal");
            Assert.AreEqual(team.FullName, "Arsenal FC");
        }

        [TestMethod]
        public void GetTeamsFromFileTeamName2()
        {
            var teams = _ds.GetTeamsFromFile();
            var team = teams[teams.Count - 1];

            Assert.AreEqual(team.Name, "Lincoln");
            Assert.AreEqual(team.FullName, "Lincoln City");
        }

        [TestMethod]
        public void InsertTeams()
        {
            _ds.InsertTeams();
            using (var ctx = new FootballEntities())
            {
                Assert.AreEqual(ctx.Teams.Count(), 20 + 24 + 24 + 24);
                Assert.AreEqual(ctx.Teams.First().FullName, "Arsenal FC");
                Assert.AreEqual(ctx.Teams.First().Name, "Arsenal");
            }
        }

    }
}
