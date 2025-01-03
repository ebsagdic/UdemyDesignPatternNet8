﻿using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class SendEmailProcessHandler:ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public SendEmailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }

        public override object Handle(object o)
        {
            var zipMemoryStream = o as MemoryStream;
            zipMemoryStream.Position = 0;
            var mailMessage = new MailMessage();

            var smptClient = new SmtpClient("smtp-mail.outlook.com");

            mailMessage.From = new MailAddress("deneme@kariyersistem.com");

            mailMessage.To.Add(new MailAddress(_toEmail));

            mailMessage.Subject = "Zip dosyası";

            mailMessage.Body = "<p>Zip dosyası ektedir.</p>";

            Attachment attachment = new Attachment(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);

            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;

            smptClient.Port = 465;

            smptClient.EnableSsl = true;

            smptClient.Timeout = 10000; 

            smptClient.Credentials = new NetworkCredential("deneme@kariyersistem.com", "Password12*");

            smptClient.Send(mailMessage);

            return base.Handle(o);
        }
    }
}
