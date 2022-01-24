using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Gymnasiearbete.Models;
using System;
using System.Collections.Generic;

namespace Gymnasiearbete.Views
{
    public partial class TestView : UserControl
    {
        const int SnakeSquareSize = 20;

        private SolidColorBrush snakeBodyBrush = new SolidColorBrush
        {
            Color = new Color(255, 0, 255, 0)
        };
        private SolidColorBrush snakeHeadBrush = new SolidColorBrush
        {
            Color = new Color(255, 255, 0, 0)
        };
        private List<SnakePart> snakeParts = new List<SnakePart>();

        public enum SnakeDirection {  Left, Right, Up, Down };
        private SnakeDirection snakeDirection = SnakeDirection.Right;
        private int snakeLength;

        private Avalonia.Threading.DispatcherTimer gameTickTimer = new Avalonia.Threading.DispatcherTimer();

        private Canvas GameArea {get; set;}
        public TestView()
        {
            InitializeComponent();

            gameTickTimer.Tick += GameTickTimer_Tick;
            GameArea = this.FindControl<Canvas>("GameArea");

            DrawGameArea();
        }

        private void GameTickTimer_Tick(object? sender, EventArgs e)
        {
            MoveSnake();
        }

        private void MoveSnake()
        {
            while(snakeParts.Count >= snakeLength)
            {
                GameArea.Children.Remove(snakeParts[0].UiElement);
                snakeParts.RemoveAt(0);
            }

            foreach(SnakePart snakePart in snakeParts)
            {
                (snakePart.UiElement as Avalonia.Controls.Shapes.Rectangle).Fill = snakeBodyBrush;
                snakePart.IsHead = false;
            }

            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
            double nextX = snakeHead.Position.X;
            double nextY = snakeHead.Position.Y;
            switch(snakeDirection)
            {
                case SnakeDirection.Left:
                    nextX -= SnakeSquareSize;
                    break;
                case SnakeDirection.Right:
                    nextX += SnakeSquareSize;
                    break;
                case SnakeDirection.Up:
                    nextY -= SnakeSquareSize;
                    break;
                case SnakeDirection.Down:
                    nextY += SnakeSquareSize;
                    break;
            }

            snakeParts.Add(new SnakePart()
            {
                Position = new Point(nextX, nextY),
                IsHead = true
            });

            DrawSnake();
        }

        private void DrawSnake()
        {
            foreach(SnakePart snakePart in snakeParts)
            {
                if(snakePart.UiElement == null)
                {
                    snakePart.UiElement = new Avalonia.Controls.Shapes.Rectangle
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                        Fill = (snakePart.IsHead ? snakeHeadBrush : snakeBodyBrush)
                    };
                    GameArea.Children.Add(snakePart.UiElement);
                    Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
                    Canvas.SetTop(snakePart.UiElement, snakePart.Position.X);
                }
            }
        }

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;
            
            while(doneDrawingBackground == false)
            {
                Avalonia.Controls.Shapes.Rectangle rect = new Avalonia.Controls.Shapes.Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = new SolidColorBrush
                    {
                        Color = nextIsOdd ? new Color(255, 255, 255, 255) : new Color(255, 0, 0, 0)
                    }
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);

                nextIsOdd = !nextIsOdd;
                nextX += SnakeSquareSize;
                if(nextX >= GameArea.Width)
                {
                    nextX = 0;
                    nextY += SnakeSquareSize;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                }

                if(nextY >= GameArea.Height)
                {
                    doneDrawingBackground = true;
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
