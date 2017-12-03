using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class ScoreModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int HalfTimeHomeGoals { get; set; }
        public int HalfTimeAwayGoals { get; set; }
        public DateTime Date { get; set; }
        public int HomeShots { get; set; }
        public int AwayShots { get; set; }
        public int HomeShotsOnTarget { get; set; }
        public int AwayShotsOnTarget { get; set; }
        public int HomeCorners { get; set; }
        public int AwayCorners { get; set; }
        public int HomeFouls { get; set; }
        public int AwayFouls { get; set; }
        public int HomeYellowCards { get; set; }
        public int AwayYellowCards { get; set; }
        public int HomeRedCards { get; set; }
        public int AwayRedCards { get; set; }
        public string Referee { get; set; }

        public string Season { get; set; }

        public Score ToDbObject()
        {
            var s = new Score();
            s.AwayCorners = AwayCorners;
            s.AwayFouls = AwayFouls;
            s.AwayGoals = AwayGoals;
            s.AwayRedCards = AwayRedCards;
            s.AwayShots = AwayShots;
            s.AwayShotsOnTarget = AwayShotsOnTarget;
            s.AwayYellowCards = AwayYellowCards;
            s.HalfTimeAwayGoals = HalfTimeAwayGoals;
            s.HalfTimeHomeGoals = HalfTimeHomeGoals;
            s.Date = Date;
            s.HomeCorners = HomeCorners;
            s.HomeFouls = HomeFouls;
            s.HomeGoals = HomeGoals;
            s.HomeRedCards = HomeRedCards;
            s.HomeShots = HomeShots;
            s.HomeShotsOnTarget = HomeShotsOnTarget;
            s.HomeYellowCards = HomeYellowCards;
            s.Referee = Referee;

            using (var ctx = new FootballEntities())
            {
                var homeTeam = ctx.Teams.FirstOrDefault(t => t.Name == HomeTeam);
                var awayTeam = ctx.Teams.FirstOrDefault(t => t.Name == AwayTeam);

                if (homeTeam != null && awayTeam != null)
                {
                    int homeId = homeTeam.Id;
                    int awayId = awayTeam.Id;

                    var match = ctx.Matches
                        .FirstOrDefault(m => m.HomeId == homeId && m.AwayId == awayId && m.Season == Season);

                    if (match != null)
                    {
                        s.MatchId = match.Id;
                    }
                }
            }
            return s;
        }

    }
}
