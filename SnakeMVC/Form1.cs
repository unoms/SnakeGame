using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeMVC
{
    public partial class Form1 : Form
    {
        private GameModel _gameModel;
        private List<PictureBox> Snake;
        private PictureBox Fruit;
        private Timer _timer;
        private int stepX;
        private int stepY;
        private int _score;
        public Form1(GameModel gameModel)
        {
            InitializeComponent();
            _gameModel = gameModel;

            this.Width = _gameModel.FieldSize + gameModel.SnakeSize * 2;
            this.Height = _gameModel.FieldSize + gameModel.SnakeSize * 3;

            Snake = new List<PictureBox>();
            Snake.Add(new PictureBox {
                Location = new Point(_gameModel.Snake[0].X, _gameModel.Snake[0].Y),
                BackColor = Color.Red,
                Size = new Size(_gameModel.SnakeSize, _gameModel.SnakeSize),
                Padding = new Padding(0),
                Margin = new Padding(0)
            });

            Fruit = new PictureBox
            {
                Location = new Point(_gameModel.Fruit.X, _gameModel.Fruit.Y),
                BackColor = Color.Yellow,
                Size = new Size(_gameModel.SnakeSize, _gameModel.SnakeSize), 
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            Controls.Add(Snake[0]);
            Controls.Add(Fruit);

            this.Text = string.Format("Score: {0}", _score);

            //Upgrade the view
            _gameModel.StateChange += (legal, eaten) => {
                if (legal)
                {
                    for(int i = 0; i < Snake.Count; i++)
                    {
                        Snake[i].Location = new Point(_gameModel.Snake[i].X, _gameModel.Snake[i].Y);
                    }
                   

                    if (eaten)
                    {
                        _score = _gameModel.Score;
                        this.Text = string.Format("Score: {0}", _score);
                        Fruit.Location = new Point { X = _gameModel.Fruit.X, Y = _gameModel.Fruit.Y };
                        Snake.Add(new PictureBox
                        {
                            Location = new Point(_gameModel.Snake[Snake.Count - 1].X,
                            _gameModel.Snake[Snake.Count - 1].Y),
                            BackColor = Color.Red,
                            Size = new Size(_gameModel.SnakeSize, _gameModel.SnakeSize),
                        });

                        Controls.Add(Snake[Snake.Count - 1]);

                    }
                   
                }
                else
                {
                    _timer.Stop();
                    MessageBox.Show(string.Format("Game Over\nYour score: {0}", _score));
                }

            };

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;

            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
        }

        //This is a controller part1
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Up":
                    stepX = 0;
                    stepY = -1;
                    break;
                case "Down":
                    stepX = 0;
                    stepY = +1;
                    break;
                case "Left":
                    stepX = -1;
                    stepY = 0;
                    break;
                case "Right":
                    stepX = +1;
                    stepY = 0;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        //This is a controller part2
        private void _timer_Tick(object sender, EventArgs e)
        {
            _gameModel.Move(stepX, stepY);

        }

    }
}
