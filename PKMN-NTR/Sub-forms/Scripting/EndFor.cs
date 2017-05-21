using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public class EndFor : ScriptAction
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

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
                return new int[] { id };
            }
            set
            {
                if (value == null)
                {
                    id = -1;
                }
                else
                {
                    id = value[0];
                }
            }
        }

        public override string Tag
        {
            get
            {
                return ($"End Loop (ID={id})");
            }
        }

        public EndFor(int newid)
        {
            startInstruction = -1;
            id = newid;
        }

        public async override Task Excecute()
        {
            Report("Script: End of current Loop");
            await Task.Delay(500);
        }
    }
}
