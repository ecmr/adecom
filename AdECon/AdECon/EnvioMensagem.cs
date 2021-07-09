using System;
using System.Linq;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;



namespace AdECon
{
    public class EnvioMensagem
    {
        public static void EnvioSms()
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACbb8f1afad635225025b0b9265e3a3416"; // Environment.GetEnvironmentVariable("ACbb8f1afad635225025b0b9265e3a3416");
            string authToken = "4466dbaca9b1489f1c70c1909f4e52b8"; // Environment.GetEnvironmentVariable("4466dbaca9b1489f1c70c1909f4e52b8");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Condominio Residencial Aricanduva. Seu sedex chegou! Já pode vir buscar.",
                from: new Twilio.Types.PhoneNumber("+18178544536"),
                to: new Twilio.Types.PhoneNumber("+5511969410446")
            );

            Console.WriteLine(message.Sid);
        }

        public static void EnvioZap()
        {
            var accountSid = "AC34558a86426edfb17d4cb820906433fd";
            var authToken = "2cc547527fefd6bf53bdb75437a4530f";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+5511969410446"));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "Condominio Residencial Aricanduva. Seu sedex chegou! Já pode vir buscar.";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }

        public static void EnvioEmail()
        { }

    }
}
