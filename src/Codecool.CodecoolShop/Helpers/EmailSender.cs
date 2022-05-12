using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Codecool.CodecoolShop
{
    public class EmailSender
    {
        public string OrderMessage(string name)
        {
            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + "<br><br>");
            messageToBeSent.Append("Thanks for ordering from Argonauts!<br>");
            messageToBeSent.Append("You can review your order details at the below link:<br>");
            messageToBeSent.Append("https://localhost:44368/Payment/Confirmation<br><br>");
            messageToBeSent.Append("Please don't hesitate to contact us if you have any questions regarding your order.<br><br>");
            messageToBeSent.Append("Have a nice a day and hope to see you again very soon!<br><br>");
            messageToBeSent.Append("The Argonauts Team");
            return messageToBeSent.ToString();
        }        
        public string RegistrationMessage(string name)
        {
            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + "<br><br>");
            messageToBeSent.Append("Welcome to Argonauts!<br>");
            messageToBeSent.Append("We hope you'll enjoy shopping with us.<br><br>");
            messageToBeSent.Append("The Argonauts Team");
            return messageToBeSent.ToString();
        }
        public void SendConfirmationEmail(string name, string email, string type)
        {
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            NetworkCredential credential = new NetworkCredential("argonauts-shop@outlook.com", "Argo@2204");
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = credential;

            MailMessage message = new MailMessage("argonauts-shop@outlook.com", email);
            message.IsBodyHtml = true;
            if (type == "order")
            {
                message.Body = OrderMessage(name);
                message.Subject = "Order Confirmation";
            }

            if (type == "registration")
            {
                message.Body = RegistrationMessage(name);
                message.Subject = "Registration Confirmation";
            }

            smtpClient.Send(message);
        }
    }
}
