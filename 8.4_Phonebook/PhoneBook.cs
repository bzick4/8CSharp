using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace _8._4_Phonebook
{
	[Serializable]
	public class PhoneBook
	{
        public Dictionary<long, string> dictionaryMobilePhone = new Dictionary<long, string>();

        public PhoneBook() { }

        public PhoneBook(string fullName, string street, int home, int appartament, long mobilePhone, long homePhone)
        {
            this.FullName = fullName;
            this.Street = street;
            this.Home = home;
            this.Appartament = appartament;
            this.MobilePhone = mobilePhone;
            this.HomePhone = homePhone;
        }

        public string FullName { get; set; }
        public string Street { get; set; }
        public int Home { get; set; }
        public int Appartament { get; set; }
        public long MobilePhone { get; set; }
        public long HomePhone { get; set; }

    }
}

