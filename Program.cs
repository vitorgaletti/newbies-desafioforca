namespace DesafioForca;

public class ForcaGame {
    private static readonly string[] wordList = { "AZUL", "AMARELO", "PRETO", "VERDE", "VERMELHO", "BRANCO", "ROXO", "ROSA", "LARANJA" };
    private readonly List<char> guessesList = new List<char>();
    public string[] getWordList() {
        return wordList;
    }

    public List<char> getGuessesList() {
        return guessesList;
    }

}
public class Program {

    public static int MAX_GUESSES = 6;
    public static string playAgainInput = "";
    public static void Main(string[] args) {

        ForcaGame forcaGame = new ForcaGame();
        string[] wordList = forcaGame.getWordList();
        List<char> guessesList = forcaGame.getGuessesList();

        do {
            Random random = new Random();
            int indexWord = random.Next(wordList.Length);
            string randomWord = wordList[indexWord];

            char[] randomWordLetters = randomWord.ToCharArray();
            char[] guessedLetters = HiddenLetters(randomWordLetters);

            playAgainInput = "";
            StartGame(randomWord, guessedLetters, guessesList);

        } while (playAgainInput.Equals("S"));

    }
    public static void StartGame(string randomWord, char[] guessedLetters, List<char> guessesList) {

        while (MAX_GUESSES > 0) {

            UpdateDisplay(randomWord, guessedLetters, guessesList);

            ConsoleKeyInfo inputLetter = GetInputLetter();

            bool isLetter = CheckLetterValidity(inputLetter.KeyChar);

            if (isLetter) {
                char formattedLetter = FormatLetter(inputLetter);

                if (randomWord.Contains(formattedLetter)) {
                    AddGuessesList(formattedLetter, guessesList);
                    UpdateGuessedLetters(randomWord, formattedLetter, guessedLetters);
                }
                else if (!guessesList.Contains(formattedLetter)) {
                    DecrementGuesses();
                    AddGuessesList(formattedLetter, guessesList);
                }
                else {
                    AddGuessesList(formattedLetter, guessesList);
                }

            }

            if (MAX_GUESSES == 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                UpdateDisplay(randomWord, guessedLetters, guessesList);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\t\tVocê perdeu!!!. Excedeu o número de Palpites.");
                PlayAgain(guessesList);
                return;
            }


            if (randomWord.SequenceEqual(guessedLetters)) {
                Console.ForegroundColor = ConsoleColor.Green;
                UpdateDisplay(randomWord, guessedLetters, guessesList);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\tParabéns você acertou a palavra.");
                PlayAgain(guessesList);
                return;

            }


        }


    }

    public static void UpdateDisplay(string randomWord, char[] guessedLetters, List<char> guessesList) {
        Console.Clear();
        Console.WriteLine("\n\t ************* DESAFIO FORCA ****************** \t");
        Console.WriteLine("\n");
        Console.WriteLine("\t Dica: COR");
        Console.WriteLine($"\n\t Uma cor com {randomWord.Length} LETRAS");
        Console.WriteLine($"\n\t Número de Palpites: {MAX_GUESSES}");
        ShowGuesses(guessesList);
        Console.WriteLine("\n\n");
        ShowGuessedLetters(guessedLetters);

    }
    public static char[] HiddenLetters(char[] randomWordLetters) {
        return new string('_', randomWordLetters.Length).ToCharArray();
    }

    public static ConsoleKeyInfo GetInputLetter() {
        Console.Write("\n\nDigite somente uma Letra: ");
        ConsoleKeyInfo inputLetter = Console.ReadKey();
        return inputLetter;
    }

    public static bool CheckLetterValidity(char inputLetter) {
        if (!char.IsLetter(inputLetter)) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nEntrada inválida!!! Por favor, insira uma letra.");
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ResetColor();
            Console.ReadLine();
            return false;
        }

        return true;
    }

    public static char FormatLetter(ConsoleKeyInfo inputLetter) {
        string str = new string(inputLetter.KeyChar, 1);
        char formatLetter = char.Parse(str.ToUpper());
        return formatLetter;
    }

    public static void UpdateGuessedLetters(string randomWord, char formattedLetter, char[] guessedLetters) {
        for (int i = 0; i < randomWord.Length; i++) {
            if (randomWord[i].Equals(formattedLetter)) {
                guessedLetters[i] = formattedLetter;
            }

        }
    }

    public static void ShowGuessedLetters(char[] guessedLetters) {
        foreach (char letter in guessedLetters) {
            Console.Write($"\t{letter}");
        }
    }


    public static void AddGuessesList(char formattedLetter, List<char> guessesList) {
        if (!guessesList.Contains(formattedLetter)) {
            guessesList.Add(formattedLetter);

        }
        else {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nVocê já digitou essa Letra, tente outra Letra.");
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ResetColor();
            Console.ReadLine();
        }
    }

    public static void ShowGuesses(List<char> guessesList) {
        Console.Write("\n\t Tentativas: ");
        for (int i = 0; i < guessesList.Count; i++) {
            Console.Write(guessesList[i] + ", ");
        }

    }

    public static int DecrementGuesses() {
        return MAX_GUESSES--;
    }

    public static void ResetGame(List<char> guessesList) {
        Console.Clear();
        Console.ResetColor();
        guessesList.Clear();
        MAX_GUESSES = 6;

    }


    public static void PlayAgain(List<char> guessesList) {
    
        while (playAgainInput != "S") {
            Console.Write("\n\tDeseja jogar Novamente S (SIM) ou N (NÃO)? ENTER para confirmar ");
            playAgainInput = Console.ReadLine().ToUpper();


            if (playAgainInput == "N") {
                Environment.Exit(0);

            }

            else if (playAgainInput != "S") {
                Console.WriteLine("\nOpção Inválida. Pressione Enter para voltar.");
                Console.ReadLine();

            }
            else {
                ResetGame(guessesList);
            }

        }


    }


}
