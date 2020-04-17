using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SplaToolMenu
{
    public class Mouse
    {
        public static Boolean Pressed = false;
        public static Boolean _Pressed = false;
        public static Boolean Clicked = false;
        public static int X = 0;
        public static int Y = 0;
        public static void Pos(int x, int y)
        {
            X = x;
            Y = y;
        }
        static public void MouseThread()
        {
            var handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);

            int mode = 0;
            if (!(NativeMethods.GetConsoleMode(handle, ref mode))) { throw new Win32Exception(); }

            mode |= NativeMethods.ENABLE_MOUSE_INPUT;
            mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
            mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

            if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }

            var record = new NativeMethods.INPUT_RECORD();
            uint recordLen = 0;
            while (true)
            {
                if (!(NativeMethods.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }
                switch (record.EventType)
                {
                    case NativeMethods.MOUSE_EVENT:
                        {
                            Mouse._Pressed = Mouse.Pressed;
                            if (record.MouseEvent.dwButtonState == 0x001)
                            {
                                Mouse.Pressed = true;
                            }
                            else
                            {
                                Mouse.Pressed = false;
                            }
                            Mouse.Pos(record.MouseEvent.dwMousePosition.X, record.MouseEvent.dwMousePosition.Y);
                            //Console.WriteLine(string.Format("    X ...............:   {0,4:0}  ", record.MouseEvent.dwMousePosition.X));
                            //Console.WriteLine(string.Format("    Y ...............:   {0,4:0}  ", record.MouseEvent.dwMousePosition.Y));
                            //Console.WriteLine(string.Format("    dwButtonState ...: 0x{0:X4}  ", record.MouseEvent.dwButtonState));
                            //Console.WriteLine(string.Format("    dwControlKeyState: 0x{0:X4}  ", record.MouseEvent.dwControlKeyState));
                            //Console.WriteLine(string.Format("    dwEventFlags ....: 0x{0:X4}  ", record.MouseEvent.dwEventFlags));
                        }
                        break;
                }
                if (Mouse.Pressed == true
                 && Mouse._Pressed == false)
                {
                    Mouse.Clicked = true;
                }
                else
                {
                    Mouse.Clicked = false;
                }
            }
        }


        private class NativeMethods
        {

            public const Int32 STD_INPUT_HANDLE = -10;

            public const Int32 ENABLE_MOUSE_INPUT = 0x0010;
            public const Int32 ENABLE_QUICK_EDIT_MODE = 0x0040;
            public const Int32 ENABLE_EXTENDED_FLAGS = 0x0080;

            public const Int32 KEY_EVENT = 1;
            public const Int32 MOUSE_EVENT = 2;


            [DebuggerDisplay("EventType: {EventType}")]
            [StructLayout(LayoutKind.Explicit)]
            public struct INPUT_RECORD
            {
                [FieldOffset(0)]
                public Int16 EventType;
                [FieldOffset(4)]
                public KEY_EVENT_RECORD KeyEvent;
                [FieldOffset(4)]
                public MOUSE_EVENT_RECORD MouseEvent;
            }

            [DebuggerDisplay("{dwMousePosition.X}, {dwMousePosition.Y}")]
            public struct MOUSE_EVENT_RECORD
            {
                public COORD dwMousePosition;
                public Int32 dwButtonState;
                public Int32 dwControlKeyState;
                public Int32 dwEventFlags;
            }

            [DebuggerDisplay("{X}, {Y}")]
            public struct COORD
            {
                public UInt16 X;
                public UInt16 Y;
            }

            [DebuggerDisplay("KeyCode: {wVirtualKeyCode}")]
            [StructLayout(LayoutKind.Explicit)]
            public struct KEY_EVENT_RECORD
            {
                [FieldOffset(0)]
                [MarshalAsAttribute(UnmanagedType.Bool)]
                public Boolean bKeyDown;
                [FieldOffset(4)]
                public UInt16 wRepeatCount;
                [FieldOffset(6)]
                public UInt16 wVirtualKeyCode;
                [FieldOffset(8)]
                public UInt16 wVirtualScanCode;
                [FieldOffset(10)]
                public Char UnicodeChar;
                [FieldOffset(10)]
                public Byte AsciiChar;
                [FieldOffset(12)]
                public Int32 dwControlKeyState;
            };


            public class ConsoleHandle : SafeHandleMinusOneIsInvalid
            {
                public ConsoleHandle() : base(false) { }

                protected override bool ReleaseHandle()
                {
                    return true; //releasing console handle is not our business
                }
            }


            [DllImportAttribute("kernel32.dll", SetLastError = true)]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            public static extern Boolean GetConsoleMode(ConsoleHandle hConsoleHandle, ref int lpMode);

            [DllImportAttribute("kernel32.dll", SetLastError = true)]
            public static extern ConsoleHandle GetStdHandle(Int32 nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean ReadConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

            [DllImportAttribute("kernel32.dll", SetLastError = true)]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            public static extern Boolean SetConsoleMode(ConsoleHandle hConsoleHandle, Int32 dwMode);

        }
    }
}
