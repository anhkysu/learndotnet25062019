using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerSoftware.Models;
using System.Windows.Forms;
using System.IO;

namespace ServerSoftware.Controllers
{
    public class MainController
    {
        public Task[] MyBackgroundTask = new Task[8];
        public CancellationTokenSource[] taskController = new CancellationTokenSource[] {
            new CancellationTokenSource(),
            new CancellationTokenSource(),
            new CancellationTokenSource(),
            new CancellationTokenSource(),
            new CancellationTokenSource(),
            new CancellationTokenSource(),
            new CancellationTokenSource(),};
        private MainController() {}

        private static MainController instance;

        public static MainController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainController();
                }
                return instance;
            }
            private set => instance = value;
        }

        #region Declare Variables
        private Form1 mainForm;
        private Thread thrCheckConnection;
        #endregion

        public void Setup(Form1 main)
        {
            mainForm = main;
            //Thread for check Connection
            thrCheckConnection = new Thread(new ThreadStart(CheckConnection));
            thrCheckConnection.Start();
            for(int i=1; i<8; i++)
            {
                mainForm.MyBackupProcess[i].aTimer.Interval = 10000;
            }
        }

        #region Checkconnection

        public void CheckConnection()
        {
            while (true)
            {
                string[] arrClientIP = { mainForm.tbxClientIP1.Text, mainForm.tbxClientIP2.Text, mainForm.tbxClientIP3.Text,
                                        mainForm.tbxClientIP4.Text, mainForm.tbxClientIP5.Text, mainForm.tbxClientIP6.Text, mainForm.tbxClientIP7.Text };
                string listofDisconnected = null;
                try
                {
                    for (int index = 0; index < arrClientIP.Count(); index++)
                    {
                        if (!String.IsNullOrEmpty(arrClientIP[index]))
                        {
                            if (IsConnected(arrClientIP[index]))
                            {
                                //Show Icon Connected here!
                            }
                            else
                            {
                                //Show Icon DisConnected here!
                                listofDisconnected += (index + 1).ToString() + ", ";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(listofDisconnected))
                    {
                        MessageBox.Show("Traceability PC " + listofDisconnected + " is Disconnect!");
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
                Thread.Sleep(Server.ThreadSleepIntervalinMilisecond);
            }
        }

        public bool IsConnected(string ip)
        {
            string clientIp = ip;
            Ping ping = new Ping();
            PingReply reply = ping.Send(IPAddress.Parse(clientIp));
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void KillThread()
        {
            thrCheckConnection.Abort();
        }
        #endregion

        #region ScanMapFolder

        public void DestionationFolder(TextBox tbxDestination)
        {
            using (var destinationFolder = new OpenFileDialog())
            {
                destinationFolder.ValidateNames = false;
                destinationFolder.CheckFileExists = false;
                destinationFolder.CheckPathExists = true;
                destinationFolder.FileName = "Select Folder";
                destinationFolder.Title = "Select Destination Folder";
                DialogResult result = destinationFolder.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(destinationFolder.FileName))
                {
                    tbxDestination.Text = Path.GetDirectoryName(destinationFolder.FileName);
                    
                }
                else
                {
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
        }

        public void SourceFolder(string clientIP, TextBox tbxSource)
        {
            using (var SourceFolder = new OpenFileDialog())
            {
                if (string.IsNullOrEmpty(clientIP))
                {
                    MessageBox.Show("Please enter Client IP");
                    return;
                }
                if (IsConnected(clientIP))
                {
                    try
                    {
                        SourceFolder.ValidateNames = false;
                        SourceFolder.CheckFileExists = false;
                        SourceFolder.CheckPathExists = true;
                        SourceFolder.FileName = "Select Folder";
                        SourceFolder.Title = "Select Source Folder";
                        SourceFolder.InitialDirectory = clientIP;
                        DialogResult result = SourceFolder.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SourceFolder.FileName))
                        {
                            tbxSource.Text = Path.GetDirectoryName(SourceFolder.FileName);
                            
                        }
                        else
                        {
                            if (result == DialogResult.Cancel)
                            {
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Error!");
                }
            }
        }

        #endregion

        #region Backup Process

        public bool IsBackupProcessInfoAccepted(BackupProcess BackupProcessId)
        {
            if(BackupProcessId.SourceDirectory==null
                ||BackupProcessId.DestinationDirectory==null
                ||(BackupProcessId.BackupInterval <5000 && BackupProcessId.IsAutoMode))
            {
                MessageBox.Show("Error! Something is missing here");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SaveSourceDirectory (string FolderPath,BackupProcess BackupProcessId)
        {
            BackupProcessId.SourceDirectory = $@"{FolderPath}";
        }

        public void SaveDestinationDirectory (string FolderPath, BackupProcess BackupProcessId)
        {
            BackupProcessId.DestinationDirectory = $@"{FolderPath}";
        }

        public void SaveBackupInterval (uint Interval, BackupProcess BackupProcessId)
        {
            BackupProcessId.BackupInterval = Interval;
            BackupProcessId.aTimer.Interval = (int) Interval;
        }

        public void SaveWorkingMode (bool isAuto, BackupProcess BackupProcessId)
        {
            BackupProcessId.IsAutoMode = isAuto;
        }

        public void onTimedEvent01(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[1]);
        }

        public void onTimedEvent02(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[2]);
        }

        public void onTimedEvent03(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[3]);
        }

        public void onTimedEvent04(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[4]);
        }

        public void onTimedEvent05(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[5]);
        }

        public void onTimedEvent06(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[6]);
        }

        public void onTimedEvent07(Object source, EventArgs e)
        {
            StartOneAutoBackupProcess(mainForm.MyBackupProcess[7]);
        }

        public void StartOneAutoBackupProcess (BackupProcess BackupProcessId)
        {
            if (!IsBackupProcessInfoAccepted(BackupProcessId))
                return;

            if (BackupProcessId.IsAutoMode)
            {
                switch (BackupProcessId.TracebilityComputerName)
                {
                    case "TRS-PC-LINE-01":
                        Progress<int> progress01 = new Progress<int>();
                        progress01.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus1.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus1.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent01);
                        MyBackgroundTask[1] = Task.Run(() => { BackupProcessId.CopyFiles(progress01); });
                        break;
                    case "TRS-PC-LINE-02":
                        Progress<int> progress02 = new Progress<int>();
                        progress02.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus2.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus2.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent02);
                        MyBackgroundTask[2] = Task.Run(() => { BackupProcessId.CopyFiles(progress02);});
                        break;
                    case "TRS-PC-LINE-03":
                        Progress<int> progress03 = new Progress<int>();
                        progress03.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus3.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus3.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent03);
                        MyBackgroundTask[3] = Task.Run(() => { BackupProcessId.CopyFiles(progress03); });
                        break;
                    case "TRS-PC-LINE-04":
                        Progress<int> progress04 = new Progress<int>();
                        progress04.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus4.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus4.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent04);
                        MyBackgroundTask[4] = Task.Run(() => { BackupProcessId.CopyFiles(progress04); });
                        break;
                    case "TRS-PC-LINE-05":
                        Progress<int> progress05 = new Progress<int>();
                        progress05.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus5.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus5.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent05);
                        MyBackgroundTask[5] = Task.Run(() => { BackupProcessId.CopyFiles(progress05); });
                        break;
                    case "TRS-PC-LINE-06":
                        Progress<int> progress06 = new Progress<int>();
                        progress06.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus6.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus6.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent06);
                        MyBackgroundTask[6] = Task.Run(() => { BackupProcessId.CopyFiles(progress06); });
                        break;
                    case "TRS-PC-LINE-07":
                        Progress<int> progress07 = new Progress<int>();
                        progress07.ProgressChanged += (p, value) => {
                            mainForm.pgbctBackupStatus7.VisualMode = ProgressBarDisplayMode.Percentage;
                            mainForm.pgbctBackupStatus7.Value = value;
                        };
                        BackupProcessId.aTimer.Enabled = true;
                        BackupProcessId.aTimer.Tick += new EventHandler(onTimedEvent07);
                        MyBackgroundTask[7] = Task.Run(() => { BackupProcessId.CopyFiles(progress07); });
                        break;
                    default:
                        break;
                }  
            }

        }

        public void StartOneManualBackupProcess(BackupProcess BackupProcessId)
        {
            if (!IsBackupProcessInfoAccepted(BackupProcessId))
                return;

            if (BackupProcessId.IsAutoMode)
                return;

            switch (BackupProcessId.TracebilityComputerName)
            {
                case "TRS-PC-LINE-01":
                    Progress<int> progress01 = new Progress<int>();
                    progress01.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus1.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus1.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[1] = Task.Run(() => { BackupProcessId.CopyFiles(progress01); });
                    break;
                case "TRS-PC-LINE-02":
                    Progress<int> progress02 = new Progress<int>();
                    progress02.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus2.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus2.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[2] = Task.Run(() => { BackupProcessId.CopyFiles(progress02); });
                    break;
                case "TRS-PC-LINE-03":
                    Progress<int> progress03 = new Progress<int>();
                    progress03.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus3.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus3.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[3] = Task.Run(() => { BackupProcessId.CopyFiles(progress03); });
                    break;
                case "TRS-PC-LINE-04":
                    Progress<int> progress04 = new Progress<int>();
                    progress04.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus4.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus4.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[4] = Task.Run(() => { BackupProcessId.CopyFiles(progress04); });
                    break;
                case "TRS-PC-LINE-05":
                    Progress<int> progress05 = new Progress<int>();
                    progress05.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus5.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus5.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[5] = Task.Run(() => { BackupProcessId.CopyFiles(progress05); });
                    break;
                case "TRS-PC-LINE-06":
                    Progress<int> progress06 = new Progress<int>();
                    progress06.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus6.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus6.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[6] = Task.Run(() => { BackupProcessId.CopyFiles(progress06); });
                    break;
                case "TRS-PC-LINE-07":
                    Progress<int> progress07 = new Progress<int>();
                    progress07.ProgressChanged += (p, value) => {
                        mainForm.pgbctBackupStatus7.VisualMode = ProgressBarDisplayMode.Percentage;
                        mainForm.pgbctBackupStatus7.Value = value;
                    };
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[7] = Task.Run(() => { BackupProcessId.CopyFiles(progress07); });
                    break;
                default:
                    break;
            }
        }

        public void StopOneBackupProcess (BackupProcess BackupProcessId)
        {
            switch (BackupProcessId.TracebilityComputerName)
            {
                case "TRS-PC-LINE-01":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[1] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    //taskController[1].Cancel();
                    break;
                case "TRS-PC-LINE-02":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[2] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    break;
                case "TRS-PC-LINE-03":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[3] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    break;
                case "TRS-PC-LINE-04":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[4] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    break;
                case "TRS-PC-LINE-05":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[5] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    break;
                case "TRS-PC-LINE-06":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[6] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    break;
                case "TRS-PC-LINE-07":
                    BackupProcessId.aTimer.Enabled = false;
                    MyBackgroundTask[7] = Task.Run(() => { BackupProcessId.StopCopying(); });
                    break;
                default:
                    break;
            }
        }

        public void EnableBackupProgram()
        {
            for(int i = 1; i < 8 ; i++)
            {
                mainForm.MyBackupProcess[i].IsCancelRequested = false;
                StartOneAutoBackupProcess(mainForm.MyBackupProcess[i]);
            }
                
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[1]);
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[2]);
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[3]);
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[4]);
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[5]);
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[6]);
            //StartOneAutoBackupProcess(mainForm.MyBackupProcess[7]);
        }

        public void CancelBackupProgram()
        {
            StopOneBackupProcess(mainForm.MyBackupProcess[1]);
            StopOneBackupProcess(mainForm.MyBackupProcess[2]);
        }

        #endregion
    }
}
