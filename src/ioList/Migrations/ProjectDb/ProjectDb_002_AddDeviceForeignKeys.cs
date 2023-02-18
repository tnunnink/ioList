using System.Data;
using FluentMigrator;

namespace ioList.Migrations.ProjectDb
{
    [Migration(2)]
    public class ProjectDb_002_AddDeviceForeignKeys : Migration
    {
        public override void Up()
        {
            Create.Table("Device_Temp")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("ControllerId").AsInt32().NotNullable()
                    .ForeignKey("FK_Device_Controller", "Controller", "Id").OnDelete(Rule.Cascade)
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Parent").AsString().NotNullable()
                .WithColumn("CatalogNumber").AsString().NotNullable()
                    .ForeignKey("FK_Device_DeviceInfo", "DeviceInfo", "CatalogNumber").OnDelete(Rule.None)
                .WithColumn("Revision").AsString().NotNullable()
                .WithColumn("Slot").AsInt32()
                .WithColumn("IP").AsString()
                .WithColumn("Description").AsString();
            
            Execute.Sql("insert into Device_Temp select * from main.Device");

            Delete.Table("Device");

            Rename.Table("Device_Temp").To("Device");
        }

        public override void Down()
        {
            Create.Table("Device_Temp")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("ControllerId").AsInt32().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Parent").AsString().NotNullable()
                .WithColumn("CatalogNumber").AsString().NotNullable()
                .WithColumn("Revision").AsString().NotNullable()
                .WithColumn("Slot").AsInt32()
                .WithColumn("IP").AsString()
                .WithColumn("Description").AsString();
            
            Execute.Sql("insert into Device_Temp select * from main.Device");

            Delete.Table("Device");

            Rename.Table("Device_Temp").To("Device");
        }
    }
}