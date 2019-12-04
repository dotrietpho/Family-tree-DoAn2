namespace CayGiaPhaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdNodeCha = c.Int(),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        personId = c.Int(nullable: false, identity: true),
                        personName = c.String(),
                        personGender = c.String(),
                        personImg = c.String(),
                        IdNodeCha = c.Int(),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.personId)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.PersonRelationships",
                c => new
                    {
                        personId = c.Int(nullable: false),
                        toPersonId = c.Int(nullable: false),
                        Ralationship = c.String(),
                    })
                .PrimaryKey(t => new { t.personId, t.toPersonId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Node_Id", "dbo.Nodes");
            DropForeignKey("dbo.Nodes", "Node_Id", "dbo.Nodes");
            DropIndex("dbo.People", new[] { "Node_Id" });
            DropIndex("dbo.Nodes", new[] { "Node_Id" });
            DropTable("dbo.PersonRelationships");
            DropTable("dbo.People");
            DropTable("dbo.Nodes");
        }
    }
}
