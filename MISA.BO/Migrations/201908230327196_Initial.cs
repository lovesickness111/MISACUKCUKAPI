namespace MISA.BO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CID = c.Guid(nullable: false,  defaultValueSql:"newid()"),
                        CCode = c.String(nullable: false),
                        CName = c.String(),
                        CCompany = c.String(),
                        CTaxCode = c.String(),
                        CAddress = c.String(),
                        CPhone = c.String(),
                        CEmail = c.String(),
                        CMemberNum = c.String(),
                        CMemberType = c.String(),
                        CDebit = c.Decimal(nullable: true, precision: 18, scale: 2),
                        CDescription = c.String(),
                        Is5Food = c.Boolean(nullable: true),
                        IsFollow = c.Boolean(nullable: true),
                    })
                .PrimaryKey(t => t.CID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
