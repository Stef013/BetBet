using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Data
{
    public interface IRepository<T>  where T : class
    {
        int GetID(T obj);
        bool Create(T obj);
        bool Delete(T obj);
        void Update(T obj);
    }
}
