using System;
using System.Collections.Generic;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class Word
{
    public string Text { get; set; }
    public Difficulty Level { get; set; }

    public Word(string text, Difficulty level)
    {
        Text = text;
        Level = level;
    }
}

public class WordBank
{
    private List<Word> words = new List<Word>();

    public void AddWord(Word word)
    {
        words.Add(word);
    }

    public Word GetRandomWord(Difficulty level)
    {
        List<Word> filtered = new List<Word>();
        foreach (Word word in words)
        {
            if (word.Level == level)
                filtered.Add(word);
        }

        if (filtered.Count == 0)
            return null;

        Random rand = new Random();
        return filtered[rand.Next(filtered.Count)];
    }
}

class Program
{
    static void Main(string[] args)
    {
        WordBank bank = new WordBank();

        bank.AddWord(new Word("apple", Difficulty.Easy));
        bank.AddWord(new Word("cat", Difficulty.Easy));
        bank.AddWord(new Word("dog", Difficulty.Easy));
        bank.AddWord(new Word("computer", Difficulty.Medium));
        bank.AddWord(new Word("keyboard", Difficulty.Medium));
        bank.AddWord(new Word("window", Difficulty.Medium));
        bank.AddWord(new Word("algorithm", Difficulty.Hard));
        bank.AddWord(new Word("encryption", Difficulty.Hard));
        bank.AddWord(new Word("synchronization", Difficulty.Hard));

        Console.WriteLine("=== ВИСЕЛИЦА ===");
        Console.WriteLine("Выбери сложность: Easy, Medium, Hard");
        string diffInput = Console.ReadLine();

        Difficulty selected;
        if (!Enum.TryParse(diffInput, true, out selected))
        {
            Console.WriteLine("Неверная сложность. Игра завершена.");
            return;
        }

        Word wordToGuess = bank.GetRandomWord(selected);
        if (wordToGuess == null)
        {
            Console.WriteLine("Нет слов такой сложности.");
            return;
        }

        string word = wordToGuess.Text;
        char[] guessed = new char[word.Length];
        for (int i = 0; i < guessed.Length; i++)
            guessed[i] = '_';

        int attempts = 6;
        List<char> wrongLetters = new List<char>();

        while (attempts > 0)
        {
            Console.WriteLine("\nСлово: " + new string(guessed));
            Console.WriteLine("Осталось попыток: " + attempts);
            Console.Write("Ошибки: ");
            foreach (char c in wrongLetters)
                Console.Write(c + " ");
            Console.WriteLine();

            Console.Write("Введи букву: ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Введи букву!");
                continue;
            }

            char letter = input.ToLower()[0];

            if (wrongLetters.Contains(letter) || new string(guessed).Contains(letter))
            {
                Console.WriteLine("Эту букву уже вводили.");
                continue;
            }

            bool correct = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    guessed[i] = letter;
                    correct = true;
                }
            }

            if (correct)
            {
                Console.WriteLine("Есть такая буква!");

                bool won = true;
                for (int i = 0; i < guessed.Length; i++)
                {
                    if (guessed[i] == '_')
                        won = false;
                }

                if (won)
                {
                    Console.WriteLine("\nПОБЕДА! Ты угадал слово: " + word);
                    return;
                }
            }
            else
            {
                Console.WriteLine("Нет такой буквы.");
                wrongLetters.Add(letter);
                attempts--;
            }
        }

        Console.WriteLine("\nТЫ ПРОИГРАЛ! Загаданное слово: " + word);
    }
}