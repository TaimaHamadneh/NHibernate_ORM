
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("C:\\Users\\User0\\Desktop\\asp Projects\\NET02.ConnectionString\\NET02.ConnectionString\\bin\\Debug\\net9.0\\appsettings.json")
    .Build();

Console.WriteLine(config.GetSection("constr").Value);