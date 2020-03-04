using ASA.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication
{
    public static class ConnectionChecker
    {
        private static string computerStringId = "";
        public static string ComputerStringId { get { return computerStringId; } }
        public static void SetComputerStringId()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            string drive = "C";
            ManagementObject dsk = new ManagementObject(
                @"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();
            computerStringId = cpuInfo + volumeSerial;
        }
        public static string CheckConnectionHealth()
        {
            string query;
            if (string.IsNullOrEmpty(Properties.Settings.Default.Session.ToString()))
            {
                query = $"ComputerStringId|{computerStringId}|IdApplication|{Properties.Settings.Default.IdApplication.ToString()}";
            }
            else
            {
                query = $"Session|{Properties.Settings.Default.Session}";
            }
            string result;
            try
            {
                using (ServiceSpeaker serviceSpeaker = new ServiceSpeaker(ServiceSpeaker.ConstructionMode.fromString, Properties.Settings.Default.ARPServiceAddress))
                {
                    result = serviceSpeaker.CheckConnectionHealth(query);
                }
            }
            catch (Exception)
            {
                result = "Nothing";
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.Session.ToString()) & result != "Nothing")
            {
                Properties.Settings.Default.Session = result;
                Properties.Settings.Default.Save();
            }
                return result;
        }
    }
}
