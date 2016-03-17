﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class MoveAction : IPlayAction
    {
        Warrior warrior;
        Game game;

        public MoveAction(Warrior warrior)
        {
            this.warrior = warrior;
        }

        public void doAction(Game game)
        {
            this.game = game;
            if (game.ActionCount == 0 || game.ActionCount == 1)
                game.ActionCount++;

            Direction direction = Board.chooseADirection();
            if (direction != Direction.INVALID_DIRECTION)
            {
                try
                {
                    this.warrior.Move(direction);
                } catch(InvalidOperationException)
                {
                    Console.WriteLine("\nCould not move the player to " + direction.ToString() + ", you miss a chance");
                }
            }                     
        }

        public bool isDone()
        {
            if ( game.ActionCount == 2)
            {
                game.ActionCount = 0; // reset count
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
