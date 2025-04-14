using TaskManager.Data.Abstractions;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Helpers;

namespace TaskManager.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : TableData, new()
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public BaseRepository()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.flags);
            connection.CreateTable<T>();
        }

        public int SaveEntity(T entity)
        {
            int result = 0;
            try
            {
                if (entity.Id == 0)
                {
                    result = connection.Insert(entity);
                    StatusMessage = $"{result} row(s) added";
                    return entity.Id;
                }
                else
                {
                    result = connection.Update(entity);
                    StatusMessage = $"{result} row(s) updated";
                    return entity.Id;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                return 0;
            }
        }

        public int SaveEntityWithChildren(T entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    connection.InsertWithChildren(entity, true);
                    return entity.Id;
                }
                else
                {
                    connection.UpdateWithChildren(entity);
                    return entity.Id;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                return 0;
            }
        }

        public T? GetEntity(int id)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public T? GetEntityWithChildren(int id)
        {
            try
            {
                return connection.GetWithChildren<T>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public T? GetEntityByName(string name)
        {
            
            try
            {           
                var entity = connection.Table<T>().FirstOrDefault(x => x.Name == name);
                if(entity != null)
                {
                    return connection.GetWithChildren<T>(entity.Id);
                }
            }
            catch(Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return null;
        }
        public List<T>? GetEntities()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public List<T>? GetEntitiesWithChildren()
        {
            try
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public void DeleteEntity(T entity)
        {
            try
            {
                connection.Delete(entity);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void DeleteEntityWithChildren(T entity)
        {
            try
            {
                connection.Delete(entity, true);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
