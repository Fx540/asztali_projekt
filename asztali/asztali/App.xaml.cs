using asztali.ModelView;

namespace asztali
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();
            Database = new DatabaseService();

            MainPage = new AppShell();
        }
    }
}