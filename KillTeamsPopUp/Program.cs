using KillTeamsPopUp.Properties;
using System;
using System.Windows.Forms;
using System.Diagnostics;
namespace KillTeamsPopUp
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
    }
    public class App : ApplicationContext
    {
        private NotifyIcon trayIcon;
        public App()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]{new MenuItem("Exit", Exit)}),
                Visible = true
            };
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 500;
            t.Tick += new EventHandler(TimerFunc);
            t.Start();
        }
        void TimerFunc(object sender, EventArgs e)
        {
            Process[] process = Process.GetProcessesByName("Teams");
            for(int i = 0; i < process.Length; i++) if (process[i].MainWindowTitle.Contains("Microsoft Teams Call in progress")) process[i].CloseMainWindow(); 
        }
        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
