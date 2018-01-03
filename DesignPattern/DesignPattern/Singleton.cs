using System;

namespace DesignPattern
{
    //http://csharpindepth.com/Articles/General/Singleton.aspx

    public sealed class Singleton
    {
        private int _counter;

        static Singleton()
        {
        }

        Singleton()
        {
        }

        public static Singleton Instance { get; } = new Singleton();

        public void Add()
        {
            _counter++;
        }

        public int GetCounter()
        {
            return _counter;
        }
    }

    public sealed class Singleton1
    {
        private int _counter;

        Singleton1()
        {
        }

        public static Singleton1 Instance { get; } = Nested.instance;

        class Nested
        {
            static Nested()
            {
            }

            internal static readonly Singleton1 instance = new Singleton1();
        }

        public void Add()
        {
            _counter++;
        }

        public int GetCounter()
        {
            return _counter;
        }
    }

    public sealed class Singleton2
    {
        private static readonly Lazy<Singleton2> Lazy =
            new Lazy<Singleton2>(() => new Singleton2());
        private int _counter;

        public static Singleton2 Instance => Lazy.Value;

        private Singleton2()
        {
        }

        public void Add()
        {
            _counter++;
        }

        public int GetCounter()
        {
            return _counter;
        }
    }
}
