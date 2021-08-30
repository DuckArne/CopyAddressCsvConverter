using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyAddressCsvConverter
{
    public class AdressbokClassMap : ClassMap<Adress>
    {
        public AdressbokClassMap()
        {
            Map(m => m.Förnamn).Name("Förnamn");
            Map(m => m.Efternamn).Name("Efternamn");
            Map(m => m.Visa).Name("Visa");
            Map(m => m.Smeknamn).Name("Smeknamn");
            Map(m => m.E_post).Name("E-post");
            Map(m => m.Ytterligaree_post).Name("Ytterligare e-post");
            Map(m => m.Skärmnamn).Name("Skärmnamn");
            Map(m => m.Telefonarbete).Name("Telefon arbete");
            Map(m => m.Telefonprivat).Name("Telefon privat");
            Map(m => m.Faxnummer).Name("Faxnummer");
            Map(m => m.Personsökare).Name("Personsökare");
            Map(m => m.Mobiltelefon).Name("Mobiltelefon");
            Map(m => m.Adressprivatrad1).Name("Adress privat (rad 1)");
            Map(m => m.Adressprivatrad2).Name("Adress privat (rad 2)");
            Map(m => m.Stadprivat).Name("Stad privat");
            Map(m => m.LandsdelStatprivat).Name("Landsdel (Stat) privat");
            Map(m => m.Postnummerprivat).Name("Postnummer privat");
            Map(m => m.Landprivat).Name("Land privat");
            Map(m => m.Adressarbeterad1).Name("Adress arbete (rad 1)");
            Map(m => m.Adressarbeterad2).Name("Adress arbete (rad 2)");
            Map(m => m.Stadarbete).Name("Stad arbete");
            Map(m => m.LandsdelStatarbete).Name("Landsdel (Stat) arbete");
            Map(m => m.Postnummerarbete).Name("Postnummer arbete");
            Map(m => m.Landarbete).Name("Land arbete");
            Map(m => m.Titel).Name("Titel");
            Map(m => m.Avdelning).Name("Avdelning");
            Map(m => m.Organisation).Name("Organisation");
            Map(m => m.Webbplatsprivat).Name("Webbplats privat");
            Map(m => m.Webbplatsarbete).Name("Webbplats arbete");
            Map(m => m.Föddår).Name("Född år");
            Map(m => m.Föddmånad).Name("Född månad");
            Map(m => m.Födddag).Name("Född dag");
            Map(m => m.Annanuppgift1).Name("Annan uppgift 1");
            Map(m => m.Annanuppgift2).Name("Annan uppgift 1");
            Map(m => m.Annanuppgift3).Name("Annan uppgift 1");
            Map(m => m.Annanuppgift4).Name("Annan uppgift 1");
            Map(m => m.Anteckningar).Name("Anteckningar");
        }
    }
}
