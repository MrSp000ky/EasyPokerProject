using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPoker_API.Models;

namespace EasyPoker_API.Models
{
    public class PokerServerService
    {
        private static readonly string[] Suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        private static readonly string[] Ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        private static readonly Dictionary<string, int> RankValues = new()
        {
            { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 }, { "6", 6 },
            { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 }, { "J", 11 },
            { "Q", 12 }, { "K", 13 }, { "A", 14 }
        };

        public Game CreateGame(List<string> playerNames)
        {
            // 1. Create a new game instance
            var game = new Game
            {
                Timestamp = DateTime.Now
            };

            // 2. Initialize and shuffle deck
            var deck = InitializeDeck();
            ShuffleDeck(deck);

            // 3. Deal hands to players
            var players = new List<Player>();
            var playerHands = new List<PlayerHand>();

            foreach (var name in playerNames)
            {
                var player = new Player { PlayerName = name, Game = game, GameId = game.Id };
                players.Add(player);

                var hand = new PlayerHand
                {
                    Game = game,
                    GameId = game.Id,
                    Player = player,
                    Card1 = deck.Pop(),
                    Card2 = deck.Pop(),
                    Card3 = deck.Pop(),
                    Card4 = deck.Pop(),
                    Card5 = deck.Pop()
                };

                playerHands.Add(hand);
            }

            // 4. Store remaining cards
            var remainingCards = deck.Select(card => new RemainingCard { Game = game, Card = card, GameId = game.Id }).ToList();

            // 5. Determine the winner
            var winner = DetermineWinner(playerHands);

            // 6. Assign results
            game.Players = players;
            game.PlayerHands = playerHands;
            game.RemainingCards = remainingCards;
            game.Winner = winner;

            return game;
        }

        private Stack<string> InitializeDeck()
        {
            var deck = new Stack<string>();
            foreach (var suit in Suits)
                foreach (var rank in Ranks)
                    deck.Push($"{rank} of {suit}");
            return deck;
        }

        private void ShuffleDeck(Stack<string> deck)
        {
            var deckList = deck.ToList();
            var rng = new Random();

            for (int i = deckList.Count - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1); 
                (deckList[i], deckList[j]) = (deckList[j], deckList[i]); 
            }

            deck.Clear();
            foreach (var card in deckList) deck.Push(card);
        }

        private string DetermineWinner(List<PlayerHand> playerHands)
        {
            return playerHands
                .OrderByDescending(h => EvaluateHand(h))
                .First()
                .Player!.PlayerName;
        }

        private int EvaluateHand(PlayerHand hand)
        {
            var cards = new[] { hand.Card1, hand.Card2, hand.Card3, hand.Card4, hand.Card5 };
            return GetHandRank(cards);
        }

        public static int GetHandRank(string[] cards)
        {
            var ranks = cards.Select(card => card.Split(' ')[0]).ToList();
            var suits = cards.Select(card => card.Split(' ')[2]).ToList();
            var rankCounts = ranks.GroupBy(r => r).ToDictionary(g => g.Key, g => g.Count());

            bool isFlush = suits.Distinct().Count() == 1;
            var sortedRanks = ranks.Select(r => RankValues[r]).OrderBy(v => v).ToList();
            bool isStraight = sortedRanks.Zip(sortedRanks.Skip(1), (a, b) => b - a).All(diff => diff == 1);

            if (isFlush && isStraight) return 8; // Straight Flush
            if (rankCounts.Values.Contains(4)) return 7; // Four of a Kind
            if (rankCounts.Values.Contains(3) && rankCounts.Values.Contains(2)) return 6; // Full House
            if (isFlush) return 5; // Flush
            if (isStraight) return 4; // Straight
            if (rankCounts.Values.Contains(3)) return 3; // Three of a Kind
            if (rankCounts.Values.Count(v => v == 2) == 2) return 2; // Two Pair
            if (rankCounts.Values.Contains(2)) return 1; // One Pair
            return 0; // High Card
        }

    }



}