using System;
public static class Fonctions
{
    // ==================== MÉTHODE VIGENÈRE ====================

    /// <summary>
    /// Crypte un texte en utilisant la méthode de Vigenère.
    /// Cette méthode utilise une clé alphabétique qui se répète.
    /// </summary>
    /// <param name="texte">Le texte à crypter</param>
    /// <param name="cle">La clé de cryptage (mot)</param>
    /// <returns>Le texte crypté en majuscules</returns>
    public static string CrypterVigenere(string texte, string cle)
    {
        string resultat = "";
        string cleMajuscule = cle.ToUpper(); // On met la clé en majuscules
        int indexCle = 0; // Position actuelle dans la clé

        // On parcourt chaque caractère du texte
        for (int i = 0; i < texte.Length; i++)
        {
            string caractere = texte[i].ToString();
            string caractereEnMajuscule = caractere.ToUpper();

            // Si c'est une lettre, on la crypte
            if (char.IsLetter(texte[i]) == true)
            {
                // On récupère la lettre de la clé pour ce caractère
                string lettreClePourCeCaractere = cleMajuscule[indexCle % cleMajuscule.Length].ToString();

                // On calcule le décalage (A=0, B=1, C=2, etc.)
                int decalage = lettreClePourCeCaractere[0] - 'A';

                // On crypte la lettre
                int positionLettre = caractereEnMajuscule[0] - 'A'; // Position de 0 à 25
                int nouvellePosition = (positionLettre + decalage) % 26; // On ajoute le décalage
                string lettreCryptee = ((char)(nouvellePosition + 'A')).ToString(); // On reconvertit en lettre

                resultat = resultat + lettreCryptee;
                indexCle = indexCle + 1; // On avance dans la clé
            }
            else
            {
                // Si ce n'est pas une lettre, on garde tel quel
                resultat = resultat + caractere;
            }
        }

        return resultat;
    }

    /// <summary>
    /// Décrypte un texte crypté avec la méthode de Vigenère.
    /// </summary>
    /// <param name="texte">Le texte crypté à décrypter</param>
    /// <param name="cle">La clé de décryptage (même que pour le cryptage)</param>
    /// <returns>Le texte décrypté en majuscules</returns>
    public static string DecrypterVigenere(string texte, string cle)
    {
        string resultat = "";
        string cleMajuscule = cle.ToUpper();
        int indexCle = 0;

        // On parcourt chaque caractère du texte crypté
        for (int i = 0; i < texte.Length; i++)
        {
            string caractere = texte[i].ToString();
            string caractereEnMajuscule = caractere.ToUpper();

            // Si c'est une lettre, on la décrypte
            if (char.IsLetter(texte[i]) == true)
            {
                // On récupère la lettre de la clé pour ce caractère
                string lettreClePourCeCaractere = cleMajuscule[indexCle % cleMajuscule.Length].ToString();

                // On calcule le décalage
                int decalage = lettreClePourCeCaractere[0] - 'A';

                // On décrypte la lettre (on enlève le décalage)
                int positionLettre = caractereEnMajuscule[0] - 'A';
                int nouvellePosition = (positionLettre - decalage + 26) % 26; // +26 pour éviter les négatifs
                string lettreDecryptee = ((char)(nouvellePosition + 'A')).ToString();

                resultat = resultat + lettreDecryptee;
                indexCle = indexCle + 1;
            }
            else
            {
                // Si ce n'est pas une lettre, on garde tel quel
                resultat = resultat + caractere;
            }
        }

        return resultat;
    }

    // ==================== MÉTHODE POLYBE ====================

    /// <summary>
    /// Crypte un texte en utilisant le carré de Polybe.
    /// Chaque lettre est remplacée par ses coordonnées dans un carré 5x5.
    /// Note : J est traité comme I (position 24).
    /// </summary>
    /// <param name="texte">Le texte à crypter</param>
    /// <returns>Le texte crypté sous forme de coordonnées séparées par des espaces</returns>
    public static string CrypterPolybe(string texte)
    {
        // Le carré de Polybe 5x5 (I et J partagent la case 24)
        string[,] carre = {
            {"A", "B", "C", "D", "E"},
            {"F", "G", "H", "I", "K"},
            {"L", "M", "N", "O", "P"},
            {"Q", "R", "S", "T", "U"},
            {"V", "W", "X", "Y", "Z"}
        };

        string resultat = "";

        // On parcourt chaque caractère du texte
        for (int i = 0; i < texte.Length; i++)
        {
            string caractere = texte[i].ToString().ToUpper();

            // Cas spécial pour J qui devient I (position 24)
            if (caractere == "J")
            {
                resultat = resultat + "24 ";
                continue; // On passe au caractère suivant
            }

            // On cherche la lettre dans le carré
            bool lettreTouvee = false;

            for (int ligne = 0; ligne < 5; ligne++)
            {
                for (int colonne = 0; colonne < 5; colonne++)
                {
                    // Si on trouve la lettre dans le carré
                    if (carre[ligne, colonne] == caractere)
                    {
                        // On ajoute les coordonnées (ligne+1, colonne+1)
                        int numeroLigne = ligne + 1;
                        int numeroColonne = colonne + 1;
                        string coordonnees = numeroLigne.ToString() + numeroColonne.ToString();
                        resultat = resultat + coordonnees + " ";
                        lettreTouvee = true;
                        break; // On sort de la boucle colonne
                    }
                }

                // Si on a trouvé la lettre, on sort aussi de la boucle ligne
                if (lettreTouvee == true)
                {
                    break;
                }
            }

            // Si ce n'est pas une lettre du carré, on garde tel quel
            if (lettreTouvee == false)
            {
                resultat = resultat + caractere + " ";
            }
        }

        // On enlève les espaces en trop à la fin
        resultat = resultat.Trim();
        return resultat;
    }

    /// <summary>
    /// Décrypte un texte crypté avec le carré de Polybe.
    /// </summary>
    /// <param name="texte">Le texte crypté (coordonnées séparées par des espaces)</param>
    /// <returns>Le texte décrypté</returns>
    public static string DecrypterPolybe(string texte)
    {
        // Le même carré de Polybe que pour le cryptage
        string[,] carre = {
            {"A", "B", "C", "D", "E"},
            {"F", "G", "H", "I", "K"},
            {"L", "M", "N", "O", "P"},
            {"Q", "R", "S", "T", "U"},
            {"V", "W", "X", "Y", "Z"}
        };

        string resultat = "";

        // On sépare le texte par les espaces pour avoir chaque code
        string[] parties = texte.Split(' ');

        // On traite chaque partie
        for (int i = 0; i < parties.Length; i++)
        {
            string partie = parties[i];

            // Si c'est un code à 2 chiffres
            if (partie.Length == 2)
            {
                string premierChiffre = partie[0].ToString();
                string deuxiemeChiffre = partie[1].ToString();

                // On vérifie que ce sont bien des chiffres
                bool premierEstChiffre = char.IsDigit(partie[0]);
                bool deuxiemeEstChiffre = char.IsDigit(partie[1]);

                if (premierEstChiffre == true && deuxiemeEstChiffre == true)
                {
                    // On convertit les chiffres en positions dans le carré
                    int ligne = int.Parse(premierChiffre) - 1; // -1 car le carré commence à 0
                    int colonne = int.Parse(deuxiemeChiffre) - 1;

                    // On vérifie que les positions sont valides (entre 0 et 4)
                    bool ligneValide = (ligne >= 0 && ligne < 5);
                    bool colonneValide = (colonne >= 0 && colonne < 5);

                    if (ligneValide == true && colonneValide == true)
                    {
                        // On récupère la lettre du carré
                        string lettre = carre[ligne, colonne];
                        resultat = resultat + lettre;
                    }
                    else
                    {
                        // Position invalide, on garde tel quel
                        resultat = resultat + partie;
                    }
                }
                else
                {
                    // Ce ne sont pas des chiffres, on garde tel quel
                    resultat = resultat + partie;
                }
            }
            else
            {
                // Ce n'est pas un code à 2 chiffres, on garde tel quel
                resultat = resultat + partie;
            }
        }

        return resultat;
    }

    // ==================== MÉTHODE BAZERIES ====================

    /// <summary>
    /// Crypte un texte en utilisant la méthode de Bazeries simplifiée.
    /// Le décalage utilisé est le carré de la clé numérique.
    /// </summary>
    /// <param name="texte">Le texte à crypter</param>
    /// <param name="cle">La clé numérique</param>
    /// <returns>Le texte crypté</returns>
    public static string CrypterBazeries(string texte, int cle)
    {
        // On utilise le carré de la clé comme décalage
        int decalage = cle * cle;
        string resultat = "";

        // On parcourt chaque caractère du texte
        for (int i = 0; i < texte.Length; i++)
        {
            string caractere = texte[i].ToString().ToUpper();

            // Si c'est une lettre, on la crypte
            if (char.IsLetter(texte[i]) == true)
            {
                // On crypte la lettre avec le décalage
                int positionLettre = caractere[0] - 'A'; // Position de 0 à 25
                int nouvellePosition = (positionLettre + decalage) % 26; // On ajoute le décalage
                string lettreCryptee = ((char)(nouvellePosition + 'A')).ToString(); // On reconvertit en lettre
                resultat = resultat + lettreCryptee;
            }
            else
            {
                // Si ce n'est pas une lettre, on garde tel quel
                resultat = resultat + caractere;
            }
        }

        return resultat;
    }

    /// <summary>
    /// Décrypte un texte crypté avec la méthode de Bazeries.
    /// </summary>
    /// <param name="texte">Le texte crypté à décrypter</param>
    /// <param name="cle">La clé numérique (même que pour le cryptage)</param>
    /// <returns>Le texte décrypté</returns>
    public static string DecrypterBazeries(string texte, int cle)
    {
        // On utilise le même décalage que pour le cryptage
        int decalage = cle * cle;
        string resultat = "";

        // On parcourt chaque caractère du texte crypté
        for (int i = 0; i < texte.Length; i++)
        {
            string caractere = texte[i].ToString().ToUpper();

            // Si c'est une lettre, on la décrypte
            if (char.IsLetter(texte[i]) == true)
            {
                // On décrypte la lettre (on enlève le décalage)
                int positionLettre = caractere[0] - 'A';
                int nouvellePosition = (positionLettre - decalage + 26) % 26; // +26 pour éviter les négatifs
                string lettreDecryptee = ((char)(nouvellePosition + 'A')).ToString();
                resultat = resultat + lettreDecryptee;
            }
            else
            {
                // Si ce n'est pas une lettre, on garde tel quel
                resultat = resultat + caractere;
            }
        }

        return resultat;
    }
}