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
            game.AddPlayer("Bill", 100000);
            game.AddPlayer("Bobby", 100000);
            game.AddPlayer("Lisa", 150000);
            game.AddPlayer("Mabel", 200000);
            int roundCount = 1; 
            while (1 == 1)
            {
                Console.WriteLine("Round: " + roundCount); 
                foreach (Player player in game.players)
                {
                    player.PlaceBet(100);
                    Console.WriteLine(player.Name + " Winnings: " +player.MoneyWon + " Money On Hand: " + player.MoneyOnHand + " Money lost: " + player.MoneyLost);
                    Console.WriteLine(player.MoneyWon);
                    Console.WriteLine(player.MoneyOnHand); 
                }
                game.StartRound();
                roundCount++; 
                while (game.PlayersWithChoices.Where(plr => plr.playerState == PlayerState.Hit).Count() > 0)
                {
                    foreach (Player player in game.PlayersWithChoices.Where(plr => plr.playerState == PlayerState.Hit))
                    {
                        if (player.IsBusted)
                        {
                            player.playerState = PlayerState.Stand;
                            break;
                        }
                        if(!player.HasBlackJack)
                        {
                            player.playerState = PlayerState.Hit;
                            break;
                        }
                        player.playerState = PlayerState.Stand; 
                            
                    }
                    game.NextDeal();
                    

                }
                game.NextDeal();
                game.players.RemoveAll(plr => plr.IsBankrupt);
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
