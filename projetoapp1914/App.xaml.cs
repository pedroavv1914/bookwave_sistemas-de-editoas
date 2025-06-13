using projetoapp1914.Helpers;

namespace projetoapp1914
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelpers _db;
        public static SQLiteDatabaseHelpers Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                    Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    "bd_bookwave.db3");
                    _db = new SQLiteDatabaseHelpers(path);
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
