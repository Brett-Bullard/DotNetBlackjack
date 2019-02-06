using System;
using System.Collections.Generic;
namespace CardLibrary
{
    public enum PlayerState
    {
        Hit,
        Stand,
        BlackJack,
        Busted,
        Bankrupt
    }
    public enum LastRoundResult
    {
        Won,
        Lost, 
        Pushed
    }
    public class Player: IEquatable<Player>
    {
        public int MoneyOnHand { get; private set; }
        public int MoneyOnTable { get; private set; }
        public int MoneyLost { get; private set; }
        public int MoneyWon { get; private set; }
        public readonly int OriginalMoney; 
        public string Name;
        public PlayerState PlayerState { get; private set; } 
        public LastRoundResult LastRoundResult { get; private set; }
        public List<Card> CardsOnHand;
        private Guid Id; 
        public int LastBetAmount { get; private set; }
        public Player(string name, int moneyOnHand)
        {
          /*  if (moneyOnHand <= 0)
                throw new ArgumentException("Cant start the game without money"); */
            Name = name;
            MoneyOnHand = moneyOnHand;
            OriginalMoney = moneyOnHand;
            MoneyWon = 0;
            MoneyLost = 0;
            MoneyOnTable = 0;
            CardsOnHand = new List<Card>();
            Id = Guid.NewGuid();
            LastRoundResult = LastRoundResult.Pushed;
            LastBetAmount = 1; 

        }

        public void ChangeState(PlayerState newState)
        {
            if (newState != PlayerState.Hit && newState != PlayerState.Stand)
                throw new Exception("You cannot change the player state to something other than hit or stand");
            else
                PlayerState = newState; 
        }

        public void AddCard(Card card)
        {
            if (PlayerState != PlayerState.Hit)
                throw new Exception("Cannot add a card to a player that is not in playerstate of hit."); 
            CardsOnHand.Add(card);
            if (CardsOnHand.IsBusted())
                PlayerState = PlayerState.Busted;
            if (CardsOnHand.HasBlackJack())
                PlayerState = PlayerState.BlackJack; 
        }

        public void PlaceBet(int dollars)
        {
            if (PlayerState == PlayerState.Bankrupt)
                throw new Exception("Player has no money to place bet."); 
            MoneyOnTable += dollars;
            MoneyOnHand = MoneyOnHand- dollars;
            LastBetAmount = dollars; 
        }

        public void LostBet()
        {
            MoneyLost += MoneyOnTable; 
            MoneyOnTable = 0;
            if (MoneyOnHand == 0)
                PlayerState = PlayerState.Bankrupt; 
            Reset();
            LastRoundResult = LastRoundResult.Lost; 
        }

        public bool IsBusted => this.PlayerState == PlayerState.Busted;

        public bool HasBlackJack => this.PlayerState == PlayerState.BlackJack; 

        public void WonBet(double multiplier)
        {
            MoneyWon += (int)(MoneyOnTable * multiplier);
            MoneyOnHand += (int)(MoneyOnTable * multiplier);
            MoneyOnTable = 0;
            Reset();
            LastRoundResult = LastRoundResult.Won; 
        }
        private void Reset()
        {
            CardsOnHand = new List<Card>();
            PlayerState = PlayerState.Hit; 
        }

        public void Push()
        {
            MoneyOnHand += MoneyOnTable;
            MoneyOnTable = 0;
            Reset();
            LastRoundResult = LastRoundResult.Pushed; 
        }
        public bool IsBankrupt
        {
            get
            {
                return MoneyOnHand <= 0; 
            }
        }

        public bool Equals(Player player)
        {
            return this.Id == player.Id; 
        }

    }
}
