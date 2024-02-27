using Microsoft.AspNetCore.Mvc;
using RabbitMqMassTransit.Consumer.MessageStorage;
using RabbitMqMassTransit.Shared.Contracts;

namespace RabbitMqMassTransit.Consumer.Controllers;

[Route("[controller]")]
[ApiController]
public class ConsumerController(IMessageStore messageStore) : ControllerBase
{
    [ProducesResponseType<IEnumerable<Message>>(200)]
    [HttpGet("messages")]
    public ActionResult GetMessages()
    {
        return Ok(messageStore.GetMessages());
    }
}