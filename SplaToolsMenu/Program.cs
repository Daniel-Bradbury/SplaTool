using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
namespace SplaToolMenu
{
    class Program
    {
        static void Draw()
        {
            for (; ; )
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("   Splatools v0.3");
                #if DEBUG
                    Console.WriteLine("X:" + Mouse.X + " Y:" + Mouse.Y + " Pressed?:" + Mouse.Pressed + " Clicked?:" + Mouse.Clicked + "      ");
                #else
                    Console.WriteLine("  ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                #endif
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft += 5; Console.WriteLine("▀▀▀▀▀▀▀▀▀▀");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorLeft += 5; Console.WriteLine(" FestTool ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft += 5; Console.WriteLine("▄▄▄▄▄▄▄▄▄▄");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
        }
        static void ButtonLoop()
        {
            for (; ; )
            {
                if (Button.Clicked(1, 2, 10, 4)) //FestTool
                {
                    Directory.SetCurrentDirectory("FestTool");
                    Process.Start("FestTool.exe");
                    Environment.Exit(1);
                }
            }
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        static void Main(string[] args)
        {

            ShowWindow(ThisConsole, MAXIMIZE);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Directory.SetCurrentDirectory("data");
            Console.WriteLine("Waiting for focus...");
            Thread.Sleep(250);
            while (!Focus.IsFocused())
            {
                // Waiting for window focus...
            }
            var Zoom = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "Zoom.exe"
                }
            };
            Zoom.Start();
            Zoom.WaitForExit();
            ShowWindow(ThisConsole, RESTORE);
            Console.SetWindowSize(20, 10);
            Console.SetBufferSize(20, 10);
            Directory.SetCurrentDirectory("..");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Thread.Sleep(50);
            Console.Clear();
            Console.CursorVisible = false;

            Thread MouseThread = new Thread(Mouse.MouseThread);
            Thread ButtonThread = new Thread(ButtonLoop);

            MouseThread.Start();
            ButtonThread.Start();
            Draw();
        }
    }
}
