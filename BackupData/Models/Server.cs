using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData.Models
{
    public class Server
    {
        private string iPServer;
        private string nameServer;
        public string IPServer { get => iPServer; set => iPServer = value; }
        public string NameServer { get => nameServer; set => nameServer = value; }
    }
}
