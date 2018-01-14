using pkmn_ntr.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkmn_ntr.Sub_forms.Scripting
{
    public class ButtonAction : ScriptAction
    {
        public enum ConsoleButton { None, A, B, X, Y, L, R, Start, Select, Up, Down, Left, Right }

        private ConsoleButton button;
        public ConsoleButton Button
        {
            get
            {
                return button;
            }
            set
            {
                button = value;
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
                return ActionType.Button;
            }
        }

        public override int[] Instruction
        {
            get
            {
                return new int[] { (int)button, time };
            }
            set
            {
                if (value == null)
                {
                    button = ConsoleButton.None;
                    time = 0;
                }
                else if (value.Length == 0)
                {
                    button = ConsoleButton.None;
                    time = 0;
                }
                else if (value.Length == 1)
                {
                    if (Enum.IsDefined(typeof(Console), value[0]))
                    {
                        button = (ConsoleButton)value[0];
                    }
                    else
                    {
                        button = ConsoleButton.None;
                    }
                    time = 0;
                }
                else
                {
                    if (Enum.IsDefined(typeof(Console), value[0]))
                    {
                        button = (ConsoleButton)value[0];
                    }
                    else
                    {
                        button = ConsoleButton.None;
                    }
                    time = value[1];
                }
            }
        }

        public override string Tag
        {
            get
            {
                if (button == ConsoleButton.None)
                {
                    return ("Release buttons");
                }
                else if (time == -1)
                {
                    return ($"Press and hold {button.ToString("G")} button");
                }
                else if (time > 0)
                {
                    return ($"Press {button.ToString("G")} button during {time.ToString()} ms");
                }
                else
                {
                    return ($"Press {button.ToString("G")} button");
                }
            }
        }

        public ButtonAction(ConsoleButton _button, int _time)
        {
            button = _button;
            if (_button == ConsoleButton.None)
            {
                time = 0;
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
            uint command;
            switch (button)
            {
                case ConsoleButton.A: command = LookupTable.ButtonA; break;
                case ConsoleButton.B: command = LookupTable.ButtonB; break;
                case ConsoleButton.X: command = LookupTable.ButtonX; break;
                case ConsoleButton.Y: command = LookupTable.ButtonY; break;
                case ConsoleButton.R: command = LookupTable.ButtonR; break;
                case ConsoleButton.L: command = LookupTable.ButtonL; break;
                case ConsoleButton.Start: command = LookupTable.ButtonStart; break;
                case ConsoleButton.Select: command = LookupTable.ButtonSelect; break;
                case ConsoleButton.Up: command = LookupTable.DPadUp; break;
                case ConsoleButton.Down: command = LookupTable.DPadDown; break;
                case ConsoleButton.Left: command = LookupTable.DPadLeft; break;
                case ConsoleButton.Right: command = LookupTable.DPadRight; break;
                default: command = LookupTable.NoButtons; break;
            }
            if (button == ConsoleButton.None)
            {
                await Program.helper.ScriptButtonRelease();
            }
            else if (time == 0)
            {
                await Program.helper.ScriptButton(command);
            }
            else if (time > 0)
            {
                await Program.helper.ScriptButtonTimed(command, time);
            }
            else if (time < 0)
            {
                await Program.helper.ScriptButtonHold(command);
            }
            else
            {
                Report("Script: Unknown button instruction.");
                await Task.Delay(500);
            }
        }
    }
}