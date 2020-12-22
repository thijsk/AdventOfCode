using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ConsoleApp2020
{
    class Day22 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();

            var player1 = input.player1;
            var player2 = input.player2;
            var round = 1;

            while (player1.Count > 0 && player2.Count > 0)
            {
                ConsoleX.WriteLine($"-- Round {round} --");

                ConsoleX.WriteLine($"Player 1's deck: {string.Join(", ", player1.ToArray())}");
                ConsoleX.WriteLine($"Player 2's deck: {string.Join(", ", player2.ToArray())}");

                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                ConsoleX.WriteLine($"Player 1 plays: {card1}");
                ConsoleX.WriteLine($"Player 2 plays: {card2}");
                if (card1 > card2)
                {
                    ConsoleX.WriteLine("Player 1 wins the round!");
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    ConsoleX.WriteLine("Player 2 wins the round!");
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }

                round++;
            }

            var winner = player1.Count > 0 ? player1 : player2;
            ConsoleX.WriteLine($"Winner's deck: {string.Join(", ", winner.ToArray())}");

            return winner.Reverse().Select((card, index) => card * (index + 1)).Sum();
            return 0;
        }

        public long Part2()
        {
            var input = ParseInput();

            var player1 = input.player1;
            var player2 = input.player2;

            int game = 1;
            PlayGame(player1, player2, game);


            var winner = player1.Count > 0 ? player1 : player2;
            ConsoleX.WriteLine($"Winner's deck: {string.Join(", ", winner.ToArray())}");

            return winner.Reverse().Select((card, index) => card * (index + 1)).Sum();

        }

        private static bool PlayGame(Queue<int> player1, Queue<int> player2, int game)
        {
            ConsoleX.WriteLine($"=== Game {game} ===");

            var roundCache = new HashSet<string>();

            var round = 1;
            while (player1.Count > 0 && player2.Count > 0)
            {
                ConsoleX.WriteLine($"-- Round {round} (Game {game}) --");

                var deck1 = string.Join(", ", player1.ToArray());
                var deck2 = string.Join(", ", player2.ToArray());
                ConsoleX.WriteLine($"Player 1's deck: {deck1}");
                ConsoleX.WriteLine($"Player 2's deck: {deck2}");

                var cacheString = $"P1{deck1}P2{deck2}";
                if (roundCache.Contains(cacheString))
                {
                    ConsoleX.WriteLine($"The winner of game {game} is player 1!");
                    return true;
                }

                roundCache.Add(cacheString);

                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                ConsoleX.WriteLine($"Player 1 plays: {card1}");
                ConsoleX.WriteLine($"Player 2 plays: {card2}");

                bool player1wins = false;

                if (player1.Count >= card1 && player2.Count >= card2)
                {
                    ConsoleX.WriteLine($"Playing a sub-game to determine the winner...");
                    var recursiveGame = PlayGame(new Queue<int>(player1.ToArray().Take(card1)), new Queue<int>(player2.ToArray().Take(card2)), game+1);
                    ConsoleX.WriteLine($"...anyway, back to game {game}.");
                    player1wins = recursiveGame;
                }
                else
                {
                    player1wins = card1 > card2;
                }
                
                if (player1wins)
                {
                    ConsoleX.WriteLine($"Player 1 wins round {round} of game {game}!");
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    ConsoleX.WriteLine($"Player 2 wins round {round} of game {game}!");
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }

                round++;
            }

            int winner = player1.Count > 0 ? 1 : 2;

            ConsoleX.WriteLine($"The winner of game {game} is player {winner}!");
            ConsoleX.WriteLine();
            ConsoleX.ReadLine();

            return player1.Count > 0;
        }

        public (Queue<int> player1, Queue<int> player2) ParseInput()
        {
            var player1 = new List<int>();
            var player2 = new List<int>();
            var player = new List<int>();
            foreach (var line in  File.ReadAllLines($"Day22.txt"))
            {
                if (line.StartsWith("Player 1:"))
                {
                    player = player1;
                }
                else if (line.StartsWith("Player 2:"))
                {
                    player = player2;
                } else if (!string.IsNullOrEmpty(line))
                {
                    player.Add(Convert.ToInt32(line));
                }
            }

            return (new Queue<int>(player1), new Queue<int>(player2));
        }
    }
}