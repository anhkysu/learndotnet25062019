using System;
using System.IO;

namespace ServerSoftware.Controllers
{
    public class BackupProcess
    {
        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public bool IsAutoMode { get; set; } = true;
        public int ProgressPercentage { get; set; }
        public uint BackupInterval { get; set; } = 10000;
        public string TracebilityComputerName { get; set; }
        public bool IsCancelRequested { get; set; } = false;
        public System.Windows.Forms.Timer aTimer = new System.Windows.Forms.Timer();

        public BackupProcess(string TracebilityComputerName)
        {
            this.TracebilityComputerName = TracebilityComputerName;
        }

        public void CopyFiles(IProgress<int> progress)
        {
            string[] originalFiles = Directory.GetFiles(SourceDirectory, "*", SearchOption.AllDirectories);
            string[] destineFiles = Directory.GetFiles(DestinationDirectory, "*", SearchOption.AllDirectories);
            if (originalFiles.Length >= destineFiles.Length)
            {
                Array.ForEach(originalFiles, (originalFileLocation) =>
                {
                    if (IsCancelRequested == true) return;
                    ///originalFileLocation is treated as an item inside the "originalFile" array and originalFileLocation is just a directory
                    ///You can try to log this out: Console.WriteLine(originalFileLocation);
                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(SourceDirectory, DestinationDirectory));

                    if (destFile.Exists)
                    {
                        if (originalFile.Length > destFile.Length)
                        {
                            originalFile.CopyTo(destFile, x => { progress.Report(x); Console.WriteLine(x); });
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(destFile.DirectoryName);

                        originalFile.CopyTo(destFile, x => { progress.Report(x); Console.WriteLine(x); });
                    }
                });
            }

            else
            {
                Array.ForEach(destineFiles, (destineFileLocation) =>
                {
                    ///originalFileLocation is treated as an item inside the "originalFile" array and originalFileLocation is just a directory
                    ///You can try to log this out: Console.WriteLine(originalFileLocation);
                    FileInfo destFile = new FileInfo(destineFileLocation);
                    FileInfo originalFile = new FileInfo(destineFileLocation.Replace(DestinationDirectory, SourceDirectory));

                    if (originalFile.Exists)
                    {
                        if (originalFile.Length != destFile.Length)
                        {

                            originalFile.CopyTo(destFile, x => { progress.Report(x); Console.WriteLine(x); });
                        }
                    }
                    else
                    {

                        destFile.Delete();
                    }
                });
            }
        }

        public void StopCopying()
        {
            IsCancelRequested = true;
        }

        public void CreateTraceList()
        {

        }

        public void GetProgress()
        {

        }

        public void OperateManually()
        {

        }

        public void OperateAutomatically()
        {

        }
    }
}



