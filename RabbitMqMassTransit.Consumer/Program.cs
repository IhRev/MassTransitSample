using MassTransit;
using RabbitMqMassTransit.Consumer.MessageStorage;
using RabbitMqMassTransit.Consumer.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMessageStore, MessageStore>();
string rabbitMqHost = builder.Configuration["RabbitMq"] 
                      ?? throw new ConfigurationException("RabbitMq connection string not found");
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<MessageConsumer>();
    
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri(rabbitMqHost));
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();