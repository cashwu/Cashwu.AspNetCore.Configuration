namespace Cashwu.AspNetCore.Configuration.Tests
{
    [ConfigurationSection("ArrayConfigStruct", isCollections: true, collectionType: typeof(int))]
    public class ArrayConfigStruct 
    {
    }
}