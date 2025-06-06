using System;
public static class Fonctions
{
    // ==================== MÉTHODE VIGENÈRE ====================

    /// <summary>
    /// Crypte un texte avec la méthode de Vigenère
    /// La clé ne doit pas être plus longue que le texte
    /// </summary>
    /// <param name="texte">Le texte à crypter (sans espaces ni accents)</param>
    /// <param name="cle">La clé (sans espaces ni accents, pas plus longue que le texte)</param>
    /// <returns>Le texte crypté</returns>
    public static string CrypterVigenere(string texte, string cle)
    {
        // On nettoie le texte et la clé
        string texteNettoye = NettoyerTexte(texte);
        string cleNettoyee = NettoyerTexte(cle);

        // On vérifie que la clé n'est pas trop longue
        if (cleNettoyee.Length > texteNettoye.Length)
        {
            return "ERREUR: La clé est trop longue !";
        }

        // Si le texte est vide après nettoyage
        if (texteNettoye.Length == 0)
        {
            return "ERREUR: Texte vide après nettoyage !";
        }

        string resultat = "";

        // On crypte chaque lettre
        for (int i = 0; i < texteNettoye.Length; i++)
        {
            char lettreTexte = texteNettoye[i];
            char lettreCle = cleNettoyee[i % cleNettoyee.Length];

            // On calcule le décalage
            int decalage = lettreCle - 'A';

            // On crypte la lettre
            int position = lettreTexte - 'A';
            int nouvellePosition = (position + decalage) % 26;
            char lettreCryptee = (char)(nouvellePosition + 'A');

            resultat = resultat + lettreCryptee;
        }

        return resultat;
    }

    /// <summary>
    /// Décrypte un texte crypté avec Vigenère
    /// </summary>
    /// <param name="texte">Le texte crypté</param>
    /// <param name="cle">La clé utilisée pour crypter</param>
    /// <returns>Le texte décrypté</returns>
    public static string DecrypterVigenere(string texte, string cle)
    {
        string cleNettoyee = NettoyerTexte(cle);
        string resultat = "";

        for (int i = 0; i < texte.Length; i++)
        {
            char lettreTexte = texte[i];
            char lettreCle = cleNettoyee[i % cleNettoyee.Length];

            // On calcule le décalage (en négatif pour décrypter)
            int decalage = lettreCle - 'A';

            // On décrypte la lettre
            int position = lettreTexte - 'A';
            int nouvellePosition = (position - decalage + 26) % 26;
            char lettreDecryptee = (char)(nouvellePosition + 'A');

            resultat = resultat + lettreDecryptee;
        }

        return resultat;
    }

    // ==================== MÉTHODE POLYBE ====================

    /// <summary>
    /// Crypte un texte avec le carré de Polybe
    /// J devient I (même case)
    /// </summary>
    /// <param name="texte">Le texte à crypter</param>
    /// <returns>Le texte crypté (coordonnées séparées par des espaces)</returns>
    public static string CrypterPolybe(string texte)
    {
        // Le carré de Polybe
        string[,] carre = {
            {"A", "B", "C", "D", "E"},
            {"F", "G", "H", "I", "K"},
            {"L", "M", "N", "O", "P"},
            {"Q", "R", "S", "T", "U"},
            {"V", "W", "X", "Y", "Z"}
        };

        string texteNettoye = NettoyerTexte(texte);
        string resultat = "";

        // On traite chaque lettre
        for (int i = 0; i < texteNettoye.Length; i++)
        {
            char lettre = texteNettoye[i];

            // J devient I
            if (lettre == 'J')
            {
                lettre = 'I';
            }

            // On cherche la lettre dans le carré
            bool trouve = false;
            for (int ligne = 0; ligne < 5; ligne++)
            {
                for (int colonne = 0; colonne < 5; colonne++)
                {
                    if (carre[ligne, colonne] == lettre.ToString())
                    {
                        // On ajoute les coordonnées
                        string coordonnees = (ligne + 1).ToString() + (colonne + 1).ToString();
                        resultat = resultat + coordonnees + " ";
                        trouve = true;
                        break;
                    }
                }
                if (trouve == true)
                {
                    break;
                }
            }
        }

        return resultat.Trim(); // On enlève l'espace à la fin
    }

    /// <summary>
    /// Décrypte un texte crypté avec Polybe
    /// </summary>
    /// <param name="texte">Le texte crypté (coordonnées avec espaces)</param>
    /// <returns>Le texte décrypté</returns>
    public static string DecrypterPolybe(string texte)
    {
        // Le même carré de Polybe
        string[,] carre = {
            {"A", "B", "C", "D", "E"},
            {"F", "G", "H", "I", "K"},
            {"L", "M", "N", "O", "P"},
            {"Q", "R", "S", "T", "U"},
            {"V", "W", "X", "Y", "Z"}
        };

        string resultat = "";
        string[] coordonnees = texte.Split(' ');

        // On traite chaque coordonnée
        for (int i = 0; i < coordonnees.Length; i++)
        {
            string coord = coordonnees[i];

            // On vérifie que c'est bien 2 chiffres
            if (coord.Length == 2)
            {
                // On récupère ligne et colonne
                int ligne = int.Parse(coord[0].ToString()) - 1;
                int colonne = int.Parse(coord[1].ToString()) - 1;

                // On vérifie que c'est valide
                if (ligne >= 0 && ligne < 5 && colonne >= 0 && colonne < 5)
                {
                    resultat = resultat + carre[ligne, colonne];
                }
            }
        }

        return resultat;
    }

    // ==================== MÉTHODE BAZERIES ====================

    /// <summary>
    /// Crypte un texte avec la méthode de Bazeries
    /// La clé ne doit pas être plus grande que le nombre de lettres du texte
    /// </summary>
    /// <param name="texte">Le texte à crypter</param>
    /// <param name="cle">La clé numérique</param>
    /// <returns>Le texte crypté</returns>
    public static string CrypterBazeries(string texte, int cle)
    {
        string texteNettoye = NettoyerTexte(texte);

        // On vérifie que la clé n'est pas trop grande
        if (cle > texteNettoye.Length)
        {
            return "ERREUR: La clé (" + cle + ") est plus grande que le nombre de lettres (" + texteNettoye.Length + ") !";
        }

        // Le décalage = clé au carré
        int decalage = cle * cle;
        string resultat = "";

        // On crypte chaque lettre
        for (int i = 0; i < texteNettoye.Length; i++)
        {
            char lettre = texteNettoye[i];

            // On calcule la nouvelle position
            int position = lettre - 'A';
            int nouvellePosition = (position + decalage) % 26;
            char lettreCryptee = (char)(nouvellePosition + 'A');

            resultat = resultat + lettreCryptee;
        }

        return resultat;
    }

    /// <summary>
    /// Décrypte un texte crypté avec Bazeries
    /// </summary>
    /// <param name="texte">Le texte crypté</param>
    /// <param name="cle">La clé numérique utilisée pour crypter</param>
    /// <returns>Le texte décrypté</returns>
    public static string DecrypterBazeries(string texte, int cle)
    {
        // Le même décalage que pour crypter
        int decalage = cle * cle;
        string resultat = "";

        // On décrypte chaque lettre
        for (int i = 0; i < texte.Length; i++)
        {
            char lettre = texte[i];

            // On calcule la position originale
            int position = lettre - 'A';
            int nouvellePosition = (position - decalage + 26) % 26;
            char lettreDecryptee = (char)(nouvellePosition + 'A');

            resultat = resultat + lettreDecryptee;
        }

        return resultat;
    }

    // ==================== FONCTIONS DE SAISIE ====================

    /// <summary>
    /// Demande un texte à l'utilisateur
    /// </summary>
    /// <param name="action">L'action à faire (ex: "à crypter")</param>
    /// <returns>Le texte saisi</returns>
    public static string DemanderTexte(string action)
    {
        string texte = "";

        // On répète tant que le texte n'est pas valide
        while (texte == "")
        {
            Console.Write("Entrez le texte " + action + " : ");
            texte = Console.ReadLine();

            // On vérifie que le texte n'est pas vide
            if (texte == null || texte == "")
            {
                Console.WriteLine("ERREUR : Le texte ne peut pas être vide !");
                texte = "";
            }
            // On vérifie que le texte contient au moins une lettre
            else if (EstTexteValide(texte) == false)
            {
                Console.WriteLine("ERREUR : Le texte doit contenir au moins une lettre !");
                texte = "";
            }
        }

        return texte;
    }

    /// <summary>
    /// Demande à l'utilisateur de choisir une méthode
    /// </summary>
    /// <returns>Le numéro de la méthode choisie</returns>
    public static string DemanderMethode()
    {
        string methode = "";

        // On répète tant que la méthode n'est pas valide
        while (methode == "")
        {
            Console.WriteLine("\nMéthodes disponibles :");
            Console.WriteLine("1. Vigenère (avec une clé mot)");
            Console.WriteLine("2. Polybe (carré 5x5)");
            Console.WriteLine("3. Bazeries (avec une clé nombre)");
            Console.Write("\nChoisissez une méthode (1, 2 ou 3) : ");

            methode = Console.ReadLine();

            // On vérifie que c'est un choix valide
            if (methode != "1" && methode != "2" && methode != "3")
            {
                Console.WriteLine("ERREUR : Choisissez 1, 2 ou 3 !");
                methode = "";
            }
        }

        return methode;
    }

    /// <summary>
    /// Demande une clé pour Vigenère
    /// </summary>
    /// <param name="texte">Le texte (pour vérifier la longueur)</param>
    /// <returns>La clé saisie</returns>
    public static string DemanderCleVigenere(string texte)
    {
        string cle = "";
        string texteNettoye = NettoyerTexte(texte);

        while (cle == "")
        {
            Console.Write("Entrez la clé (lettres seulement, max " + texteNettoye.Length + " lettres) : ");
            cle = Console.ReadLine();

            // On vérifie que la clé n'est pas vide
            if (cle == null || cle == "")
            {
                Console.WriteLine("ERREUR : La clé ne peut pas être vide !");
                cle = "";
            }
            // On vérifie que la clé est valide
            else if (EstCleValide(cle) == false)
            {
                Console.WriteLine("ERREUR : La clé doit contenir au moins une lettre !");
                cle = "";
            }
            // On vérifie que la clé n'est pas trop longue
            else if (NettoyerTexte(cle).Length > texteNettoye.Length)
            {
                Console.WriteLine("ERREUR : La clé est trop longue ! Maximum " + texteNettoye.Length + " lettres.");
                cle = "";
            }
        }

        return cle;
    }

    /// <summary>
    /// Demande une clé numérique pour Bazeries
    /// </summary>
    /// <param name="texte">Le texte (pour vérifier la taille)</param>
    /// <returns>La clé numérique</returns>
    public static int DemanderCleNumerique(string texte)
    {
        int cle = 0;
        bool cleValide = false;
        string texteNettoye = NettoyerTexte(texte);

        while (cleValide == false)
        {
            Console.Write("Entrez la clé numérique (max " + texteNettoye.Length + ") : ");
            string cleTexte = Console.ReadLine();

            // On essaie de convertir en nombre
            bool estNombre = int.TryParse(cleTexte, out cle);

            if (estNombre == false)
            {
                Console.WriteLine("ERREUR : Entrez un nombre valide !");
            }
            else if (cle <= 0)
            {
                Console.WriteLine("ERREUR : La clé doit être positive !");
            }
            else if (cle > texteNettoye.Length)
            {
                Console.WriteLine("ERREUR : La clé (" + cle + ") est trop grande ! Maximum " + texteNettoye.Length + ".");
            }
            else
            {
                cleValide = true;
            }
        }

        return cle;
    }

    // ==================== FONCTIONS D'AIDE ====================

    /// <summary>
    /// Nettoie un texte : enlève les espaces, accents et garde seulement les lettres A-Z
    /// </summary>
    /// <param name="texte">Le texte à nettoyer</param>
    /// <returns>Le texte nettoyé en majuscules</returns>
    public static string NettoyerTexte(string texte)
    {
        if (texte == null || texte == "")
        {
            return "";
        }

        string resultat = "";

        // On parcourt chaque caractère
        for (int i = 0; i < texte.Length; i++)
        {
            char c = texte[i];

            // On enlève les accents courants
            if (c == 'à' || c == 'á' || c == 'â' || c == 'ä' || c == 'À' || c == 'Á' || c == 'Â' || c == 'Ä')
            {
                c = 'A';
            }
            else if (c == 'è' || c == 'é' || c == 'ê' || c == 'ë' || c == 'È' || c == 'É' || c == 'Ê' || c == 'Ë')
            {
                c = 'E';
            }
            else if (c == 'ì' || c == 'í' || c == 'î' || c == 'ï' || c == 'Ì' || c == 'Í' || c == 'Î' || c == 'Ï')
            {
                c = 'I';
            }
            else if (c == 'ò' || c == 'ó' || c == 'ô' || c == 'ö' || c == 'Ò' || c == 'Ó' || c == 'Ô' || c == 'Ö')
            {
                c = 'O';
            }
            else if (c == 'ù' || c == 'ú' || c == 'û' || c == 'ü' || c == 'Ù' || c == 'Ú' || c == 'Û' || c == 'Ü')
            {
                c = 'U';
            }
            else if (c == 'ç' || c == 'Ç')
            {
                c = 'C';
            }

            // On garde seulement les lettres
            if (char.IsLetter(c) == true)
            {
                resultat = resultat + char.ToUpper(c);
            }
        }

        return resultat;
    }

    /// <summary>
    /// Vérifie qu'un texte ne contient que des lettres (après nettoyage)
    /// </summary>
    /// <param name="texte">Le texte à vérifier</param>
    /// <returns>True si le texte est valide, False sinon</returns>
    public static bool EstTexteValide(string texte)
    {
        string texteNettoye = NettoyerTexte(texte);
        return texteNettoye.Length > 0;
    }

    /// <summary>
    /// Vérifie qu'une clé ne contient que des lettres
    /// </summary>
    /// <param name="cle">La clé à vérifier</param>
    /// <returns>True si la clé est valide, False sinon</returns>
    public static bool EstCleValide(string cle)
    {
        string cleNettoyee = NettoyerTexte(cle);
        return cleNettoyee.Length > 0;
    }
}