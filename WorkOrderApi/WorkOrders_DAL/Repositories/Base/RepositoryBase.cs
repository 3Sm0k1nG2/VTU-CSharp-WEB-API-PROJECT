using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;
using WorkOrders_DAL.Interfaces.Repositories;
using WorkOrders_DAL.Mappers.Entity.Base;

namespace WorkOrders_DAL.Repositories.Base
{
    public class RepositoryBase<TEntity, TDto, TDtoPatch> : IRepository<TEntity, TDto, TDtoPatch>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
        where TDtoPatch : IDto
    {
        protected readonly WorkOrderDbContext _context;
        protected readonly IEntityMapper<TEntity, TDto, TDtoPatch> _mapper;
        protected readonly IMemoryCache? _memoryCache;

        public RepositoryBase(WorkOrderDbContext context, IMemoryCache? memoryCache)
        {
            _context = context;
            _mapper = new EntityMapper<TEntity, TDto, TDtoPatch>();
            _memoryCache = memoryCache;
        }

        public RepositoryBase(WorkOrderDbContext context, IEntityMapper<TEntity, TDto, TDtoPatch> entityMapper, IMemoryCache? memoryCache)
        {
            _context = context;
            _mapper = entityMapper;
        }

        #region Basic CRUD
        public async virtual Task<TDto> Add(TDto dto)
        {
            TEntity entity = _mapper.Map(dto);

            await _context.Set<TEntity>().AddAsync(entity);
            _context.SaveChanges();

            return _mapper.Map(entity);
        }

        public async virtual Task<IEnumerable<TDto>> GetAll()
        {
            return _mapper.Map(await _context.Set<TEntity>().ToListAsync());
        }

        public async virtual Task<TDto> GetById(Guid id)
        {
            TEntity? entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException(Constants.ERROR_ENTITY_NOT_FOUND);
            }

            return _mapper.Map(entity);
        }

        public async virtual Task<TDto> UpdateById(Guid id, TDtoPatch dto)
        {
            TEntity? entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException(Constants.ERROR_ENTITY_NOT_FOUND);
            }

            _mapper.Patch(entity, dto);

            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();

            return _mapper.Map(entity);
        }

        public async virtual Task<TDto> RemoveById(Guid id)
        {
            TEntity? entity = await _context.Set<TEntity>().FindAsync(id);

            if(entity == null)
            {
                throw new NullReferenceException(Constants.ERROR_ENTITY_NOT_FOUND);
            }

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

            return _mapper.Map(entity);
        }
        #endregion
    }
}
