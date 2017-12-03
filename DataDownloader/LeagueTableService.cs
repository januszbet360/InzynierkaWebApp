using DataModel.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloader
{
    public class LeagueTableService
    {
        private ApiDownloader _api = new ApiDownloader();

        public List<LeagueTableTeamModel> GetMatches(int matchday)
        {
            var table = new List<LeagueTableTeamModel>();

            var o = JObject.Parse(_api.GetTableJson());

            var jsonTeams = o["standing"].Children();
            foreach (var t in jsonTeams)
            {
                var team = new LeagueTableTeamModel();
                team.Position = (int)t["position"];
                team.TeamName = (String)t["teamName"];
                team.PlayedGames = (int)t["playedGames"];
                team.Points = (int)t["points"];
                team.Goals = (int)t["goals"];
                team.GoalsAgainst = (int)t["goalsAgainst"];
                team.Wins = (int)t["wins"];
                team.Draws = (int)t["draws"];
                team.Losses = (int)t["losses"];

                team.ImageUrl = (String)t["crestURI"];
                table.Add(team);
            }
            return table;
        }

    }
}
