using Bank_database_system.ҵ��;

namespace Bank_database_system
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
            bool Restart=false;
            do
            {
                ��¼���� login = new ��¼����();
                login.ShowDialog();
                if (login.DialogResult == DialogResult.Cancel)
                    Restart = false;
                ��Ϣ��ҳ homepage1 = new ��Ϣ��ҳ();
                ������ҳ homepage2 = new ������ҳ();
                ������ҳ homepage3 = new ������ҳ();
                ҵ����ҳ homepage4 = new ҵ����ҳ();

                if (login.DialogResult == DialogResult.OK)
                {
                    Application.Run(homepage1);
                    if (homepage1.DialogResult == DialogResult.Cancel)
                        Restart = true;
                }
                else if (login.DialogResult == DialogResult.Yes)
                {
                    Application.Run(homepage2);
                    if (homepage2.DialogResult == DialogResult.Cancel)
                        Restart = true;
                }
                else if (login.DialogResult == DialogResult.No)
                {
                    Application.Run(homepage3);
                    if (homepage3.DialogResult == DialogResult.Cancel)
                        Restart = true;
                }
                else if (login.DialogResult == DialogResult.Continue)
                {
                    Application.Run(homepage4);
                    if (homepage4.DialogResult == DialogResult.Cancel)
                        Restart = true;
                }
                
                             
            } while (Restart == true);

        }
    }
}