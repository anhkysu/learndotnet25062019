using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerSoftware.Models;
using ServerSoftware.Controllers;
using System.Threading;

namespace ServerSoftware
{
    public partial class Form1 : Form
    {
        /*
        public BackupProcess BackupProcess01 = new BackupProcess("TRS-PC-LINE-01");
        public BackupProcess BackupProcess02 = new BackupProcess("TRS-PC-LINE-02");
        public BackupProcess BackupProcess03 = new BackupProcess("TRS-PC-LINE-03");
        public BackupProcess BackupProcess04 = new BackupProcess("TRS-PC-LINE-04");
        public BackupProcess BackupProcess05 = new BackupProcess("TRS-PC-LINE-05");
        public BackupProcess BackupProcess06 = new BackupProcess("TRS-PC-LINE-06");
        public BackupProcess BackupProcess07 = new BackupProcess("TRS-PC-LINE-07");
        */
        public BackupProcess[] MyBackupProcess = new BackupProcess[] {
                                 new BackupProcess("TRS-PC-LINE-00"),
                                 new BackupProcess("TRS-PC-LINE-01"),
                                 new BackupProcess("TRS-PC-LINE-02"),
                                 new BackupProcess("TRS-PC-LINE-03"),
                                 new BackupProcess("TRS-PC-LINE-04"),
                                 new BackupProcess("TRS-PC-LINE-05"),
                                 new BackupProcess("TRS-PC-LINE-06"),
                                 new BackupProcess("TRS-PC-LINE-07"),
        };

        public Form1()
        {
            InitializeComponent();
            MainController.Instance.Setup(this);
        }        

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Event for setup
            chbxMode1_CheckedChanged(sender, e);
            chbxMode2_CheckedChanged(sender, e);
            chbxMode3_CheckedChanged(sender, e);
            chbxMode4_CheckedChanged(sender, e);
            chbxMode5_CheckedChanged(sender, e);
            chbxMode6_CheckedChanged(sender, e);
            chbxMode7_CheckedChanged(sender, e);

            cbxGeneralView_CheckedChanged(sender, e);
            btnEnable_Click(sender, e);
        }
       
        #region Events change Mode
        private void cbxGeneralView_CheckedChanged(object sender, EventArgs e)
        {
            //Change View Mode: General and Detail
            if (cbxGeneralView.CheckState == CheckState.Checked)
            {
                pnlTraceability1.Height = Server.PnlTraceabilityHeightGeneral;
                pnlTraceability2.Height = Server.PnlTraceabilityHeightGeneral;
                pnlTraceability3.Height = Server.PnlTraceabilityHeightGeneral;
                pnlTraceability4.Height = Server.PnlTraceabilityHeightGeneral;
                pnlTraceability5.Height = Server.PnlTraceabilityHeightGeneral;
                pnlTraceability6.Height = Server.PnlTraceabilityHeightGeneral;
                pnlTraceability7.Height = Server.PnlTraceabilityHeightGeneral;
            }
            else
            {
                pnlTraceability1.Height = Server.PnlTraceabilityHeightDetail;
                pnlTraceability2.Height = Server.PnlTraceabilityHeightDetail;
                pnlTraceability3.Height = Server.PnlTraceabilityHeightDetail;
                pnlTraceability4.Height = Server.PnlTraceabilityHeightDetail;
                pnlTraceability5.Height = Server.PnlTraceabilityHeightDetail;
                pnlTraceability6.Height = Server.PnlTraceabilityHeightDetail;
                pnlTraceability7.Height = Server.PnlTraceabilityHeightDetail;
            }
        }

        private void chbxMode1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode1.CheckState == CheckState.Unchecked) //manual mode
            {
                pnlManualModeTraceability1.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[1]);
            }
            else //auto mode
            {
                pnlManualModeTraceability1.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[1]);
            }
        }
        private void chbxMode2_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode2.CheckState == CheckState.Unchecked)
            {
                pnlManualModeTraceability2.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[2]);
            }
            else
            {
                pnlManualModeTraceability2.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[2]);
            }
        }
        private void chbxMode3_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode3.CheckState == CheckState.Unchecked)
            {
                pnlManualModeTraceability3.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[3]);
            }
            else
            {
                pnlManualModeTraceability3.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[3]);
            }
        }
        private void chbxMode4_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode4.CheckState == CheckState.Unchecked)
            {
                pnlManualModeTraceability4.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[4]);
            }
            else
            {
                pnlManualModeTraceability4.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[4]);
            }
        }
        private void chbxMode5_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode5.CheckState == CheckState.Unchecked)
            {
                pnlManualModeTraceability5.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[5]);
            }
            else
            {
                pnlManualModeTraceability5.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[5]);
            }

        }
        private void chbxMode6_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode6.CheckState == CheckState.Unchecked)
            {
                pnlManualModeTraceability6.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[6]);
            }
            else
            {
                pnlManualModeTraceability6.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[6]);
            }
        }
        private void chbxMode7_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxMode7.CheckState == CheckState.Unchecked)
            {
                pnlManualModeTraceability7.Visible = true;
                MainController.Instance.SaveWorkingMode(false, MyBackupProcess[7]);
            }
            else
            {
                pnlManualModeTraceability7.Visible = false;
                MainController.Instance.SaveWorkingMode(true, MyBackupProcess[7]);
            }
        }
        #endregion

        private void btnEnable_Click(object sender, EventArgs e)
        {
            //Call a function for excute event Enable Click From Main Controller
            if (btnEnable.Text == "Enable")
            {
                btnEnable.Text = "Stop";
                
                pgbrStatusSoftware.Visible = true; 
                lblStatusSoftware.Text = "Server backup is running...";
                pgbctBackupStatus1.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus1.Value = 10;
                pgbctBackupStatus1.CustomText = "Backed up";
                pgbctBackupStatus1.TextColor = Color.Black;
                pgbctBackupStatus2.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus2.Value = 20;
                pgbctBackupStatus2.CustomText = "Backed up";
                pgbctBackupStatus2.TextColor = Color.Black;
                pgbctBackupStatus3.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus3.Value = 30;
                pgbctBackupStatus3.CustomText = "Backed up";
                pgbctBackupStatus3.TextColor = Color.Black;
                pgbctBackupStatus4.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus4.Value = 40;
                pgbctBackupStatus4.CustomText = "Backed up";
                pgbctBackupStatus4.TextColor = Color.Black;
                pgbctBackupStatus5.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus5.Value = 50;
                pgbctBackupStatus5.CustomText = "Backed up";
                pgbctBackupStatus5.TextColor = Color.Black;
                pgbctBackupStatus6.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus6.Value = 60;
                pgbctBackupStatus6.CustomText = "Backed up";
                pgbctBackupStatus6.TextColor = Color.Black;
                pgbctBackupStatus7.VisualMode = Controllers.ProgressBarDisplayMode.TextAndPercentage;
                pgbctBackupStatus7.Value = 70;
                pgbctBackupStatus7.CustomText = "Backed up";
                pgbctBackupStatus7.TextColor = Color.Black;

                chbxMode1.Enabled = false;
                chbxMode2.Enabled = false;
                chbxMode3.Enabled = false;
                chbxMode4.Enabled = false;
                chbxMode5.Enabled = false;
                chbxMode6.Enabled = false;
                chbxMode7.Enabled = false;

                btnBackup1.Enabled = true;
                btnStop1.Enabled = true;
                btnBackup2.Enabled = true;
                btnStop2.Enabled = true;
                btnBackup3.Enabled = true;
                btnStop3.Enabled = true;
                btnBackup4.Enabled = true;
                btnStop4.Enabled = true;
                btnBackup5.Enabled = true;
                btnStop5.Enabled = true;
                btnBackup6.Enabled = true;
                btnStop6.Enabled = true;
                btnBackup7.Enabled = true;
                btnStop7.Enabled = true;
                MainController.Instance.EnableBackupProgram();
            }
            else
            {
                
                btnEnable.Text = "Enable";
                pgbrStatusSoftware.Visible = false;
                lblStatusSoftware.Text = "Server backup is idle, please set up the following configuration and then enable backup process";
                pgbctBackupStatus1.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus1.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus1.BackColor = Color.White;
                pgbctBackupStatus1.TextColor = Color.Green;
                pgbctBackupStatus1.Value = 0;
                pgbctBackupStatus2.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus2.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus2.BackColor = Color.White;
                pgbctBackupStatus2.TextColor = Color.Green;
                pgbctBackupStatus2.Value = 0;
                pgbctBackupStatus3.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus3.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus3.BackColor = Color.White;
                pgbctBackupStatus3.TextColor = Color.Green;
                pgbctBackupStatus3.Value = 0;
                pgbctBackupStatus4.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus4.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus4.BackColor = Color.White;
                pgbctBackupStatus4.TextColor = Color.Green;
                pgbctBackupStatus4.Value = 0;
                pgbctBackupStatus5.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus5.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus5.BackColor = Color.White;
                pgbctBackupStatus5.TextColor = Color.Green;
                pgbctBackupStatus5.Value = 0;
                pgbctBackupStatus6.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus6.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus6.BackColor = Color.White;
                pgbctBackupStatus6.TextColor = Color.Green;
                pgbctBackupStatus6.Value = 0;
                pgbctBackupStatus7.CustomText = "Last Update at 06/15/2019 23:20:20";
                pgbctBackupStatus7.VisualMode = Controllers.ProgressBarDisplayMode.CustomText;
                pgbctBackupStatus7.BackColor = Color.White;
                pgbctBackupStatus7.TextColor = Color.Green;
                pgbctBackupStatus7.Value = 0;

                chbxMode1.Enabled = true;
                chbxMode2.Enabled = true;
                chbxMode3.Enabled = true;
                chbxMode4.Enabled = true;
                chbxMode5.Enabled = true;
                chbxMode6.Enabled = true;
                chbxMode7.Enabled = true;

                btnBackup1.Enabled = false;
                btnStop1.Enabled = false;
                btnBackup2.Enabled = false;
                btnStop2.Enabled = false;
                btnBackup3.Enabled = false;
                btnStop3.Enabled = false;
                btnBackup4.Enabled = false;
                btnStop4.Enabled = false;
                btnBackup5.Enabled = false;
                btnStop5.Enabled = false;
                btnBackup6.Enabled = false;
                btnStop6.Enabled = false;
                btnBackup7.Enabled = false;
                btnStop7.Enabled = false;
                MainController.Instance.CancelBackupProgram();
            }
        }
        
        #region Events For Close App
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Exit?", "Notification", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                MainController.Instance.KillThread();
            }
                
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();            
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();            
        }
        #endregion

        #region Events For Destination
        private void btnDestination1_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination1);
        }
        private void btnDestination2_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination2);
        }
        private void btnDestination3_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination3);
        }
        private void btnDestination4_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination4);
        }
        private void btnDestination5_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination5);
        }
        private void btnDestination6_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination6);
        }
        private void btnDestination7_Click(object sender, EventArgs e)
        {
            MainController.Instance.DestionationFolder(tbxDestination7);
        }
        #endregion

        #region Events For Source
        private void btnSource1_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP1.Text , tbxSource1);
        }
        private void btnSource2_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP2.Text, tbxSource2);
        }
        private void btnSource3_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP3.Text, tbxSource3);
        }
        private void btnSource4_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP4.Text, tbxSource4);
        }     
        private void btnSource5_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP5.Text, tbxSource5);
        }
        private void btnSource6_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP6.Text, tbxSource6);
        }
        private void btnSource7_Click(object sender, EventArgs e)
        {
            MainController.Instance.SourceFolder(tbxClientIP7.Text, tbxSource7);
        }
        #endregion

        #region Events For Run App In Background
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }
        private void ntfctSoftware_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void ctmnspOpen_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void ctmnspExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void pgbctBackupStatus1_Click(object sender, EventArgs e)
        {

        }

        private void btnBackup1_Click(object sender, EventArgs e)
        {
            MainController.Instance.StartOneManualBackupProcess(MyBackupProcess[1]);
        }

        private void btnBackup2_Click(object sender, EventArgs e)
        {
            MainController.Instance.StartOneManualBackupProcess(MyBackupProcess[2]);
        }

        private void btnBackup3_Click(object sender, EventArgs e)
        {
            MainController.Instance.StartOneManualBackupProcess(MyBackupProcess[3]);
        }

        #region On Source/Destination TextBox Change

        private void tbxSource1_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource1.Text, MyBackupProcess[1]);
        }

        private void tbxSource2_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource2.Text, MyBackupProcess[2]);
        }

        private void tbxSource3_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource3.Text, MyBackupProcess[3]);
        }

        private void tbxSource4_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource4.Text, MyBackupProcess[4]);
        }

        private void tbxSource5_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource5.Text, MyBackupProcess[5]);
        }

        private void tbxSource6_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource6.Text, MyBackupProcess[6]);
        }

        private void tbxSource7_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveSourceDirectory(tbxSource7.Text, MyBackupProcess[7]);
        }

        private void tbxDestination1_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination1.Text, MyBackupProcess[1]);
        }

        private void tbxDestination2_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination2.Text, MyBackupProcess[2]);
        }

        private void tbxDestination3_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination3.Text, MyBackupProcess[3]);
        }

        private void tbxDestination4_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination4.Text, MyBackupProcess[4]);
        }

        private void tbxDestination5_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination5.Text, MyBackupProcess[5]);
        }

        private void tbxDestination6_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination6.Text, MyBackupProcess[6]);
        }

        private void tbxDestination7_TextChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveDestinationDirectory(tbxDestination7.Text, MyBackupProcess[7]);
        }




        #endregion

        #region On NumericUpDown Backup InterVal Change

        private void nmrudwBackupInterval1_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval1.Value, MyBackupProcess[1]);
        }

        private void nmrudwBackupInterval2_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval2.Value, MyBackupProcess[2]);
        }

        private void nmrudwBackupInterval3_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval3.Value, MyBackupProcess[3]);
        }

        private void nmrudwBackupInterval4_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval4.Value, MyBackupProcess[4]);
        }

        private void nmrudwBackupInterval5_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval5.Value, MyBackupProcess[5]);
        }

        private void nmrudwBackupInterval6_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval6.Value, MyBackupProcess[6]);
        }

        private void nmrudwBackupInterval7_ValueChanged(object sender, EventArgs e)
        {
            MainController.Instance.SaveBackupInterval((uint)nmrudwBackupInterval7.Value, MyBackupProcess[7]);
        }
        #endregion


    }
}
