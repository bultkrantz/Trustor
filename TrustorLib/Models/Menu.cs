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
        public static string WelcomeText => "               .__   __                                           __  .__.__  .__   \r\n___  _______  |  | |  | ______   _____   _____   ____   ____   _/  |_|__|  | |  |  \r\n\\  \\/ /\\__  \\ |  | |  |/ /  _ \\ /     \\ /     \\_/ __ \\ /    \\  \\   __\\  |  | |  |  \r\n \\   /  / __ \\|  |_|    <  <_> )  Y Y  \\  Y Y  \\  ___/|   |  \\  |  | |  |  |_|  |__\r\n  \\_/  (____  /____/__|_ \\____/|__|_|  /__|_|  /\\___  >___|  /  |__| |__|____/____/\r\n            \\/          \\/           \\/      \\/     \\/     \\/                      ";

        public static string Logo =>
            "   __                         __                 ___.                  __    \r\n_/  |________ __ __  _______/  |_  ___________  \\_ |__ _____    ____ |  | __\r\n\\   __\\_  __ \\  |  \\/  ___/\\   __\\/  _ \\_  __ \\  | __ \\\\__  \\  /    \\|  |/ /\r\n |  |  |  | \\/  |  /\\___ \\  |  | (  <_> )  | \\/  | \\_\\ \\/ __ \\|   |  \\    < \r\n |__|  |__|  |____//____  > |__|  \\____/|__|     |___  (____  /___|  /__|_ \\\r\n                        \\/                           \\/     \\/     \\/     \\/\r\n\r\n";
    }
}
