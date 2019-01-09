using System.Collections.Generic;
using BetBet.Model;

namespace BetBet.Data
{
    public interface ITeamRepository
    {
        string GetName(int id);
        Team GetTeam(int id);
        List<Team> GetTeams();
        void Update(Team team);
    }
}