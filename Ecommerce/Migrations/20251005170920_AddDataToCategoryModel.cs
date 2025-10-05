using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into Categories (Name, Description, Status) values ('Mobiles', 'Aenean fermentum.', 1);
                                    insert into Categories (Name, Description, Status) values ('Computers', 'Nulla tellus.', 1);
                                    insert into Categories (Name, Description, Status) values ('Laptops', 'Nulla mollis molestie lorem.', 1);
                                    insert into Categories (Name, Description, Status) values ('Cameras', 'Vivamus vel nulla eget eros elementum pellentesque.', 1);
                                    insert into Categories (Name, Description, Status) values ('Accessories', 'Vivamus tortor.', 1);
                                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Categories" );
        }
    }
}
