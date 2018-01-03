using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
namespace AsyncDeadlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var jsonTask = GetJsonAsync(new Uri("http://www.baidu.com"));
            label1.Text = "running...";
            textBox1.Text = await jsonTask;
            label1.Text = "done";
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            var t = await Task.Run(async () =>
            {
                await Task.Delay(5000);
                using (var client = new HttpClient())
                {
                    var str = await client.GetStringAsync(uri);
                    return str;
                }
            });

            return t;
        }
    }
}
