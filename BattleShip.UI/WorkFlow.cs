using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class WorkFlow
    {
        public void Start()
        {
            // Brings in Input and Output
            Input input = new Input();
            Output output = new Output();

            Console.WriteLine(output.SplashScreen());
            // Add Ask method in Input
            int player_turn = 1;
                
            do
            {
                // Creates two instances of Board for each player
                string player1Name = input.Ask("Player 1, what's your name?");
                string player2Name = input.Ask("Player 2, what's your name?");

                // Add Player class for players
                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);

                Setup(player1.Name, player1.Board);
                Setup(player2.Name, player2.Board);

                ShotStatus status;
                do
                {
                    if(player_turn == 1)
                    {
                        // string attacker, Board attackedBoard
                        status = Turn(player1, player2);
                        player_turn = 2;
                        
                    }
                    else
                    {
                        status = Turn(player2, player1);
                        player_turn = 1;
                    }
                }
                while (status != ShotStatus.Victory);
                
                Console.WriteLine("Play Again? ( Y | N )");
                string play_again = Console.ReadLine().ToLower();
                if(play_again.Equals("y"))
                {
                    Console.WriteLine("Have Fun!");
                }
                else
                {
                    break;
                }
            } while (true);
            
        }

        public void PlaceShipOnBoard(Board playerBoard, ShipType shipType)
        {
            Input input = new Input();
            PlaceShipRequest request = new PlaceShipRequest();

            Console.WriteLine("Where do you what do set your " + shipType);
            request.ShipType = shipType;
            int count = 0;

            ShipPlacement result;
            while(count < 5){
                // Goes to input and asks for coordinates
                request.Coordinate = input.AskCoord("Coordinates: ");
                request.Direction = input.AskDirection("Direction: ");
                result = playerBoard.PlaceShip(request);

                if(result == ShipPlacement.NotEnoughSpace)
                {
                    Console.WriteLine("There is not enough space, Try again");
                    break;
                }
                else if(result == ShipPlacement.Overlap)
                {
                    Console.WriteLine("Your ship's are overlapping, Try again");
                
                }else if(result == ShipPlacement.Ok)
                {
                    Console.WriteLine("Your ship has been placed");
                    Console.WriteLine();
                    count++;
                    break;
                }
            }

        }

        private void Setup(string playerName, Board playerBoard) 
        {
            Console.Clear();
            Console.WriteLine(playerName + " place your ships: ");
            PlaceShipOnBoard(playerBoard, ShipType.Battleship);
            PlaceShipOnBoard(playerBoard, ShipType.Carrier);
            PlaceShipOnBoard(playerBoard, ShipType.Cruiser);
            PlaceShipOnBoard(playerBoard, ShipType.Destroyer);
            PlaceShipOnBoard(playerBoard, ShipType.Submarine);
        }

        private ShotStatus Turn(Player player1, Player player2) 
        {

            Input input = new Input();
            Output output = new Output();

            output.PrintBoard(player1, player2);
            ShotStatus shotStatus;

            do
            {
                Coordinate coordinates = input.AskCoord("Coordinates to Attack: ");
                FireShotResponse fireShotResponse = player2.Board.FireShot(coordinates);
                shotStatus = fireShotResponse.ShotStatus;
            
                //string[,] board = output.CreateBoard(attackedBoard);   
                switch (shotStatus)
                {
                    case ShotStatus.Invalid:
                        Console.WriteLine("Invalid Shot");
                        break;
                    case ShotStatus.Duplicate:
                        Console.WriteLine("You Took This Shot Before");
                        break;
                    case ShotStatus.Miss:
                        Console.WriteLine("You Missed!");
                        break;
                    case ShotStatus.Hit:
                        Console.WriteLine("You Hit A Target!");
                        break;
                    case ShotStatus.HitAndSunk:
                        Console.WriteLine("You Sunk Your Opponent's " + fireShotResponse.ShipImpacted);
                        break;
                    case ShotStatus.Victory:
                        Console.WriteLine("Victory! You Have Sunken All Of Your Opponent's Battleships");
                        break;
                } 
            } while (shotStatus == ShotStatus.Invalid || shotStatus == ShotStatus.Duplicate);
            Console.WriteLine("Press Key to Continue");
            Console.ReadKey();
            Console.Clear();
            return shotStatus;
        }

        public class Player{
            public string Name { get; }
            public Board Board { get; }
            public Player(string prompt) {
                Name = prompt;
                Board = new Board();
            }
        }
    }
}
