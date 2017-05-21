using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public class StickAction : ScriptAction
    {
        private int xCoord;
        public int XCoord
        {
            get
            {
                return xCoord;
            }
            set
            {
                if (value < -100)
                {
                    xCoord = -100;
                }
                else if (value > 100)
                {
                    xCoord = 100;
                }
                else
                {
                    xCoord = value;
                }
            }
        }

        private int yCoord;
        public int YCoord
        {
            get
            {
                return yCoord;
            }
            set
            {
                if (value < -100)
                {
                    yCoord = -100;
                }
                else if (value > 100)
                {
                    yCoord = 100;
                }
                else
                {
                    yCoord = value;
                }
            }
        }

        private int time;
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                if (value >= 0)
                {
                    time = value;
                }
                else
                {
                    time = -1;
                }
            }
        }

        public override ActionType Type
        {
            get
            {
                return ActionType.Stick;
            }
        }

        public override int[] Instruction
        {
            get
            {
                return new int[] { xCoord, yCoord, time };
            }
            set
            {
                if (value == null)
                {
                    xCoord = 0;
                    yCoord = 0;
                    time = 0;

                }
                else if (value.Length < 2)
                {
                    xCoord = 0;
                    yCoord = 0;
                    time = 0;
                }
                else if (value.Length == 2)
                {
                    xCoord = value[0];
                    yCoord = value[1];
                    time = 0;
                }
                else
                {
                    xCoord = value[0];
                    yCoord = value[1];
                    time = value[2];
                }
            }
        }

        public override string Tag
        {
            get
            {
                if (xCoord == 0 && yCoord == 0)
                {
                    return ("Release control stick");
                }
                else if (time == -1)
                {
                    return ($"Move and hold the control stick to {xCoord}, {yCoord}");
                }
                else if (time > 0)
                {
                    return ($"Move the control stick {xCoord}, {yCoord} during {time.ToString()} ms");
                }
                else
                {
                    return ($"Move and release the control stick to {xCoord}, {yCoord}");
                }
            }
        }

        public StickAction(int _xCoord, int _yCoord, int _time)
        {
            if (_xCoord < -100)
            {
                xCoord = -100;
            }
            else if (_xCoord > 100)
            {
                xCoord = 100;
            }
            else
            {
                xCoord = _xCoord;
            }

            if (_yCoord < -100)
            {
                yCoord = -100;
            }
            else if (_yCoord > 100)
            {
                yCoord = 100;
            }
            else
            {
                yCoord = _yCoord;
            }

            if (_time >= 0)
            {
                time = _time;
            }
            else
            {
                time = -1;
            }
        }

        public async override Task Excecute()
        {
            if (xCoord == 0 && yCoord == 0)
            {
                await Program.helper.ScriptStickRelease();
            }
            else if (time == 0)
            {
                await Program.helper.ScriptStick(xCoord, yCoord);
            }
            else if (time > 0)
            {
                await Program.helper.ScriptStickTimed(xCoord, yCoord, time);
            }
            else if (time < 0)
            {
                await Program.helper.ScriptStickHold(xCoord, yCoord);
            }
            else
            {
                Report("Script: Unknown control stick instruction");
                await Task.Delay(500);
            }
        }
    }
}
