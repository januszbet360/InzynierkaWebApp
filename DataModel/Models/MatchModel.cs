using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? HomeGoalsPredicted { get; set; }
        public int? AwayGoalsPredicted { get; set; }
        public string Season { get; set; }
        public int Matchweek { get; set; }

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public Match ToDbObject()
        {
            var m = new Match();

            m.Matchweek = Matchweek;
            m.Season = Season;
            m.Date = Date;
            using (var ctx = new FootballEntities())
            {
                m.HomeId = ctx.Teams.First(t => t.FullName == HomeTeam).Id;
                m.AwayId = ctx.Teams.First(t => t.FullName == AwayTeam).Id;
            }
            return m;
        }
    }
}
