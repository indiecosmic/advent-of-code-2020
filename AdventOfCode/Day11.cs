namespace AdventOfCode
{
    public static class Day11
    {
        public static void Run()
        {
            var input = Input.ReadAllLines(nameof(Day11));
            //var input = new string[]{
            //    "L.LL.LL.LL"
            //    ,"LLLLLLL.LL"
            //    ,"L.L.L..L.."
            //    ,"LLLL.LL.LL"
            //    ,"L.LL.LL.LL"
            //    ,"L.LLLLL.LL"
            //    ,"..L.L....."
            //    ,"LLLLLLLLLL"
            //    ,"L.LLLLLL.L"
            //    ,"L.LLLLL.LL"
            //};

            var width = input[0].Length;
            var height = input.Length;
            char[,] grid = CreateGrid(input, width, height);
            var result1 = Part1(width, height, grid);

            grid = CreateGrid(input, width, height);
            var result2 = Part2(width, height, grid);
        }

        private static int Part2(int width, int height, char[,] grid)
        {
            while (true)
            {
                var newState = new char[width, height];
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        newState[x, y] = SimulateSeat2(grid, (x, y), width, height);
                    }
                }
                var occ = CountOccupied(grid);
                if (occ == CountOccupied(newState))
                {
                    return occ;
                }

                grid = newState;
            }
        }

        private static int Part1(int width, int height, char[,] grid)
        {
            while (true)
            {
                var newState = new char[width, height];
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        newState[x, y] = SimulateSeat(grid, (x, y), width, height);
                    }
                }
                var occ = CountOccupied(grid);
                if (occ == CountOccupied(newState))
                {
                    return occ;
                }

                grid = newState;
            }
        }

        private static char[,] CreateGrid(string[] input, int width, int height)
        {
            var grid = new char[input[0].Length, input.Length];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    grid[x, y] = input[y][x];
                }
            }

            return grid;
        }

        public static char SimulateSeat(char[,] input, (int x, int y) pos, int width, int height)
        {
            var curr = input[pos.x, pos.y];
            if (curr == '.') return '.';

            int numOccupied = CountAdjacentOccupiedSeats(input, pos, width, height);

            if (curr == 'L' && numOccupied == 0)
                return '#';
            if (curr == '#' && numOccupied >= 4)
                return 'L';
            return curr;
        }

        private static int CountAdjacentOccupiedSeats(char[,] input, (int x, int y) pos, int width, int height)
        {
            var numOccupied = 0;
            for (var y = pos.y - 1; y < pos.y + 2; y++)
            {
                if (y < 0 || y >= height)
                    continue;

                for (var x = pos.x - 1; x < pos.x + 2; x++)
                {
                    if (x < 0 || x >= width)
                        continue;
                    if (x == pos.x && y == pos.y) continue;

                    if (input[x, y] == '#') numOccupied++;
                }
            }

            return numOccupied;
        }

        public static bool LookDir(char[,] input, (int x, int y) pos, (int dx, int dy) dir, int width, int height)
        {
            var (x, y) = (pos.x + dir.dx, pos.y + dir.dy);
            while (true)
            {
                if (x < 0 || y < 0)
                    return false;
                if (x >= width || y >= height)
                    return false;
                if (input[x, y] == '#')
                    return true;
                if (input[x, y] == 'L')
                    return false;
                x += dir.dx;
                y += dir.dy;
            }
        }

        public static char SimulateSeat2(char[,] input, (int x, int y) pos, int width, int height)
        {
            var curr = input[pos.x, pos.y];
            if (curr == '.') return '.';
            int numOccupied = CountVisibleOccupiedSeats(input, pos, width, height);

            if (curr == 'L' && numOccupied == 0)
                return '#';
            if (curr == '#' && numOccupied >= 5)
                return 'L';
            return curr;
        }

        private static int CountVisibleOccupiedSeats(char[,] input, (int x, int y) pos, int width, int height)
        {
            var numOccupied = 0;
            if (LookDir(input, pos, (-1, 0), width, height))
                numOccupied++;
            if (LookDir(input, pos, (1, 0), width, height))
                numOccupied++;
            if (LookDir(input, pos, (0, -1), width, height))
                numOccupied++;
            if (LookDir(input, pos, (0, 1), width, height))
                numOccupied++;
            if (LookDir(input, pos, (-1, -1), width, height))
                numOccupied++;
            if (LookDir(input, pos, (1, 1), width, height))
                numOccupied++;
            if (LookDir(input, pos, (1, -1), width, height))
                numOccupied++;
            if (LookDir(input, pos, (-1, 1), width, height))
                numOccupied++;
            return numOccupied;
        }

        public static int CountOccupied(char[,] grid)
        {
            var count = 0;
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                for (var x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == '#') count++;
                }
            }
            return count;
        }
    }
}
