using System.Net.Mail;
using System.Net;
using meuCuidado.Models;
using System.Web.Services.Description;

namespace meuCuidado.Controllers
{
    public class EmailController
    {
        public void EnviarEmail(Ajuda model)
        {
            var mensagem = new MailMessage();
            mensagem.From = new MailAddress("72000953@aluno.faculdadecotemig.br");
            mensagem.To.Add("72000953@aluno.faculdadecotemig.br");
            mensagem.Subject = model.Titulo;
            mensagem.Body = model.Descricao 
            + "\nTelefone: " + model.Telefone
            + "\nEmail: " + model.Email
            + "\nForma de Retorno: " + model.FormaDeRetorno;
            mensagem.IsBodyHtml = false;

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com"; // mesmo host configurado no web.config
                smtp.Port = 587; // mesmo port configurado no web.config
                smtp.EnableSsl = true; // mesmo enableSsl configurado no web.config
                smtp.Credentials = new NetworkCredential("diovan.taylor@gmail.com", "jdit yrav nsjw qjwj");
                smtp.Send(mensagem);
            }
        }
    }
}