using System;
using System.Collections.Generic;
using System.Linq; 

namespace CardLibrary
{
    public class BlackjackGame
    {


        public List<Player> players;
        private Deck deck;
        private List<Card> dealerCards; 
        public BlackjackGame()
        {
            deck = new CardFactory().GetDeckType(DeckType.Blackjack);
            deck.GenerateDecks(5); 
            players = new List<Player>();
            dealerCards = new List<Card>(); 
        }

        public void AddPlayer(string name, int money)
        {
            players.Add(new Player(name, money)); 
        }

        public List<Player> PlayersWithChoices
        {
            get
            {
                return this.players.Where(plr => !plr.HasBlackJack && !plr.IsBusted).ToList(); 
            }
        }

        public void StartRound()
        {
            deck.ShuffleDeck();
            InitialDeal();

        }

        private void UpdatePlayerStatuses()
        {
            foreach(Player player in players)
            {
                if (player.IsBusted || player.HasBlackJack)
                    player.playerState = PlayerState.Stand; 
            }
        }

        public void NextDeal()
        {
            if (players.Where(plr => plr.playerState == PlayerState.Hit).Count() > 0)
            {
                HitPlayers();
                UpdatePlayerStatuses();
            }

            else
            {
                while (dealerCards.CardValues()[0] < 16 || dealerCards.CardValues()[1] < 16)
                {
                    dealerCards.Add(deck.GetCard());
                }
                foreach (Player player in players)
                {
                    if (player.IsBusted)
                    {
                        player.LostBet();
                        continue;
                    }
                    if (player.HasBlackJack)
                    {
                        if (dealerCards.HasBlackJack())
                            player.Push();
                        else
                            player.WonBet(2.5);
                        continue;
                    }
                    else
                    {
                        if (dealerCards.IsBusted())
                            player.WonBet(2);
                        if (player.CardsOnHand.BestBlackJackValue() > dealerCards.BestBlackJackValue())
                            player.WonBet(2);
                        else
                            player.LostBet();

                        continue;
                    }

                }

                deck.GatherCards();

            }
        }


        public void HitPlayers()
        {
            foreach(Player player in players.Where(plr => plr.playerState == PlayerState.Hit))
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
                dealerCards.Add(deck.GetCard());
            }

            UpdatePlayerStatuses();
        }
    }
}
