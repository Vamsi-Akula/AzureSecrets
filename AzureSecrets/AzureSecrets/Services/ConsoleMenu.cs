using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSecrets.Services
{
    public class ConsoleMenu
    {
       

        public int menu(int option=1) {

            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the world of C#!");
            Console.ResetColor();
            Console.WriteLine("\nUse up  and down  to navigate and press Enter/Return to select:");
            (int left, int top) = Console.GetCursorPosition();
            var decorator = "->";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                Console.WriteLine($"{(option == 1 ? decorator : "   ")}Fetch Secrets");
                Console.WriteLine($"{(option == 2 ? decorator : "   ")}Upload");
                Console.WriteLine($"{(option == 3 ? decorator : "   ")}Exit");

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? 3 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == 3 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            
            return option;
        }
    }
}
