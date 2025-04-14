using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : TableData, new()
    {

        // Create/Update
        int SaveEntity(T entity);
        int SaveEntityWithChildren(T entity);
        // Read one/read many
        T? GetEntity(int id);
        T? GetEntityWithChildren(int id);
        T? GetEntityByName(string name);
        List<T>? GetEntities();
        List<T>? GetEntitiesWithChildren();
        //Delete
        void DeleteEntity(T entity);
        void DeleteEntityWithChildren(T entity);


    }
}
