using System;
using System.Text;
using System.IO;

namespace SerializationTask.infrustructure
{
    public class PersonFactory
    {
        #region [GetNamesAndSurnamesFromFiles]
        static string pathToMaleNames = @"..\..\..\ru-pnames-list\male_names_rus.txt";
        static string[] maleNames = File.ReadAllLines(pathToMaleNames, Encoding.GetEncoding("UTF-8"));

        static string pathToFemaleNames = @"..\..\..\ru-pnames-list\female_names_rus.txt";
        static string[] femaleNames = File.ReadAllLines(pathToFemaleNames, Encoding.GetEncoding("UTF-8"));

        static string pathToMaleSurnames = @"..\..\..\ru-pnames-list\male_surnames_rus.txt";
        static string[] maleSurnames = File.ReadAllLines(pathToMaleSurnames, Encoding.GetEncoding("UTF-8"));

        static string pathToFemaleSurnames = @"..\..\..\ru-pnames-list\female_surnames_rus.txt";
        static string[] femaleSurnames = File.ReadAllLines(pathToFemaleSurnames, Encoding.GetEncoding("UTF-8"));
        #endregion

        static int sequenceId = 0;
        static int surnameId = 0;

        public int GetId()
        {
            Random rndId = new Random((int)DateTime.Now.Ticks);
            return rndId.Next(1000000000);
        }
        public Guid GetTransportId()
        {
            return Guid.NewGuid();
        }

        public string GetFirstName(Gender gender)
        {
            Random nameId = new Random((int)DateTime.Now.Ticks);

            if (gender == Gender.Male)
                return maleNames[nameId.Next(0, maleNames.Length-1)];
            else
                return femaleNames[nameId.Next(0, femaleNames.Length-1)];
        }

        public string GetLastName(Gender gender)
        {
            Random nameId = new Random((int)DateTime.Now.Ticks);
            surnameId = nameId.Next(0, maleSurnames.Length - 1);

            if (gender == Gender.Male)
                return maleSurnames[surnameId];
            else
                return femaleSurnames[surnameId];
        }
        public int GetSequenceId()
        {
            return ++sequenceId;
        }
        public string[] GetCardNumbers()
        {
            Random rndCardNumberPart = new Random((int)DateTime.Now.Ticks);
            string[] cardNumbers = new string[rndCardNumberPart.Next(1, 5)];

            for (int i = 0; i < cardNumbers.Length; i++)
            {
                cardNumbers[i] = rndCardNumberPart.Next(1000, 10000) + " " 
                               + rndCardNumberPart.Next(1000, 10000) + " "
                               + rndCardNumberPart.Next(1000, 10000) + " "
                               + rndCardNumberPart.Next(1000, 10000);
            }

            return cardNumbers;
        }

        public int GetAge()
        {
            var rndAge = new Random((int)DateTime.Now.Ticks);
            return rndAge.Next(18, 65);
        }

        public string[] GetPhones()
        {
            var rndPhonePart = new Random((int)DateTime.Now.Ticks);
            string[] phones = new string[rndPhonePart.Next(1, 4)];

            for (int i = 0; i < phones.Length; i++)
            {
                phones[i] = "8(9" + rndPhonePart.Next(10, 100) + ")-"
                          + rndPhonePart.Next(100, 1000) + "-"
                          + rndPhonePart.Next(10, 100) + "-"
                          + rndPhonePart.Next(10, 100);
            }

            return phones;
        }

        public long GetBirthDate(int age)
        {
            var rndBirthDatePart = new Random((int)DateTime.Now.Ticks);

            var birthYear = (int)DateTime.Now.Year - age;
            var birthMonth = rndBirthDatePart.Next(1, 13);
            var birthDay = rndBirthDatePart.Next(1, 29);

            DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime birthDate = new DateTime(birthYear, birthMonth, birthDay, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan tsInterval = birthDate.Subtract(dt1970);
            Int64 milliseconds = Convert.ToInt64(tsInterval.TotalMilliseconds);

            return milliseconds;
        }
        public double GetSalary()
        {
            var rndSalary = new Random((int)DateTime.Now.Ticks);
            double salary = rndSalary.Next(13000, 200000) + rndSalary.Next(0, 100) / 100d;
            return salary;
        }

        public bool GetIsMarried()
        {
            var rndIsMarried = new Random((int)DateTime.Now.Ticks);

            if (rndIsMarried.Next(0, 2) == 0)
                return false;
            else
                return true;
        }
        public Gender GetGender()
        {
            Random rndGender = new Random((int)DateTime.Now.Ticks);
            return (int)Gender.Female == rndGender.Next(0, 2) ? Gender.Female : Gender.Male;
        }

        public Child[] GetChildren(Person parent, int surnameIdParent)
        {
            Random rndNumberOfChildren = new Random((int)DateTime.Now.Ticks);
            var NumberOfShildren = rndNumberOfChildren.Next(0, 5);

            var children = new Child[NumberOfShildren];
            var childFactory = new ChildFactory(parent, surnameIdParent);

            for (int i = 0; i < NumberOfShildren; i++)
            {
                children[i] = childFactory.GetChild();
            }
            return children;
        }

        public Person GetPerson()
        {
            var person = new Person();

            person.Id = this.GetId();
            person.TransportId = this.GetTransportId();
            person.Gender = this.GetGender();
            person.FirstName = this.GetFirstName(person.Gender);
            person.LastName = this.GetLastName(person.Gender);
            person.SequenceId = this.GetSequenceId();
            person.CreditCardNumbers = this.GetCardNumbers();
            person.Age = this.GetAge();
            person.BirthDate = this.GetBirthDate(person.Age);
            person.Salary = this.GetSalary();
            person.IsMarred = this.GetIsMarried();
            person.Phones = this.GetPhones();
            person.Children = this.GetChildren(person, surnameId);

            return person;
        }
    }
}
