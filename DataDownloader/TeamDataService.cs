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
    public class TeamDataService
    {
        public List<Team> GetTeamsFromFile()
        {
            var teams = new List<Team>();
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            //var lines = File.ReadLines(dir + '\\' +"App_Data\\"+ Constants.TEAMS_INFO_FILE_NAME);

            var lines = File.ReadLines(dir + '\\'  + Constants.TEAMS_INFO_FILE_NAME);
            foreach (var line in lines)
            {
                var info = line.Split(',');
                var team = new Team();
                team.Name = info[0];
                team.FullName = info[1];
                team.ImageURL = info[2];
                teams.Add(team);
            }

            return teams;
        }

        public void InsertTeams()
        {
            var teams = GetTeamsFromFile();

            using (var ctx = new FootballEntities())
            {
                if (ctx.Teams.Count() == 0)
                {
                    using (var transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            if (teams.Count > 0)
                            {
                                foreach (var t in teams)
                                {
                                    ctx.Teams.Add(t);
                                }
                                ctx.SaveChanges();
                                transaction.Commit();
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }
    }
}
