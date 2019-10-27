using Infrastructure.Data;
using MyNotesCore.Abstract;
using MyNotesCore.Common;
using MyNotesCore.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MyNotesRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly MyNotesDatabase _database;
        
        public MyNotesRepository()
        {
            _database = new MyNotesDatabase(Path.Combine
                (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyNotesSQLite.db3"));
            _database.Database.CreateTableAsync<Note>();
        }

        public async Task<Result<T>> Create(T item)
        {
            try
            {
                Result<T> result = new Result<T>(false, "An error ocurred saving the note", item);
                int id = await _database.Database.InsertAsync(item);
                if(id > 0)
                {
                    item.Id = id;
                    result = new Result<T>(true, item);
                }

                return result;

            }
            catch (SQLiteException e)
            {
                return new Result<T>(false, e.InnerException?.Message ?? e.Message);
            }
        }

        public async Task<Result<T>> Delete(int id)
        {
            try
            {
                T item = await _database.Database.GetAsync<T>(id);
                int result = await _database.Database.DeleteAsync(item); 
                if(result > 0)
                   return new Result<T>(true, item);
                return new Result<T>(false, "This note can't be deleted");
            }
            catch (SQLiteException e)
            {
                return new Result<T>(false, e.InnerException?.Message ?? e.Message);
            }
        }

        public async Task<ICollection<T>> GetAll()
        {
            try
            {
                return await _database.Database.Table<T>().ToListAsync();
            }
            catch (SQLiteException)
            {
                return new List<T>();
            }
        }

        public async Task<Result<T>> GetById(int id)
        {
            try
            {
                T item = await _database.Database.FindAsync<T>(id);
                if (item != null)
                    return new Result<T>(true, item);
                return new Result<T>(false, "Item not found");
            }
            catch (SQLiteException e)
            {
                return new Result<T>(false, e.InnerException?.Message ?? e.Message);
            }
        }

        public async Task<Result<T>> Update(T item)
        {
            try
            {
                int id = await _database.Database.UpdateAsync(item);
                if (id > 0)
                    return new Result<T>(true, item);
                return new Result<T>(false, "This item can't be updated");
            }
            catch (SQLiteException e)
            {
                return new Result<T>(false, e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
