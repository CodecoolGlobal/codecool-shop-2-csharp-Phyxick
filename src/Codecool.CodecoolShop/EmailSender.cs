using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Codecool.CodecoolShop
{
    public class EmailSender
    {
        public void SendConfirmationEmail(string name, string email)
        {
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            NetworkCredential credential = new NetworkCredential("argonauts-shop@outlook.com", "Argo@2204");
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = credential;

            string FilePath = @"wwwroot\templates\Confirmation.cshtml";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + ",\n\n");
            messageToBeSent.Append(MailText);

            MailMessage message = new MailMessage("argonauts-shop@outlook.com", email);
            message.Subject = "Order Confirmation";
            message.Body = messageToBeSent.ToString();
            message.IsBodyHtml = true;
            smtpClient.Send(message);
        }
    }
}
