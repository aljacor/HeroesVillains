using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Helpers
{
    public class UsingApiHelper
    {
        private IConfiguration Configuration;

        public UsingApiHelper(IConfiguration Config)
        {
            Configuration = Config;
        }

        public string CompleteSearchUri(string param)
        {
            try
            {
                if (string.IsNullOrEmpty(param))
                    return Configuration.GetConnectionString("HeroesApi").ToString();

                string con = Configuration.GetConnectionString("HeroesApi").ToString();
                return  con + "/search/" + param;

            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }

        }

        public string SearchById(string param)
        {
            return Configuration.GetConnectionString("HeroesApi").ToString() + "/" + param;
        }
    }
}
