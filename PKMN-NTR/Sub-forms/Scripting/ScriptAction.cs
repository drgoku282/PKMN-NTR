using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public abstract class ScriptAction
    {
        public enum ActionType { Button, Touch, Stick, Delay, StartFor, EndFor }

        public abstract ActionType Type { get; }
        public abstract int[] Instruction { get; set; }
        public abstract string Tag { get; }
        public abstract Task Excecute();

        protected const int defaultTime = 250;

        public static void Report(string msg)
        {
            Program.gCmdWindow.AddToLog(msg);
        }
    }
}
