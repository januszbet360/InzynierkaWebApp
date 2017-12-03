using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictiveSystemManagement
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Team(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
