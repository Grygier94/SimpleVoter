// <auto-generated />
namespace SimpleVoter.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class AddExpirationDateColumnToPollsTable : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddExpirationDateColumnToPollsTable));
        
        string IMigrationMetadata.Id
        {
            get { return "201708050956044_AddExpirationDateColumnToPollsTable"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
