using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== APPLICATION DE CRYPTAGE ===");
        Console.WriteLine("===============================");

        // Demander le pseudo
        Console.Write("\nEntrez votre pseudo : ");
        string pseudo = Console.ReadLine();
        Console.WriteLine("\nBienvenue " + pseudo + " !");

        bool continuer = true;

        while (continuer)
        {
            Console.WriteLine("\nMENU PRINCIPAL :");
            Console.WriteLine("1. Crypter un texte");
            Console.WriteLine("2. Décrypter un texte");
            Console.WriteLine("3. Quitter");
            Console.Write("\nVotre choix (1-3) : ");

            string choix = Console.ReadLine();

            if (choix == "1" || choix == "2")
            {
                bool estCryptage = choix == "1";
                string operation = estCryptage ? "CRYPTAGE" : "DÉCRYPTAGE";

                Console.WriteLine("\n=== " + operation + " ===");

                // Saisie du texte
                string texte = "";
                while (texte == "")
                {
                    Console.Write("\nEntrez le texte à " + (estCryptage ? "crypter" : "décrypter") + " : ");
                    texte = Console.ReadLine();
                    if (texte == "") Console.WriteLine("Erreur : le texte ne peut pas être vide !");
                }

                // Choix de la méthode
                Console.WriteLine("\nMéthodes disponibles :");
                Console.WriteLine("1. Vigenère");
                Console.WriteLine("2. Polybe");
                Console.WriteLine("3. Bazeries");
                Console.Write("\nChoisissez une méthode (1-3) : ");
                string methode = Console.ReadLine();

                // Traitement selon la méthode
                string resultat = "";
                if (methode == "1") // Vigenère
                {
                    Console.Write("\nEntrez la clé (un mot) : ");
                    string cle = Console.ReadLine();
                    resultat = estCryptage
                        ? Fonctions.CryptVigenere(texte, cle)
                        : Fonctions.DecryptVigenere(texte, cle);
                }
                else if (methode == "2") // Polybe
                {
                    resultat = estCryptage
                        ? Fonctions.CryptPolybe(texte)
                        : Fonctions.DecryptPolybe(texte);
                }
                else if (methode == "3") // Bazeries
                {
                    Console.Write("\nEntrez la clé (un nombre) : ");
                    int cle;
                    while (!int.TryParse(Console.ReadLine(), out cle))
                    {
                        Console.Write("Erreur : entrez un nombre valide : ");
                    }
                    resultat = estCryptage
                        ? Fonctions.CryptBazeries(texte, cle)
                        : Fonctions.DecryptBazeries(texte, cle);
                }
                else
                {
                    Console.WriteLine("Méthode invalide !");
                    continue;
                }

                // Affichage du résultat
                Console.WriteLine("\n=== RÉSULTAT ===");
                Console.WriteLine(estCryptage ? "Texte crypté : " : "Texte décrypté : ");
                Console.WriteLine(resultat);
            }
            else if (choix == "3")
            {
                continuer = false;
            }
            else
            {
                Console.WriteLine("Choix invalide !");
            }

            // Demander si on veut continuer
            if (continuer)
            {
                Console.Write("\nVoulez-vous faire une autre opération ? (o/n) : ");
                continuer = Console.ReadLine().ToLower() == "o";
            }
        }

        Console.WriteLine("\nMerci " + pseudo + " d'avoir utilisé notre application !");
    }
}