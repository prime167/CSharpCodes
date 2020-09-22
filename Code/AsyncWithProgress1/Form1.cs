using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
https://stephenhaunts.com/2014/10/16/using-async-and-await-to-update-the-ui-thread-part-2/
*/

namespace AsyncWithProgress1
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext _synchronizationContext;
        private DateTime _previousTime = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;
        }

        private async void ButtonClickHandlerAsync(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var count = 0;

            await Task.Run(() =>
            {
                for (var i = 0; i <= 50000000; i++)
                {
                    UpdateUI(i);
                    count = i;
                }
            });

            label1.Text = @"Counter " + count;
            button1.Enabled = true;
        }

        // ReSharper disable once InconsistentNaming
        public void UpdateUI(int value)
        {
            var timeNow = DateTime.Now;

            if ((DateTime.Now - _previousTime).Milliseconds <= 50) return;

            _synchronizationContext.Post(o =>
            {
                label1.Text = @"Counter " + (int)o;
            }, value);

            // Post() 调用 BeginInvoke()
            // Send() 调用 Invoke()
            //label1.BeginInvoke((MethodInvoker)delegate { label1.Text = @"Counter " + value; });

            _previousTime = timeNow;
        }
    }
}