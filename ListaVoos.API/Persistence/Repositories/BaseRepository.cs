using ListaVoos.API.Persistence.Context;

namespace ListaVoos.API.Persistence.Repositories
{
  public class BaseRepository
  {
    protected readonly DataContext _context;
    public BaseRepository(DataContext context)
    {
      _context = context;
    }
  }
}