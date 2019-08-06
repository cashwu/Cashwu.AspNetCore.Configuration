# Asp.Net Core configuration strongly type extension

[![Build status](https://ci.appveyor.com/api/projects/status/b7dod491mc1pa3j1?svg=true)](https://ci.appveyor.com/project/cashwu/cashwu-aspnetcore-configuration)

[![codecov](https://codecov.io/gh/cashwu/Cashwu.AspNetCore.Configuration/branch/master/graph/badge.svg)](https://codecov.io/gh/cashwu/Cashwu.AspNetCore.Configuration)

---

[![Nuget](https://img.shields.io/badge/Nuget-Cashwu.AspnetCore.Configuration-blue.svg)](https://www.nuget.org/packages/Cashwu.AspnetCore.Configuration)

---

> 自動對應 config 區塊到實體型別，並註冊到 DI

## 註冊

在 service 註冊 Config，並且傳入 `IConfiguration` 和 `Assembly Name` 當參數

```csharp
public IConfiguration Configuration { get; }

public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

public void ConfigureServices(IServiceCollection services)
{
    services.AddConfig(Configuration, "Assembly Name");
}
```

## 單一物件

新增一個對應 config 的類別加上 `ConfigurationSection` attribute，
第一個參數為 config 的物件名稱，類別的 property 對應到 config 的內容

```csharp
[ConfigurationSection("Test")]
public TestConfig : IConfig
{
    public int Id { get; set; }
}
```

```json
{
    "Test" : {
        "Id" : 1
    }
}
```

使用的話就直接注入 config 的類別就好了

```csharp
public class IndexController : Controller
{
    public IndexController(TestConfig testConfig){ ... }
}   
```

預設注入的生命周期為 Scope，如果需要修改的話，請傳入第二個參數 

```csharp
[ConfigurationSection("Test", ServiceLifetime.Singleton)]
public TestConfig : IConfig
{
    public int Id { get; set; }
}
```

## 集合物件

如果 config 是集合時，需要在 attribute 加上第三個參數 `isCollections = true`，和第四個參數 `collectionType 集合的型別`

```csharp
[ConfigurationSection("Test", isCollections: true, collectionType: typeof(int)))]
public TestConfig : IConfig
{
}
```

```json
{
    "Test" : [ 1, 2, 3]
}
```

使用的話就直接注入 List of 集合的型別 

```csharp
public class IndexController : Controller
{
    public IndexController(List<int> testConfig){ ... }
}   
```

集合的型別也可以是 reference type

```csharp
[ConfigurationSection("Test", isCollections: true, collectionType: typeof(TestConfig)))]
public TestConfig : IConfig
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

```json
{
    "Test" : [
        { "Id" : 1, "Name" : "AA" },
        { "Id" : 2, "Name" : "BB" }
    ]
}
```
