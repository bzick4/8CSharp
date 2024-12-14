using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;

namespace _8._4_Phonebook
{
    [Serializable]
    public class Repository
    {
        
        XElement myPhoneBook = new XElement("PhoneBook");
        PhoneBook phoneBook = new PhoneBook();
        public List<PhoneBook> people = new List<PhoneBook>();
        
        HashSet<long> uniqeHomePhone = new HashSet<long>();
        HashSet<long> uniqeMobilePhone = new HashSet<long>();

        string note = string.Empty;
        string mobilePhone;
        string homePhone;
        public string path;

        public void FirstMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n>>>ГЛАВНОЕ МЕНЮ<<<\n");
                Console.WriteLine("1 - Ввести новую запись");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": //Ввод новой запси
                        CreateNote();
                        break;
                     default:
                        Console.Write("Такого меню нет, повторите попытку!\n");
                        break;
                }
            }
        }
        public void ContinuedMenu()
        {
            Console.WriteLine("\n>>>Продолжить ввод данных или перейти в главное меню?<<<\n");
            Console.WriteLine("1 - Продолжить ввод данных");
            Console.WriteLine("2 - Главное меню");

            string choice2 = Console.ReadLine();
            switch (choice2)
            {
                case "1":
                    CreateNote();
                    AddSave();
                    break;

                case "2":
                    AddSave();
                    FirstMenu();
                    break;

                default:
                    Console.Write("Такого меню нет, повторите попытку!\n");
                    break;

            }
        }

        public void AddRecord()
        {
            MethodFullName();
            MethodMobilePhone();
            MethodStreet();
            MethodHome();
            MethodAppartament();
            MethodHomePhone();           
        }
        public void CreateNote()
        {
            phoneBook = new();
            AddRecord();
            myPhoneBook.Add(CreatePerson());
            ContinuedMenu();        
        }
        public void AddSave()
        {
            myPhoneBook.Save("_Person.xml");
        }

        public void MethodFullName()
        {
            Console.Write("Введите Имя : ");
            string newName = Console.ReadLine();

            Console.Write("Введите Фамилию : ");
            string newLastName = Console.ReadLine();

            Console.Write("Введите Отчество  : ");
            string newMiddleName = Console.ReadLine();

            phoneBook.FullName = newLastName + " " + newName + " " + newMiddleName;
        }
        public void MethodStreet()
        {
            Console.Write("Введите улицу : ");
            phoneBook.Street = Console.ReadLine();
            //note += $"{phoneBook.Street}\t";
        }
        public void MethodHome()
        {
            Console.Write("Введите номер дома: ");
            string home = Console.ReadLine();
            while (true)
            {
                bool result = int.TryParse(home, out int i);
                if (result)
                {
                    phoneBook.Home = i;
                    note += $"{phoneBook.Home}\t";
                    break;
                }
                else
                {
                    Console.WriteLine("Повторите попытку");
                    Console.Write("Введите номер дома: ");
                    home = Console.ReadLine();
                }
            }
        }
        public void MethodAppartament()
        {
            Console.Write("Введите номер квартиры: ");
            string appartament = Console.ReadLine();
            while (true)
            {
                bool result = int.TryParse(appartament, out int k);
                if (result)
                {
                    phoneBook.Appartament = k;
                    note += $"{phoneBook.Appartament}\t";
                    break;
                }
                else
                {
                    Console.WriteLine("Повторите попытку");
                    Console.Write("Введите номер квартиры: ");
                    appartament = Console.ReadLine();
                }
            }
        }
        public void MethodMobilePhone()
        {
            while (true)
            {
                Console.WriteLine($"\nДобавьте номер телефона через '8' для {phoneBook.FullName}\nИли оставьте строку пустой для прекращения ввода:");
                mobilePhone = Console.ReadLine(); //Временная переменная для ввода
                bool result = long.TryParse(mobilePhone, out long i);// конвертирование из стринга в лонг
                if (String.IsNullOrWhiteSpace(mobilePhone)) //если строка пустая то Брейк
                {
                    break;
                }

                else if (result) //если есть цифры 
                {
                    HashMobilePhone(); //проверка Хэшсет на индивидуальность 

                    phoneBook.MobilePhone = i; //присваевает i  к переменной из класса
                    phoneBook.dictionaryMobilePhone.Add(phoneBook.MobilePhone, phoneBook.FullName); //добавлякт в словарь ключ и значение 
                    note += $"{phoneBook.MobilePhone}\t";//добавляет телефон в переменную листа

                }

                else
                {
                    Console.WriteLine("Повторите попытку");
                    Console.WriteLine($"\nДобавьте номер телефона через '8' для {phoneBook.FullName}\nИли оставьте строку пустой для прекращения ввода:");
                    mobilePhone = Console.ReadLine();
                }
            }

        }//
        public void MethodHomePhone()
        {
            while (true)
            {
                Console.WriteLine($"\nДобавьте домашний номер телефона через '8' для {phoneBook.FullName}\nИли оставьте строку пустой для прекращения ввода:");

                homePhone = Console.ReadLine();
                bool result = long.TryParse(homePhone, out long k);
                if (String.IsNullOrWhiteSpace(homePhone))
                {
                    break;
                }

                else if (result)
                {
                    HashHomePhone();

                    phoneBook.HomePhone = k;
                    note += $"{phoneBook.HomePhone}\t";

                }

                else
                {
                    Console.WriteLine("Повторите попытку");
                    Console.WriteLine($"\nДобавьте номер телефона через '8' для {phoneBook.FullName}\nИли оставьте строку пустой для прекращения ввода:");
                    homePhone = Console.ReadLine();
                }

            }

        }//

        public void HashMobilePhone()
        {
            while (true)
            {
                if (long.TryParse(mobilePhone, out long convertMobilePhone))
                {

                    if (uniqeMobilePhone.Add(convertMobilePhone))
                    {
                        Console.WriteLine($"Номер {mobilePhone} сохранен для абонента {phoneBook.FullName}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nНомер {mobilePhone} уже занят. Повторите попытку");
                        mobilePhone = Console.ReadLine();
                    }
                }
            }
        }
        public void HashHomePhone()
        {

            while (true)
            {
                if (long.TryParse(homePhone, out long convertHomePhone))
                {

                    if (uniqeHomePhone.Add(convertHomePhone))
                    {
                        Console.WriteLine($"Номер {homePhone} сохранен для абонента {phoneBook.FullName}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nНомер {homePhone} уже занят. Повторите попытку");
                        homePhone = Console.ReadLine();
                    }
                }
            }

        }

        public void Delay()
        {
            Console.ReadKey();
        }


        public XElement CreatePerson()
        {
            XElement tempPerson = new XElement("Person");
            XElement tempAddress = new XElement("Address");
            XElement tempPhones = new XElement("Phones");

            XElement tempStreet = new XElement("Street", phoneBook.Street);
            XElement tempHome = new XElement("Home", phoneBook.Home);
            XElement tempAppartament = new XElement("Appartament", phoneBook.Appartament);

            XElement tempMobilePhone = new XElement("MobilePhone");
            
            foreach (var item in phoneBook.dictionaryMobilePhone.Keys)
            {
                tempMobilePhone = new XElement("MobilePhone");
                tempMobilePhone.Add(item.ToString());
                tempPhones.Add(tempMobilePhone);
            }
           
            XElement tempHomePhone = new XElement("HomePhone", phoneBook.HomePhone);

            XAttribute tempName = new XAttribute("Name", phoneBook.FullName);

            tempAddress.Add(tempStreet, tempHome, tempAppartament);
            tempPhones.Add(tempHomePhone);
            tempPerson.Add(tempAddress, tempPhones, tempName);

            return tempPerson;

        }



    }
   
}

