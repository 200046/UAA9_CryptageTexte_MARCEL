using System;

/// <summary>
/// Programme principal pour l'application de cryptage
/// Auteur: TASNIER MARCEL
/// Date: 06/06/2025
/// Gère uniquement l'affichage et les interactions avec l'utilisateur
/// </summary>
class Program
{
    // Variable pour stocker le pseudo de l'utilisateur
    private static string pseudoUtilisateur = "";

    /// <summary>
    /// Point d'entrée du programme
    /// </summary>
    static void Main(string[] args)
    {
        // On affiche le titre
        AfficherTitre();

        // On demande le pseudo
        pseudoUtilisateur = DemanderPseudo();
        Console.WriteLine("Bienvenue " + pseudoUtilisateur + " !");

        // Boucle principale du programme
        bool continuer = true;
        while (continuer == true)
        {
            continuer = AfficherMenuEtTraiter();

            // Si on continue, on demande si l'utilisateur veut faire autre chose
            if (continuer == true)
            {
                continuer = DemanderSiContinuer();
            }
        }

        // Message de fin
        AfficherMessageFin();
    }

    /// <summary>
    /// Affiche le titre de l'application
    /// </summary>
    private static void AfficherTitre()
    {
        Console.WriteLine("================================");
        Console.WriteLine("   APPLICATION DE CRYPTAGE");
        Console.WriteLine("================================");
    }

    /// <summary>
    /// Demande le pseudo à l'utilisateur
    /// </summary>
    /// <returns>Le pseudo saisi</returns>
    private static string DemanderPseudo()
    {
        Console.Write("\nEntrez votre pseudo : ");
        string pseudo = Console.ReadLine();

        // Si l'utilisateur n'a rien tapé, on met un pseudo par défaut
        if (pseudo == null || pseudo == "")
        {
            pseudo = "Utilisateur";
        }

        return pseudo;
    }

    /// <summary>
    /// Affiche le menu principal et traite le choix de l'utilisateur
    /// </summary>
    /// <returns>True si on continue le programme, False si on quitte</returns>
    private static bool AfficherMenuEtTraiter()
    {
        // Affichage du menu
        Console.WriteLine("\n--- MENU PRINCIPAL ---");
        Console.WriteLine("1. Crypter un texte");
        Console.WriteLine("2. Décrypter un texte");
        Console.WriteLine("3. Quitter le programme");
        Console.Write("\nVotre choix (1, 2 ou 3) : ");

        // Lecture du choix
        string choix = Console.ReadLine();

        // Traitement du choix
        if (choix == "1")
        {
            FaireCryptage();
            return true; // On continue
        }
        else if (choix == "2")
        {
            FaireDecryptage();
            return true; // On continue
        }
        else if (choix == "3")
        {
            return false; // On arrête
        }
        else
        {
            Console.WriteLine("ERREUR : Choisissez 1, 2 ou 3 !");
            return true; // On continue
        }
    }

    /// <summary>
    /// Gère le cryptage d'un texte
    /// </summary>
    private static void FaireCryptage()
    {
        Console.WriteLine("\n=== CRYPTAGE ===");

        // On demande le texte (maintenant dans Fonctions.cs)
        string texte = Fonctions.DemanderTexte("à crypter");

        // On demande la méthode (maintenant dans Fonctions.cs)
        string methode = Fonctions.DemanderMethode();

        // On fait le cryptage selon la méthode
        string resultat = "";

        if (methode == "1")
        {
            string cle = Fonctions.DemanderCleVigenere(texte);
            resultat = Fonctions.CrypterVigenere(texte, cle);
        }
        else if (methode == "2")
        {
            resultat = Fonctions.CrypterPolybe(texte);
        }
        else if (methode == "3")
        {
            int cle = Fonctions.DemanderCleNumerique(texte);
            resultat = Fonctions.CrypterBazeries(texte, cle);
        }

        // On affiche le résultat
        AfficherResultat(resultat, true);
    }

    /// <summary>
    /// Gère le décryptage d'un texte
    /// </summary>
    private static void FaireDecryptage()
    {
        Console.WriteLine("\n=== DÉCRYPTAGE ===");

        // On demande le texte (maintenant dans Fonctions.cs)
        string texte = Fonctions.DemanderTexte("à décrypter");

        // On demande la méthode (maintenant dans Fonctions.cs)
        string methode = Fonctions.DemanderMethode();

        // On fait le décryptage selon la méthode
        string resultat = "";

        if (methode == "1")
        {
            string cle = Fonctions.DemanderCleVigenere(texte);
            resultat = Fonctions.DecrypterVigenere(texte, cle);
        }
        else if (methode == "2")
        {
            resultat = Fonctions.DecrypterPolybe(texte);
        }
        else if (methode == "3")
        {
            int cle = Fonctions.DemanderCleNumerique(texte);
            resultat = Fonctions.DecrypterBazeries(texte, cle);
        }

        // On affiche le résultat
        AfficherResultat(resultat, false);
    }

    /// <summary>
    /// Affiche le résultat d'une opération
    /// </summary>
    /// <param name="resultat">Le résultat à afficher</param>
    /// <param name="estCryptage">True si c'est un cryptage, False si c'est un décryptage</param>
    private static void AfficherResultat(string resultat, bool estCryptage)
    {
        Console.WriteLine("\n=== RÉSULTAT ===");

        // Si c'est une erreur (commence par "ERREUR")
        if (resultat.StartsWith("ERREUR"))
        {
            Console.WriteLine(resultat);
        }
        else
        {
            // Affichage normal
            if (estCryptage == true)
            {
                Console.WriteLine("Texte crypté :");
            }
            else
            {
                Console.WriteLine("Texte décrypté :");
            }

            Console.WriteLine(resultat);
        }
    }

    /// <summary>
    /// Demande à l'utilisateur s'il veut continuer
    /// </summary>
    /// <returns>True si l'utilisateur veut continuer, False sinon</returns>
    private static bool DemanderSiContinuer()
    {
        Console.Write("\nVoulez-vous faire autre chose ? (o/n) : ");
        string reponse = Console.ReadLine();

        if (reponse == "o" || reponse == "O" || reponse == "oui" || reponse == "OUI")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Affiche le message de fin du programme
    /// </summary>
    private static void AfficherMessageFin()
    {
        Console.WriteLine("\n================================");
        Console.WriteLine("Merci " + pseudoUtilisateur + " d'avoir utilisé notre application !");
        Console.WriteLine("À bientôt !");
        Console.WriteLine("================================");

        Console.WriteLine("\nAppuyez sur une touche pour fermer...");
        Console.ReadKey();
    }
}
