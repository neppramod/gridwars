using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public interface PlayAction
    {
        void doAction(Game game);
        bool isDone();
    }
}
