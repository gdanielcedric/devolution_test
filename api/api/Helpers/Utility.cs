using api.DTO.Abstract;
using api.Enums;
using api.Models;
using Humanizer;
using iText.Layout.Element;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace api.Helpers
{
    public class Utility
    {
        public static IConfiguration _config;

        public Utility(IConfiguration config) { _config = config; }

        // Permit to parse a date into Timestamp
        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        // Regex for phone
        public static bool isGoodPhone(string phone)
        {
            Regex validatePhoneNumberRegex = new Regex("^[0-9]{10,14}$");
            var isOkPhone = validatePhoneNumberRegex.IsMatch(phone);
            if (isOkPhone)
            {
                return true;
            }
            return false;
        }

        // To generate random string
        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }

        // To calculate Prime
        public static double CalculatePrime(Vehicle vehicle, List<string> garanties) 
        {
            double amount = 0;

            // check if garanties contain RC and calculate the appropriate prime to add global amount
            if (garanties.Contains(GuaranTypeEnum.RC.Humanize()))
            {
                var vg = new RCGuaranty
                {
                    FiscalPower = vehicle.FiscalPower,
                };

                amount += vg.CalculatePrime();
            }

            // check if garanties contain TIERCE_COLLISION and calculate the appropriate prime to add global amount
            if (garanties.Contains(GuaranTypeEnum.TIERCE_COLLISION.Humanize()))
            {
                var vg = new CollisionTierceGuaranty
                {
                    Value = vehicle.ValueNeuve,
                };

                amount += vg.CalculatePrime();
            }

            // check if garanties contain DAMAGES and calculate the appropriate prime to add global amount
            if (garanties.Contains(GuaranTypeEnum.DAMAGES.Humanize()))
            {
                var vg = new DamageGuaranty
                {
                    Value = vehicle.ValueNeuve,
                };

                amount += vg.CalculatePrime();
            }

            // check if garanties contain TIERCE PLAFONNEE and calculate the appropriate prime to add global amount
            if (garanties.Contains(GuaranTypeEnum.TIERCE_PLAFONNEE.Humanize()))
            {
                var vg = new PlafondTierceGuaranty
                {
                    Value = vehicle.ValueVenale,
                };

                amount += vg.CalculatePrime();
            }

            // check if garanties contain VOL and calculate the appropriate prime to add global amount
            if (garanties.Contains(GuaranTypeEnum.VOL.Humanize()))
            {
                var vg = new VolGuaranty
                {
                    Value = vehicle.ValueVenale,
                };

                amount += vg.CalculatePrime();
            }

            // check if garanties contain INCENDIE and calculate the appropriate prime to add global amount
            if (garanties.Contains(GuaranTypeEnum.INCENDIE.Humanize()))
            {
                var vg = new IncendieGuaranty
                {
                    Value = vehicle.ValueVenale,
                };

                amount += vg.CalculatePrime();
            }

            return amount;
        }

        // To generate PDF
        public static string GeneratePDF(Subscription subs, Suscriber susb, Vehicle vehicle, AssurProduct product)
        {
            return @$"<!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                      <meta charset=""utf-8"">
                      <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                      <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"">
                      <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js""></script>
                      <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js""></script>
                    </head>
                    <body>

                    <div class=""container"">
                      <h3>NSIAGO'ASSUR : {subs.QuoteReference}</h3>
                      <p>Cette attestation a pour but de faire valoir vos droits.</p>   
                      <h3>Souscripteur</h3>
                      <table class=""table"">
                        <tbody>
                          <tr>
                            <td>Nom & prenoms</td>
                            <td>{susb.Nom} {susb.Prenom}</td>
                          </tr>
                          <tr>
                            <td>Telephone</td>
                            <td>{susb.Telephone}</td>
                          </tr>
                          <tr>
                            <td>CNI</td>
                            <td>{susb.CNI}</td>
                          </tr>
                        </tbody>
                      </table>
                      <h3>Vehicule</h3>
                      <table class=""table"">
                        <tbody>
                          <tr>
                            <td>Immatriculation</td>
                            <td>{vehicle.ImmatriculationNumber}</td>
                          </tr>
                          <tr>
                            <td>Couleur</td>
                            <td>{vehicle.Couleur}</td>
                          </tr>
                          <tr>
                            <td>Nombre de portes</td>
                            <td>{vehicle.NombrePorte}</td>
                          </tr>
                           <tr>
                            <td>Nombre de sièges</td>
                            <td>{vehicle.NombreSiege}</td>
                          </tr>
                          <tr>
                            <td>Puissance fiscale</td>
                            <td>{vehicle.FiscalPower}</td>
                          </tr>
                          <tr>
                            <td>Valeur neuve</td>
                            <td>{vehicle.ValueNeuve}</td>
                          </tr>
                           <tr>
                            <td>Valeur venale</td>
                            <td>{vehicle.ValueVenale}</td>
                          </tr>
                        </tbody>
                      </table>
                      <h3>Produit Assurance</h3>
                      <table class=""table"">
                        <tbody>
                          <tr>
                            <td>Nom</td>
                            <td>{product.Name}</td>
                          </tr>
                        </tbody>
                      </table>
                      <h3>Prime</h3>
                      <table class=""table"">
                        <tbody>
                          <tr>
                            <td>Total</td>
                            <td><b>{subs.Price} XOF</b></td>
                          </tr>
                        </tbody>
                      </table>
                    </div>

                    </body>
                    </html>";
        }


    }
}
