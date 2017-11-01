using System;
using System.Collections.Generic;
using System.Text;

namespace TrustorLib.Models
{
    public static class Menu
    {
        public static string MenuText =>
                "HUVUDMENY " +
                "\n 0) Avsluta och spara " +
                "\n 1) Sök kund " +
                "\n 2) Visa kundbild " +
                "\n 3) Skapa kund " +
                "\n 4) Ta bort kund " +
                "\n 5) Skapa konto " +
                "\n 6) Ta bort konto " +
                "\n 7) Insättning " +
                "\n 8) Uttag " +
                "\n 9) Överföring";
        public static string WelcomeText => "******************************* VÄLKOMMEN TILL BANKAPP 1.0 *******************************\n\n";
    }
}
