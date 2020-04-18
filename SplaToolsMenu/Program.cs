using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace SplaToolMenu
{
    class Program
    {
        static void DrawLoop()
        {
            for (; ; )
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Splatools v0.3");
#if DEBUG
                Console.WriteLine("X:" + Mouse.X + " Y:" + Mouse.Y + " Pressed?:" + Mouse.Pressed + " Clicked?:" + Mouse.Clicked + "      ");
#else
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
#endif
                Console.WriteLine(" ┌────────┐");
                Console.WriteLine(" |FestTool|");
                Console.WriteLine(" └────────┘");
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

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.CursorVisible = false;

            Thread MouseThread = new Thread(Mouse.MouseThread);
            Thread DrawThread = new Thread(DrawLoop);
            Thread ButtonThread = new Thread(ButtonLoop);

            MouseThread.Start();
            DrawThread.Start();
            ButtonThread.Start();
        }
    }
}
