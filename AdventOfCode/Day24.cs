using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day24
    {
        private static readonly (int q, int r, int s)[] Directions = { (1, 0, -1), (1, -1, 0), (0, -1, 1), (-1, 0, 1), (-1, 1, 0), (0, 1, -1) };

        public static (int q, int r, int s)[] Neighbors((int q, int r, int s) tile)
        {
            return Directions.Select(dir => (tile.q + dir.q, tile.r + dir.r, tile.s + dir.s)).ToArray();
        }

        private static (int q, int r, int s) Neighbor((int q, int r, int s) tile, int dir)
        {
            var direction = Directions[dir];
            return (tile.q + direction.q, tile.r + direction.r, tile.s + direction.s);
        }
        
        public static void Run(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day24));

            //input = new[]
            //{
            //    "sesenwnenenewseeswwswswwnenewsewsw",
            //    "neeenesenwnwwswnenewnwwsewnenwseswesw",
            //    "seswneswswsenwwnwse",
            //    "nwnwneseeswswnenewneswwnewseswneseene",
            //    "swweswneswnenwsewnwneneseenw",
            //    "eesenwseswswnenwswnwnwsewwnwsene",
            //    "sewnenenenesenwsewnenwwwse",
            //    "wenwwweseeeweswwwnwwe",
            //    "wsweesenenewnwwnwsenewsenwwsesesenwne",
            //    "neeswseenwwswnwswswnw",
            //    "nenwswwsewswnenenewsenwsenwnesesenew",
            //    "enewnwewneswsewnwswenweswnenwsenwsw",
            //    "sweneswneswneneenwnewenewwneswswnese",
            //    "swwesenesewenwneswnwwneseswwne",
            //    "enesenwswwswneneswsenwnewswseenwsese",
            //    "wnwnesenesenenwwnenwsewesewsesesew",
            //    "nenewswnwewswnenesenwnesewesw",
            //    "eneswnwswnwsenenwnwnwwseeswneewsenese",
            //    "neswnwewnwnwseenwseesewsenwsweewe",
            //    "wseweeenwnesenwwwswnew"
            //};
            var grid = new Dictionary<(int q, int r, int s), bool>();
            var paths = new List<string[]>();
            foreach (var line in input)
            {
                var directions = new Queue<char>(line);
                var path = new List<string>();
                while (directions.TryDequeue(out var d))
                {
                    if (d == 'e' || d == 'w')
                    {
                        path.Add(d.ToString());
                    }
                    else
                    {
                        path.Add(d.ToString() + directions.Dequeue());
                    }
                }
                paths.Add(path.ToArray());
            }

            foreach (var path in paths)
            {
                var curr = (0, 0, 0);
                foreach (var dir in path)
                {
                    switch (dir)
                    {
                        case "e":
                            curr = Neighbor(curr, 0);
                            break;
                        case "ne":
                            curr = Neighbor(curr, 1);
                            break;
                        case "nw":
                            curr = Neighbor(curr, 2);
                            break;
                        case "w":
                            curr = Neighbor(curr,3);
                            break;
                        case "sw":
                            curr = Neighbor(curr,4);
                            break;
                        case "se":
                            curr = Neighbor(curr,5);
                            break;
                    }
                }

                if (grid.ContainsKey(curr))
                {
                    grid[curr] = !grid[curr];
                }
                else
                {
                    grid.Add(curr, true);
                }
            }

            Console.WriteLine(grid.Values.Count(c => c == true));
            
            for (var i = 0; i < 100; i++)
            {
                var newState = new Dictionary<(int q, int r, int s), bool>(grid);
                var tiles = grid.Keys.ToList();
                var keys = grid.Keys.ToHashSet();

                foreach (var tile in tiles)
                {
                    var neighbors = Neighbors(tile);
                    foreach (var neighbor in neighbors)
                    {
                        newState[neighbor] = UpdateTile(neighbor, keys, grid);
                    }

                    newState[tile] = UpdateTile(tile, keys, grid);
                }

                grid = new Dictionary<(int q, int r, int s), bool>(newState);
                Console.WriteLine(grid.Values.Count(c => c == true));
            }
            
        }

        private static bool UpdateTile((int q, int r, int s) tile, IReadOnlySet<(int q, int r, int s)> keys, Dictionary<(int q, int r, int s), bool> grid)
        {
            var exists = keys.Contains(tile);
            var blackNeighbours = Neighbors(tile).Count(n => keys.Contains(n) && grid[n] == true );
            var isBlack = exists && grid[tile];

            if (isBlack && (blackNeighbours == 0 || blackNeighbours > 2))
            {
                return false;
            }
            else if (!isBlack && blackNeighbours == 2)
            {
                return true;
            }
            else if (!exists)
                return false;
            return grid[tile];
        }

        struct Hex
        {
            public Hex((int q, int r, int s) pos) : this(pos.q, pos.r, pos.s)
            {

            }
            
            public Hex(int q, int r, int s)
            {
                this.q = q;
                this.r = r;
                this.s = s;
                if (q + r + s != 0) throw new ArgumentException("q + r + s must be 0");
            }
            public readonly int q;
            public readonly int r;
            public readonly int s;

            public (int q, int r, int s) Pos => (this.q, this.r, this.s);
            
            public Hex Add(Hex b)
            {
                return new Hex(q + b.q, r + b.r, s + b.s);
            }

            public Hex Subtract(Hex b)
            {
                return new Hex(q - b.q, r - b.r, s - b.s);
            }

            static public List<Hex> directions = new List<Hex> { new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1) };

            static public Hex Direction(int direction)
            {
                return Hex.directions[direction];
            }

            public Hex Neighbor(int direction)
            {
                return Add(Hex.Direction(direction));
            }

            static public List<Hex> diagonals = new List<Hex> { new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2) };

            public Hex DiagonalNeighbor(int direction)
            {
                return Add(Hex.diagonals[direction]);
            }

            public int Length()
            {
                return (int)((Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2);
            }

            public int Distance(Hex b)
            {
                return Subtract(b).Length();
            }
        }

    }
}
