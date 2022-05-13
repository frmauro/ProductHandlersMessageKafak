using System;
using System.Collections.Generic;
using System.Linq;
using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ProductHandlerKafka.Models;
using ProductHandlerKafka.DB;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Run Product Consumer App !");




string nomeTopic = "fila_produto";

var conf = new ConsumerConfig
{
    GroupId = "fila-consumer-product",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};



using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
{
    consumer.Subscribe(nomeTopic);

    try
    {
        while (true)
        {
            var cr = consumer.Consume(cts.Token);
            var dto = JsonSerializer.Deserialize<ItemsDto>(cr.Value);
            if (dto != null)
            {
                var items = dto.Items;
                if (items != null)
                {
                    using (var context = new ProductContext())
                    {
                        items.ToList().ForEach(vm =>
                                   {
                                       var product = context.Products.Find(vm.Id);
                                       if (vm.IsSum)
                                           product.Amount += Convert.ToInt32(vm.Quantity);
                                       else
                                           product.Amount -= Convert.ToInt32(vm.Quantity);

                                       context.Update(product);
                                       context.SaveChanges();
                                   });

                    }

                }

            }
        }
    }
    catch (OperationCanceledException)
    {
        consumer.Close();
    }
}



// static void CreateDbIfNotExists(IHost host)
// {
//     using (var scope = host.Services.CreateScope())
//     {
//         var services = scope.ServiceProvider;
//         try
//         {
//             var context = services.GetRequiredService<ProductContext>();
//             context.Database.EnsureCreated();
//             // DbInitializer.Initialize(context);
//         }
//         catch (Exception ex)
//         {
//             var logger = services.GetRequiredService<ILogger<Program>>();
//             logger.LogError(ex, "An error occurred creating the DB.");
//         }
//     }
// }
