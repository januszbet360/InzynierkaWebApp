using DataDownloader;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models
{
    public class MainPageModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Fixtures { get; set; }
        public Nullable<int> AwayScorePrediction { get; set; }
        public Nullable<int> HomeScorePrediction { get; set; }
        public Nullable<int> HomeScore { get; set; }
        public Nullable<int> AwayScore { get; set; }
        public DateTime? Date { get; set; }
        public MainPageModel(string home, string away, int? homeScorePrediction, int? awayScorePrediction, int? awayScore, int? homeScore, DateTime? date)
        {
            HomeTeam = home;
            AwayTeam = away;
            HomeScorePrediction = homeScorePrediction;
            AwayScorePrediction = awayScorePrediction;
            AwayScore = awayScore;
            HomeScore = homeScore;
            Date = date;

        }
    }
}