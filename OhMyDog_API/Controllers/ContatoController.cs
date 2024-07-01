using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Contato;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController
    {
        [HttpPost]
        [Route("EnviarNovoEmail")]
        public async Task<IActionResult> PostNovoEmail([FromBody] ContatoModel contato)

        {
            try
            {
                if (contato.NomeContato == "string" || string.IsNullOrEmpty(contato.NomeContato))
                    return new OkObjectResult("Preencha os Campos, por favor");

                var emailRemetente = new MailAddress(contato.EmailRemetente, "Oh My Dog");
                var emailDestinatario = new MailAddress(contato.EmailDestinatario, "OhMyDogContato");
                var emailCliente = new MailAddress(contato.EmailContato, contato.NomeContato);
                const string subject = "[Duvidas] - Contato Oh My Dog";
                string body = contato.MensagemContato;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailRemetente.Address, "azqe ccdk uzyu dmek")
                };

                using var message = new MailMessage(emailRemetente, emailDestinatario)
                {
                    Subject = subject,
                    Body = body
                };

                message.CC.Add(emailCliente);
                smtp.Send(message);

                return new OkObjectResult("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Erro ao enviar e-mail: {ex.Message}");

            }
        }
    }
}