namespace GameOfLifeUI;

public class GridParameters
{
    public static bool[,] StartingArray
    {
        get
        {
            // CHANGE GRID SIZE HERE !!!!
            int size = 150;
            bool[,] grid = new bool[size, size];
            int[,] pattern = {
                {1,5},{1,6},{2,5},{2,6},{11,5},{11,6},{11,7},{12,4},{12,8},{13,3},{13,9},
                {14,3},{14,9},{15,6},{16,4},{16,8},{17,5},{17,6},{17,7},{18,6},{21,3},
                {21,4},{21,5},{22,3},{22,4},{22,5},{23,2},{23,6},{25,1},{25,2},{25,6},
                {25,7},{35,3},{35,4},{36,3},{36,4}
            };

            for (int i = 0; i < pattern.GetLength(0); i++)
            {
                int x = pattern[i, 0] + 10;
                int y = pattern[i, 1] + 10;
                grid[x, y] = true;
            }
            return grid;
        }
    }
}