using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public class TouchAction : ScriptAction
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
                if (value < -1)
                {
                    xCoord = -1;
                }
                else if (value > 319)
                {
                    xCoord = 319;
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
                if (value < -1)
                {
                    yCoord = -1;
                }
                else if (value > 239)
                {
                    yCoord = 239;
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
                return ActionType.Touch;
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
                    xCoord = -1;
                    yCoord = -1;
                    time = 0;
                }
                else if (value.Length < 2)
                {
                    xCoord = -1;
                    yCoord = -1;
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
                if (xCoord < 0 || yCoord < 0)
                {
                    return ("Release touch screen");
                }
                else if (time == -1)
                {
                    return ($"Touch and hold the screen at {xCoord}, {yCoord}");
                }
                else if (time > 0)
                {
                    return ($"Touch the screen at {xCoord}, {yCoord} during {time.ToString()} ms");
                }
                else
                {
                    return ($"Touch the screen at {xCoord}, {yCoord}");
                }
            }
        }

        public TouchAction(int _xCoord, int _yCoord, int _time)
        {
            if (_xCoord > 319)
            {
                xCoord = 319;
            }
            else if (_xCoord >= 0)
            {
                xCoord = _xCoord;
            }
            else
            {
                xCoord = -1;
            }

            if (_yCoord > 239)
            {
                yCoord = 239;
            }
            else if (_yCoord >= 0)
            {
                yCoord = _yCoord;
            }
            else
            {
                yCoord = -1;
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
            if (xCoord < 0 || yCoord < 0)
            {
                await Program.helper.ScriptTouchRelease();
            }
            else if (time == 0)
            {
                await Program.helper.ScriptTouch(xCoord, yCoord);
            }
            else if (time > 0)
            {
                await Program.helper.ScriptTouchTimed(xCoord, yCoord, time);
            }
            else if (time < 0)
            {
                await Program.helper.ScriptTouchHold(xCoord, yCoord);
            }
            else
            {
                Report("Script: Unknown touch screen instruction");
                await Task.Delay(500);
            }
        }
    }
}
