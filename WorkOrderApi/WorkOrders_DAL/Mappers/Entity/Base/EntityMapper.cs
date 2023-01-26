using System.Reflection;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;
using WorkOrders_DAL.Mappers.Base;

namespace WorkOrders_DAL.Mappers.Entity.Base
{
    public class EntityMapper<TEntity, TDto, TDtoPatch> : Mapper<TEntity, TDto>, IEntityMapper<TEntity, TDto, TDtoPatch>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
        where TDtoPatch : IDto
    {
        public virtual void Patch(TEntity f, TDtoPatch s)
        {
            var sProps = typeof(TDtoPatch).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            Type fType = typeof(TEntity);

            foreach (var prop in sProps)
            {
                var sValue = prop.GetValue(s, null);
                PropertyInfo fProp = fType.GetProperty(prop.Name);

                if (sValue != null && fProp != null && fProp.CanWrite)
                    fProp.SetValue(f, sValue);
            }
        }

        public virtual TEntity Map(TDto dto) => base.Map(dto);
        public virtual TDto Map(TEntity entity)
        {
            var entityAsDto = base.Map(entity);
            entityAsDto.SetInitId(entity.Id);

            return entityAsDto;
        }

        public virtual IList<TEntity> Map(IList<TDto> sList)
        {
            IList<TEntity> fList = new List<TEntity>();

            foreach (TDto s in sList)
            {
                fList.Add(Map(s));
            }

            return fList;
        }
        public virtual IList<TDto> Map(IList<TEntity> fList)
        {
            IList<TDto> sList = new List<TDto>();

            foreach (TEntity f in fList)
            {
                sList.Add(Map(f));
            }

            return sList;
        }


        ////public void Patch(TEntity entity, TDto dto)
        ////{
        ////    TEntity dtoAsEntity = base.Map(dto);

        ////}

        ////public virtual TEntity Map(TPostDto s)
        ////{
        ////    TEntity f = new TEntity();

        ////    var sProps = typeof(TPostDto).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
        ////    Type fType = typeof(TEntity);

        ////    foreach (var prop in sProps)
        ////    {
        ////        var sValue = prop.GetValue(s, null);
        ////        PropertyInfo fProp = fType.GetProperty(prop.Name);

        ////        if (sValue != null && fProp != null)
        ////            fProp.SetValue(f, sValue);
        ////    }

        ////    return f;
        ////}

        ////public void Patch(TDto dto)
        ////{

        ////}

        //public override TEntity Map(TDto dto)
        //{
        //    TEntity entity = base.Map(dto);
        //    entity.Id = dto.Id;
        //    entity.SetInitId(dto.Id);

        //    return entity;
        //}

        //public override TDto Map(TEntity entity)
        //{
        //    TDto dto = base.Map(entity);

        //    return dto;
        //}

        //public override IList<TEntity> Map(IList<TDto> dtos)
        //{
        //    IList<TEntity> entities = new List<TEntity>();

        //    foreach (TDto dto in dtos)
        //    {
        //        entities.Add(this.Map(dto));
        //    }

        //    return entities;
        //}

        //public override IList<TDto> Map(IList<TEntity> entities)
        //{
        //    IList<TDto> dtos = new List<TDto>();

        //    foreach (TEntity entity in entities)
        //    {
        //        dtos.Add(this.Map(entity));
        //    }

        //    return dtos;
        //}

        //public virtual void Combine(TEntity entity, TEntity dtoAsEntity)
        //{
        //    var dProps = typeof(TDto).GetProperties();

        //    foreach (var dProp in dProps)
        //    {
        //        var dValue = dProp.GetValue(dtoAsEntity);

        //        if (dValue == null)
        //            continue;

        //        try
        //        {
        //            dProp.SetValue(entity, dValue);
        //        }
        //        catch (ArgumentException e)
        //        {
        //            if (e.Message != Constants.ErrorMessages.ARGUMENT_EXCEPTION_SET_NOT_FOUND)
        //                throw e;
        //        }
        //    }
        //}

    }

    //////public abstract class EntityMapper_New<TEntity, TDto, TPostDto> : Mapper<EntityBase, DtoBase>
    //////    where TEntity : EntityBase, new()
    //////    where TDto : DtoBase, new()
    //////    where TPostDto : IPostDto
    //////{
    //////    public virtual void Patch(TEntity entity, TDto dto)
    //////    {
    //////        TEntity f = new TEntity(dto.Id);


    //////        var sProps = typeof(TPostDto).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
    //////        Type fType = typeof(TEntity);

    //////        foreach (var prop in sProps)
    //////        {
    //////            var sValue = prop.GetValue(s, null);
    //////            PropertyInfo fProp = fType.GetProperty(prop.Name);

    //////            if (sValue != null && fProp != null)
    //////                fProp.SetValue(f, sValue);
    //////        }
    //////    }

    //////    public new virtual TEntity Map(TDto dto)
    //////    {
    //////        return null;
    //////    }

    //////    public new virtual TDto Map(TEntity entity)
    //////    {
    //////        return null;

    //////    }
    //////}
}
