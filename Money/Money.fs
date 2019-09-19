namespace Money


type CreditCardType =
| MasterCard
| Visa
//| AmericianExpress   // Uncomment to see errors because not being handled

type Payment =
| Cash of amount:int
| CreditCard of amount:int * CardNumber:string * CardType:CreditCardType
   
module Validation =
    let validateAmount a =
        if a >= 1 then Ok ()
                  else Error "We do not give refunds"

    let validate p = 
        let fine = Ok ()
        let invalid = Error "Invalid card type and number"

        match p with 
        | Cash(amount) -> validateAmount(amount)
        | CreditCard(amount,cardNo,cardType) ->
            match validateAmount(amount) with
            | Error _ as x -> x
            | Ok _ ->
                match(cardType) with
                | Visa -> 
                    if cardNo = "1234-5678-1234-5678" then fine else invalid
                | MasterCard ->
                    if cardNo = "8888-7777-6666-5555" then fine else invalid


    // Example linked list defined by recursive union type
    type LList<'t> =
    | End
    | Cell of 't * LList<'t>

    let ExampleListInstance = Cell(3,Cell(2,End))

    (* Example of currying in F# *)

    // Example addition function with 2 params
    let add a b = a + b
    // This defines a new function expecting just one param, by 'baking in' the 5 as the first parameter
    let addFive = add 5


open System.Net
open System.Net.Mail


module Email = 

    /// Send email via office 365
    let SendEmail toEmail body =
        let fromEmail = "dave.glassborow@myemail.co.uk"
        use msg = new MailMessage(fromEmail,toEmail)
        msg.Subject <- "New Payment"
        msg.Body <- body 
        use client = new SmtpClient("smtp.office365.com",587)
        client.EnableSsl <- true  
        client.Credentials <- NetworkCredential(fromEmail,"****")
        //client.Send(msg)
