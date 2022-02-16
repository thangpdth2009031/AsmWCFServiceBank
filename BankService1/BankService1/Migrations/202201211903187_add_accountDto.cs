namespace BankService1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_accountDto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountDtoes",
                c => new
                    {
                        AccountNumber = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        PasswordConfirm = c.String(),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        IdentityNumber = c.String(nullable: false),
                        Balance = c.Double(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountNumber = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        PasswordComfirm = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        IdentityNumber = c.String(),
                        Balance = c.Double(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountDtoes");
        }
    }
}
