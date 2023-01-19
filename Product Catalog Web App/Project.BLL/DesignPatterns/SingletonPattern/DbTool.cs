using Project.DAL.Context;

namespace Project.BLL.DesignPatterns.SingletonPattern
{
    public class DbTool
    {
        DbTool() { }

        static MyContext _dbInstance;

        public static MyContext DBInstance
        {
            get
            {

                if (_dbInstance == null)
                {
                    _dbInstance = new MyContext();
                }
                return _dbInstance;
            }
        }
    }
}
