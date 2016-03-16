using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class Game
    {
        PlayerType Turn
        {
            get; set;
        }
                
        public const int WARRIOR_SIZE = 6;
        public const int PLAYER_COUNT = 2;
        List<Warrior> allWarriors;
        public bool Exit { set; get; }
        

        public Game()
        {
            int selectPlayer = Util.random.Next(0, 2);
            Turn = selectPlayer == 0 ? PlayerType.PLAYER_1 : PlayerType.PLAYER_2;
            allWarriors = new List<Warrior>();            
        }      

        public void welcomeScreen()
        {
            Console.WriteLine("Welcome to Grid Wars. ^(- -)^ ");
            Console.WriteLine();
            Console.WriteLine("Press P - Play, H - Help");

            char inputChar = Console.ReadKey().KeyChar;
            if (inputChar == 'P' || inputChar == 'p')
            {
                setup();
                play();
            }
            else if (inputChar == 'H' || inputChar == 'h')
            {
                printHelp();
            }
            Console.WriteLine();        
        }

        public List<Warrior> getWarriorsForAPlayer(PlayerType playerType)
        {
            return allWarriors.FindAll(w => w.Player == playerType);
        }

        private void printHelp()
        {
            Console.WriteLine(" ^( - - )^ Grid war is a turn based game");
            Console.WriteLine(" The play menu will instruct you with easy. Nothing to see here. Go and Play. :) ");
        }

        private void switchTurn()
        {
            this.Turn = this.Turn == PlayerType.PLAYER_1 ? PlayerType.PLAYER_2 : PlayerType.PLAYER_1;
        }

        private PlayerType getTurn()
        {
            return this.Turn;
        }

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

            if(positionIsOccupied(position))
            {
                Console.Write("\nPosition: " + row + ", " + column + " is occupied. Please select new position");
                Console.Write(", Position <ROW><COL>: ");
                int newRow = readInputInt();
                int newColumn = readInputInt();
                setupPostion(newRow, newColumn);
            }

            return position;
        }

        private bool positionIsOccupied(Position position)
        {
            return Board.ROOMS[position.X,position.Y] == 1;
        }

        private Warrior createWarrior(char warriorType)
        {
            Warrior warrior;

            if (char.ToUpper(warriorType) == 'M')
                warrior = new MeleeWorrior();
            else
                warrior = new MagicWorrior();

            return warrior;
        }

        private void setup()
        {
            Console.WriteLine("\nReady to play");
            Console.WriteLine("You will place 6 players of either M=Melee or G=Magic type");
            Console.WriteLine("in a 6x6 grid");
            Console.WriteLine("Player 1's range is first two rows [0-1][0-5])");
            Console.WriteLine("Player 2's range is last two rows [4,5][0-5]");            

            for (int i = 0; i < WARRIOR_SIZE; i++)
            {
                placePlayer(i);
                switchTurn();
                placePlayer(i);
                switchTurn();
            }
            Console.WriteLine("\nAdd Weapons");
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                Console.WriteLine("\nTurn: " + Turn.ToString());
                Warrior warrior1 = chooseAWarrior();
                Weapon weapon = new Weapon(warrior1 is MeleeWorrior ? WeaponType.Sword : WeaponType.Staff);                
                warrior1.addWeapon(weapon);
                Console.WriteLine("Added a " + weapon.WeaponType.ToString() + " to " + warrior1.Name);

                Warrior warrior2 = chooseAWarrior();
                if (warrior1.GetType() == warrior2.GetType())
                {
                    Console.WriteLine("Cannot add another weapon for " + warrior1.GetType());
                } else
                {
                    weapon = new Weapon(warrior2 is MeleeWorrior ? WeaponType.Sword : WeaponType.Staff);                    
                    warrior2.addWeapon(weapon);
                    Console.WriteLine("Added a " + weapon.WeaponType.ToString() + " to " + warrior2.Name);
                }
                switchTurn();
            }                    
        }        
        
        private Warrior chooseAWarrior()
        {
            List<Warrior> warriors = getWarriorsForAPlayer(Turn);
            warriors.ForEach(w => Console.Write(w.Id + " : " + w.Name + ", "));
            Console.WriteLine("\nWarrior Id: ");
            int warriorId = readInputInt();
            return warriors.First(w => w.Id == warriorId);
        }

        private void displayPlayOptions()
        {
            Console.WriteLine("Choose Options: I=Statistics Information, M=Move, A=Attack, S=Surrender");
        }
        
        private PlayAction choosePlayAction(Warrior warrior)
        {
            PlayAction action;
            char playOption = char.ToUpper(Console.ReadKey().KeyChar);

            if (playOption == 'S')
                action = new SurrenderAction(warrior);
            else
                action = new InformationAction(warrior);

            return action;
        }

        private void play()
        {
            Warrior warrior = chooseAWarrior();

            while(true)  // Turn Loop
            {
                displayPlayOptions();               
                
                PlayAction action = choosePlayAction(warrior);
                action.doAction(this);

                if (action.isDone())
                    break;                
            }            

            if (!Exit)
            {
                switchTurn();
                play();
            }   
        }       
              
    }
}
