using System;
using System.Collections.Generic;
using System.Linq; 

namespace CardLibrary
{
    public class BlackjackGame
    {


        public List<Player> players;
        private Deck deck;
        private List<Card> DealerCards;
        public List<Card> DealerUpCards; 
        public BlackjackGame()
        {
            deck = new CardFactory().GetDeckType(DeckType.Blackjack);
            deck.GenerateDecks(5); 
            players = new List<Player>();
            DealerCards = new List<Card>();
            DealerUpCards = new List<Card>();
        }

        public void AddPlayer(string name, int money)
        {
            players.Add(new Player(name, money)); 
        }

        public List<Player> PlayersWithChoices
        {
            get
            {
                return this.players.Where(plr => plr.PlayerState == PlayerState.Hit).ToList(); 
            }
        }

        public void StartRound()
        {
            deck.ShuffleDeck();
            InitialDeal();

        }

        public void EndRound()
        {
            while (DealerCards.CardValues()[0] < 16 || DealerCards.CardValues()[1] < 16)
            {
                DealerCards.Add(deck.GetCard());
            }
            foreach (Player player in players)
            {
                DealerUpCards = DealerCards;
                if (player.IsBusted)
                {
                    player.LostBet();
                    continue;
                }
                if (player.HasBlackJack)
                {
                    if (DealerCards.HasBlackJack())
                        player.Push();
                    else
                        player.WonBet(2.5);
                    continue;
                }
                else
                {
                    if (DealerCards.IsBusted())
                        player.WonBet(2);
                    if (player.CardsOnHand.BestBlackJackValue() > DealerCards.BestBlackJackValue())
                        player.WonBet(2);
                    else
                        player.LostBet();

                    continue;
                }

            }

            deck.GatherCards();
            DealerCards = new List<Card>();
            DealerUpCards = new List<Card>();
            foreach(Player player in players)
            {
                player.CardsOnHand = new List<Card>(); 
            }
            players.RemoveAll(plr => plr.IsBankrupt);
        }

        public void HitPlayers()
        {
            foreach(Player player in players.Where(plr => plr.PlayerState == PlayerState.Hit))
            {
                player.AddCard(deck.GetCard()); 
            }
        }


        private void InitialDeal()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in players)
                {
                    player.AddCard(deck.GetCard());
                }
                DealerCards.Add(deck.GetCard());
            }
            DealerUpCards.Add(DealerCards[0]); 

        }
    }
}
