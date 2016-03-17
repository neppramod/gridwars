using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public interface IPlayAction
    {
        void doAction();
        bool isDone();
    }
}
