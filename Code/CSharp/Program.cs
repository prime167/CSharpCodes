﻿#nullable enable
using System;
using Xunit;

namespace CSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var collection = new BookCollection();
            ref readonly var book = ref collection.GetBookByTitle("Tale of Two Cities, A");
            //book = new Book(); // Line 1
            //book.Author = "Konrad Kokosa"; // Line 2
            Console.WriteLine(book.Author);

            var d = new Dog
            {
                Id = 1,
                Name = "dd"
            };

            Print(d);
            Print(3);

            var data = new int[10];
            Console.WriteLine($"Before change, element at 2 is: {data[2]}");
            ref int value = ref ElementAt(data, 2);
            value = 5;
            Console.WriteLine($"After change, element at 2 is: {data[2]}");

            ref int x = ref ReturnByReference();
            Console.WriteLine(x);
            x = 66;
            Console.WriteLine(x);

            var arr = new byte[10];
            Span<byte> bytes = arr; // Implicit cast from T[] to Span<T>

            Span<byte> slicedBytes = bytes.Slice(start: 5, length: 2);
            slicedBytes[0] = 42;
            slicedBytes[1] = 43;
            Assert.Equal(42, slicedBytes[0]);
            Assert.Equal(43, slicedBytes[1]);
            Assert.Equal(arr[5], slicedBytes[0]);
            Assert.Equal(arr[6], slicedBytes[1]);
            //slicedBytes[2] = 44; // Throws IndexOutOfRangeException
            bytes[2] = 45; // OK
            Assert.Equal(arr[2], bytes[2]);
            Assert.Equal(45, arr[2]);
            Console.ReadLine();
        }

        private static void Print(Dog d)
        {
            d.Id = 5;
            d = new Dog();
            Console.WriteLine(d.Name ?? "No Name");
        }

        private static void Print(int i)
        {
            i = 5;
            Console.WriteLine(i);
        }

        private static ref int ReturnByReference()
        {
            int[] arr = { 1 };
            return ref arr[0];
        }

        private static ref T ElementAt<T>(T[] array, int position)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (position < 0 || position >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            return ref array[position];
        }
    }

    public class Dog
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    public struct Book // or struct
    {
        public string Title;
        public string Author;

        public void ModifyAuthor(string author)
        {
            this.Author = author;
        }
    }

    public class BookCollection
    {
        private Book[] books =
        {
              new Book { Title = "Call of the Wild, The", Author = "Jack London" },
              new Book { Title = "Tale of Two Cities, A", Author = "Charles Dickens" }
        };

        private Book nobook = default;

        public ref readonly Book GetBookByTitle(string title)
        {
            for (int ctr = 0; ctr < books.Length; ctr++)
            {
                if (title == books[ctr].Title)
                    return ref books[ctr];
            }
            return ref nobook;
        }
    }
}
