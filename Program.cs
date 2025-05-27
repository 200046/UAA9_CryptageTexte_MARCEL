using System;

class Program
{
    static void Main(string[] args)
    {
        // On affiche le titre de l'application
        AfficherTitre();

        // On demande le pseudo de l'utilisateur
        string pseudo = DemanderPseudo();
        Console.WriteLine("\nBienvenue " + pseudo + " !");

        // Variable pour savoir si on continue
        bool continuer = true;

        // Boucle principale du programme
        while (continuer == true)
        {
            continuer = TraiterMenuPrincipal();

            // Si l'utilisateur n'a pas choisi de quitter, on demande s'il veut continuer
            if (continuer == true)
            {
                continuer = DemanderContinuer();
            }
        }

        // Message de fin
        AfficherMessageFin(pseudo);
    }

    /// <summary>
    /// Affiche le titre de l'application.
    /// </summary>
    private static void AfficherTitre()
    {
        Console.WriteLine("=== APPLICATION DE CRYPTAGE ===");
        Console.WriteLine("===============================");
    }

    /// <summary>
    /// Demande et récupère le pseudo de l'utilisateur.
    /// </summary>
    /// <returns>Le pseudo saisi par l'utilisateur</returns>
    private static string DemanderPseudo()
    {
        Console.Write("\nEntrez votre pseudo : ");
        string pseudo = Console.ReadLine();
        return pseudo;
    }

    /// <summary>
    /// Affiche le menu principal et traite le choix de l'utilisateur.
    /// </summary>
    /// <returns>True si l'utilisateur veut continuer, False s'il veut quitter</returns>
    private static bool TraiterMenuPrincipal()
    {
        // On affiche le menu
        AfficherMenuPrincipal();

        // On lit le choix de l'utilisateur
        string choix = Console.ReadLine();

        // On traite le choix
        if (choix == "1")
        {
            TraiterOperationCryptage(true); // true = cryptage
            return true;
        }
        else if (choix == "2")
        {
            TraiterOperationCryptage(false); // false = décryptage
            return true;
        }
        else if (choix == "3")
        {
            return false; // L'utilisateur veut quitter
        }
        else
        {
            Console.WriteLine("Choix invalide !");
            return true;
        }
    }

    /// <summary>
    /// Affiche le menu principal avec les options disponibles.
    /// </summary>
    private static void AfficherMenuPrincipal()
    {
        Console.WriteLine("\nMENU PRINCIPAL :");
        Console.WriteLine("1. Crypter un texte");
        Console.WriteLine("2. Décrypter un texte");
        Console.WriteLine("3. Quitter");
        Console.Write("\nVotre choix (1-3) : ");
    }

    /// <summary>
    /// Traite une opération de cryptage ou décryptage.
    /// </summary>
    /// <param name="estCryptage">True pour crypter, False pour décrypter</param>
    private static void TraiterOperationCryptage(bool estCryptage)
    {
        // On affiche le titre de l'opération
        AfficherTitreOperation(estCryptage);

        // On demande le texte à traiter
        string texte = DemanderTexte(estCryptage);

        // On demande la méthode
        string methode = DemanderMethode();

        // On exécute l'opération
        string resultat = ExecuterOperation(texte, methode, estCryptage);

        // Si on a un résultat, on l'affiche
        if (resultat != "")
        {
            AfficherResultat(resultat, estCryptage);
        }
    }

    /// <summary>
    /// Affiche le titre de l'opération en cours.
    /// </summary>
    /// <param name="estCryptage">True pour cryptage, False pour décryptage</param>
    private static void AfficherTitreOperation(bool estCryptage)
    {
        if (estCryptage == true)
        {
            Console.WriteLine("\n=== CRYPTAGE ===");
        }
        else
        {
            Console.WriteLine("\n=== DÉCRYPTAGE ===");
        }
    }

    /// <summary>
    /// Demande et valide le texte à traiter.
    /// </summary>
    /// <param name="estCryptage">True pour cryptage, False pour décryptage</param>
    /// <returns>Le texte saisi par l'utilisateur</returns>
    private static string DemanderTexte(bool estCryptage)
    {
        string texte = "";

        // On répète tant que le texte est vide
        while (texte == "")
        {
            // On affiche le message selon l'opération
            if (estCryptage == true)
            {
                Console.Write("\nEntrez le texte à crypter : ");
            }
            else
            {
                Console.Write("\nEntrez le texte à décrypter : ");
            }

            texte = Console.ReadLine();

            // Si le texte est vide, on affiche une erreur
            if (texte == "")
            {
                Console.WriteLine("Erreur : le texte ne peut pas être vide !");
            }
        }

        return texte;
    }

    /// <summary>
    /// Demande à l'utilisateur de choisir une méthode de cryptage.
    /// </summary>
    /// <returns>Le numéro de la méthode choisie</returns>
    private static string DemanderMethode()
    {
        Console.WriteLine("\nMéthodes disponibles :");
        Console.WriteLine("1. Vigenère");
        Console.WriteLine("2. Polybe");
        Console.WriteLine("3. Bazeries");
        Console.Write("\nChoisissez une méthode (1-3) : ");

        string methode = Console.ReadLine();
        return methode;
    }

    /// <summary>
    /// Exécute l'opération de cryptage/décryptage selon la méthode choisie.
    /// </summary>
    /// <param name="texte">Le texte à traiter</param>
    /// <param name="methode">La méthode choisie (1, 2 ou 3)</param>
    /// <param name="estCryptage">True pour crypter, False pour décrypter</param>
    /// <returns>Le résultat de l'opération ou une chaîne vide en cas d'erreur</returns>
    private static string ExecuterOperation(string texte, string methode, bool estCryptage)
    {
        if (methode == "1")
        {
            return TraiterVigenere(texte, estCryptage);
        }
        else if (methode == "2")
        {
            return TraiterPolybe(texte, estCryptage);
        }
        else if (methode == "3")
        {
            return TraiterBazeries(texte, estCryptage);
        }
        else
        {
            Console.WriteLine("Méthode invalide !");
            return "";
        }
    }

    /// <summary>
    /// Traite l'opération avec la méthode Vigenère.
    /// </summary>
    /// <param name="texte">Le texte à traiter</param>
    /// <param name="estCryptage">True pour crypter, False pour décrypter</param>
    /// <returns>Le résultat de l'opération</returns>
    private static string TraiterVigenere(string texte, bool estCryptage)
    {
        Console.Write("\nEntrez la clé (un mot) : ");
        string cle = Console.ReadLine();

        string resultat = "";

        if (estCryptage == true)
        {
            resultat = Fonctions.CrypterVigenere(texte, cle);
        }
        else
        {
            resultat = Fonctions.DecrypterVigenere(texte, cle);
        }

        return resultat;
    }

    /// <summary>
    /// Traite l'opération avec la méthode Polybe.
    /// </summary>
    /// <param name="texte">Le texte à traiter</param>
    /// <param name="estCryptage">True pour crypter, False pour décrypter</param>
    /// <returns>Le résultat de l'opération</returns>
    private static string TraiterPolybe(string texte, bool estCryptage)
    {
        string resultat = "";

        if (estCryptage == true)
        {
            resultat = Fonctions.CrypterPolybe(texte);
        }
        else
        {
            resultat = Fonctions.DecrypterPolybe(texte);
        }

        return resultat;
    }

    /// <summary>
    /// Traite l'opération avec la méthode Bazeries.
    /// </summary>
    /// <param name="texte">Le texte à traiter</param>
    /// <param name="estCryptage">True pour crypter, False pour décrypter</param>
    /// <returns>Le résultat de l'opération</returns>
    private static string TraiterBazeries(string texte, bool estCryptage)
    {
        Console.Write("\nEntrez la clé (un nombre) : ");
        string cleTexte = Console.ReadLine();

        // On vérifie que c'est bien un nombre
        int cle = 0;
        bool nombreValide = int.TryParse(cleTexte, out cle);

        // On répète tant que ce n'est pas un nombre valide
        while (nombreValide == false)
        {
            Console.Write("Erreur : entrez un nombre valide : ");
            cleTexte = Console.ReadLine();
            nombreValide = int.TryParse(cleTexte, out cle);
        }

        string resultat = "";

        if (estCryptage == true)
        {
            resultat = Fonctions.CrypterBazeries(texte, cle);
        }
        else
        {
            resultat = Fonctions.DecrypterBazeries(texte, cle);
        }

        return resultat;
    }

    /// <summary>
    /// Affiche le résultat de l'opération.
    /// </summary>
    /// <param name="resultat">Le résultat à afficher</param>
    /// <param name="estCryptage">True pour cryptage, False pour décryptage</param>
    private static void AfficherResultat(string resultat, bool estCryptage)
    {
        Console.WriteLine("\n=== RÉSULTAT ===");

        if (estCryptage == true)
        {
            Console.WriteLine("Texte crypté : ");
        }
        else
        {
            Console.WriteLine("Texte décrypté : ");
        }

        Console.WriteLine(resultat);
    }

    /// <summary>
    /// Demande à l'utilisateur s'il veut continuer.
    /// </summary>
    /// <returns>True si l'utilisateur veut continuer, False sinon</returns>
    private static bool DemanderContinuer()
    {
        Console.Write("\nVoulez-vous faire une autre opération ? (o/n) : ");
        string reponse = Console.ReadLine();

        if (reponse == "o" || reponse == "O")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Affiche le message de fin de l'application.
    /// </summary>
    /// <param name="pseudo">Le pseudo de l'utilisateur</param>
    private static void AfficherMessageFin(string pseudo)
    {
        Console.WriteLine("\nMerci " + pseudo + " d'avoir utilisé notre application !");
    }
}