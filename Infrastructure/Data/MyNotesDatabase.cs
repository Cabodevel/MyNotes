using SQLite;

namespace Infrastructure.Data
{
    public class MyNotesDatabase
    {
        private SQLiteAsyncConnection _database;
        public SQLiteAsyncConnection Database { get => _database; private set { _database = value; } }

        public MyNotesDatabase(string Path)
        {
            if(_database == null)
                _database = new SQLiteAsyncConnection(Path);
        }
    }
}
