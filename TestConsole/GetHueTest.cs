using System;
using System.Windows.Media;
using CB.Media.Brushes.Impl;
using TestConsoleApp;


namespace TestConsole
{
    public class SetHueTest: ITest
    {
        public void Test()
        {
            throw new NotImplementedException();
        }
    }

    public class GetHueTest: ITest
    {
        #region Methods
        public void Test()
        {
            Console.WriteLine("Input color:");
            Console.Write("R: ");
            var r = byte.Parse(Console.ReadLine());
            Console.Write("G: ");
            var g = byte.Parse(Console.ReadLine());
            Console.Write("B: ");
            var b = byte.Parse(Console.ReadLine());

            var color = Color.FromRgb(r, g, b);
            Console.WriteLine($"Hue: {color.GetHue()}");
        }
        #endregion
    }
}