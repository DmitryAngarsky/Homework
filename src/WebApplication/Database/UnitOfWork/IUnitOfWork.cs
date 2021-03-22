using System;
using System.Threading.Tasks;
using WebApplication;
using WebApplication.Database;

namespace Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; set; }
        IBookRepository Books { get; set; }
        IGenreRepository Genres { get; set; }
        IPersonRepository Persons { get; set; }

        Task<int> Commit();
        void Dispose();
    }
}