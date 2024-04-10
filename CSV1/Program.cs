using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSV1
{
    internal class Program
    {


        public static void Printclass<T>(T std) where T : Member
        {
            Type type = std.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine(prop.GetValue(std));
            }
        }
        public static T Setclass<T>() where T:new ()
        {
            T t = new T();
            var props = t.GetType().GetProperties();
            foreach (var prop in props)
            {
                switch (prop.Name)
                {
                    case "Number":
                        prop.SetValue(t, "2");
                        break;
                    case "Name":
                        prop.SetValue(t, "Andy");
                        break;
                    case "Birth":
                        prop.SetValue(t, "1999");
                        break;
                }
            }
            return t;
        }
       

        static void Main(string[] args)
        {
            //StreamWriter Writer = new StreamWriter("data.csv");
            //Writer.WriteLine("No1, Nicki, 19941028");
            //Writer.WriteLine("No2, Allen, 19961014");
            //Writer.WriteLine("No3, Amy, 20010921");
            //Writer.Flush();
            //Writer.Close();
            Member mem = Setclass<Member>();
            //Printclass<Member>(mem);

            CsvHelper  csvread = new CsvHelper();
            List < CostItem > list = new List<CostItem>();
            //csvread.Write(mem, "C:\\Users\\USER\\Desktop\\CSharp\\CSV1\\CSV2\\data1.csv");
            list = csvread.Read<CostItem>("C:\\Users\\USER\\Desktop\\CSharp\\CSV1\\2023-12-21\\2023-12-21.csv", false);

            //Console.WriteLine(student.Birth);
            foreach (CostItem m in list)
            {
                Console.WriteLine (m.Item);
            }

            Console.ReadKey();

        }
    }
}
