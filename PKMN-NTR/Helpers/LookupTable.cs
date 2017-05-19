using PKHeX.Core;
using System.Linq;

namespace pkmn_ntr.Helpers
{
    public static class LookupTable
    {
        #region RAM Address

        public static readonly uint nfcOff = 0x3E14C0; // 1.0 offset was 0x3DFFD0

        public static readonly uint nfcVal = 0xE3A01000;

        public static uint trainercardOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C79C3C;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C81340;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D67D0;
                    default:
                        return 0;
                }
            }
        }

        public static uint trainercardSize
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x200;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0xC0;
                    default:
                        return 0;
                }
            }
        }

        public static uint trainercardLocation
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x14000;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x01200;
                    default:
                        return 0;
                }
            }
        }

        public static uint itemsOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C67564;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C6EC70;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D5934;
                    default:
                        return 0;
                }
            }
        }

        public static uint itemsSize
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0xC00;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0xDE0;
                    default:
                        return 0;
                }
            }
        }

        public static uint itemsLocation
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x0400;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x00;
                    default:
                        return 0;
                }
            }
        }

        public static uint nameOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C79C84;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C81388;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D6808;
                    default:
                        return 0;
                }
            }
        }

        public static uint tidOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C79C3C;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C81340;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D67D0;
                    default:
                        return 0;
                }
            }
        }

        public static uint sidOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C79C3E;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C81342;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D67D2;
                    default:
                        return 0;
                }
            }
        }

        public static uint moneyOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C6A6AC;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C71DC0;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D8FC0;
                    default:
                        return 0;
                }
            }
        }

        public static uint festivalcoinsOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x33124D58;
                    default:
                        return 0;
                }
            }
        }

        public static uint totalfcOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x33124D5C;
                    default:
                        return 0;
                }
            }
        }

        public static uint milesOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C82BA0;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C8B36C;
                    default:
                        return 0;
                }
            }
        }

        public static uint bpOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C6A6E0;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C71DE8;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D90D8;
                    default:
                        return 0;
                }
            }
        }

        public static uint langOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8C79C69;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8C8136D;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x330D6805;
                    default:
                        return 0;
                }
            }
        }

        public static uint timeOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 0x8CE2814;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 0x8CFBD88;
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x34197648;
                    default:
                        return 0;
                }
            }
        }

        public static uint eggseedOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x3313EDDC;
                    default:
                        return 0;
                }
            }
        }

        public static uint legseedOff
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.SN:
                    case GameVersion.MN:
                        return 0x325A3878;      //1.1: 0x325A3838;
                    default:
                        return 0;
                }
            }
        }

        public static byte[] oppPattern
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return new byte[] { 0x60, 0x75, 0xC6, 0x08, 0xDC, 0xA8, 0xC7, 0x08, 0xD0, 0xB6, 0xC7, 0x08 };
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return new byte[] { 0x60, 0xE7, 0xC6, 0x08, 0x6C, 0xEC, 0xC6, 0x08, 0xE0, 0x1F, 0xC8, 0x08, 0x00, 0x39, 0xC8, 0x08 };
                    default:
                        return null;
                }
            }
        }

        public static uint offsetOpp
        {
            get
            {
                switch (Program.gCmdWindow.SAV.Version)
                {
                    case GameVersion.X:
                    case GameVersion.Y:
                        return 637;
                    case GameVersion.OR:
                    case GameVersion.AS:
                        return 673;
                    default:
                        return 0;
                }
            }
        }

        #endregion RAM Address

        #region Formula

        public static int getTSV(ushort TID, ushort SID)
        {
            return (TID ^ SID) >> 4;
        }

        public static int getG7ID(ushort TID, ushort SID)
        {
            return (int)((uint)(TID | (SID << 16)) % 1000000);
        }

        public static int getMaxSpace(int box, int slot)
        {
            int result = 0;
            result += (31 - slot);
            switch (Program.gCmdWindow.SAV.Generation)
            {
                case 6:
                    result += (31 - box) * 30;
                    break;
                case 7:
                    result += (32 - box) * 30;
                    break;
            }
            return result;
        }

        #endregion Formula

        #region Buttons

        public static uint keyA = 0xFFE;
        public static uint keyB = 0xFFD;
        public static uint keyX = 0xBFF;
        public static uint keyY = 0x7FF;
        public static uint keyR = 0xEFF;
        public static uint keyL = 0xDFF;
        public static uint keySTART = 0xFF7;
        public static uint keySELECT = 0xFFB;
        public static uint DpadUP = 0xFBF;
        public static uint DpadDOWN = 0xF7F;
        public static uint DpadLEFT = 0xFDF;
        public static uint DpadRIGHT = 0xFEF;
        public static uint runUP = 0xFBD;
        public static uint runDOWN = 0xF7D;
        public static uint runLEFT = 0xFDD;
        public static uint runRIGHT = 0xFED;
        public static uint softReset = 0xCF7;
        public static uint nokey = 0xFFF;
        public static uint notouch = 0x02000000;
        public static uint nostick = 0x00800800;

        #endregion Buttons

        #region Box Position

        // Gen 6
        public static uint[] pokeposX6 = { 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180 };
        public static uint[] pokeposY6 = { 60, 60, 60, 60, 60, 60, 90, 90, 90, 90, 90, 90, 120, 120, 120, 120, 120, 120, 150, 150, 150, 150, 150, 150, 180, 180, 180, 180, 180, 180 };
        public static uint[] boxposX6 = { 20, 60, 100, 140, 180, 220, 260, 300, 20, 60, 100, 140, 180, 220, 260, 300, 20, 60, 100, 140, 180, 220, 260, 300, 20, 60, 100, 140, 180, 220, 260 };
        public static uint[] boxposY6 = { 24, 24, 24, 24, 24, 24, 24, 24, 72, 72, 72, 72, 72, 72, 72, 72, 120, 120, 120, 120, 120, 120, 120, 120, 168, 168, 168, 168, 168, 168, 168 };

        // Gen 7
        public static uint[] pokeposX7 = { 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180, 30, 60, 90, 120, 150, 180 };
        public static uint[] pokeposY7 = { 70, 70, 70, 70, 70, 70, 100, 100, 100, 100, 100, 100, 130, 130, 130, 130, 130, 130, 160, 160, 160, 160, 160, 160, 190, 190, 190, 190, 190, 190 };
        public static uint[] boxposX7 = { 33, 69, 105, 141, 177, 213, 249, 285, 33, 69, 105, 141, 177, 213, 249, 285, 33, 69, 105, 141, 177, 213, 249, 285, 33, 69, 105, 141, 177, 213, 249, 285 };
        public static uint[] boxposY7 = { 36, 36, 36, 36, 36, 36, 36, 36, 84, 84, 84, 84, 84, 84, 84, 84, 132, 132, 132, 132, 132, 132, 132, 132, 180, 180, 180, 180, 180, 180, 180, 180 };

        #endregion Box Position
    }

}