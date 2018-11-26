using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Model;

namespace BetBet.Data
{
    public class MatchRepository : IRepository<Match>
    {
        BetBetDB database = new BetBetDB();

        public bool Create(Match match)
        {


            return true;
        }

        public int GetID(Match match)
        {
            string command = $"SELECT MatchID FROM match WHERE Username = '{match.HomeTeam}'";
            int id = database.getID(command);

            return id;
        }

        public bool Delete(Match match)
        {
            if (match != null)
            {
                int id = GetID(match);
                string command = $"DELETE * FROM match Where MatchID =  '{id}'";
                database.executeCMD(command);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Match match)
        {
            //---------------------------------- moet nog code bij------------------------------------------------
            return true;
        }
    }
}
