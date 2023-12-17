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

            /*if (File.Exists(LoggedUserJsonFilePath))
            {
                StreamReader streamReader = new(LoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                var localUser = JsonSerializer.Deserialize<User>(json);
                streamReader.Close();

                if (localUser != null)
                {
                    LoggedUser = localUser;
                }
            }*/
            MainPage = new AppShell();
        }
    }
}
