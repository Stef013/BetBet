using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Data
{
    public interface IRepository<T,U>  where T : class where U : class
    {
        bool Create(T obj);
        int GetID(T obj);
        void Update(U obj);
    }
}
