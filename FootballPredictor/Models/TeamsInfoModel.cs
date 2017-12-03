using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models
{
    public class TeamsInfoModel
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string SeasonSince { get; set; }
        public string SeasonTo { get; set; }
        public IEnumerable<TeamsInfoModel> teamsInfoModel { get; set; }

    }
}