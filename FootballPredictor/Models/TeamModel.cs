using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models
{
    public class TeamModel
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int PlayedMatches { get; set; }
        public int Points { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GoalsShotted { get; set; }
        public int GoalsLost { get; set; }
        public int GoalsDifference { get; set; }
        public String Season { get; set; }

        public TeamModel (string name, int position, int playedMatches, int points, int won, int drawn, int lost, int goalsShotted, int goalsLost, int goalsDifference,String season)
        {
            this.Name = name;
            this.Position = position;
            this.PlayedMatches = playedMatches;
            this.Points = points;
            this.Won = won;
            this.Drawn = drawn;
            this.Lost = lost;
            this.GoalsShotted = goalsShotted;
            this.GoalsLost = goalsLost;
            this.GoalsDifference = goalsDifference;
            this.Season = season;
        }

      
    }
}