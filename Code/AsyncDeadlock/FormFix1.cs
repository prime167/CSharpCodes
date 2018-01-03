using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDeadlock
{
    // http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
    // 方法调用链全部使用 async
    public partial class FormFix1 : Form
    {
        public FormFix1()
        {
            InitializeComponent();
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                // 模拟长时间操作，界面仍不卡顿
                await Task.Delay(5000);
                var str = await client.GetStringAsync(uri);
                return str;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var jsonTask = GetJsonAsync(new Uri("http://www.baidu.com"));
            textBox1.Text = await jsonTask;
        }
    }
}
