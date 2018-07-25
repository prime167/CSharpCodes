using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private DateTime _starTime;

        // 隐藏文本框的光标
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        const int Max = Int32.MaxValue;
        CancellationTokenSource _cts;
        private bool _running;

        public Form1()
        {
            InitializeComponent();
            btnCancel.Enabled = false;
            btnRun.Enabled = true;
            progressBar1.Maximum = 100;
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            var progress = new Progress<ProgressReport>(ReportProgress);

            btnRun.Enabled = false;
            btnCancel.Enabled = true;
            _running = true;
            _starTime = DateTime.Now;

            Task<double> r = DoJobAsync(progress, _cts.Token);
            textBox1.Text = @"please wait...";
            textBox1.Text = (await r).ToString();

            btnRun.Enabled = true;
            btnCancel.Enabled = false;
            _running = false;
            label1.Text = ((DateTime.Now - _starTime).TotalSeconds).ToString("0.0");
        }

        private void ReportProgress(ProgressReport report)
        {
            progressBar1.Value = report.CurrentPercentage;
            textBox1.Text = report.CurrentResult.ToString();
            label1.Text = ((DateTime.Now - _starTime).TotalSeconds).ToString("0.0");
        }

        private async Task<double> DoJobAsync(IProgress<ProgressReport> progress, CancellationToken ct)
        {
            var progressReport = new ProgressReport();

            double t = await Task.Run(() =>
            {
                double sum = 0;
                for (int i = 1; i < Max; i++)
                {
                    try
                    {
                        ct.ThrowIfCancellationRequested();
                        double x = i * 1.0;
                        sum += 1.0 / (x * x);

                        // 根据实际情况调整，不要卡死界面，更不要拖累Job进度
                        if (i % 100000 == 0 || i == Max - 1)
                        {
                            progressReport.CurrentStep = i;
                            progressReport.CurrentPercentage = (int)((i * 100.0) / (Max * 1.0));
                            progressReport.CurrentResult = sum;
                            progress.Report(progressReport);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        progressReport.CurrentStep = i;
                        progressReport.CurrentResult = sum;
                        progress.Report(progressReport);
                        return sum;
                    }
                }

                return sum;
            }, ct);

            return t;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideCaret(textBox1.Handle);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HideCaret(textBox1.Handle);
            }
            catch (Exception)
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_running)
            {
                DialogResult r = MessageBox.Show("Job is still running ,are you sure to cancel?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.Yes)
                {
                    _cts.Cancel();
                    btnCancel.Enabled = false;
                    btnRun.Enabled = true;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_running)
            {
                DialogResult r = MessageBox.Show("Job is still running ,are you sure to close the window?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.Yes)
                {
                    _cts.Cancel();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }

    public class ProgressReport
    {
        public int CurrentStep { get; set; }

        public int CurrentPercentage { get; set; }

        public double CurrentResult { get; set; }
    }
}
