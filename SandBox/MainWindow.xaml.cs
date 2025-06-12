using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Schema;
using SandBoxEngine;

namespace SandBox
{

    public partial class MainWindow : Window
    {
        private const int CellSize = 10;
        private const int Rows = 108;
        private const int Cols = 192;
        private bool[,] _grid = new bool[Rows, Cols];
        private readonly DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация (например, glider)
            _grid[5, 5] = true;
            _grid[5, 6] = true;
            _grid[5, 7] = true;
            _grid[4, 7] = true;
            _grid[3, 6] = true;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            _timer.Tick += (s, e) => UpdateGame();
            _timer.Start();

            DrawGrid();
        }

        private void UpdateGame()
        {
            _grid = CalculateNextGeneration(_grid);
            DrawGrid();
        }

        private void DrawGrid()
        {
            LifeCanvas.Children.Clear();
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    if (!_grid[y, x]) continue;

                    var rect = new Rectangle
                    {
                        Width = CellSize,
                        Height = CellSize,
                        Fill = Brushes.Black
                    };
                    Canvas.SetLeft(rect, x * CellSize);
                    Canvas.SetTop(rect, y * CellSize);
                    LifeCanvas.Children.Add(rect);
                }
            }
        }

        private bool[,] CalculateNextGeneration(bool[,] currentGrid)
        {
            var newGrid = new bool[Rows, Cols];
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    int neighbors = CountAliveNeighbors(currentGrid, x, y);
                    bool isAlive = currentGrid[y, x];

                    newGrid[y, x] = (isAlive && (neighbors == 2 || neighbors == 3)) ||
                                    (!isAlive && neighbors == 3);
                }
            }
            return newGrid;
        }

        private int CountAliveNeighbors(bool[,] grid, int x, int y)
        {
            int count = 0;
            for (int ny = -1; ny <= 1; ny++)
            {
                for (int nx = -1; nx <= 1; nx++)
                {
                    if (nx == 0 && ny == 0) continue;
                    int newX = x + nx;
                    int newY = y + ny;
                    if (newX >= 0 && newX < Cols && newY >= 0 && newY < Rows && grid[newY, newX])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}