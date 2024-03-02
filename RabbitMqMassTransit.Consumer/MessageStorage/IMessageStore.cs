namespace RabbitMqMassTransit.Consumer.MessageStorage;

public interface IMessageStore
{
    void SaveMessage(string message);

    IEnumerable<string> GetMessages();
}