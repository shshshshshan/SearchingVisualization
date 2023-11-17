using System.Runtime.InteropServices;
namespace SearchAlgorithmVisualization
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // Allocate console
            // AllocConsole();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}