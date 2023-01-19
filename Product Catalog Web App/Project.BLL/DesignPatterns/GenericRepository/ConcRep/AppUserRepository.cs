using Project.BLL.DesignPatterns.GenericRepository.BaseRep;
using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;

namespace Project.BLL.DesignPatterns.GenericRepository.ConcRep
{
    public class AppUserRepository<T> : BaseRepository<T> where T : AppUser
    {
        MyContext _db;

        public AppUserRepository()
        {
            _db = DbTool.DBInstance;
        }

        public void UpdateMail(T item)
        {
            item.ModifiedDate = DateTime.Now;
            item.Status = ENTITIES.Enums.DataStatus.Updated;
            item.IsVerified = true;
            T toBeUpdated = Find(item.ID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            Save();
        }
    }
}
