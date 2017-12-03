using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models
{
    public class MatchStatisticsModel
    {
        public String HomeTeam { get; set; }
        public String AwayTeam { get; set; }
        public DateTime? Date { get; set; }
        public int? AwayScorePrediction { get; set; }
        public int? HomeScorePrediction { get; set; }
        public int Round { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
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
        public int HalfTimeHomeGoals { get; set; }
        public int HalfTimeAwayGoals { get; set; }
        public string Referee { get; set; }
        public string Season { get; set; }

        public MatchStatisticsModel(string homeTeam, string awayTeam, DateTime? date, int? awayScorePrediction, int? homeScorePrediction, 
                                    int round, int homeGoals, int awayGoals, int homeShots, int awayShots, int homeShotsOnTarget, 
                                    int awayShotsOnTarget, int homeCorners, int awayCorners, int homeFouls, int awayFouls, 
                                    int homeYellowCards, int awayYellowCards, int homeRedCards, int awayRedCards, int halfTimeHomeGoals,
                                    int halfTimeAwayGoals, string referee, string season)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Date = date;
            AwayScorePrediction = awayScorePrediction;
            HomeScorePrediction = homeScorePrediction;
            Round = round;
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
            HomeShots = homeShots;
            AwayShots = awayShots;
            HomeShotsOnTarget = homeShotsOnTarget;
            AwayShotsOnTarget = awayShotsOnTarget;
            HomeCorners = homeCorners;
            AwayCorners = awayCorners;
            HomeFouls = homeFouls;
            AwayFouls = awayFouls;
            HomeYellowCards = homeYellowCards;
            AwayYellowCards = awayYellowCards;
            HomeRedCards = homeRedCards;
            AwayRedCards = awayRedCards;
            HalfTimeHomeGoals = halfTimeHomeGoals;
            HalfTimeAwayGoals = halfTimeAwayGoals;
            Referee = referee;
            Season = season;
        }
    }
}