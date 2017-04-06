using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Keturi.Models
{
    public class Number
    {
        public string Answer { get; set; }
        public Number() { }
        public string FinalAnswer { get; set; }
        public List<string> Notes { get; set; }
        public void createNotes(List<string> notes)
        {
            this.Notes = notes;
        }

        //Metodas skaiciui sugeneruoti
        public void Generate()
        {
            //Hashsetas supildomas skaitmenimis
            //
            int number;
            HashSet<int> Answer = new HashSet<int>();
            while (Answer.Count != 4)
            {
                Random random = new Random();
                number = random.Next(0, 9999);
                int digit = number % 10;
                Answer.Add(digit);
            }

            //Hashsetas paverciamas string'u
            // 
            string answer = "";
            foreach (var item in Answer)
            {
                answer += item.ToString();
            }
            this.Answer = answer;
        }

        // tikrinama ar ivestas skaicius atitinka sugeneruota
        // jeigu neatitinka tuomet deda ji i sarasa
        public Boolean compareValues(string guess, string answer)
        {
            bool ok = false;
            if (guess == answer)
            {
                ok = true;
            }
            else
            {
                int semiCorrect = 0; int correct = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (answer.Contains(guess[i]))
                    {
                        semiCorrect++;
                    }
                    if (answer[i] == guess[i])
                    {
                        correct++;
                    }
                }
                Notes.Insert(0, string.Format("Jūsų spėjimas:{0}, Atspėta skaitmenų: {1}, Iš jų savo vietoje: {2}", guess, semiCorrect.ToString(), correct.ToString()));
            }
            return ok;
        }
    }
}