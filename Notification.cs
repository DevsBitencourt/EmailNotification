using EmailNotification.Dto;
using EmailNotification.Implementacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailNotification;

public class Notification
{
    private readonly ILogger<Notification> _logger;

    public Notification(ILogger<Notification> logger)
    {
        _logger = logger;
    }

    [Function("SendAsync")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var response = new ObjectResponse();

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            response.Data = JsonConvert.DeserializeObject<EmailDadosDto>(requestBody);

            var secret = await CredentialsKeyVault.Obter();

            var _emailSend = new EnviarEmailDto((EmailDadosDto)response.Data, secret);

            var _emailServices = new EmailServices(_emailSend);
            await _emailServices.SendEmailAsync();

            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return new OkObjectResult(response);
    }
}