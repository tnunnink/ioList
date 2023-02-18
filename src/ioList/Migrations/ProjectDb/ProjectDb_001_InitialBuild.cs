using FluentMigrator;

namespace ioList.Migrations.ProjectDb
{
    [Migration(1)]
    public class ProjectDb_001_InitialBuild : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Controller")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Processor").AsString();

            Create.Table("Device")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("ControllerId").AsInt32().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Parent").AsString().NotNullable()
                .WithColumn("CatalogNumber").AsString().NotNullable()
                .WithColumn("Revision").AsString().NotNullable()
                .WithColumn("Slot").AsInt32()
                .WithColumn("IP").AsString()
                .WithColumn("Description").AsString();

            Create.Table("DeviceInfo")
                .WithColumn("CatalogNumber").AsString().PrimaryKey()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Vendor").AsString().NotNullable()
                .WithColumn("ProductType").AsString().NotNullable()
                .WithColumn("ProductCode").AsInt32().NotNullable();
            
            Create.Table("Category")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("Name").AsString().Unique().NotNullable();

            Create.Table("DeviceInfoCategory")
                .WithColumn("CatalogNumber").AsString().NotNullable()
                .WithColumn("CategoryId").AsInt32().NotNullable();
        }
    }
}