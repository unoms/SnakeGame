using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMVC
{

    public class GameModel
    {
        public class Point
        {
            public int X;
            public int Y;
        }
        public readonly int FieldSize;
        public readonly int SnakeSize;
        public Point Fruit;
        public int Score { get; set; }

        public List<Point> Snake;
        private Random _rnd;
        public GameModel(int sizeField, int sizeSnake)
        {
            Score = 0;
            FieldSize = sizeField;
            SnakeSize = sizeSnake;
            _rnd = new Random();
            Snake = new List<Point>();

            //Create the first element of a snake
            int x = _rnd.Next(0, FieldSize / SnakeSize);
            int y = _rnd.Next(0, FieldSize / SnakeSize);
            Point startPointSnake = new Point();
            startPointSnake.X = x * SnakeSize;
            startPointSnake.Y = y * SnakeSize;
            Snake.Add(startPointSnake);

            //Create a fruit
            Fruit = new Point();
            int xFruit;
            int yFruit;
            do
            {
                xFruit = _rnd.Next(0, FieldSize / SnakeSize);
                yFruit = _rnd.Next(0, FieldSize / SnakeSize);
            } while (x == xFruit || y == yFruit);
            Fruit.X = xFruit* SnakeSize;
            Fruit.Y = yFruit * SnakeSize;
        }
        public void Move(int xMove, int yMove)
        {
            //Save the head
            int headX = Snake.ElementAt(0).X;
            int headY = Snake.ElementAt(0).Y;
            Point head = new Point { X = headX, Y = headY };

            //Save the last element of the snake
            int lastX = Snake.ElementAt(Snake.Count - 1).X;
            int lastY = Snake.ElementAt(Snake.Count - 1).Y;
            Point last = new Point { X = lastX, Y = lastY };

            //Make a move
            Snake.ElementAt(0).X += SnakeSize * xMove;
            Snake.ElementAt(0).Y += SnakeSize * yMove;

            bool legal = IsLegal();

            int i = 1;
            Point previous = head;
            while(i < Snake.Count)
            {
                Point tmp = Snake.ElementAt(i);
                Snake[i] = previous;
                previous = tmp;
                i++;
            }
            bool eaten = IsEaten();
            if (eaten)
            {
                Score++;
                RelocateFruit();
                Snake.Add(last);//we add a new element of the snake to the end of it
            }         
            //Inform model about changing
            if (StateChange != null)
                StateChange(legal, eaten);
        }

        public bool IsLegal()
        {
            if(Snake.ElementAt(0).X < 0 || Snake.ElementAt(0).X > FieldSize ||
                Snake.ElementAt(0).Y < 0 || Snake.ElementAt(0).Y > FieldSize)
            {
                return false;
            }

            //Check for self-collision
            for (int i = 1; i < Snake.Count; i++)
            {
                if (Snake.ElementAt(0).X == Snake[i].X &&
                    Snake.ElementAt(0).Y == Snake[i].Y)
                    return false;
            }

            return true;
        }

        public bool IsEaten()
        {
            if (Snake.ElementAt(0).X == Fruit.X
                && Snake.ElementAt(0).Y == Fruit.Y)
            {
                return true;
            }
            return false;
        }

        public void RelocateFruit()
        {
            int xFruit;
            int yFruit;
            do
            {
                xFruit = _rnd.Next(0, FieldSize / SnakeSize);
                yFruit = _rnd.Next(0, FieldSize / SnakeSize);
            } while (Snake.ElementAt(0).X == xFruit || Snake.ElementAt(0).Y == yFruit);
            Fruit.X = xFruit * SnakeSize;
            Fruit.Y = yFruit * SnakeSize;
        }

        public event Action<bool, bool> StateChange;
    }
}
