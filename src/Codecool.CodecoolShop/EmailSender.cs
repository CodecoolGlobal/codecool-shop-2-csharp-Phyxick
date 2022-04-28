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

            //string FilePath = @"Views\Payment\Confirmation.cshtml";
            //StreamReader str = new StreamReader(FilePath);
            //string MailText = str.ReadToEnd();
            //str.Close();

            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + "<br><br>");
            messageToBeSent.Append("Thanks for ordering from Argonauts!<br>");
            messageToBeSent.Append("You can review your order details at the below link:<br>");
            messageToBeSent.Append("https://localhost:44368/Payment/Confirmation<br><br>");
            messageToBeSent.Append("Please don't hesitate to contact us if you have any questions regarding your order.<br><br>");
            messageToBeSent.Append("Have a nice a day and hope to see you again very soon!<br><br>");
            messageToBeSent.Append("The Argonauts Team");

            MailMessage message = new MailMessage("argonauts-shop@outlook.com", email);
            message.Subject = "Order Confirmation";
            message.IsBodyHtml = true;
            message.Body = messageToBeSent.ToString();

            smtpClient.Send(message);
        }
    }
}
