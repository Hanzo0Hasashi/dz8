using System;
using System.Collections.Generic;

public class ConsoleUI
{
    private WordBank wordBank;
    private Difficulty currentDifficulty;
    private Word currentWord;
    private char[] guessedLetters;
    private int attempts;
    private List<char> wrongLetters;

    public ConsoleUI(WordBank bank)
    {
        wordBank = bank;
    }

    public void Run()
    {
        ShowWelcomeScreen();
        
        if (!SelectDifficulty())
            return;

        if (!SelectWord())
            return;

        InitializeGame();
        PlayGame();
    }

    private void ShowWelcomeScreen()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("           ВИСЕЛИЦА");
        Console.WriteLine("=================================");
        Console.WriteLine("Добро пожаловать в игру!");
        Console.WriteLine();
    }

    private bool SelectDifficulty()
    {
        while (true)
        {
            Console.WriteLine("Выберите сложность:");
            Console.WriteLine("1 - Easy (Легко)");
            Console.WriteLine("2 - Medium (Средне)");
            Console.WriteLine("3 - Hard (Сложно)");
            Console.Write("Введите номер (1-3): ");

            string input = Console.ReadLine();

            // Защита от некорректного ввода
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым!");
                continue;
            }

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Ошибка: введите число!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    currentDifficulty = Difficulty.Easy;
                    return true;
                case 2:
                    currentDifficulty = Difficulty.Medium;
                    return true;
                case 3:
                    currentDifficulty = Difficulty.Hard;
                    return true;
                default:
                    Console.WriteLine("Ошибка: выберите 1, 2 или 3!");
                    break;
            }
        }
    }

    private bool SelectWord()
    {
        currentWord = wordBank.GetRandomWord(currentDifficulty);
        
        if (currentWord == null)
        {
            Console.WriteLine($"Ошибка: нет слов сложности {currentDifficulty}!");
            return false;
        }

        return true;
    }

    private void InitializeGame()
    {
        guessedLetters = new char[currentWord.Text.Length];
        for (int i = 0; i < guessedLetters.Length; i++)
            guessedLetters[i] = '_';

        attempts = 6;
        wrongLetters = new List<char>();
    }

    private void PlayGame()
    {
        while (attempts > 0)
        {
            ShowGameState();
            
            char letter = GetPlayerGuess();
            
            if (letter == '\0')
                continue;

            ProcessGuess(letter);
            
            if (CheckWin())
            {
                ShowWinMessage();
                return;
            }
        }

        ShowLoseMessage();
    }

    private void ShowGameState()
    {
        Console.WriteLine("\n" + new string('=', 30));
        Console.WriteLine("Слово: " + new string(guessedLetters));
        Console.WriteLine("Осталось попыток: " + attempts);
        
        Console.Write("Ошибочные буквы: ");
        if (wrongLetters.Count > 0)
        {
            foreach (char c in wrongLetters)
                Console.Write(c + " ");
        }
        else
        {
            Console.Write("(нет)");
        }
        Console.WriteLine();
    }

    private char GetPlayerGuess()
    {
        while (true)
        {
            Console.Write("Введите букву: ");
            string input = Console.ReadLine();

            // Защита от пустого ввода
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: введите букву!");
                continue;
            }

            // Защита от ввода больше одного символа
            if (input.Length > 1)
            {
                Console.WriteLine("Ошибка: введите только одну букву!");
                continue;
            }

            char letter = char.ToLower(input[0]);

            // Защита от ввода не букв
            if (!char.IsLetter(letter))
            {
                Console.WriteLine("Ошибка: введите букву, а не символ или цифру!");
                continue;
            }

            // Проверка на повторный ввод
            if (wrongLetters.Contains(letter) || new string(guessedLetters).Contains(letter))
            {
                Console.WriteLine($"Буква '{letter}' уже была введена ранее!");
                continue;
            }

            return letter;
        }
    }

    private void ProcessGuess(char letter)
    {
        bool correct = false;
        
        for (int i = 0; i < currentWord.Text.Length; i++)
        {
            if (currentWord.Text[i] == letter)
            {
                guessedLetters[i] = letter;
                correct = true;
            }
        }

        if (correct)
        {
            Console.WriteLine($"✓ Буква '{letter}' есть в слове!");
        }
        else
        {
            Console.WriteLine($"✗ Буквы '{letter}' нет в слове.");
            wrongLetters.Add(letter);
            attempts--;
        }
    }

    private bool CheckWin()
    {
        foreach (char c in guessedLetters)
        {
            if (c == '_')
                return false;
        }
        return true;
    }

    private void ShowWinMessage()
    {
        Console.WriteLine("\n" + new string('=', 30));
        Console.WriteLine("🎉 ПОБЕДА! 🎉");
        Console.WriteLine($"Ты угадал слово: {currentWord.Text}");
        Console.WriteLine(new string('=', 30));
    }

    private void ShowLoseMessage()
    {
        Console.WriteLine("\n" + new string('=', 30));
        Console.WriteLine("💀 ТЫ ПРОИГРАЛ! 💀");
        Console.WriteLine($"Загаданное слово: {currentWord.Text}");
        Console.WriteLine(new string('=', 30));
    }
}
