using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictiveSystemManagement
{
    public class Match
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public int AwayId { get; set; }
        public string Date { get; set; }
        public int PredictedScoreId { get; set; }
        public int RealScoreId { get; set; }
        public string Referee { get; set; }

        public Match(int id, int homeId, int awayId, string date, int predictedScoreId, int realScoreId, string referee)
        {
            this.Id = id;
            this.HomeId = homeId;
            this.AwayId = awayId;
            this.Date = date;
            this.PredictedScoreId = predictedScoreId;
            this.RealScoreId = realScoreId;
            this.Referee = referee;
        }
    }
}
