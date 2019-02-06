using System;
using System.Collections.Generic;
namespace CardLibrary
{
    public enum PlayerState
    {
        Hit,
        Stand
    }
    public class Player: IEquatable<Player>
    {
        public int MoneyOnHand { get; private set; }
        public int MoneyOnTable { get; private set; }
        public int MoneyLost { get; private set; }
        public int MoneyWon { get; private set; }
        public readonly int OriginalMoney; 
        public string Name;
        public PlayerState playerState; 
        public List<Card> CardsOnHand;
        private Guid Id; 
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
        }

        public void AddCard(Card card)
        {
            CardsOnHand.Add(card);
            if (CardsOnHand.IsBusted())
                playerState = PlayerState.Stand; 
        }

        public void PlaceBet(int dollars)
        {
            MoneyOnTable += dollars;
            MoneyOnHand = MoneyOnHand- dollars; 
        }

        public void LostBet()
        {
            MoneyLost += MoneyOnTable; 
            MoneyOnTable = 0;
            Reset();
        }

        public bool IsBusted => this.CardsOnHand.IsBusted();

        public bool HasBlackJack => this.CardsOnHand.HasBlackJack(); 

        public void WonBet(double multiplier)
        {
            MoneyWon += (int)(MoneyOnTable * multiplier);
            MoneyOnHand += (int)(MoneyOnTable * multiplier);
            MoneyOnTable = 0;
            Reset(); 
        }
        private void Reset()
        {
            CardsOnHand = new List<Card>();
            playerState = PlayerState.Hit; 
        }

        public void Push()
        {
            MoneyOnHand += MoneyOnTable;
            MoneyOnTable = 0;
            Reset();
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
