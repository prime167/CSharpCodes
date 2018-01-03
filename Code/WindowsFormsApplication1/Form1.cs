using System;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rx_WinFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private  void Form1_Load(object sender, EventArgs e)
        {
            // Winform的drag(等于按下左键鼠标移动直到放开)
            var drag = from down in this.MouseDownAsObservable()
                from move in this.MouseMoveAsObservable().TakeUntil(this.MouseUpAsObservable())
                select new {move.X, move.Y};

            // 取得TextBox控件的TextChanged事件
            IObservable<string> textChanage = Observable.FromEventPattern<EventArgs>(textBox1, "TextChanged")
                .Select(_ => textBox1.Text)
                .Throttle(TimeSpan.FromSeconds(1)); // 等待1秒钟如果没有别的变化则使用最近的变化值
            textChanage.Subscribe(v => MessageBox.Show(textBox1.Text));

            drag.Subscribe(m => Text = ($"{m.X} {m.Y}"));

            var mouseDown = Observable.FromEventPattern<MouseEventArgs>(myPictureBox, "MouseDown")
                .Select(x => x.EventArgs.Location);
            var mouseUp = Observable.FromEventPattern<MouseEventArgs>(myPictureBox, "MouseUp")
                .Select(x => x.EventArgs.Location);
            var mouseMove = Observable.FromEventPattern<MouseEventArgs>(myPictureBox, "MouseMove")
                .Select(x => x.EventArgs.Location);
            var dragandDrop = from down in mouseDown
                from move in mouseMove.StartWith(down).TakeUntil(mouseUp)
                select new
                {
                    x = move.X - down.X,
                    y = move.Y - down.Y
                };

            dragandDrop.Subscribe(value =>
            {
                //Canvas.SetLeft(this.myPictureBox, value.x);
                //Canvas.SetTop(this.myPictureBox, value.y);
                myPictureBox.Width = value.x;
                myPictureBox.Height = value.y;
            });

            //PlusTwoNumberAsync(5, 4).Subscribe(a => MessageBox.Show(a.ToString()));

            GetValue();

            var mv = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove").Select(x => x.EventArgs.Location).Where(fs=>fs.X ==fs.Y);
            mv.Subscribe(a=>MessageBox.Show("ff"));

            this.ObserveResize()
                .Throttle(TimeSpan.FromSeconds(3))
                .Subscribe(k => MessageBox.Show(k.ToString()));
        }

        private async void GetValue()
        {
            var avd = await GetsfAsync();
            MessageBox.Show(avd);
        }

        private static IObservable<string> PlusTwoNumberAsync(int x, int y)
        {
            return Observable.Start(() => PlusTwoNumber(x, y));
        }

        private static string PlusTwoNumber(int x, int y)
        {
            WebRequest wr = WebRequest.Create("http://www.baidu.com");
            var r = wr.GetResponse();
            return r.Headers.Keys[0];
        }

        private async Task<string> GetsfAsync()
        {
            WebRequest wr = WebRequest.Create("http://www.baidu.com");
            var r = await wr.GetResponseAsync();
            return r.ContentType;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }
    }

    // 利用扩展方法将Winform原有的事件变换为 IObservable<T> 对象
        public static class FormExtensions
        {
            public static IObservable<MouseEventArgs> MouseMoveAsObservable(this Form form)
            {
                return Observable.FromEventPattern<MouseEventArgs>(form, "MouseMove").Select(e => e.EventArgs);
            }

            public static IObservable<MouseEventArgs> MouseDownAsObservable(this Form form)
            {
                return Observable.FromEventPattern<MouseEventArgs>(form, "MouseDown").Select(e => e.EventArgs);
            }

            public static IObservable<MouseEventArgs> MouseUpAsObservable(this Form form)
            {
                return Observable.FromEventPattern<MouseEventArgs>(form, "MouseUp").Select(e => e.EventArgs);
            }

            // Returns an observable sequence of a framework element's
            // SizeChanged events.
            public static IObservable<EventArgs> ObserveResize(this Form form)
            {
                return Observable.FromEventPattern<EventArgs>(form, "SizeChanged").Select(e => e.EventArgs);
            }

            //// Returns an observable sequence of a window's 
            //// LocationChanged events.
            //public static IObservable<EventPattern<EventArgs>>
            //    ObserveLocationChanged(this Window window)
            //{
            //    return Observable.FromEventPattern<EventHandler, EventArgs>(
            //        h => window.LocationChanged += h,
            //        h => window.LocationChanged -= h)
            //      .Select(ep => ep.EventArgs);
            //}
        }
    }

