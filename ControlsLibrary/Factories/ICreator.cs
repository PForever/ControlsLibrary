using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary.Factories
{
    interface ICreator
    {
        IFactory factory { get; }
    }
}