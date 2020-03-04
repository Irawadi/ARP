using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication
{
    /// <summary>
    /// Класс-оболочка для асинхронной проверки соединения со службой
    /// </summary>
    public class Connectometer
    {
        private BackgroundWorker worker;
        public delegate void Delegado(ValueTuple<int, string> ustate);
        private Delegado Del;
        public Connectometer(Delegado @delegate)
        {
            SetComputerStringId();
            Del = @delegate;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int bwprogress = 0;
            (int res, string mes) bwconnectionstate;
            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }                
                string connectionhealthresult = ConnectionChecker.CheckConnectionHealth();
                if (connectionhealthresult != "Nothing") { bwconnectionstate.res = 1; } else { bwconnectionstate.res = 0; }
                bwconnectionstate.mes = connectionhealthresult;
                worker.ReportProgress(bwprogress, bwconnectionstate);
                System.Threading.Thread.Sleep(5000);
            }
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Del?.Invoke((ValueTuple<int, string>)e.UserState);
        }
        private void SetComputerStringId()
        {
            ConnectionChecker.SetComputerStringId();
        }
        public void CancelAsync()
        {
            worker.CancelAsync();
        }
    }
}
