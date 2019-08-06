namespace Cashwu.AspNetCore.Configuration.Tests
{
    [ConfigurationSection("SingleConfig")]
    public class SingleConfig : IConfig
    {
        public int Id { get; set; }
    }
}