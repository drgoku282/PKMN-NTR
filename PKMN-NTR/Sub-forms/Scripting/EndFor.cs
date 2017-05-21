using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public class EndFor : ScriptAction
    {
        private int startInstruction;
        public int StartInstruction
        {
            get
            {
                return startInstruction;
            }
            set
            {
                startInstruction = value;
            }
        }

        public override ActionType Type
        {
            get
            {
                return ActionType.EndFor;
            }
        }

        public override int[] Instruction
        {
            get
            {
                return new int[] { 0 };
            }
            set { }
        }

        public override string Tag
        {
            get
            {
                return ($"End Loop");
            }
        }

        public EndFor()
        {
            startInstruction = -1;
        }

        public async override Task Excecute()
        {
            Report("Script: End of current Loop");
            await Task.Delay(500);
        }
    }
}
