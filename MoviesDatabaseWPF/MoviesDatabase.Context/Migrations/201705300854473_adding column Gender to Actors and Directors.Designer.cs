// <auto-generated />
namespace MoviesDatabase.Context.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class addingcolumnGendertoActorsandDirectors : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(addingcolumnGendertoActorsandDirectors));
        
        string IMigrationMetadata.Id
        {
            get { return "201705300854473_adding column Gender to Actors and Directors"; }
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