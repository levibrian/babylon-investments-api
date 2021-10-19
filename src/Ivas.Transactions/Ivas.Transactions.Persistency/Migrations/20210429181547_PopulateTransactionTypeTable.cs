using Microsoft.EntityFrameworkCore.Migrations;

namespace Ivas.Transactions.Persistency.Migrations
{
    public partial class PopulateTransactionTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT TransactionTypes ON
                
                INSERT INTO TransactionTypes (Id, Description) VALUES (1, 'Buy');
                INSERT INTO TransactionTypes (Id, Description) VALUES (2, 'Sell');
                INSERT INTO TransactionTypes (Id, Description) VALUES (3, 'Dividend');
                
                SET IDENTITY_INSERT TransactionTypes OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                TRUNCATE TABLE TransactionTypes
            ");
        }
    }
}
