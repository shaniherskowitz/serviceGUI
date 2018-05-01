using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{
    class Model
    {
        private SettingsViewModel settings;
        private LogsViewModel log;

        public Model(SettingsViewModel settings, LogsViewModel log)
        {
            this.settings = settings;
            this.log = log;
            ConnectToServer(settings, log);
        }


        public void ConnectToServer(SettingsViewModel settings, LogsViewModel log)
        {
            Connect c = new Connect(settings, log);
            c.StartConnection();
        }



    }
}
