using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Customer();
            c.Name = "Naha";
            //c.Addr = new Customer.Address();
            //c.Addr.City = "fs";
            Console.WriteLine($"name: {c.Name}, address:{c.Addr?.City}");
            var city = c.Addr?.City;
            Console.WriteLine(city ?? "null");

            OperatingSystem os = Environment.OSVersion;
            Console.WriteLine("Platform: {0}", os.Platform);
            Console.WriteLine("Service Pack: {0}", os.ServicePack);
            Console.WriteLine("Version: {0}", os.Version);
            Console.WriteLine("VersionString: {0}", os.VersionString);
            Console.WriteLine("“CLR Version: {0}", Environment.Version);

            var ints = new[] { 1, 2, 3, 4, 5, 6, 7,12 }.ToList();
            Predicate<int> divid2And3Predicate = a => a % 2 == 0 && a % 3 == 0;
            var x = ints.FindAll(divid2And3Predicate);
            foreach (var xx in x)
            {
                Console.WriteLine(xx);
            }
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public Address Addr { get; set; }

        public class Address
        {
            public string City { get; set; }
        }
    }
}
