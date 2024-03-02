using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMqMassTransit.Shared.Contracts;

namespace RabbitMqMassTransit.Producer.Controllers;

[Route("[controller]")]
[ApiController]
public class ProducerController(ISendEndpointProvider sendEndpointProvider) : ControllerBase
{
    private const string ExchangeName = "message_exchange";
    
    [HttpPost("send_message")]
    public async Task<ActionResult> SendMessage([FromQuery]string message)
    {
        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(GetUri());
        await sendEndpoint.Send(new Message() { Content = message });
        return Ok();
    }

    private static Uri GetUri() => new($"exchange:{ExchangeName}");
}