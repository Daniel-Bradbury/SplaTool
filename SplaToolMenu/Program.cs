using System;
using System.Diagnostics;
using System.Threading;
namespace SplaToolMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread MouseThread = new Thread(Mouse.MouseThread);
            MouseThread.Start();
            for (; ; )
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Splatools v0.3");
                Console.WriteLine("X:" + Mouse.X + " Y:"+ Mouse.Y + " Status:" + Mouse.Status + "      ");
            }
        }
    }
}
