using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Project.BLL.DesignPatterns.GenericRepository.IntRep
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region List Commands
        List<T> GetAll();
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();
        #endregion

        #region Modify Commands
        /// <summary>
        /// Kendini verilen tipe göre ayarlayarak ekleme yapan metodumuz
        /// </summary>
        /// <param name="item">Lütfen ilgili Entity tipinde bir argüman veriniz</param>
        void Add(T item);
        void Update(T item);
        /// <summary>
        /// Verinizi pasife çeken metottur, veriyi yok etmez.
        /// </summary>
        /// <param name="item">İlgili entity tipinde argüman giriniz</param>
        void Delete(T item); //Veriyi Pasife Çeker
        /// <summary>
        /// Verinizi yok eden metottur.Dikkatli kullanınız!
        /// </summary>
        /// <param name="item">İlgili entity tipinde argüman giriniz</param>
        void Destroy(T item); //Veriyi yok eder!
        #endregion

        #region Linq Expressions
        /// <summary>
        /// Veritabanında ilgili ifadeyle ilgili yapı var mı yok mu bunu cevaplar
        /// </summary>
        /// <param name="exp">Expression ifadesi giriniz (T,bool)</param>
        /// <returns></returns>
        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);
        #endregion

        /// <summary>
        /// Primary key'e göre veri sorgulayıp döndüren metot.
        /// </summary>
        /// <param name="id">Verinizin primary key değerini giriniz</param>
        /// <returns></returns>
        T Find(int id);
    }
}
