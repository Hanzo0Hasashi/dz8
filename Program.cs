csharp
using System;

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

        ConsoleUI ui = new ConsoleUI(bank);
        ui.Run();
    }
}
