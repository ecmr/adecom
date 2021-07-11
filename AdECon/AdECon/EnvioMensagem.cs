using AdECon.Model;
using System;
using System.Net.Mail;
using System.Reactive.Subjects;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using whatsapp.dotnet;

namespace AdECon
{
    public class EnvioMensagem
    {
        #region 
        //private ws _ws;
        private readonly Subject<Notification> rxSubject = new();
        delegate void SetTextMessage(string message);
        delegate void SetChangeMode(bool value);
        #endregion

        public static void EnvioSms(Morador morador)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACbb8f1afad635225025b0b9265e3a3416"; // Environment.GetEnvironmentVariable("ACbb8f1afad635225025b0b9265e3a3416");
            string authToken = "4466dbaca9b1489f1c70c1909f4e52b8"; // Environment.GetEnvironmentVariable("4466dbaca9b1489f1c70c1909f4e52b8");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Condominio Residencial Aricanduva. Seu sedex chegou! Já pode vir buscar.",
                from: new Twilio.Types.PhoneNumber("+18178544536"),
                to: new Twilio.Types.PhoneNumber(string.Concat("+55", morador.NumeroCelular))
            );

            Console.WriteLine(message.Sid);
        }

        public static void EnvioSmsTeste(Morador morador)
        {
            string toPhoneNumber = "+5511" + morador.NumeroCelular;
            string login = "SeuLogin";
            string password = "SuaSenha";
            string compression = "assunto";
            string body = "Condominio Residencial Aricanduva. Seu sedex chegou! Já pode vir buscar.";

            try
            {
                try
                {
                    MailMessage Message = new MailMessage();
                    Message.From = new MailAddress(login + "@ipipi.com");
                    Message.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                    Message.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", login);
                    Message.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
                    Message.Subject = compression;
                    Message.Body = body;

                    try
                    {
                        System.Net.Mail.SmtpClient smtpClient = new("ipipi.com");
                        smtpClient.Send(Message);
                    }
                    catch (Exception ehttp)
                    {
                        Console.WriteLine("{0}", ehttp.Message);
                        Console.WriteLine("Here is the full error message output");
                        Console.Write("{0}", ehttp.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }          
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Unknown Exception occurred {0}", e.Message);
                Console.WriteLine("Here is the Full Message output");
                Console.WriteLine("{0}", e.ToString());
            }
        }

        public static void EnvioZap(Morador morador)
        {
            #region
            var accountSid = "AC34558a86426edfb17d4cb820906433fd";
            var authToken = "2cc547527fefd6bf53bdb75437a4530f";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+55" + morador.NumeroCelular));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "Condominio Residencial Aricanduva. Sua camiseta do TIMÃO chegou! Já pode vir buscar.";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            #endregion
        }

        public static void EnvioEmail()
        { }
    }
}
