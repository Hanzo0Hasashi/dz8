using System;
using System.Collections.Generic;

namespace viselica
{
   
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }

    public class Game
    {
       
        private List<Player> players = new List<Player>();
        
    
        private HashSet<string> levels = new HashSet<string>();

        public Game()
        {
            
            levels.Add("Easy");
            levels.Add("Medium");
            levels.Add("Hard");
        }

        
        public void AddPlayer(string name)
        {
            players.Add(new Player(name));
            Console.WriteLine($"Игрок {name} добавлен. Всего игроков: {players.Count}");
        }

      
        public void ShowAllPlayers()
        {
            Console.WriteLine("Список игроков:");
            foreach (Player p in players)
            {
                Console.WriteLine($"- {p.Name}: {p.Score} очков");
            }
        }

     
        public bool AddLevel(string level)
        {
            if (levels.Add(level))
            {
                Console.WriteLine($"Уровень {level} добавлен");
                return true;
            }
            Console.WriteLine($"Уровень {level} уже существует");
            return false;
        }

     
        public void ShowLevels()
        {
            Console.WriteLine("Доступные уровни:");
            foreach (string level in levels)
            {
                Console.WriteLine($"- {level}");
            }
        }
    }
}
