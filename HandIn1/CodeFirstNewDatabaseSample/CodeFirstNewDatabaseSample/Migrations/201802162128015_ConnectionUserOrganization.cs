namespace CodeFirstNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ConnectionUserOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Organization_OrganizationId", c => c.Int());
            CreateIndex("dbo.Users", "Organization_OrganizationId");
            AddForeignKey("dbo.Users", "Organization_OrganizationId", "dbo.Organizations", "OrganizationId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Users", "Organization_OrganizationId", "dbo.Organizations");
            DropIndex("dbo.Users", new[] { "Organization_OrganizationId" });
            DropColumn("dbo.Users", "Organization_OrganizationId");
        }
    }
}
