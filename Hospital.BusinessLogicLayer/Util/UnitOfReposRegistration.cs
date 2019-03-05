using Hospital.DataAccessLayer.Interfaces;
using Hospital.DataAccessLayer.Repositories;
using Ninject.Modules;
namespace Hospital.BusinessLogicLayer.Util
{

    public class UnitOfReposRegistration : NinjectModule
    {
        private string _connectionString;
        public UnitOfReposRegistration(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        public override void Load()
        {
            Bind<IUnitOfRepositories>().To<UnitOfRepositories>().WithConstructorArgument(_connectionString);
        }
    }
}
