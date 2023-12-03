using gwork.maui.Models;

namespace gwork.maui
{
    public partial class App : Application
    {
        public static User? LoggedUser;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
