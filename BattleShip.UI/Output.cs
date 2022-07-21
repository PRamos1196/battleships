using BattleShip.BLL.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    class Output
    {
        public string[,] CreateBoard(Board playerBoard)
        {
            string[,] board = new string[10,10];
            Coordinate coord;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for( int j = 0; j < board.GetLength(1); j++)
                {
                    coord = new Coordinate(j + 1 , i + 1);

                    ShotHistory shot = playerBoard.CheckCoordinate(coord);

                    switch (shot)
                    {
                        case ShotHistory.Hit:
                            board[i, j] = "H";
                            break;
                        case ShotHistory.Miss:
                            board[i, j] = "M";
                            break;
                        case ShotHistory.Unknown:
                            board[i, j] = "-";
                            break;
                    }
                }
            }
            return board;            
        }
        public void PrintBoard(WorkFlow.Player player1, WorkFlow.Player player2)
        {
            string[,] _board = new string[10, 10];
            Coordinate coord;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(player1.Name + "'s Turn");
            Console.WriteLine("Here's " + player2.Name + " Board");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("* 1  2  3  4  5  6  7  8  9  10");
            string[] _letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(_letters[i]);
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    
                    coord = new Coordinate(j + 1, i + 1);
                    ShotHistory shot = player2.Board.CheckCoordinate(coord);
                    switch (shot)
                    {
                        case ShotHistory.Hit:
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            _board[i, j] = " H ";
                            Console.Write(" H ");
                            break;
                        case ShotHistory.Miss:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            _board[i, j] = " M ";
                            Console.Write(" M ");
                            break;
                        case ShotHistory.Unknown:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            _board[i, j] = " - ";
                            Console.Write(" - ");
                            break;
                    }
                }Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public string SplashScreen()
        {
            string splash = " #####  ##### ##### ##### #     ##### ##### #   # #####  ##### #####  \n" +
                            " #   #  #   #   #     #   #     #     #     #   #   #    #   # #      \n" +
                            " # ###  #####   #     #   #     ####  ##### #####   #    ##### #####  \n" +
                            " #   #  #   #   #     #   #     #         # #   #   #    #         #  \n" +
                            " #####  #   #   #     #   ##### ##### ##### #   # #####  #     #####  \n"  ;
            return splash;
        }
    }
}
