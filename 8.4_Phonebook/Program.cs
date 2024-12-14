using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace _8._4_Phonebook;

class Program
{

    static void Main(string[] args)
    {
        {
            string path = @"_Person.xml";
            NewFile(path);
            Repository re = new Repository();
            re.FirstMenu();
            re.Delay();
        }
    }

    static void NewFile(string path)
    {
        Repository re = new Repository();
        if (File.Exists(path))

        {
            Console.WriteLine("Файл _Person.xml найден");
            
        }
        else
        {
            File.Create(path).Close();
            string[] notepage = new string[] { };
            File.AppendAllLines(path, notepage);
            Console.WriteLine("Файл _Person.xml Создан");
        }

    }
}


