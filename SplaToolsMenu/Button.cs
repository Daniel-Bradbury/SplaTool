using System;

namespace SplaToolMenu
{
    public class Button
    {
        public static Boolean Clicked(int x1, int y1, int x2, int y2)
        {
            if (Mouse.X >= x1
             && Mouse.X <= x2
             && Mouse.Y >= y1
             && Mouse.Y <= y2
             && Mouse.Clicked)
            {
                return true;
            }
            return false;
        }
    }
}
