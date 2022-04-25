using HeroesVillains.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Services
{
    public class HeroVillianService<T> : IApi<T>
    {
        public Task<T> CallApi(string url)
        {
            throw new NotImplementedException();
        }
    }
}
