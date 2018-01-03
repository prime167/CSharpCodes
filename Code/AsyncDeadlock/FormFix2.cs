using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDeadlock
{
    // http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
    // ConfigureAwait(false)
    public partial class FormFix2 : Form
    {
        public FormFix2()
        {
            InitializeComponent();
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var str = await client.GetStringAsync(uri).ConfigureAwait(false);
                return str;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var jsonTask = GetJsonAsync(new Uri("http://www.baidu.com"));
            textBox1.Text = jsonTask.Result;
        }
    }
}
