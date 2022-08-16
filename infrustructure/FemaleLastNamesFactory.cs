using System;
using System.IO;
using System.Text;

namespace SerializationTask.infrustructure
{
    public class FemaleLastNamesFactory
    {
        public static void Femalization(string[] maleLastNames)
        {
            string[] femaleLastNames = new string[maleLastNames.Length];

            for (int i = 0; i < maleLastNames.Length; i++)
            {
                string[] endingsOne = { "ев", "ов", "ин", "ын"};
                string[] endingsTwo = { "ой", "ий", "ый" };

                for (int j = 0; j< endingsOne.Length;j++)
                {
                    if (maleLastNames[i].Substring(maleLastNames[i].Length - 2) == endingsOne[j])
                    {
                        femaleLastNames[i] = maleLastNames[i] + "а";
                    }
                }

                for (int k = 0; k < endingsTwo.Length; k++)
                {
                    if (maleLastNames[i].Substring(maleLastNames[i].Length - 2) == endingsTwo[k])
                    {
                        femaleLastNames[i] = maleLastNames[i].Remove(maleLastNames[i].Length - 2) + "ая";
                    }
                }

                if (femaleLastNames[i]==null)
                {
                    femaleLastNames[i] = maleLastNames[i];  
                }
            }

            File.WriteAllLines(@"..\..\..\ru-pnames-list\female_surnames_rus.txt", femaleLastNames, Encoding.GetEncoding("UTF-8"));
        }
    }
}
