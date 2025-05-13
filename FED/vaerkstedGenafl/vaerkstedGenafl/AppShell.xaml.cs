using vaerkstedGenafl.Views;

namespace vaerkstedGenafl
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(FakturaPage), typeof(FakturaPage));
            Routing.RegisterRoute(nameof(NyOpgavePage), typeof(NyOpgavePage));
        }
    }
}
