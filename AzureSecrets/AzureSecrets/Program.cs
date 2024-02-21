using AzureSecrets.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        int option = 1;

        ConsoleMenu menu = new ConsoleMenu();

        option = menu.menu(option);

        if( option == 3)
        {
            GC.Collect();
            Environment.Exit(1);
        }

        if( option == 1) {
            string filePath = Directory.GetCurrentDirectory() + "/" + "Vaults.json";
            try
            {
                new Options().fetchSecrets(filePath);
            }catch(FileNotFoundException ex)
            {
                Console.WriteLine("No File with name: Vaults.json");
            }
            menu.menu(1);
        }

        if (option == 2)
        {
            string filePath = Directory.GetCurrentDirectory() + "/" + "Secrets.json";
            try
            {
                new Options().fetchSecrets(filePath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("No File with name: Secrets.json");
            }
            menu.menu(1);
        }
       
    }
}