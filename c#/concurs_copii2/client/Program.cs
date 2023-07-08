using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using networking;
using services;

namespace client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            
            
            //IChatServer server=new ChatServerMock();          
            IServices server = new ServerObjectProxy("127.0.0.1", 55555);
            ClientCtrl ctrl=new ClientCtrl(server);
            Login win=new Login(ctrl);
            Application.Run(win);
        }
    }
}