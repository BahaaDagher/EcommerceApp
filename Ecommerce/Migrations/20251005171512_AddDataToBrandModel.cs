using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToBrandModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into Brands (Name, Description, Status) values ('Apple', 'Vivamus tortor.', 1);
                                    insert into Brands (Name, Description, Status) values ('Samsung', 'Ut tellus.', 1);
                                    insert into Brands (Name, Description, Status) values ('Oppo', 'Etiam pretium iaculis justo.', 1);
                                    insert into Brands (Name, Description, Status) values ('HP', 'Aliquam sit amet diam in magna bibendum imperdiet.', 1);
                                    insert into Brands (Name, Description, Status) values ('Dell', 'Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.', 1);
                                 ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Brands");
        }
    }
}
