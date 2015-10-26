using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kTwitch2.Model
{
    public abstract class ModeBase : Model
    {
        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
