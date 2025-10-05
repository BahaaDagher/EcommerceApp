using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 11', 'Nam tristique tortor eu pede.', '1.png', 145143, 41, 3.6, 0, 80.76, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 11 Pro', 'In hac habitasse platea dictumst.', '2.png', 128568, 55, 2.3, 1, 39.3, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 11 Pro Max', 'Mauris lacinia sapien quis libero.', '2.png', 138195, 77, 2.9, 1, 13.91, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 12', 'Nulla justo.', '5.png', 24301, 83, 1.7, 0, 5.29, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 12 Pro', 'Aenean auctor gravida sem.', '5.png', 132335, 73, 3.5, 1, 32.71, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 12 Pro Max', 'Nulla ac enim.', '3.png', 39279, 48, 3.9, 1, 95.77, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 13', 'Etiam pretium iaculis justo.', '2.png', 34717, 83, 2.3, 1, 1.99, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 13 Pro', 'Curabitur in libero ut massa volutpat convallis.', '4.png', 148235, 34, 4.0, 1, 84.07, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 13 Pro Max', 'Duis at velit eu est congue elementum.', '3.png', 54857, 24, 5.0, 1, 22.7, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 14', 'Integer non velit.', '4.png', 91494, 9, 1.0, 0, 25.86, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 14 Pro', 'Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo.', '2.png', 31861, 88, 2.7, 1, 23.48, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 14 Pro Max', 'Vestibulum sed magna at nunc commodo placerat.', '2.png', 41738, 58, 4.1, 1, 8.12, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 15', 'Suspendisse potenti.', '2.png', 55984, 39, 3.4, 1, 33.9, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 15 Pro', 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante.', '1.png', 75583, 63, 2.3, 1, 10.3, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 15 Pro Max', 'Nulla justo.', '5.png', 60971, 36, 4.9, 1, 48.03, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 11', 'Duis bibendum.', '5.png', 99369, 18, 1.1, 1, 91.91, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 11 Pro', 'Nullam varius.', '5.png', 130213, 89, 3.8, 0, 30.66, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 11 Pro Max', 'Morbi non lectus.', '3.png', 66520, 87, 4.5, 0, 66.48, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 12', 'Maecenas tincidunt lacus at velit.', '1.png', 71234, 12, 2.9, 1, 47.21, 1, 1);
                                    insert into Products (Name, Description, MainImg, Price, Quantity, Rate, Status, Discount, CategoryId, BrandId) values ('IPhone 12 Pro', 'Vivamus vestibulum sagittis sapien.', '1.png', 114722, 24, 2.3, 1, 53.05, 1, 1);
                                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Products");
        }
    }
}
