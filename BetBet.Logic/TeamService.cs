using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Data;
using BetBet.Model;

namespace BetBet.Logic
{
    public class TeamService
    {
        TeamRepository TeamRep = new TeamRepository();

        public List<Team> GetTeams ()
        {
            List<Team> TeamList = new List<Team>();

            TeamList = TeamRep.GetTeams();

            return TeamList;
        }
    }
}
