class Fonctions
{
    // ==================== VIGENÈRE ====================
    public static string CryptVigenere(string texte, string cle)
    {
        string resultat = "";
        cle = cle.ToUpper();
        int indexCle = 0;

        foreach (char c in texte.ToUpper())
        {
            if (char.IsLetter(c))
            {
                int decalage = cle[indexCle % cle.Length] - 'A';
                char crypte = (char)(((c - 'A' + decalage) % 26) + 'A');
                resultat += crypte;
                indexCle++;
            }
            else
            {
                resultat += c;
            }
        }
        return resultat;
    }

    public static string DecryptVigenere(string texte, string cle)
    {
        string resultat = "";
        cle = cle.ToUpper();
        int indexCle = 0;

        foreach (char c in texte.ToUpper())
        {
            if (char.IsLetter(c))
            {
                int decalage = cle[indexCle % cle.Length] - 'A';
                char decrypte = (char)(((c - 'A' - decalage + 26) % 26) + 'A');
                resultat += decrypte;
                indexCle++;
            }
            else
            {
                resultat += c;
            }
        }
        return resultat;
    }

    // ==================== POLYBE ====================
    public static string CryptPolybe(string texte)
    {
        // Carré de Polybe 5x5 (I et J partagent la même case)
        char[,] carre = {
            {'A', 'B', 'C', 'D', 'E'},
            {'F', 'G', 'H', 'I', 'K'},
            {'L', 'M', 'N', 'O', 'P'},
            {'Q', 'R', 'S', 'T', 'U'},
            {'V', 'W', 'X', 'Y', 'Z'}
        };

        string resultat = "";

        foreach (char c in texte.ToUpper())
        {
            if (c == 'J') // On traite J comme I
            {
                resultat += "24";
                continue;
            }

            bool trouve = false;
            for (int ligne = 0; ligne < 5; ligne++)
            {
                for (int colonne = 0; colonne < 5; colonne++)
                {
                    if (carre[ligne, colonne] == c)
                    {
                        resultat += (ligne + 1).ToString() + (colonne + 1).ToString();
                        trouve = true;
                        break;
                    }
                }
                if (trouve) break;
            }

            if (!trouve) resultat += c;
            resultat += " "; // Espace entre chaque caractère/code
        }

        return resultat.Trim();
    }

    public static string DecryptPolybe(string texte)
    {
        char[,] carre = {
            {'A', 'B', 'C', 'D', 'E'},
            {'F', 'G', 'H', 'I', 'K'},
            {'L', 'M', 'N', 'O', 'P'},
            {'Q', 'R', 'S', 'T', 'U'},
            {'V', 'W', 'X', 'Y', 'Z'}
        };

        string resultat = "";
        string[] parties = texte.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string partie in parties)
        {
            if (partie.Length == 2 && char.IsDigit(partie[0]) && char.IsDigit(partie[1]))
            {
                int ligne = int.Parse(partie[0].ToString()) - 1;
                int colonne = int.Parse(partie[1].ToString()) - 1;

                if (ligne >= 0 && ligne < 5 && colonne >= 0 && colonne < 5)
                {
                    resultat += carre[ligne, colonne];
                }
                else
                {
                    resultat += partie;
                }
            }
            else
            {
                resultat += partie;
            }
        }

        return resultat;
    }

    // ==================== BAZERIES ====================
    public static string CryptBazeries(string texte, int cle)
    {
        // Chiffrement simplifié utilisant le carré de la clé comme décalage
        int decalage = cle * cle;
        string resultat = "";

        foreach (char c in texte.ToUpper())
        {
            if (char.IsLetter(c))
            {
                char crypte = (char)(((c - 'A' + decalage) % 26) + 'A');
                resultat += crypte;
            }
            else
            {
                resultat += c;
            }
        }

        return resultat;
    }

    public static string DecryptBazeries(string texte, int cle)
    {
        int decalage = cle * cle;
        string resultat = "";

        foreach (char c in texte.ToUpper())
        {
            if (char.IsLetter(c))
            {
                char decrypte = (char)(((c - 'A' - decalage + 26) % 26) + 'A');
                resultat += decrypte;
            }
            else
            {
                resultat += c;
            }
        }

        return resultat;
    }
}