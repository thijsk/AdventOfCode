using Common;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp2015
{
    class Day22 : IDay 
    {
        private class Spell
        {
            public string Name;
            public int Cost;
            public int Damage;
            public int Armor;
            public bool Effect;
            public int TurnsWithEffect;

            public Spell(string name, int cost, int damage, int armor, bool effect, int turnsWithEffect = 0)
            {
                Name = name;
                Cost = cost;
                Damage = damage;
                Armor = armor;
                Effect = effect;
                TurnsWithEffect = turnsWithEffect;
            }

        }

        private class Player
        {
            public int HitPoints;
            public int Mana;
            public int Damage;

            public Player()
            {
            }

            public Player(Player other)
            {
                HitPoints = other.HitPoints;
                Mana = other.Mana;
                Damage = other.Damage;
            }
        }

        private class Game
        {
            public int ManaSpent;
            public Player Hero;
            public Player Boss;
            public Dictionary<Spell, int> Effects = new Dictionary<Spell, int>();
            public Game PreviousRound;

            public Game()
            {
            }

            public Game(Game other)
            {
                ManaSpent = other.ManaSpent;
                Hero = new Player(other.Hero);
                Boss = new Player(other.Boss);
                Effects = new Dictionary<Spell, int>(other.Effects);
                PreviousRound = other;
            }

            internal void PlayRound(Spell spell)
            {

                //player turn
                ManaSpent += spell.Cost;
                Hero.Mana -= spell.Cost;

                if (spell.Effect)
                {
                    Effects.Add(spell, spell.TurnsWithEffect);
                }


                //boss turn
                Hero.HitPoints -= Boss.Damage - Effects.Keys.Sum(e => e.Armor);

            }
        }

        const string input = @"";

        public long Part1()
        {
            var spells = CreateSpells();

            var inputHitPoints = 71;
            var inputDamage = 10;

            var boss = new Player() { HitPoints = inputHitPoints, Damage = inputDamage };
            var hero = new Player() { HitPoints = 50, Mana = 500 };
            var newgame = new Game() { Boss = boss, Hero = hero };

            var games = new Queue<Game>();
            games.Enqueue(newgame);

            do
            {
                var parent = games.Dequeue();
                foreach (var spell in spells.Except(parent.Effects.Keys).Where(spell => spell.Cost <= parent.Hero.Mana))
                {
                    var round = new Game(parent);

                    round.PlayRound(spell);

                    // only queue if nobody won
                    if (round.Hero.HitPoints > 0 && round.Boss.HitPoints > 0 && round.Hero.Mana > 0)
                    {
                        games.Enqueue(round);
                    }
                    else
                    {
                        // game ended
                    }
                }
            } while (games.Count > 0);


            return -1;
        }

        private static List<Spell> CreateSpells()
        {
            return new List<Spell>
            {
                new Spell("Magic Missile", 53, 4, 0, false),
                new Spell("Drain", 73, 2, 0, false),
                new Spell("Shield", 113, 0, 8, true, 6),
                new Spell("Poison", 173, 3, 0, true, 6),
                new Spell("Recharge", 229, 0, 0, true, 5)
            };
        }

        public long Part2()
        {
            return -1;
        }
    }
}
