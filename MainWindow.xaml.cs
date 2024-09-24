using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameOfLifeUI
{
    public partial class MainWindow : Window
    {
        private bool[,] currentState;
        private Grid myGrid;
        private DispatcherTimer timer;

        public MainWindow()
        {
            currentState = GridParameters.StartingArray;
            InitialiseGrid();
            StartSimulation();
        }

        private void InitialiseGrid()
        {
            myGrid = new Grid();
           
            
            for (int i = 0; i < currentState.GetLength(0); i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < currentState.GetLength(0); row++)
            {
                for (int col = 0; col < currentState.GetLength(1); col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.StrokeThickness = 0;
                    Grid.SetRow(rect, row);
                    Grid.SetColumn(rect, col);
                    myGrid.Children.Add(rect);
                }
            }

            Content = myGrid;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            for (int row = 0; row < currentState.GetLength(0); row++)
            {
                for (int col = 0; col < currentState.GetLength(1); col++)
                {
                    Rectangle rect = myGrid.Children[row * currentState.GetLength(1) + col] as Rectangle;
                    rect.Fill = currentState[row, col] ? Brushes.Coral : Brushes.Black;
                }
            }
        }

        private void StartSimulation()
        {
            timer = new DispatcherTimer();
            // CHANGE SPEED HERE!!!
            timer.Interval = TimeSpan.FromMilliseconds(1); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentState = GetNextGeneration();
            UpdateGrid();
        }

        private bool[,] GetNextGeneration()
        {
            int rows = currentState.GetLength(0);
            int cols = currentState.GetLength(1);
            bool[,] nextGen = new bool[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int liveNeighbors = CountLiveNeighbors(row, col);
                    
                    if (currentState[row, col])
                    {
                        nextGen[row, col] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        nextGen[row, col] = liveNeighbors == 3;
                    }
                }
            }

            return nextGen;
        }

        private int CountLiveNeighbors(int row, int col)
        {
            int count = 0;
            int rows = currentState.GetLength(0);
            int cols = currentState.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int newRow = (row + i + rows) % rows;
                    int newCol = (col + j + cols) % cols;
                    if (currentState[newRow, newCol])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}