using vaerkstedGenafl.Views;

namespace vaerkstedGenafl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}