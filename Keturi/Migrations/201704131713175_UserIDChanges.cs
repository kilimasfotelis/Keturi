namespace Keturi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIDChanges : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Highscores", name: "User_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Highscores", name: "IX_User_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Highscores", name: "IX_ApplicationUserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Highscores", name: "ApplicationUserId", newName: "User_Id");
        }
    }
}
