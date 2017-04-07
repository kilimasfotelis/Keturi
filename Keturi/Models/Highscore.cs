using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Keturi.Models
{
    public class Highscore
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Score { get; set; }
    }

    public class HighscoreDBContext : DbContext
    {
        public DbSet<Highscore> Highscores { get; set; }
    }
}