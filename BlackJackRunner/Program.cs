using System;
using CardLibrary;
using System.Linq;
using System.Threading;

namespace BlackJackRunner
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting game");
            BlackjackGame game = new BlackjackGame();
            game.AddPlayer("Bill", 10000);
            game.AddPlayer("Bobby", 10000);
            game.AddPlayer("Lisa", 15000);
            game.AddPlayer("Mabel", 20000);
            int roundCount = 1; 
            while (1 == 1)
            {
                Console.WriteLine("Round: " + roundCount); 
                foreach (Player player in game.PlayersWithChoices)
                {
                    Console.WriteLine(player.Name + " Last Round Result " + player.LastRoundResult.ToString() +" Current Bet " + player.LastBetAmount + " Winnings: " + player.MoneyWon + " Money On Hand: " + player.MoneyOnHand + " Money lost: " + player.MoneyLost);
                    if (roundCount == 1)
                    {
                        player.PlaceBet(100);
                    }
                    else
                    {
                        if (player.LastRoundResult == LastRoundResult.Lost)
                            player.PlaceBet(player.LastBetAmount * 2);
                        else
                            player.PlaceBet(100); 
                    }
                   
                }
                game.StartRound();
                roundCount++; 
                while (game.PlayersWithChoices.Count() > 0)
                {
                    foreach (Player player in game.PlayersWithChoices) 
                    {
                        if (game.DealerUpCards.First().Values[0] > 6 || game.DealerUpCards.First().CardName == CardName.Ace)
                        {
                            if (player.CardsOnHand.BestBlackJackValue() >= 12)
                            {
                                player.ChangeState(PlayerState.Stand);
                            }
                            else
                            {
                                player.ChangeState(PlayerState.Hit);
                            }
                        }
                        else
                        {
                            if (player.CardsOnHand.BestBlackJackValue() <= 11)
                            {
                                player.ChangeState(PlayerState.Hit);
                            }
                            else
                            {
                                player.ChangeState(PlayerState.Stand);
                            }
                        }

                    }
                    game.HitPlayers();
                    

                }
                game.EndRound();
                if (game.players.Count() == 0)
                {
                    Console.WriteLine("Game Over");
                    Thread.Sleep(1000000); 
                    break;  
                }


            }
        }
    }
}
