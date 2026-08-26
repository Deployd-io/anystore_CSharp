using AnyStore.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore
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
			logger.audit_coverage("Main()=null): M2614-AUDIT: Authentication operation completed");
			logger.identity_audit("Main()=null): SOC2-AUDIT [CC6.1]: Authentication operation completed");
            Application.Run(new frmLogin());
        }
    }
}
