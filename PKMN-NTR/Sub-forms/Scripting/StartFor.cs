using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public class StartFor : ScriptAction
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

        private int loops;
        public int Loops
        {
            get
            {
                return loops;
            }
            set
            {
                loops = value;
            }
        }

        private int totalLoops;
        public int TotalLoops
        {
            get
            {
                return totalLoops;
            }
            set
            {
                if (value > 0)
                {
                    totalLoops = value;
                }
                else
                {
                    totalLoops = 1;
                }
            }
        }

        private int endInstruction;
        public int EndInstruction
        {
            get
            {
                return endInstruction;
            }
            set
            {
                endInstruction = value;
            }
        }

        public bool IsFinished
        {
            get
            {
                return loops >= totalLoops;
            }
        }

        public override ActionType Type
        {
            get
            {
                return ActionType.StartFor;
            }
        }

        public override int[] Instruction
        {
            get
            {
                return new int[] { TotalLoops, id };
            }
            set
            {
                if (value == null)
                {
                    TotalLoops = 1;
                }
                else if (value.Length == 1)
                {
                    TotalLoops = value[0];
                    id = -1;
                }
                else
                {
                    TotalLoops = value[0];
                    id = value[1];
                }
            }
        }

        public override string Tag
        {
            get
            {
                return ($"Loop {totalLoops} (ID={id})");
            }
        }

        public StartFor(int _loop, int newid)
        {
            totalLoops = _loop > 0 ? _loop : 1;
            loops = 0;
            endInstruction = -1;
            id = newid;
        }

        public async override Task Excecute()
        {
            loops++;
            Report($"Script: Excecuting loop {loops} of {totalLoops}");
            await Task.Delay(200);
        }
    }
}
