using System;
using System.Runtime.Intrinsics.Arm;

namespace DesafioForca;
public class Program {

    public static List<char> guessList = new List<char>();
    public static int guessesNumber = 6;
    public static void Main(string[] args) {

        string[] wordList = { "AZUL", "AMARELO", "PRETO", "VERDE", "VERMELHO", "BRANCO", "ROXO", "ROSA", "LARANJA" };
        Random random = new Random();
        int indexWord = random.Next(wordList.Length);
        string randomWord = wordList[indexWord];

        char[] randomWordLetters = randomWord.ToCharArray();
        char[] guessedLetters = hiddenLetters(randomWordLetters);

        startGame(randomWord, guessedLetters);

    }
    public static void startGame(string randomWord, char[] guessedLetters) {

        while (guessesNumber > 0) {

            updateDisplay(randomWord, guessedLetters);

            Console.Write("\n\nDigite somente uma Letra: ");
            ConsoleKeyInfo inputLetter = Console.ReadKey();

            bool isLetter = checkLetter(inputLetter.KeyChar);

            char formattedLetter = formatLetter(inputLetter);

            if (isLetter) {
                if (randomWord.Contains(formattedLetter)) {
                    for (int i = 0; i < randomWord.Length; i++) {
                        if (randomWord[i].Equals(formattedLetter)) {
                            guessedLetters[i] = formattedLetter;
                        }

                    }
                    addLettersGuesses(formattedLetter);
                }
                else if (!guessList.Contains(formattedLetter)) {
                    decrementGuesses();
                    addLettersGuesses(formattedLetter);
                }
                else {
                    addLettersGuesses(formattedLetter);
                }

            }

            if (guessesNumber == 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                updateDisplay(randomWord, guessedLetters);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\t\tVocê perdeu!!!. Excedeu o número de Palpites.");
                return;
            }


            if (randomWord.SequenceEqual(guessedLetters)) {
                Console.ForegroundColor = ConsoleColor.Green;
                updateDisplay(randomWord, guessedLetters);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\tParabéns você acertou a palavra.");
                return;

            }


        }


    }

    public static void updateDisplay(string randomWord, char[] guessedLetters) {
        Console.Clear();
        Console.WriteLine("\n\t ************* DESAFIO FORCA ****************** \t");
        Console.WriteLine("\n");
        Console.WriteLine("\t Dica: COR");
        Console.WriteLine("\n\t Uma cor com " + randomWord.Length + " LETRAS");
        Console.WriteLine("\n\t Número de Palpites: " + guessesNumber);
        showGuesses();
        Console.WriteLine("\n\n");
        showGuessedLetters(guessedLetters);

    }

    public static char[] hiddenLetters(char[] randomWordLetters) {
        return new string('_', randomWordLetters.Length).ToCharArray();
    }

    public static void showGuessedLetters(char[] guessedLetters) {
        for (int i = 0; i < guessedLetters.Length; i++) {
            Console.Write("\t" + guessedLetters[i]);
        }
    }

    public static bool checkLetter(char inputLetter) {
        if (!char.IsLetter(inputLetter)) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nLetra Inválida!!!. Pressione ENTER pra continuar....");
            Console.ResetColor();
            Console.ReadLine();
            return false;
        }

        return true;
    }

    public static char formatLetter(ConsoleKeyInfo inputLetter) {
        string str = new string(inputLetter.KeyChar, 1);
        char formatLetter = char.Parse(str.ToUpper());
        return formatLetter;
    }


    public static void addLettersGuesses(char formattedLetter) {
        if (!guessList.Contains(formattedLetter)) {
            guessList.Add(formattedLetter);

        }
        else {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nVocê já digitou essa Letra, tente outra Letra.");
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ResetColor();
            Console.ReadLine();
        }
    }

    public static void showGuesses() {
        Console.Write("\n\t Tentativas: ");
        for (int i = 0; i < guessList.Count; i++) {
            Console.Write(guessList[i] + ", ");
        }

    }

    public static int decrementGuesses() {
        return guessesNumber--;
    }


}
