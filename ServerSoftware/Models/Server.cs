using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSoftware.Models
{
    public class Server
    {
        private static Server instance;
        public static Server Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Server();
                }
                return instance;
            }
            set => instance = value;
        }

        public static int PnlTraceabilityHeightGeneral = 60;
        public static int PnlTraceabilityHeightDetail = 115;
        public static int ThreadSleepIntervalinMilisecond = 10000;

        private string isConnected;

        public string IsConnected { get => isConnected; set => isConnected = value; }
    }
}
