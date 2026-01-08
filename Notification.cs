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
    #region Propriedades

    private readonly ILogger<Notification> _logger;

    #endregion

    #region Construtores

    public Notification(ILogger<Notification> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Funções

    [Function("SendAsync")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var response = new ObjectResponse();

        try
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                response.Data = JsonConvert.DeserializeObject<EmailDadosDto>(requestBody) ?? throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Corpo da requisição inválido");
            }

            var _emailServices = new EmailServices((EmailDadosDto)response.Data);
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

    #endregion
}