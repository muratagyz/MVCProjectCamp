namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_imagesizechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Contents", "ContentValue", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Writers", "WriterName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Writers", "WriterImage", c => c.String(maxLength: 250));
            AlterColumn("dbo.Writers", "WriterMail", c => c.String(maxLength: 100));
            AlterColumn("dbo.Writers", "WriterPassword", c => c.String(maxLength: 100));
            AlterColumn("dbo.Contacts", "UserName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "Message", c => c.String(maxLength: 1500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Message", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Contacts", "UserName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Writers", "WriterPassword", c => c.String(maxLength: 200));
            AlterColumn("dbo.Writers", "WriterMail", c => c.String(maxLength: 200));
            AlterColumn("dbo.Writers", "WriterImage", c => c.String(maxLength: 200));
            AlterColumn("dbo.Writers", "WriterName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Contents", "ContentValue", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 200));
        }
    }
}
