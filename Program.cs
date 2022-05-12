using Confluent.Kafka;
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
        }
    }
    catch (OperationCanceledException)
    {
        consumer.Close();
    }
}
