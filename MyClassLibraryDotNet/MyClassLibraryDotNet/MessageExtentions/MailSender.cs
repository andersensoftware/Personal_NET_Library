using System;
using System.Net;
using System.Net.Mail;

namespace MyClassLibraryDotNet.MessageExtentions
{
    public class MailSender
    {
        public static void Send(string senderAddress, string toAddress, string toName, string subject, string body, string author, string server, ushort port, bool ssl, string senderName, string senderPass)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderAddress, author);
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = server;
            smtpClient.Port = port;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = ssl;
            smtpClient.Credentials = new NetworkCredential(senderName, senderPass);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

