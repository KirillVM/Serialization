using System;
using System.IO;
using System.Text;

namespace SerializationTask.infrustructure
{
    class ChildFactory
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
        readonly Person parent;
        readonly int surnameIdParent;
        public ChildFactory(Person parent, int surnameIdParent)
        {
            this.parent = parent;
            this.surnameIdParent = surnameIdParent;
        }
        public Gender GetGender()
        {
            Random rndGender = new Random((int)DateTime.Now.Ticks);
            return (int)Gender.Female == rndGender.Next(0, 2) ? Gender.Female : Gender.Male;
        }
        public int GetId()
        {
            Random rndId = new Random((int)DateTime.Now.Ticks);
            return rndId.Next(1000000000);
        }

        public string GetFirstName(Gender gender)
        {
            Random rndNameId = new Random((int)DateTime.Now.Ticks);
            var nameId = rndNameId.Next(0, maleNames.Length - 1);

            if (gender == Gender.Male)
                return maleNames[nameId];
            else
                return femaleNames[nameId];
        }

        public string GetLastName(Person parent, Gender gender, int surnameIdParent)
        {
                if (gender == Gender.Male)
                    return maleSurnames[surnameIdParent];
                else
                    return femaleSurnames[surnameIdParent];
        }

        public long GetBirthDate(Person parent)
        {
            var rndBirthDatePart = new Random((int)DateTime.Now.Ticks);

            var birthYear = rndBirthDatePart.Next(DateTime.Now.Year - parent.Age + 18, 2023);
            var birthMonth = rndBirthDatePart.Next(1, 13);
            var birthDay = rndBirthDatePart.Next(1, 29);

            DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var birthDate = new DateTime(birthYear, birthMonth, birthDay, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan tsInterval = birthDate.Subtract(dt1970);
            Int64 milliseconds = Convert.ToInt64(tsInterval.TotalMilliseconds);

            return milliseconds;
        }
        public Child GetChild()
        {
            Child child = new Child();

            child.Id = this.GetId();
            child.Gender = this.GetGender();
            child.FirstName = this.GetFirstName(child.Gender);
            child.LastName = this.GetLastName(parent, child.Gender, surnameIdParent);
            child.BirthDate = this.GetBirthDate(parent);

            return child;
        }
    }
}
