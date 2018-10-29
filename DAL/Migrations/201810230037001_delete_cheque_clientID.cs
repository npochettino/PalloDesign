namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_cheque_clientID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cheques", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.Cheques", new[] { "Cliente_Id" });
            DropColumn("dbo.Cheques", "Cliente_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cheques", "Cliente_Id", c => c.Int());
            CreateIndex("dbo.Cheques", "Cliente_Id");
            AddForeignKey("dbo.Cheques", "Cliente_Id", "dbo.Clientes", "Id");
        }
    }
}
