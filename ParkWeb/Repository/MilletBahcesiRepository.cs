using ParkWeb.Models;
using ParkWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkWeb.Repository
{
    public class MilletBahcesiRepository:GenericRepository<MilletBahcesi>, IMilletBahcesiRepository
    {
        private readonly IHttpClientFactory _clientfactory;

        public MilletBahcesiRepository(IHttpClientFactory clientfactory):base (clientfactory)
        {
            _clientfactory = clientfactory;
        }
    }
}
