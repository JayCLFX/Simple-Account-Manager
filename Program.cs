namespace AccountManager
{
    public class Program
    {
        public static void Main()
        {
            Library.Initialize_Library_Public(); Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Main Menu \n\n\n"); Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" 1. Add Account \n 2. Search for Accounts \n 3. Delete Game Specific Account\n 4. Delete Database\n 5. Create New AES Keys\n 6. Exit");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                Library.Add_Account_Public(); break;
                case "2":
                Library.Search_Account_Public(); break;
                case "3":
                Library.Delete_Specific_Account_Public(); break;
                case "4":
                Library.Delete_Database_Public(); break;
                case "5":
                Library.Display_New_AES_Public(); break;
                case "6":
                Environment.Exit(0); break;
            }
            Console.Clear(); Main();
        }
    }
}
