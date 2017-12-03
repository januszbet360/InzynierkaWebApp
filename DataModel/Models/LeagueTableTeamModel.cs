using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class LeagueTableTeamModel
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        public int PlayedGames { get; set; }
        public int Points { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalsDifference { get { return Goals - GoalsAgainst; } }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }

        public string ImageUrl { get; set; }
    }
}
