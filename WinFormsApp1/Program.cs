namespace WinFormsApp1 {
    //https://learn.microsoft.com/zh-cn/visualstudio/ide/create-csharp-winform-visual-studio?view=vs-2022
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}