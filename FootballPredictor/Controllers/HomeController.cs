using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using FootballPredictor.Models;
using DataModel;
using DataDownloader;

//liczenie daty
namespace FootballPredictor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult NextRound()
        {
            using (var ctx = new FootballEntities())
            {
                var data = new List<MainPageModel>();
                var actualSeason = GetCurrentSeason(DateTime.Today);
                var actualRound = ctx.Scores.Count(e => e.Match.Season.Equals(actualSeason)) / 10;
                //tu dodac jeszcze obrazek na koniec sezonu
                foreach (var x in ctx.Matches.Where(d => (d.Matchweek == actualRound + 1) && d.Season.Equals(actualSeason))) // to zmienic żeby na koniec sezonu wyswietlalo obrazek
                {
                    data.Add(new MainPageModel(x.Team1.FullName, x.Team.FullName, x.HomeGoalsPredicted, x.AwayGoalsPredicted, null, null, x.Date.Date));
                }
                ViewBag.round = actualRound + 1;
                return View(data);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Table()
        {
            using (var ctx = new FootballEntities())
            {
               var actualSeason = SeasonHelper.GetCurrentSeason(DateTime.UtcNow);
                var teams = new List< TeamModel>();
                var position = 1;
                foreach (var singleTeam in ctx.FullStatistics.Where(e => e.Season == actualSeason).OrderByDescending(e => e.Points))
                { var name = singleTeam.Team.FullName;
                    var team = new TeamModel(name, position++, singleTeam.MatchesPlayed, singleTeam.Points, singleTeam.MatchesWon,
                        singleTeam.MatchesDrawn, singleTeam.MatchesLost, singleTeam.GoalsScored, singleTeam.GoalsLost,
                        singleTeam.GoalsScored - singleTeam.GoalsLost,null);
                    teams.Add(team);
                }               
            return View(teams);
            }
                
        }

        public ActionResult H2H(TeamsInfoModel model)
        {
            using (var ctx = new FootballEntities())
            {
                var teams = ctx.Teams.Select(x => x.FullName).ToList();
                ViewBag.Teams = teams;
                ViewBag.allSeasons = GetListOfAvailablesSeasons(ctx);
                if (model.Team1 == null || model.Team2 == null || model.Team1.Equals(model.Team2)) return View();

                var team1Id = ctx.Teams.FirstOrDefault(e => e.FullName.Equals(model.Team1)).Id;
                var team2Id = ctx.Teams.FirstOrDefault(e => e.FullName.Equals(model.Team2)).Id;

                var singleMatchStats = new List<MatchStatisticsModel>();
                var today = DateTime.UtcNow.Date;
                var actualSeason = GetCurrentSeason(today);
                var seasonTwoYearsAgo = GetCurrentSeason(DateTime.Today.AddYears(-2));
                if (model.SeasonTo == null) model.SeasonTo = actualSeason;
                if (model.SeasonSince == null) model.SeasonSince = seasonTwoYearsAgo;

                var iFrom = Int32.Parse(model.SeasonSince.Remove(4));
                var iTo = Int32.Parse(model.SeasonTo.Remove(4));
                if (iFrom > iTo)
                {
                    var tmp = model.SeasonSince;
                    model.SeasonSince = model.SeasonTo;
                    model.SeasonTo = tmp;
                }

                foreach (var match in ctx.Matches.Where(e => ((e.HomeId == team1Id && e.AwayId == team2Id) ||
                                                              e.HomeId == team2Id && e.AwayId == team1Id)
                                                             && DateTime.Compare(e.Date, today) < 0)
                    .OrderByDescending(d => d.Date))
                {
                    if (!SeasonsComapare(model.SeasonSince, model.SeasonTo, match.Season)) continue;
                    var homeTeam = ctx.Teams.First(q => q.Id == match.HomeId).FullName;
                    var awayTeam = ctx.Teams.First(q => q.Id == match.AwayId).FullName;
                    var actualRound =
                        ctx.Scores.Count(e => e.Date > new DateTime(2017, 06, 30)) / 10; //!!!!zmienic pozniej (z tego sezonu ilosc meczów z wynikiem / 10)
                    var stats = ctx.Scores.FirstOrDefault(q => q.MatchId == match.Id);
                    if (stats == null) continue;
                    var season = GetCurrentSeason(match.Date);
                    singleMatchStats.Add(new MatchStatisticsModel(homeTeam, awayTeam, match.Date,
                        match.HomeGoalsPredicted, match.AwayGoalsPredicted, match.Matchweek, stats.HomeGoals,
                        stats.AwayGoals,
                        stats.HomeShots, stats.AwayShots, stats.HomeShotsOnTarget, stats.AwayShotsOnTarget,
                        stats.HomeCorners,
                        stats.AwayCorners, stats.HomeFouls, stats.AwayFouls, stats.HomeYellowCards,
                        stats.AwayYellowCards,
                        stats.HomeRedCards, stats.AwayRedCards, stats.HalfTimeHomeGoals, stats.HalfTimeAwayGoals,
                        stats.Referee, season));
                }

                ViewBag.Matches = singleMatchStats;
                return View();
            }
        }

        public ActionResult TeamStatistics(TeamsInfoModel model)
        {
            if (model.Team1 == null) model.Team1 = "";
            using (var ctx = new FootballEntities())
            {
                var seasonStatsOfTeam = new List<TeamModel>();
                var teams = ctx.Teams.Select(x => x.FullName).ToList();
                ViewBag.teams = teams;
                ViewBag.allSeasons = GetListOfAvailablesSeasons(ctx);
                if (model.Team1.Equals("")) return View();

                var today = DateTime.UtcNow.Date;
                var season = GetCurrentSeason(today);
                var seasonTwoYearsAgo = GetCurrentSeason(DateTime.Today.AddYears(-2));
                if (model.SeasonTo == null) model.SeasonTo = season;
                if (model.SeasonSince == null) model.SeasonSince = seasonTwoYearsAgo;

                var iFrom = Int32.Parse(model.SeasonSince.Remove(4));
                var iTo = Int32.Parse(model.SeasonTo.Remove(4));
                if (iFrom > iTo)
                {
                    var tmp = model.SeasonSince;
                    model.SeasonSince = model.SeasonTo;
                    model.SeasonTo = tmp;
                }
                var statsOfPrediction = GetSuccessRateOfPrediction(model);
                ViewBag.matchesPredicted = statsOfPrediction.Item1;
                ViewBag.rightResultPrediction = statsOfPrediction.Item2;
                ViewBag.rightScoresPrediction = statsOfPrediction.Item3;


                var teamId = ctx.Teams.FirstOrDefault(e => e.FullName.Equals(model.Team1)).Id;   
                //dodac liczenia miejsca w tabeli
                foreach (var singleTeam in  ctx.FullStatistics.Where(e => e.TeamId == teamId).OrderByDescending(d=>d.Season))
                {
                    if (!SeasonsComapare(model.SeasonSince, model.SeasonTo, singleTeam.Season)) continue;
                    var name = singleTeam.Team.FullName;
                    var team = new TeamModel(name, 0, singleTeam.MatchesPlayed, singleTeam.Points,
                        singleTeam.MatchesWon,
                        singleTeam.MatchesDrawn, singleTeam.MatchesLost, singleTeam.GoalsScored,
                        singleTeam.GoalsLost,
                        singleTeam.GoalsScored - singleTeam.GoalsLost, singleTeam.Season);
                    seasonStatsOfTeam.Add(team);
                }
                ViewBag.SeasonStats = seasonStatsOfTeam;


                var singleMatchStats = new List<MatchStatisticsModel>();
                var actualRound = ctx.Scores.Count(e => e.Match.Season.Equals(season)) / 10; //!!!!zmienic pozniej 

                foreach (var match in ctx.Matches.Where(e => ((e.HomeId == teamId || e.AwayId == teamId) && (e.Matchweek <= actualRound || e.Season != season))
                                                        && DateTime.Compare(e.Date, today) <0).OrderByDescending(d=>d.Date))
                {
                    if (!SeasonsComapare(model.SeasonSince, model.SeasonTo, match.Season)) continue;
                    var homeTeam = ctx.Teams.First(q => q.Id == match.HomeId).FullName;
                    var awayTeam = ctx.Teams.First(q => q.Id == match.AwayId).FullName;
                    var seasonOfMatch = GetCurrentSeason(match.Date);
                    var stats = ctx.Scores.FirstOrDefault(q => q.MatchId == match.Id);
                    
                    singleMatchStats.Add(new MatchStatisticsModel(homeTeam, awayTeam, match.Date,
                        match.AwayGoalsPredicted, match.HomeGoalsPredicted, match.Matchweek,stats.HomeGoals,stats.AwayGoals,
                        stats.HomeShots,stats.AwayShots,stats.HomeShotsOnTarget,stats.AwayShotsOnTarget,stats.HomeCorners,
                        stats.AwayCorners,stats.HomeFouls,stats.AwayFouls,stats.HomeYellowCards,stats.AwayYellowCards,
                        stats.HomeRedCards,stats.AwayRedCards,stats.HalfTimeHomeGoals,stats.HalfTimeAwayGoals,
                        stats.Referee, seasonOfMatch));                 
                }

                ViewBag.Matches = singleMatchStats;

                //url
                var url = ctx.Teams.FirstOrDefault(x => x.FullName.Equals(model.Team1)).ImageURL;
                ViewBag.url = url;

                return View();
                
            }
        }

      

        public ActionResult Schedule(int selectedRound  = -1) {
            using (var ctx = new FootballEntities())
            {            
                var actualSeason = GetCurrentSeason(DateTime.Today);
                var actualRound = ctx.Scores.Count(e =>e.Match.Season.Equals(actualSeason))/10; //!!!!zmienic pozniej 
                var data = new List<MainPageModel>();
                if (selectedRound == -1) selectedRound = actualRound;
                int success = 0; // zmienic liczneie skutecznosci na jakis wzor
                 
                    foreach (var x in ctx.Matches.Where(d => (d.Matchweek == selectedRound) && d.Season.Equals(actualSeason)))
                    {
                        if (selectedRound <= actualRound)
                        {
                            var homeGoals = ctx.Scores.FirstOrDefault(w => w.MatchId == x.Id).HomeGoals;
                            var awayGoals = ctx.Scores.FirstOrDefault(w => w.MatchId == x.Id).AwayGoals;
                            data.Add(new MainPageModel(x.Team1.FullName, x.Team.FullName, x.HomeGoalsPredicted,
                                x.AwayGoalsPredicted, awayGoals, homeGoals, x.Date.Date));
                            bool rightPredicted = CompareScores(homeGoals, awayGoals, x.HomeGoalsPredicted,
                                x.AwayGoalsPredicted);
                            if (rightPredicted) success += 10;
                        }
                        else
                        {
                             data.Add(new MainPageModel(x.Team1.FullName, x.Team.FullName, x.HomeGoalsPredicted, x.AwayGoalsPredicted, null, null, x.Date.Date));
                        }
                }
               
                ViewBag.actualRound = actualRound;
                ViewBag.selectedRound = selectedRound;
                if(selectedRound <= actualRound) ViewBag.success = success;
                return View(data.Distinct());
            }
        }

        private static bool CompareScores(int homeGoals, int awayGoals, int? homeGoalsPredicted, int? awayGoalsPredicted)
        {
            if (homeGoalsPredicted == null || awayGoalsPredicted == null) return false;
            else
            {
                if (homeGoals > awayGoals && homeGoalsPredicted > awayGoalsPredicted)
                    return true;
                else if (homeGoals < awayGoals && homeGoalsPredicted < awayGoalsPredicted)
                    return true;
                else if (homeGoals == awayGoals && homeGoalsPredicted == awayGoalsPredicted)
                    return true;
                else
                    return false;
            }
        }

        public  string GetCurrentSeason(DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            if (date.Month >= 7)
            {
                sb.Append(date.Year);
                sb.Append('/');
                sb.Append(date.Year + 1);
            }
            else
            {
                sb.Append(date.Year - 1);
                sb.Append('/');
                sb.Append(date.Year);
            }
            return sb.ToString();
        }


        public List<string> GetListOfAvailablesSeasons(FootballEntities ctx)
        {
            return ctx.FullStatistics.Select(e => e.Season).Distinct().ToList();
        }

        public bool SeasonsComapare(string seasonFrom, string seasonTo, string actualSeason)
        {
            var iFrom = Int32.Parse(seasonFrom.Remove(4));
            var iTo = Int32.Parse(seasonTo.Remove(4));
            var iActual = Int32.Parse(actualSeason.Remove(4));

            if (iFrom <= iActual && iActual <= iTo) return true;
            else return false;
        }

        private Tuple<int, int, int> GetSuccessRateOfPrediction(TeamsInfoModel model)
        {
            int successScorePredictions = 0;
            int successResultPredictions = 0;
            int allMatches = 0;
            var teamName = model.Team1;
            var actualSeason = GetCurrentSeason(DateTime.Today);

            using (var ctx = new FootballEntities())
            {
                var actualRound = ctx.Scores.Count(e => e.Match.Season.Equals(actualSeason)) / 10; //!!!!zmienic pozniej 
                var teamId = ctx.Teams.FirstOrDefault(e => e.FullName.Equals(teamName)).Id;

                foreach (var match in ctx.Matches.Where(e => ((e.HomeId == teamId || e.AwayId == teamId) && (e.Matchweek <= actualRound || e.Season != actualSeason))
                                                             && DateTime.Compare(e.Date, DateTime.Today) < 0))
                {
                    var scoreOfMatch = match.Scores.First();
                    if (!SeasonsComapare(model.SeasonSince, model.SeasonTo, match.Season)) continue;
                    if (match.AwayGoalsPredicted == null || match.HomeGoalsPredicted == null) continue;

                    if ((scoreOfMatch.HomeGoals > scoreOfMatch.AwayGoals &&
                         match.HomeGoalsPredicted > match.AwayGoalsPredicted)
                        || (scoreOfMatch.HomeGoals < scoreOfMatch.AwayGoals &&
                            match.HomeGoalsPredicted < match.AwayGoalsPredicted)
                        || (scoreOfMatch.HomeGoals == scoreOfMatch.AwayGoals &&
                            match.HomeGoalsPredicted == match.AwayGoalsPredicted))
                    {
                        successResultPredictions++;
                    }

                    if (scoreOfMatch.HomeGoals == match.HomeGoalsPredicted &&
                        scoreOfMatch.AwayGoals == match.AwayGoalsPredicted)
                        successScorePredictions++;

                    allMatches++;
                }
            }
            return Tuple.Create(allMatches, successResultPredictions, successScorePredictions);
        }
    }
}
