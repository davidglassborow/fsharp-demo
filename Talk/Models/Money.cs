using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;


namespace Talk.MoneyC
{
    public interface IPayment
    {
        (bool, string) Validate();
    }

    public class CashPayment : IPayment
    {
        public int Amount { get; set; }

        public (bool, string) Validate() =>
            Amount >= 1 ? (true, "") : (false, "We do not give refunds");
    }

    public enum CreditCardType
    {
        Mastercard = 1,
        Visa = 2
    }


    public class CreditCard : IPayment
    {

        public int Amount { get; set; }
        public string CardNumber { get; set; }
        public CreditCardType CardType { get; set; }

        public (bool, string) Validate()
        {
            if (Amount < 1)
                return (false, "We do not give refunds");

            if (CardNumber == "1234-5678-1234-5678" && CardType == CreditCardType.Visa)
                return (true, "");
            else if (CardNumber == "8888-7777-6666-5555" && CardType == CreditCardType.Mastercard)
                return (true, "");
            else
                return (false, "Invalid card type and number");
        }
    }








    public static class Emailer
    {
        /// <summary>
        /// Send email via office 365
        /// </summary>
        public static void SendEmail(string toEmail, string body)
        {
            var fromEmail = "dave.glassborow@myemail.co.uk";
            using (var msg = new MailMessage(fromEmail, toEmail))
            {
                msg.Subject = "New Payment";
                msg.Body = body;
                using (var client = new SmtpClient("smtp.office365.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(fromEmail, "*****");
                    //client.Send(msg);
                }
            }
        }

    }

}