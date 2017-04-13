namespace Keturi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NicknameChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Highscores", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Highscores", "User_Id");
            AddForeignKey("dbo.Highscores", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Highscores", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Highscores", new[] { "User_Id" });
            DropColumn("dbo.Highscores", "User_Id");
        }
    }
}
