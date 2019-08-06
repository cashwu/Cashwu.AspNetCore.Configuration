namespace Cashwu.AspNetCore.Configuration.Tests
{
    [ConfigurationSection("ArrayConfigObject", isCollections: true, collectionType: typeof(ArrayConfigObject))]
    public class ArrayConfigObject : IConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}