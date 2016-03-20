using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            Position w = (Position)obj;
            return (w.X == X && w.Y == Y);           
        }

        public override int GetHashCode()
        {
            int hash = 17;            
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();            
            return hash;
        }
    }
}
