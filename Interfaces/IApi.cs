using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Interfaces
{
    public interface IApi<T>
    {
        public Task<T> CallApi(string url);
    }
}
