using Microsoft.Extensions.DependencyInjection;


namespace Volo.Abp.Kafka;

public class KafkaModule
{
    //public override void ConfigureServices(this IServiceCollection services)
    //{
    //    //var configuration = services.GetConfiguration();
    //    //Configure<AbpKafkaOptions>(configuration.GetSection("Kafka"));
    //}

    //public override void OnApplicationShutdown(ApplicationShutdownContext context)
    //{
    //    context.ServiceProvider
    //        .GetRequiredService<IConsumerPool>()
    //        .Dispose();

    //    context.ServiceProvider
    //        .GetRequiredService<IProducerPool>()
    //        .Dispose();
    //}
}
