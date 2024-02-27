using MassTransit;
using RabbitMqMassTransit.Consumer.MessageStorage;
using RabbitMqMassTransit.Shared.Contracts;

namespace RabbitMqMassTransit.Consumer.Messaging;

public class MessageConsumer(IMessageStore messageStore) : IConsumer<Message>
{
    public Task Consume(ConsumeContext<Message> context)
    {
        messageStore.SaveMessage(context.Message.Content);
        return Task.CompletedTask;
    }
}