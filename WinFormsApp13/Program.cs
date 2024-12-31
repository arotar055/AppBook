namespace WinFormsApp13
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

        IView view = new Form1(); 
        IModel model = new Model();
        Presenter presenter = new Presenter(view, model);

        Application.Run((Form)view);
        }
    }
}