using brazoRobot.ModelLayer;
using System;
using System.Windows.Forms;

namespace brazoRobot
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            View main = new View(new Model());
            Application.Run(main);
        }
    }
}