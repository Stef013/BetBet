﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Model;
using BetBet.Data;

namespace BetBet.Logic
{
    public class BetService
    {
        BetRepository betrep = new BetRepository();
        //MatchRepository matchrep = new MatchRepository();
        MatchService matchservice;
        UserService userservice;

        public bool CheckIfBetExists(int matchID, int userID)
        {
            bool result;

            int betExists = betrep.CheckBet(matchID, userID);

            if (betExists == 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool CreateBet(Bet bet)
        {
            bool result = betrep.Create(bet);

            return result;
        }

        public List<Bet> GetBetsFromUser(User user)
        {
            matchservice = new MatchService();

            List<Bet> betList = new List<Bet>();
            betList = betrep.GetBetsByUser(user);

            foreach (Bet bet in betList)
            {
                bet.Match = matchservice.GetUpcomingMatch(bet.MatchID);
            }

            return betList;
        }

        public void Payout (FinishedMatch match)
        {
            List<Bet> betList = new List<Bet>();

            betList = betrep.GetBetsByMatch(match);

            foreach (Bet bet in betList)
            {
                if (bet.Prediction == match.Result)
                {
                    userservice = new UserService();

                    User user = new User
                    {
                        UserID = bet.UserID,
                        Balance = userservice.GetBalance(bet.UserID)
                    };

                    if (bet.Prediction == MatchResult.HomeTeam)
                    {
                        bet.Amount = bet.Amount * match.MultiplierHome;
                    }

                    else if (bet.Prediction == MatchResult.AwayTeam)
                    {
                        bet.Amount = bet.Amount * match.MultiplierAway;
                    }

                    else
                    {
                        bet.Amount = bet.Amount * match.MultiplierDraw;
                    }

                    userservice.AddFunds(user, bet.Amount);
                }
            }
        }
    }
}
