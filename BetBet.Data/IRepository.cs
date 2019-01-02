using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Data
{
    public interface IRepository<T>  where T : class
    {
        bool Create(T obj);
        //List<T> Read(T obj);
        int GetID(T obj);
        void Update(T obj);
        bool Delete(int id);
    }
}
