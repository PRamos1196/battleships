using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    class Input
    {
        // Asks user for input
        public string Ask(string prompt)
        {
            string name;
            Console.WriteLine(prompt);
            name = Console.ReadLine();
            return name;
        }
        public Coordinate AskCoord(string prompt) 
        {

            int x = 1;
            int y = 1;
            bool letter_validation = false;
            bool number_validation = false;

            while (!letter_validation || !number_validation )
            {
                Console.Write(prompt);
                string _prompt = Console.ReadLine();


                if (string.IsNullOrEmpty(_prompt))
                {
                    continue;
                }
                string letter = _prompt.Substring(0, 1).ToLower();
                int length = _prompt.Length;
                string number = _prompt.Substring(1);

                

                char _letter;

                int check;
                
                if (char.TryParse(letter.ToLower(), out _letter))
                {
                    //Checks if guess is letter or not
                    if (_letter >= 'a' && _letter <= 'j')
                    {
                        letter_validation = true;
                        switch (letter)
                        {
                            case "a":
                                x = 1;
                                break;
                            case "b":
                                x = 2;
                                break;
                            case "c":
                                x = 3;
                                break;
                            case "d":
                                x = 4;
                                break;
                            case "e":
                                x = 5;
                                break;
                            case "f":
                                x = 6;
                                break;
                            case "g":
                                x = 7;
                                break;
                            case "h":
                                x = 8;
                                break;
                            case "i":
                                x = 9;
                                break;
                            case "j":
                                x = 10;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Out of Range Value, Try Again.");
                    }
                }
                if (int.TryParse(number, out check))
                {
                    if (check >= 1 && check <= 10)
                    {
                        number_validation = true;
                        y = check;
                    }
                    else
                    {
                        Console.WriteLine("Input Out Of Range, Try Again.");
                    }
                }
                else
                {
                    Console.WriteLine("Please Follow the Format, After a Letter Type in a Number (Ex: A10)");
                }
            }
            Coordinate coord = new Coordinate(y, x);
            return coord;
        }
        
        //Asks the user what direction they want their ship to be in
        public ShipDirection AskDirection(string prompt)
        {
            
            bool direction_validation = false;

            while(!direction_validation)
            {
                Console.Write(prompt);
                string direction = Console.ReadLine().ToLower();
                switch (direction)
                {
                    case "up":
                        return ShipDirection.Up;
                    case "down":
                        return ShipDirection.Down;
                    case "left":
                        return ShipDirection.Left;
                    case "right":
                        return ShipDirection.Right;
                    default:
                        Console.WriteLine("That was not the correct input, Try Again");
                        break;
                }
            }
            return ShipDirection.Up;
        }
    }
}
