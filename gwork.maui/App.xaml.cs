using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Services;
using System.Text.Json;

namespace gwork.maui
{
    public partial class App : Application
    {
        public static User? LoggedUser;
        public static string LoggedUserJsonFilePath = Path.Combine(FileSystem.AppDataDirectory, "gworkLoggedUser.json");

        public App()
        {
            InitializeComponent();

            if (File.Exists(LoggedUserJsonFilePath))
            {
                StreamReader streamReader = new(LoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                var localUser = JsonSerializer.Deserialize<User>(json);
                streamReader.Close();

                if (localUser != null)
                {
                    LoggedUser = localUser;
                }
            }

            /*if (File.Exists(LoggedUserJsonFilePath))
            {
                StreamReader streamReader = new(LoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                var localUser = JsonSerializer.Deserialize<User>(json);
                streamReader.Close();

                if (localUser != null)
                {
                    var userDatabase = new UserDatabase();
                    var storedUserTask = userDatabase.GetUserAsync(localUser.Id);//pobranie z bazy uzytkownika 
                    storedUserTask.Wait();
                    var storedUser = storedUserTask.Result;

                    if (storedUser != null)
                    {

                        if (localUser.Password == storedUser.Password)//sprawdzenie czy hasła się zgadzają
                            LoggedUser = storedUser;
                        else
                            File.Delete(LoggedUserJsonFilePath);

                        Shell.Current.DisplayAlert("123", LoggedUser.Email, "c");
                    }
                }
            }*/

            MainPage = new AppShell();
        }
    }
}
