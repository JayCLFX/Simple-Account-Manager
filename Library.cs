using System.Security.Cryptography;
using Newtonsoft.Json;

namespace AccountManager
{
    public class Library
    {
        private static protected Dictionary<string,string> Rust_Dictionary = new Dictionary<string,string>();
        private static protected Dictionary<string,string> DBD_Dictionary = new Dictionary<string,string>();
        private static protected Dictionary<string,string> CS2_Dictionary = new Dictionary<string, string>();
        private static protected Dictionary<string,string> Misc_Dictionary = new Dictionary<string, string>();

        private static string Software_Path = Environment.CurrentDirectory;
        private static string Envoirment_Path = Environment.CurrentDirectory + "/" + "saves";

        private static string Rust_Accounts_Save_Path = Environment.CurrentDirectory + "/" + "saves" + "/" + "account1.am";
        private static string DBD_Accounts_Save_Path = Environment.CurrentDirectory + "/" + "saves" + "/" + "account2.am";
        private static string CS2_Accounts_Save_Path = Environment.CurrentDirectory + "/" + "saves" + "/" + "account3.am";
        private static string Misc_Accouts_Save_Path = Environment.CurrentDirectory + "/" + "saves" + "/" + "account4.am";

        private static protected Aes? AESModule;
        private const string AESKey = "";  //Add youre own Key otherwise this wont work
        private const string AESVector = ""; //Add youre own Vector otherwise this wont work

        //Public Functions
        public static void Initialize_Library_Public() => Initialize_Library();
        public static void Add_Account_Public() => Add_Account();
        public static void Search_Account_Public() => Search_Accounts();
        public static void Display_New_AES_Public() => Display_Generated_AES();
        public static void Delete_Specific_Account_Public() => Delete_Specific_Account();
        public static void Save_Rust_Dictionary_Public()
        {
            var save = Task.Run((Func<Task>)Save_Rust_Dictionary);
            save.Wait();
            return;
        }
        public static void Save_DBD_Dictionary_Public()
        {
            var save = Task.Run((Func<Task>)Save_DBD_Dictionary);
            save.Wait();
            return;
        }
        public static void Save_CS2_Dictionary_Public()
        {
            var save = Task.Run((Func<Task>)Save_CS2_Dictionary);
            save.Wait();
            return;
        }
        public static void Save_Misc_Dictionary_Public()
        {
            var save = Task.Run((Func<Task>)Save_Misc_Dictionary);
            save.Wait();
            return;
        }
        public static void Load_All_Data_Public()
        {
            var load = Task.Run((Func<Task>)Load_All_Data);
            load.Wait();
            return;
        }
        public static void Delete_Database_Public()
        {
            var delete = Task.Run((Func<Task>)Delete_Database);
            delete.Wait();
            return;
        }




        //Internal Functions


        private static protected void Initialize_Library()
        {
            if (!Directory.Exists(Envoirment_Path)) Directory.CreateDirectory(Envoirment_Path);

            if (File.Exists(Rust_Accounts_Save_Path) && File.Exists(DBD_Accounts_Save_Path) && File.Exists(CS2_Accounts_Save_Path) && File.Exists(Misc_Accouts_Save_Path))
            {
                var load = Task.Run((Func<Task>)Load_All_Data);
                load.Wait();

                return;
            }
            else
            {
                var saverust = Task.Run((Func<Task>)Save_Rust_Dictionary);
                saverust.Wait();
                var savedbd = Task.Run((Func<Task>)Save_DBD_Dictionary);
                savedbd.Wait();
                var savecs2 = Task.Run((Func<Task>)Save_CS2_Dictionary);
                savecs2.Wait();
                var savemisc = Task.Run((Func<Task>)Save_Misc_Dictionary);
                savemisc.Wait();
                return;
            }
        }


        private static protected async Task Save_Rust_Dictionary()
        {
            try
            {
                string temp_data = JsonConvert.SerializeObject(Rust_Dictionary, Formatting.Indented);
                string encrypted_data = EncryptDataWithAes(temp_data, AESKey, AESVector);
                File.WriteAllText(Rust_Accounts_Save_Path, encrypted_data);
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A Problem Occured while Savin the Rust Dictionary:   {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine(); Environment.Exit(10);
            }
        }

        private static protected async Task Save_DBD_Dictionary()
        {
            try
            {
                string temp_data = JsonConvert.SerializeObject(DBD_Dictionary, Formatting.Indented);
                string encrypted_data = EncryptDataWithAes(temp_data, AESKey, AESVector);
                File.WriteAllText(DBD_Accounts_Save_Path, encrypted_data);
                return;
            }
            catch (Exception ex )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal Error while Saving the DBD Dictionary:    {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine(); Environment.Exit(10);
            }
        }

        private static protected async Task Save_CS2_Dictionary()
        {
            try
            {
                string temp_data = JsonConvert.SerializeObject(CS2_Dictionary, Formatting.Indented);
                string encrypted_data = EncryptDataWithAes(temp_data, AESKey, AESVector);
                File.WriteAllText(CS2_Accounts_Save_Path, encrypted_data);
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal Error while Saving the CS2 Dictionary:    {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine(); Environment.Exit(10);
            }
        }

        private static protected async Task Save_Misc_Dictionary()
        {
            try
            {
                string temp_data = JsonConvert.SerializeObject(Misc_Dictionary, Formatting.Indented);
                string encrypted_data = EncryptDataWithAes(temp_data, AESKey, AESVector);
                File.WriteAllText(Misc_Accouts_Save_Path, encrypted_data);
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal Error while Saving the Misc Dictionary:   {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine(); Environment.Exit(10);
            }
        }

        private static protected async Task Load_All_Data()
        {
            try
            {
                string Rust_Data = null;
                string DBD_Data = null;
                string CS2_Data = null;
                string Misc_Data = null;

                string temp_data1 = File.ReadAllText(Rust_Accounts_Save_Path);
                Rust_Data = DecryptDataWithAes(temp_data1, AESKey, AESVector);

                string temp_data2 = File.ReadAllText(DBD_Accounts_Save_Path);
                DBD_Data = DecryptDataWithAes(temp_data2, AESKey, AESVector);

                string temp_data3 = File.ReadAllText(CS2_Accounts_Save_Path);
                CS2_Data = DecryptDataWithAes(temp_data3, AESKey, AESVector);

                string temp_data4 = File.ReadAllText(Misc_Accouts_Save_Path);
                Misc_Data = DecryptDataWithAes(temp_data4, AESKey, AESVector);


                Rust_Dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Rust_Data);
                DBD_Dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(DBD_Data);
                CS2_Dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(CS2_Data);
                Misc_Dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Misc_Data);
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal Error while Loading Data:    {ex.Message} \n\n\nPress any Key To Continue");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine(); Error_Menu();
            }
        }

        private static protected async Task Delete_Database()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"This cant be undone!"); Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Youre sure =\n Y/N");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "Y":
                    break;
                case "N":
                    return;
            }

            if (answer != "Y" && answer != "N") return;

            try
            {
                File.Delete(Rust_Accounts_Save_Path);
                File.Delete(DBD_Accounts_Save_Path);
                File.Delete(CS2_Accounts_Save_Path);
                File.Delete(Misc_Accouts_Save_Path);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success Closing Application!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal Error while trying to Delete Database:   {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine(); Environment.Exit(10);
            }
        }

        private static protected void Add_Account()
        {
            Console.Clear();
            Console.WriteLine($"State youre Game!\n 1. Rust\n 2. Dead By Daylight\n 3. Counter Strike 2\n 4. Other\n 5. Back\n\n");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                Add_Rust_Account(); break;
                case "2":
                Add_DBD_Account(); break;
                case "3":
                Add_CS2_Account(); break;
                case "4":
                Add_Misc_Account(); break;
                case "5":
                return;
            }
            Console.Clear(); return;
        }

        private static protected void Add_Rust_Account()
        {
            string username = null;
            string passcode = null;
            Console.Clear(); Console.WriteLine("State Username\n\n");
            username = Console.ReadLine();
            Console.Clear(); Console.WriteLine("State Passcode\n\n");
            passcode = Console.ReadLine();

            Rust_Dictionary.Add(username, passcode);
            Thread.Sleep(1000);

            var save = Task.Run((Func<Task>)Save_Rust_Dictionary);
            save.Wait();
            username = null;
            passcode = null;
            return;
        }

        private static protected void Add_DBD_Account()
        {
            string username = null;
            string passcode = null;
            Console.Clear(); Console.WriteLine("State Username\n\n");
            username = Console.ReadLine();
            Console.Clear(); Console.WriteLine("State Passcode\n\n");
            passcode = Console.ReadLine();

            DBD_Dictionary.Add(username, passcode);
            Thread.Sleep(1000);

            var save = Task.Run((Func<Task>)Save_DBD_Dictionary);
            save.Wait();
            username = null;
            passcode = null;
            return;
        }

        private static protected void Add_CS2_Account()
        {
            string username = null;
            string passcode = null;
            Console.Clear(); Console.WriteLine("State Username\n\n");
            username = Console.ReadLine();
            Console.Clear(); Console.WriteLine("State Passcode\n\n");
            passcode = Console.ReadLine();

            CS2_Dictionary.Add(username, passcode);
            Thread.Sleep(1000);

            var save = Task.Run((Func<Task>)Save_CS2_Dictionary);
            save.Wait();
            username = null;
            passcode = null;
            return;
        }

        private static protected void Add_Misc_Account()
        {
            string username = null;
            string passcode = null;
            Console.Clear(); Console.WriteLine("State Username\n\n");
            username = Console.ReadLine();
            Console.Clear(); Console.WriteLine("State Passcode\n\n");
            passcode = Console.ReadLine();

            Misc_Dictionary.Add(username, passcode);
            Thread.Sleep(1000);

            var save = Task.Run((Func<Task>)Save_Misc_Dictionary);
            save.Wait();
            username = null;
            passcode = null;
            return;
        }

        private static protected void Search_Accounts()
        {
            Console.Clear(); Console.WriteLine("State a Game\n 1. Rust\n 2. Dead By Daylight\n 3. Counter Strike 2\n 4. Other\n 5. Back\n\n");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                Search_Rust(); break;
                case "2":
                Search_DBD(); break;
                case "3":
                Search_CS2(); break;
                case "4":
                Search_Other(); break;
                case "5":
                return;
            }
            return;
        }

        private static protected void Search_Rust()
        {
            Console.Clear(); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("Rust Login Data\n\n\n"); Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in Rust_Dictionary)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine(); Console.Clear(); Program.Main();
        }
        private static protected void Search_DBD()
        {
            Console.Clear(); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("Dead By Daylight Login Data\n\n\n"); Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in DBD_Dictionary)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine(); Console.Clear(); Program.Main();
        }
        private static protected void Search_CS2()
        {
            Console.Clear(); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("Counter Strike 2 Login Data\n\n\n"); Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in CS2_Dictionary)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine(); Console.Clear(); Program.Main();
        }
        private static protected void Search_Other()
        {
            Console.Clear(); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("Other Login Data\n\n\n"); Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in Misc_Dictionary)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine(); Console.Clear(); Program.Main();
        }

        private static protected void Display_Generated_AES()
        {
            string iv = CreateNewVector();
            string key = CreateNewAESKey();
            Console.Clear(); Console.WriteLine($"\n AES-Key:  {key}\n AES-Vector: {iv}\n\n\n Press any Key To Continue!");
            iv = null; key = null;
            Console.ReadLine(); return;
        }

        private static protected void Error_Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed; 
            Console.WriteLine("An Fatal Error Occured while Loading please choose an Option!\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" 1. Delete Database\n 2. Close Program\n");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                Delete_Database(); break;
                case "2":
                Environment.Exit(2); break;
            }
            Console.Clear(); Program.Main();
        }

        private static protected void Delete_Specific_Account()
        {
            Console.Clear(); Console.WriteLine("Please state a Game\n\n\n 1. Rust\n 2. Dead By Daylight\n 3. Counter Strike 2\n 4. Other\n 5. Back");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                Delete_Account_Rust(); break;
                case "2":
                Delete_Account_DBD(); break;
                case "3":
                Delete_Account_CS2(); break;
                case "4":
                Delete_Account_Misc(); break;
                case "5":
                return;
            }
            return;
        }

        private static protected void Delete_Account_Rust()
        {
            string username = null;
            Console.Clear(); Console.WriteLine("State the Username you wanna delete\n\n");
            username = Console.ReadLine();

            if (Rust_Dictionary.Remove(username))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Successfully deleted User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_Rust_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Failed to delete User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_Rust_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
        }
        private static protected void Delete_Account_DBD()
        {
            string username = null;
            Console.Clear(); Console.WriteLine("State the Username you wanna delete\n\n");
            username = Console.ReadLine();

            if (DBD_Dictionary.Remove(username))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Successfully deleted User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_DBD_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Failed to delete User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_DBD_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
        }
        private static protected void Delete_Account_CS2()
        {
            string username = null;
            Console.Clear(); Console.WriteLine("State the Username you wanna delete\n\n");
            username = Console.ReadLine();

            if (CS2_Dictionary.Remove(username))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Successfully deleted User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_CS2_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Failed to delete User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_CS2_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
        }
        private static protected void Delete_Account_Misc()
        {
            string username = null;
            Console.Clear(); Console.WriteLine("State the Username you wanna delete\n\n");
            username = Console.ReadLine();

            if (Misc_Dictionary.Remove(username))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Successfully deleted User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null;
                Save_Misc_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Failed to delete User:   {username}"); Console.ForegroundColor = ConsoleColor.White;
                username = null; 
                Save_Misc_Dictionary_Public();
                Thread.Sleep(2000); return;
            }
        }







        private static protected string DecryptDataWithAes(string text, string aes, string iv)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(aes);
                aesAlgorithm.IV = Convert.FromBase64String(iv);
                aesAlgorithm.Padding = PaddingMode.ISO10126;


                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                byte[] bytes = Convert.FromBase64String(text);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static protected string EncryptDataWithAes(string text, string aes, string iv)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(aes);
                aesAlgorithm.IV = Convert.FromBase64String(iv);
                aesAlgorithm.Padding = PaddingMode.ISO10126;

                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(text);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }

        private static protected string CreateNewAESKey()
        {
            using (AESModule = Aes.Create())
            {
                AESModule.KeySize = 256;
                AESModule.GenerateKey();
                string result = Convert.ToBase64String(AESModule.Key);
                return result;
            }
        }
        private static protected string CreateNewVector()
        {
            using (AESModule = Aes.Create())
            {
                AESModule.KeySize = 256;
                AESModule.GenerateIV();
                string result = Convert.ToBase64String(AESModule.IV);
                return result;
            }
        }
    }
}
