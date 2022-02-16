namespace BankService1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_transactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionHistories",
                c => new
                    {
                        TransactionId = c.String(nullable: false, maxLength: 128),
                        Amount = c.Double(nullable: false),
                        SenderAccountNumber = c.String(),
                        ReceiverAccountNumber = c.String(),
                        Message = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        TransactionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            DropTable("dbo.AccountDtoes");
        }
        
        public override void Down()
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
            
            DropTable("dbo.TransactionHistories");
        }
    }
}
