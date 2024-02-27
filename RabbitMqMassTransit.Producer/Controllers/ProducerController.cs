using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMqMassTransit.Shared.Contracts;

namespace RabbitMqMassTransit.Producer.Controllers;

[Route("[controller]")]
[ApiController]
public class ProducerController : ControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IConfiguration _configuration;

    public ProducerController(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _configuration = configuration;
    }
    
    [HttpPost("send_message")]
    public async Task<ActionResult> SendMessage([FromQuery]string message)
    {
        string uri = _configuration["RabbitMq"];
        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(uri));
        await sendEndpoint.Send(new Message() { Content = message });
        return Ok();
    }
}