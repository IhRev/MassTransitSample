namespace RabbitMqMassTransit.Consumer.MessageStorage;

public class MessageStore : IMessageStore
{
    private readonly List<string> _messages = [];
    
    public void SaveMessage(string message) => _messages.Add(message);

    public IEnumerable<string> GetMessages() => _messages;
}