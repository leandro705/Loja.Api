using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;

namespace Loja.Repository.Repositories
{
    public class MunicipioRepository : Repository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(LojaDbContext context) : base(context)
        {
        }
    }
}
