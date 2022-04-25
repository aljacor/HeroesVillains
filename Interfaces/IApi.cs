using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Interfaces
{
    interface IApi<T>
    {
        Task<T> CallApi(string url);
    }
}
