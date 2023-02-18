using System.Data;
using FluentMigrator;

namespace ioList.Migrations.ProjectDb
{
    [Migration(3)]
    public class ProjectDb_003_AddDeviceInfoCategoryForeignKeys : Migration
    {
        public override void Up()
        {
            Create.Table("DeviceInfoCategory_Temp")
                .WithColumn("CatalogNumber").AsString().NotNullable()
                    .ForeignKey("FK_DeviceInfoCategory_DeviceInfo", "DeviceInfo", "CatalogNumber").OnDelete(Rule.Cascade)
                .WithColumn("CategoryId").AsInt32().NotNullable()
                    .ForeignKey("FK_DeviceInfoCategory_Category", "Category", "Id").OnDelete(Rule.Cascade);

            Execute.Sql("insert into DeviceInfoCategory_Temp select * from DeviceInfoCategory");
            Delete.Table("DeviceInfoCategory");
            Rename.Table("DeviceInfoCategory_Temp").To("DeviceInfoCategory");
        }

        public override void Down()
        {
            Create.Table("DeviceInfoCategory_Temp")
                .WithColumn("CatalogNumber").AsString().NotNullable()
                .WithColumn("CategoryId").AsInt32().NotNullable();
            
            Execute.Sql("insert into DeviceInfoCategory_Temp select * from DeviceInfoCategory");
            Delete.Table("DeviceInfoCategory");
            Rename.Table("DeviceInfoCategory_Temp").To("DeviceInfoCategory");
        }
    }
}