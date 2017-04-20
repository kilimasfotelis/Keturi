using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Keturi.Models
{
    public class Highscore
    {
        public int ID { get; set; }
        public string Nickname { get; set; }
        public int Score { get; set; }

        public virtual ApplicationUser User { get; set; }
        
        public string ApplicationUserId { get; set; }
    }
}