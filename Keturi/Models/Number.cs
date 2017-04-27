using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Keturi.Models
{
    public class Number
    {
        [Required]
        [Display(Name = "Jūsų spėjimas:")]
        [RegularExpression(@"\d{4}", ErrorMessage ="keturi skaitmenys")]
        public string Guess { get; set; }
        public string Answer { get; set; }
        public Number() { }
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
        public Boolean compareValues()
        {
            bool ok = false;
            if (Guess == Answer)
            {
                ok = true;
            }
            else
            {
                int semiCorrect = 0; int correct = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (Answer.Contains(Guess[i]))
                    {
                        semiCorrect++;
                    }
                    if (Answer[i] == Guess[i])
                    {
                        correct++;
                    }
                }
                Notes.Insert(0, string.Format("Jūsų spėjimas:{0}, Atspėta skaitmenų: {1}, Iš jų savo vietoje: {2}", Guess, semiCorrect.ToString(), correct.ToString()));
            }
            return ok;
        }
    }
}