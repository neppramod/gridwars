using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class Status
    {
        public static int ActionCount { get; set; }

        public static bool Exit { set; get; }

        // Direction selected after an action is taken
        public static Direction ActionDirection
        {
            get; set;
        }

        public static PlayerType Turn
        {
            get; set;
        }
    }
}
