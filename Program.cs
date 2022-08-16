using SerializationTask.infrustructure;
using System;
using System.IO;

namespace SerializationTask
{
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete(@"..\..\..\Persons.json");
            //Creation
            var persons = new Person[10000];
            var personFactory = new PersonFactory();
            for (int i = 0; i < 10000; i++)
            {
                persons[i] = personFactory.GetPerson();
            }

            //Serialization
            string path = @"..\..\..\Persons.json";
            JsonStreamer.Write<Person>(path, persons);

            System.Threading.Thread.Sleep(500);
            Array.Clear(persons, 0, persons.Length - 1);

            //Deserialization
            persons = JsonStreamer.Read<Person>(path);

            //Output
            Console.WriteLine("Количество персон: " + persons.Length);
            int CreditCardsCount = 0;
            long averageBirthOfAllChildren = 0;
            int counterOfAllChildren = 0;
            for (int i = 9000; i < 10000; i += 10)
            {
                CreditCardsCount += persons[i].CreditCardNumbers.Length;

                for (int j = 0; j < persons[i].Children.Length; j++)
                {
                    averageBirthOfAllChildren += persons[i].Children[j].BirthDate;
                    counterOfAllChildren++;
                }
            }
            Console.WriteLine("Колличество кредитных карт: " + CreditCardsCount);

            TimeSpan averageBirthDateSpan = TimeSpan.FromMilliseconds(averageBirthOfAllChildren/ counterOfAllChildren);
            DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime averageBirthDate = dt1970.Add(averageBirthDateSpan);

            int averageAge = DateTime.Now.Year-averageBirthDate.Year;
            Console.WriteLine($"Средний возраст ребенка: {averageAge} лет");

        }
    }
}
