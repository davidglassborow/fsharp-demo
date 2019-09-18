using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Money;
using Talk.MoneyC;

/* 1. Review email
 * 2. See email in https://sharplab.io
 * 3. Repl
 * 4. Money types in c# and f#
 * 5. Additional card type
 */

namespace Talk.Controllers
{
    public class HomeVM
    {
        public bool? IsValid { get; set; }
        public int Amount { get; set; }
        public string ValidationResult { get; set; }
        public string CardNumber { get; set; }
    }

    public class HomeController : Controller
    {

        public ActionResult Index(string payment = "", int amount = 0, int cardType = 1, string cardNumber = "1234-5678-1234-5678")
        {
            var vm = new HomeVM
            {
                Amount = amount,
                CardNumber = cardNumber,
                IsValid = null
            };

            if (Request.HttpMethod == "POST")
            {
                IPayment paid = null;
                if (payment == "cash")
                    paid = new CashPayment { Amount = amount };
                else if (payment == "creditcard")
                    paid = new CreditCard { Amount = amount, CardNumber = cardNumber, CardType = (MoneyC.CreditCardType)cardType };
                else
                    throw new Exception($"Unknown payment type {payment}");
                var (isValid, validationMessage) = paid.Validate();
                if (isValid)
                {
                    vm.IsValid = true;
                    vm.ValidationResult = "Valid";
                }
                else
                {
                    vm.IsValid = false;
                    vm.ValidationResult = validationMessage;
                }

                //Payment paid = null;
                //if (payment == "cash")
                //    paid = Payment.NewCash(amount);
                //else if (payment == "creditcard")
                //    paid = Payment.NewCreditCard(amount, cardNumber, cardType == 1 ? Money.CreditCardType.MasterCard : Money.CreditCardType.Visa);
                //else
                //    throw new Exception($"Unknown payment type {payment}");

                //var result = Validation.validate(paid);
                //if (result.IsOk)
                //{
                //    vm.IsValid = true;
                //    vm.ValidationResult = "Valid";
                //}
                //else
                //{
                //    vm.IsValid = false;
                //    vm.ValidationResult = result.ErrorValue;
                //}

            }
            return View(vm);
        }


    }
}
