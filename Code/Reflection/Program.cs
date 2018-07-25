using Hello.Interface;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    [Serializable]
    public class MyClass
    {
        public int a;

        public MyClass()
        {
        }

        public MyClass(int b)
        {
            a = b;
        }

        public virtual int AddNumb(int numb1, int numb2)
        {
            int result = numb1 + numb2;
            return result;
        }

        public void Method<T>(T t)
        {
            Type result = t.GetType();
            Console.WriteLine(result.Name);
        }
    }

    public class MyClass1
    {
        public virtual int AddNumb(int numb1, int numb2)
        {
            int result = numb1 + numb2;
            return result;
        }
    }

    internal class MyMainClass
    {
        public static void Main()
        {
            Type typeOfClass = typeof(MyClass);
            if (typeOfClass.IsAbstract)
            {
                Console.WriteLine("abtract class, can not create instance");
                Console.ReadLine();
            }

            MyClass myClassObj = Activator.CreateInstance<MyClass>();

            Type myTypeObj = myClassObj.GetType();

            MethodInfo myMethodInfo = myTypeObj.GetMethod("AddNumb");
            object[] mParam = { 5, 10 };
            Console.Write("\nFirst method - " + myTypeObj.FullName + " returns " +
                          myMethodInfo.Invoke(myClassObj, mParam) + "\n");
            PrintAttributeInfo(myTypeObj);

            MethodInfo m = myTypeObj.GetMethod("Method");
            MethodInfo generic = m.MakeGenericMethod(typeof(double));
            object[] p = { 5 };

            Console.WriteLine(generic.Invoke(myClassObj, p));

            // get constructor
            ConstructorInfo constructor = typeOfClass.GetConstructor(new[] { typeof(int) });
            object lk = constructor.Invoke(new object[] { 2 });
            MyClass w = lk as MyClass;
            Console.WriteLine(w.a);

            // another class without attribute 
            MyClass1 myClassObj1 = Activator.CreateInstance<MyClass1>();
            Type myTypeObj1 = myClassObj1.GetType();
            PrintAttributeInfo(myTypeObj1);

            // load assembly dynamicly
            string language = ConfigurationManager.AppSettings["language"] ?? "zh-cn";
            string helloType = "Hello.Hello";
            Assembly assembly = Assembly.Load("Hello." + language); // load assembly
            Type type = assembly.GetType(helloType); // class type
            object obj = assembly.CreateInstance(helloType); // create class instance
            // or use 
            // The Assembly.CreateInstance actually calls Activator.CreateInstance under the hood
            obj = Activator.CreateInstance(type); // create class instance
            MethodInfo method = type.GetMethod("SayHello"); // get method 
            method.Invoke(obj, new object[] { "Jeff" }); // invoke

            // LoadFile()
            // 1、需要绝对路径(.dll)
            // 2、可以加载相同dll的多个copy
            string absolutePath = Directory.GetCurrentDirectory() + @"\V2\Hello." + language + ".dll";
            if (File.Exists(absolutePath))
            {
                Assembly cnV2 = Assembly.LoadFile(absolutePath);
                Type stype = cnV2.GetType(helloType);
                object sobj = cnV2.CreateInstance(helloType);

                method = stype.GetMethod("SayHello");
                method.Invoke(sobj, new object[] { "Jeff" });
            }

            // or use dynamic
            dynamic o = obj;
            o.SayHello("Jeff");

            // or use interface
            IHello io = obj as IHello;
            io.SayHello("Jeff");

            Console.ReadLine();
        }

        private static void PrintAttributeInfo(Type t)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(t);

            if (attrs.OfType<SerializableAttribute>().Any())
            {
                Console.WriteLine("{0} has attribute SerializableAttribute", t.Name);
            }
            else
            {
                Console.WriteLine("{0} has no attribute SerializableAttribute", t.Name);
            }
        }
    }
}
