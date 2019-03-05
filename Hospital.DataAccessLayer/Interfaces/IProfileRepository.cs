using Hospital.DataAccessLayer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        void Create(T user);
        T Get(string  userId);

    }
}
