using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Place
    {
        public readonly List<Road> Roads = new List<Road>();

        public string Name;

        public Place(string name)
        {
            Name = name;
        }

    }

    class Road
    {
        public Place To;
        public int Distance;
    }

    class World
    {
        public List<Place> Places;

        public World()
        {
            Places = new List<Place>();
        }


        public Place FindPlace(string Name)
        {
            var place = Places.FirstOrDefault(p => p.Name == Name);
            if (place == null)
            {
                place = new Place(Name);
                Places.Add(place);
            }
            return place;
        }
    }

    class Route
    {
        public List<Place> Places;
        public List<Road> Roads;

        public Route()
        {
            Places = new List<Place>();
            Roads = new List<Road>();
        }

        public int Distance()
        {
            return Roads.Sum(r => r.Distance);
        }

        public Route(Route other)
        {
            this.Places = other.Places.ConvertAll(x => x);
            this.Roads = other.Roads.ConvertAll(x => x);
        }
    }


    class Day9 : IDay
    {
        private string input = @"Tristram to AlphaCentauri = 34
Tristram to Snowdin = 100
Tristram to Tambi = 63
Tristram to Faerun = 108
Tristram to Norrath = 111
Tristram to Straylight = 89
Tristram to Arbre = 132
AlphaCentauri to Snowdin = 4
AlphaCentauri to Tambi = 79
AlphaCentauri to Faerun = 44
AlphaCentauri to Norrath = 147
AlphaCentauri to Straylight = 133
AlphaCentauri to Arbre = 74
Snowdin to Tambi = 105
Snowdin to Faerun = 95
Snowdin to Norrath = 48
Snowdin to Straylight = 88
Snowdin to Arbre = 7
Tambi to Faerun = 68
Tambi to Norrath = 134
Tambi to Straylight = 107
Tambi to Arbre = 40
Faerun to Norrath = 11
Faerun to Straylight = 66
Faerun to Arbre = 144
Norrath to Straylight = 115
Norrath to Arbre = 135
Straylight to Arbre = 127";

        public int Part1()
        {
//            input = @"London to Dublin = 464
//London to Belfast = 518
//Dublin to Belfast = 141";

            var world = CreateWorld();

            var routes = CreateRoutes(world);

            var shortest = routes.OrderBy(r => r.Distance()).First();

            return shortest.Distance();

        }

        private List<Route> CreateRoutes(World world)
        {
            var result = new List<Route>();

            foreach (var place in world.Places)
            {
                var startRoute = new Route();
                startRoute.Places.Add(place);
                var routes = FindRoutes(world, startRoute);
                result.AddRange(routes);
            }

            return result;

        }

        private List<Route> FindRoutes(World world, Route startroute)
        {
            var currentPlace = startroute.Places.Last();
          
            var result = new List<Route>();
            var roads = currentPlace.Roads;
            var toVisit = world.Places.Where(p => !startroute.Places.Contains(p));

            if (!toVisit.Any())
            {
                return new List<Route>(){ startroute };
            }

            foreach (var road in roads)
            {
                if (toVisit.Contains(road.To))
                {
                    var newRoute = new Route(startroute);
                    newRoute.Places.Add(road.To);
                    newRoute.Roads.Add(road);
                    result.AddRange(FindRoutes(world, newRoute));
                }
            }

            return result;
        }

        private World CreateWorld()
        {
            var world = new World();
            var roadTxts = input.Split('\n').Select(i => i.Trim());
            foreach (var roadTxt in roadTxts)
            {
                var parts = roadTxt.Split(" ");
                var fromPlace = world.FindPlace(parts[0]);
                var toPlace = world.FindPlace(parts[2]);
                var distance = int.Parse(parts[4]);

                var road = new Road() {Distance = distance, To = toPlace};
                fromPlace.Roads.Add(road);
                road = new Road() { Distance = distance, To = fromPlace };
                toPlace.Roads.Add(road);
            }

            return world;
        }


        public int Part2()
        {
            //            input = @"London to Dublin = 464
            //London to Belfast = 518
            //Dublin to Belfast = 141";

            var world = CreateWorld();

            var routes = CreateRoutes(world);

            var shortest = routes.OrderBy(r => r.Distance()).Last();

            return shortest.Distance();

        }
    }
}

