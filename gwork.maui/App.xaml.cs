using gwork.maui.Models;

namespace gwork.maui
{
    public partial class App : Application
    {
        public static User? LoggedUser;
        public static string LoggedUserJsonFilePath = Path.Combine(FileSystem.AppDataDirectory, "gworkLoggedUser.json");

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}