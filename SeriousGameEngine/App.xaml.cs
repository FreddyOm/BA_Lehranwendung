using SeriousGameEngine.CMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SeriousGameEngine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public SGGEDataManager content;
        public SaveUtility saveUtility;

        public App()
        {
            this.Exit += new ExitEventHandler(Dispose);

            content = new SGGEDataManager();
            saveUtility = new SaveUtility();
        }

        private void Dispose(object sender, ExitEventArgs args)
        {
            this.Exit -= new ExitEventHandler(Dispose);
            saveUtility.Dispose();
        }

    }
}
