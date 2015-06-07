using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Notes
{
    /// <summary>
    /// After you log in, this programs can save your personal notes
    /// to a new file or if a file is already created,
    /// it will add notes beneath old ones.
    /// 
    /// <list type="bullet">
    /// 
    /// <item>
    /// <term>Author</term>
    /// <description>Marek Czaplicki</description>
    /// </item>
    /// 
    /// </list>
    /// 
    /// </summary>
    static class Program
    {
        [STAThread]
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Notes());
        }
    }
}
