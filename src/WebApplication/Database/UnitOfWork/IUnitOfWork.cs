using WebApplication;
using WebApplication.Database;

namespace Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; set; }
        IGenreRepository Genres { get; set; }
        IPersonRepository Persons { get; set; }
    }
}