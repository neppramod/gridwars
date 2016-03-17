using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    /**
     * Game class takes user input, displays information and initializes the warriors and plays the game
     */
    public class Game
    {
        public Game()
        {
            int selectPlayer = Util.random.Next(0, 2);
            Status.Turn = selectPlayer == 0 ? PlayerType.PLAYER_1 : PlayerType.PLAYER_2;
            
            Status.ActionCount = 0;            
        }      

        public void welcomeScreen()
        {
            Console.WriteLine("Welcome to Grid Wars. ^(- -)^ ");
            Console.WriteLine();
            chooseWelcomeMenu();            
        }

        private void chooseWelcomeMenu()
        {
            Console.WriteLine("Press P - Play, H - Help, Q - Quit");
            char inputChar = char.ToUpper(Console.ReadKey().KeyChar);
            if (inputChar == 'P')
            {
                setup();
                play();
            }
            else if (inputChar == 'H')
            {
                printHelp();
                chooseWelcomeMenu();
            } else if (inputChar == 'Q')
            {
                Console.WriteLine("\nQuiting...");
            }
            Console.WriteLine();
        }
              

        private void printHelp()
        {
            Console.WriteLine("\n ^( - - )^ Grid war is a turn based game");
            Console.WriteLine(" Game menu will instruct you all the options. Nothing to see here. Go and Play. :) \n");
        }                

        /*
        private void placePlayer(int i)
        {
            Console.Write("\nTurn: " + Turn.ToString());
            Console.Write(", Warriror " + i);
            Console.Write(", Type [M,G]: ");
            char warriorType = Console.ReadKey().KeyChar;
            Console.Write(" Position <ROW><COL>: ");

            int row = readInputInt();
            int column = readInputInt();
            
            Warrior warrior = createWarrior(warriorType);
            warrior.Id = i;
            warrior.Name = (int)Turn + "_" + i;
            warrior.Position = setupPostion(row, column);
            warrior.Player = Turn;
            Board.ROOMS[warrior.Position.X,warrior.Position.Y] = 1;
            allWarriors.Add(warrior);
        }
        */

        // Fake placePlayer. Comment this and uncommet above to get user input
        private void placePlayer(int i)
        {
            Console.Write("\nTurn: " + Status.Turn.ToString());
            Console.Write(", Warriror " + i);

            int warriorChoice = Util.random.Next(0, 2);
            char warriorType = warriorChoice == 0 ? 'M' : 'G';

            Warrior warrior = GameUtil.createWarrior(warriorType);
            warrior.Id = i;            

            int row = Status.Turn == PlayerType.PLAYER_1 ? 0 : 5;
            int column = i;
            warrior.Position = setupPostion(row, column);
            warrior.Player = Status.Turn;
            Board.ROOMS[warrior.Position.X, warrior.Position.Y] = 1;
            GameUtil.allWarriors.Add(warrior);            
        }

        private int readInputInt()
        {
            int readValue = -1;
            ConsoleKeyInfo input = Console.ReadKey();
            if (char.IsDigit(input.KeyChar))
            {
                readValue = int.Parse(input.KeyChar.ToString());
            }

            if (readValue == -1)
                throw new InvalidOperationException("Could not read proper int value");

            return readValue;
        }

        private Position setupPostion(int row, int column)
        {
            Position position = new Position { X = row, Y = column };

            if(GameUtil.isPositionOccupied(position))
            {
                Console.Write("\nPosition: " + row + ", " + column + " is occupied. Please select new position");
                Console.Write(", Position <ROW><COL>: ");
                int newRow = readInputInt();
                int newColumn = readInputInt();
                setupPostion(newRow, newColumn);
            }

            return position;
        }

        private void setup()
        {
            Console.WriteLine("\nReady to play");
            Console.WriteLine("You will place 6 players of either M=Melee or G=Magic type");
            Console.WriteLine("in a 6x6 grid");
            Console.WriteLine("Player 1's range is first two rows [0-1][0-5])");
            Console.WriteLine("Player 2's range is last two rows [4,5][0-5]");            

            for (int i = 0; i < GameUtil.WARRIOR_SIZE; i++)
            {
                placePlayer(i);
                GameUtil.switchTurn();
                placePlayer(i);
                GameUtil.switchTurn();
            }
            Console.WriteLine("\nAll warriors setup");            
            //addWeapons();                  
        } 
        
        private void addWeapons()
        {
            Console.WriteLine("\nAdd Weapons");
            for (int i = 0; i < GameUtil.PLAYER_COUNT; i++)
            {
                Console.WriteLine("\nTurn: " + Status.Turn.ToString());
                Warrior warrior1 = chooseAWarrior();
                Weapon weapon = new Weapon(warrior1 is MeleeWarrior ? WeaponType.Sword : WeaponType.Staff);
                warrior1.addWeapon(weapon);
                Console.WriteLine("Added a " + weapon.WeaponType.ToString() + " to warrior " + warrior1.Id);

                Warrior warrior2 = chooseAWarrior();
                if (warrior1.GetType() == warrior2.GetType())
                {
                    Console.WriteLine("Cannot add another weapon for " + warrior1.GetType());
                }
                else
                {
                    weapon = new Weapon(warrior2 is MeleeWarrior ? WeaponType.Sword : WeaponType.Staff);
                    warrior2.addWeapon(weapon);
                    Console.WriteLine("Added a " + weapon.WeaponType.ToString() + " to warrior " + warrior2.Id);
                }
                GameUtil.switchTurn();
            }
        }       
        
        private Warrior chooseAWarrior()
        {
            List<Warrior> warriors = GameUtil.getWarriorsForAPlayer(Status.Turn);
            warriors.ForEach(w => Console.Write(w.Id + " : " + w.GetType().Name + ", "));
            Console.Write("\nWarrior Id: ");
            int warriorId = readInputInt();
            return warriors.First(w => w.Id == warriorId);
        }

        private void displayPlayOptions()
        {
            Console.WriteLine("\nChoose Options: I=Statistics Information, M=Move, A=Attack, S=Surrender");
        }
        
        private IPlayAction choosePlayAction(Warrior warrior)
        {
            IPlayAction action;
            char playOption = char.ToUpper(Console.ReadKey().KeyChar);

            Status.ActionDirection = (playOption == 'M' || playOption == 'A') ? chooseADirection() : Direction.INVALID_DIRECTION;

            if (playOption == 'S')
                action = new SurrenderAction(warrior);
            else if (playOption == 'M')
                action = new MoveAction(warrior);
            else if (playOption == 'A')
                action = new AttackAction(warrior);
            else
                action = new InformationAction(warrior);

            return action;
        }

        private void play()
        {
            PrintBoard();
            Console.WriteLine("\nTurn: " + Status.Turn);            
            Console.WriteLine("\nChoose A warrior for play");
            Warrior warrior = chooseAWarrior();

            while(true)  // Turn Loop
            {
                displayPlayOptions();               
                
                IPlayAction action = choosePlayAction(warrior);
                action.doAction();

                if (action.isDone())
                    break;                
            }            

            if (!Status.Exit)
            {
                GameUtil.switchTurn();
                play();
            }           
        }

        public Direction chooseADirection()
        {
            Console.WriteLine("\nDirection: E:EAST, W:WEST, N:NORTH, S:SOUTH, A:EAST_NORTH, B:EAST_SOUTH, C:WEST_SOUTH, D:WEST_NORTH");
            char inputChar = char.ToUpper(Console.ReadKey().KeyChar);

            if (inputChar == 'E')
                return Direction.EAST;
            else if (inputChar == 'W')
                return Direction.WEST;
            else if (inputChar == 'N')
                return Direction.NORTH;
            else if (inputChar == 'S')
                return Direction.SOUTH;
            else if (inputChar == 'A')
                return Direction.EAST_NORTH;
            else if (inputChar == 'B')
                return Direction.EAST_SOUTH;
            else if (inputChar == 'C')
                return Direction.WEST_SOUTH;
            else if (inputChar == 'D')
                return Direction.WEST_NORTH;
            else
            {
                Console.WriteLine("Did not select a proper direction. You miss.");
                return Direction.INVALID_DIRECTION;
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("\nBoard\n");
            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                {
                    Console.Write(" | " + Board.ROOMS[i, j] + " | ");
                }
                Console.WriteLine();
            }
        }

    }
}
