using CsvHelper;
using DataModel;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloader
{
    public class CsvScoreService
    {
        string dir = AppDomain.CurrentDomain.BaseDirectory;

        public List<ScoreModel> ParseCsvScores(DateTime startDate)
        {
            var scores = new List<ScoreModel>();

            //using (TextReader reader = File.OpenText(dir + "\\" + "App_Data\\" + Constants.CSV_FILE_NAME))
            using (TextReader reader = File.OpenText(dir + "\\" + Constants.CSV_FILE_NAME))
            {
                using (CsvReader csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var date = csv.GetField<DateTime>("Date");
                        var season = SeasonHelper.GetCurrentSeason(date);

                        if (date >= startDate)
                        {
                            var score = new ScoreModel();
                            score.HomeTeam = csv.GetField<string>("HomeTeam");
                            score.AwayTeam = csv.GetField<string>("AwayTeam");
                            score.HomeGoals = csv.GetField<int>("FTHG");
                            score.AwayGoals = csv.GetField<int>("FTAG");
                            score.HomeShots = csv.GetField<int>("HS");
                            score.AwayShots = csv.GetField<int>("AS");
                            score.HomeShotsOnTarget = csv.GetField<int>("HST");
                            score.AwayShotsOnTarget = csv.GetField<int>("AST");
                            score.HalfTimeHomeGoals = csv.GetField<int>("HTHG");
                            score.HalfTimeAwayGoals = csv.GetField<int>("HTAG");
                            score.HomeCorners = csv.GetField<int>("HC");
                            score.AwayCorners = csv.GetField<int>("AC");
                            score.HomeFouls = csv.GetField<int>("HF");
                            score.AwayFouls = csv.GetField<int>("AF");
                            score.HomeYellowCards = csv.GetField<int>("HY");
                            score.AwayYellowCards = csv.GetField<int>("AY");
                            score.HomeRedCards = csv.GetField<int>("HR");
                            score.AwayRedCards = csv.GetField<int>("AR");
                            score.Referee = csv.GetField<string>("Referee");
                            score.Season = season;

                            scores.Add(score);
                        }
                    }
                }
            }
            return scores;
        }

        public int InsertScores(DateTime startDate)
        {
            var scores = ParseCsvScores(startDate);
            int counter = 0;

            using (var ctx = new FootballEntities())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        //TODO error model

                        foreach (var s in scores)
                        {
                            ctx.Scores.Add(s.ToDbObject());

                            UpdateLeagueTable(s);
                            counter++;
                            ctx.SaveChanges();
                        }

                        ctx.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return counter;

        }

        public void UpdateLeagueTable(ScoreModel s)
        {
            using (var ctx = new FootballEntities())
            {
                var home = ctx.FullStatistics
                    .FirstOrDefault(t => t.Team.Name == s.HomeTeam && t.Season == s.Season);

                // jesli nie ma jeszcze teamu w tabeli, to go dodaj z zerowym dorobkiem
                if (home == null)
                {
                    home = AddToLeagueTable(s.HomeTeam, s.Season);
                }

                // jesli nie ma jeszcze teamu w tabeli, to go dodaj z zerowym dorobkiem
                var away = ctx.FullStatistics
                    .FirstOrDefault(t => t.Team.Name == s.AwayTeam && t.Season == s.Season);

                if (away == null)
                {
                    away = AddToLeagueTable(s.AwayTeam, s.Season);
                }

                home.MatchesPlayed++;
                home.GoalsScored += s.HomeGoals;
                home.GoalsLost += s.AwayGoals;

                away.MatchesPlayed++;
                away.GoalsScored += s.AwayGoals;
                away.GoalsLost += s.HomeGoals;

                if (s.HomeGoals > s.AwayGoals)
                {
                    home.MatchesWon++;
                    home.Points += 3;
                    away.MatchesLost++;
                }
                else if (s.HomeGoals < s.AwayGoals)
                {
                    away.MatchesWon++;
                    away.Points += 3;
                    home.MatchesLost++;
                }
                else
                {
                    home.MatchesDrawn++;
                    away.MatchesDrawn++;
                    home.Points++;
                    away.Points++;
                }
                ctx.SaveChanges();
            }
        }

        public FullStatistic AddToLeagueTable(string teamName, string season)
        {
            using (var ctx = new FootballEntities())
            {
                var fs = new FullStatistic();
                var team = ctx.Teams.First(t => t.Name == teamName);
                fs.TeamId = team.Id;
                fs.Season = season;

                ctx.FullStatistics.Add(fs);
                ctx.SaveChanges();
                return fs;
            }
        }
    }
}
