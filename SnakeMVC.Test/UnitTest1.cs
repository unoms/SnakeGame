using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeMVC;

namespace SnakeMVC.Test
{
    [TestClass]
    public class UnitTest1
    {
        private GameModel _gameModel;
       
        [TestInitialize]
        public void Init()
        {
            _gameModel = new GameModel(600, 20);
        }

        [TestMethod]
        public void RellocateFruitNotEqualSnake()
        {
            //Assert
            _gameModel.Snake[0].X = 20;
            _gameModel.Snake[0].Y = 20;

            //Act
            _gameModel.RelocateFruit();
            int fruitX = _gameModel.Fruit.X;
            int fruitY = _gameModel.Fruit.Y;

            //Assert
            Assert.AreNotEqual(20, fruitX);
            Assert.AreNotEqual(20, fruitY);
        }

        [TestMethod]
        public void MoveOnX()
        {
            //Assert
            _gameModel.Snake[0].X = 80;
            _gameModel.Snake[0].Y = 0;
            //Increase the snake
            for(int i = 1; i < 5; i++)
            {
                //Snake in a line
                _gameModel.Snake.Add(new GameModel.Point { 
                    X= _gameModel.Snake[i - 1].X - _gameModel.SnakeSize, 
                    Y= _gameModel.Snake[0].Y
                });
            }

            //Act
            _gameModel.Move(1, 0);

            //Assert
            Assert.AreEqual(100, _gameModel.Snake[0].X);
            Assert.AreEqual(0, _gameModel.Snake[0].Y);
            Assert.AreEqual(80, _gameModel.Snake[1].X);
            Assert.AreEqual(60, _gameModel.Snake[2].X);
            Assert.AreEqual(40, _gameModel.Snake[3].X);
            Assert.AreEqual(20, _gameModel.Snake[4].X);
        }

        [TestMethod]
        public void MoveEatenTrue()
        {
            //Assert
            _gameModel.Snake[0].X = 80;
            _gameModel.Snake[0].Y = 0;
            _gameModel.Fruit.X = 100;
            _gameModel.Fruit.Y = 0;

            //Increase the snake
            for (int i = 1; i < 5; i++)
            {
                //Snake in a line
                _gameModel.Snake.Add(new GameModel.Point
                {
                    X = _gameModel.Snake[i - 1].X - _gameModel.SnakeSize,
                    Y = _gameModel.Snake[0].Y
                });
            }

            //Act
            _gameModel.Move(1, 0);

            //Assert
            Assert.AreEqual(20, _gameModel.Snake[4].X);
        }

    }
}
