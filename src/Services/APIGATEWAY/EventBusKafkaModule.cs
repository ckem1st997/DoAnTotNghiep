using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Kafka;
using Volo.Abp.Kafka;
using Volo.Abp.Modularity;


namespace APIGATEWAY
{
    [DependsOn(
      typeof(AbpEventBusModule),
      typeof(AbpKafkaModule))]
    public class EventBusKafkaModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpKafkaEventBusOptions>(configuration.GetSection("Kafka:EventBus"));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context
                .ServiceProvider
                .GetRequiredService<KafkaDistributedEventBus>()
                .Initialize();
        }
    }
}