using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System;
using static System.Collections.Specialized.BitVector32;


//////////////// -- Transfer -- /////////////////
using (var session = CreateSession())
{
    using (var transaction = session.BeginTransaction())
    {
        var idFrom = 3;
        var idTo = 2;
        var amountToTransfer = 1000;

        var walletFrom = session.Get<Wallet>(idFrom);

        var walletTo = session.Get<Wallet>(idTo);

        walletFrom.Balance -= amountToTransfer;
        walletTo.Balance += amountToTransfer;

        session.Update(walletFrom);
        session.Update(walletTo);

        transaction.Commit();
    }
}

//////////////// -- Delete Wallet -- ///////////////////
using(var session = CreateSession())
{
    using (var transaction = session.BeginTransaction())
    {
        var idToDelete = 17;

        var wallet = session.Get<Wallet>(idToDelete);

        session.Delete(wallet);

        transaction.Commit();

    }
}

/////////////// -- Update Wallet -- ///////////////////
using (var session = CreateSession())
{
    using (var transaction = session.BeginTransaction())
    {
        var idToUpdate = 1;

        var wallet = session.Get<Wallet>(idToUpdate);

        wallet.Balance = 5000m;

        session.Update(wallet);

        transaction.Commit();

        Console.WriteLine(wallet);
    }
}

/////////////// -- Retrieve Wallet By Id -- ///////////////////
using (var session = CreateSession())
{
    using (var transaction = session.BeginTransaction())
    {
        var idToFind = 2;
        //var wallet = session.Query<Wallet>()
        //    .FirstOrDefault(x => x.Id == idToFind);

        var wallet = session.Get<Wallet>(idToFind);
        Console.WriteLine(wallet);
        transaction.Commit();
    }
}

/////////////// -- simple insert -- ///////////////////
using (var session = CreateSession())
{
    using (var transaction = session.BeginTransaction())
    {
        var walletToAdd = new Wallet { Holder = "Tamara", Balance = 1000 };

        session.Save(walletToAdd);

        Console.WriteLine(walletToAdd);

        transaction.Commit();
    }
}

///////////// -- Retrieve Data -- ///////////////////
using (var session = CreateSession())
{
    using (var transaction = session.BeginTransaction())
    {
        var wallets = session.Query<Wallet>();
        foreach (var wallet in wallets)
        {
            Console.WriteLine(wallet);
        }

        transaction.Commit();
    }
}

Console.ReadKey();

static ISession CreateSession()
{
    var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

    var constr = config.GetSection("constr").Value;


    var mapper = new ModelMapper();

    // list all of type mappings from assembly

    mapper.AddMappings(typeof(Wallet).Assembly.ExportedTypes);

    // Compile class mapping
    HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

    // optional
    Console.WriteLine(domainMapping.AsString());

    // allow the application to specify propertties and mapping documents
    // to be used when creating

    var hbConfig = new Configuration();

    // settings from app to nhibernate 
    hbConfig.DataBaseIntegration(c =>
    {
        // strategy to interact with provider
        c.Driver<MicrosoftDataSqlClientDriver>();

        // dialect nhibernate uses to build syntaxt to rdbms
        c.Dialect<MsSql2012Dialect>();

        // connection string
        c.ConnectionString = constr;

        // log sql statement to console
        c.LogSqlInConsole = true;

        // format logged sql statement
        c.LogFormattedSql = true;
    });

    // add mapping to nhiberate configuration
    hbConfig.AddMapping(domainMapping);


    // instantiate a new IsessionFactory (use properties, settings and mapping)
    var sessionFactory = hbConfig.BuildSessionFactory();

    var session = sessionFactory.OpenSession();

    return session;
}
