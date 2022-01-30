using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Partnership.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public class EmailSender
    {
        TenderService tenderService = new TenderService(new TenderRepository());
        public void SendMailToWinningPharmacy(string pharmacyEmail, Tender tender)
        {
            TenderOffer offer = tender.TenderOffers.ElementAt(0);
            offer = tenderService.GetTenderOfferWithOfferItems(offer.PharmacyName, offer.TenderOfferHash);
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(pharmacyEmail, "Tender results"));
            mm.From = new MailAddress("pswklinika2022@gmail.com");
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<!DOCTYPE html>
                <html>
                    <head></head>
                    <body>
                        <p>You have successfully won " + tender.Title + " with offer: <br>");
            mm.Body = @"<!DOCTYPE html>
                <html>
                    <head></head>
                    <body>
                        <p>You have successfully won " + tender.Title + "with offer: <br>";
            foreach (OfferItem item in offer.OfferItems)
            {
                builder.Append(item.MedicineName + " " + item.Dosage + "mg, " + item.Quantity + "x " + item.Price + " $");
                builder.Append("<br>");
            }
            builder.Append(@"</p>
                         </body>
                             </html>");

            mm.Body = builder.ToString();
            mm.IsBodyHtml = true;
            mm.Subject = "Tender results";
            SmtpClient smcl = new SmtpClient();
            smcl.Host = "smtp.gmail.com";
            smcl.Port = 587;
            smcl.Credentials = new NetworkCredential("pswklinika2022@gmail.com", "srbija123!");
            smcl.EnableSsl = true;
            smcl.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smcl.Send(mm);
            }
            catch (SmtpException e)
            {
                Console.WriteLine("Error: {0}", e.StatusCode);
            }
        }

        public void SendMailToLosingPharmacy(String pharmacyEmail, Tender tender)
        {
            TenderOffer offer = tender.TenderOffers.ElementAt(0);
            offer = tenderService.GetTenderOfferWithOfferItems(offer.PharmacyName, offer.TenderOfferHash);
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(pharmacyEmail, "Tender results"));
            mm.From = new MailAddress("pswklinika2022@gmail.com");

            mm.Body = @"<!DOCTYPE html>
                <html>
                    <head></head>
                    <body>
                        <p>You have lost " + tender.Title + @"</p>
                    </body>
                </html>
            ";
            mm.IsBodyHtml = true;
            mm.Subject = "Tender results";
            SmtpClient smcl = new SmtpClient();
            smcl.Host = "smtp.gmail.com";
            smcl.Port = 587;
            smcl.Credentials = new NetworkCredential("pswklinika2022@gmail.com", "srbija123!");
            smcl.EnableSsl = true;
            smcl.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smcl.Send(mm);
            }
            catch (SmtpException e)
            {
                Console.WriteLine("Error: {0}", e.StatusCode);
            }
        }
    }
}
