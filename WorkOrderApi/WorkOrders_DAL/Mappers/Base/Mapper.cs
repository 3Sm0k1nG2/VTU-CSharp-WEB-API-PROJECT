using System.Reflection;
using WorkOrders_DAL.Interfaces.Mappers;

namespace WorkOrders_DAL.Mappers.Base
{
    public class Mapper<TFirst, TSecond> : IMapper<TFirst, TSecond>
        where TFirst : class, new()
        where TSecond : class, new()
    {
        public virtual TFirst Map(TSecond s)
        {
            TFirst f = new TFirst();

            var sProps = typeof(TSecond).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            Type fType = typeof(TFirst);

            foreach (var prop in sProps)
            {
                var sValue = prop.GetValue(s, null);
                PropertyInfo fProp = fType.GetProperty(prop.Name);

                if (sValue != null && fProp != null)
                    fProp.SetValue(f, sValue);
            }

            return f;
        }
        public virtual TSecond Map(TFirst f)
        {
            TSecond s = new TSecond();

            var fProps = typeof(TFirst).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            Type sType = typeof(TSecond);

            foreach (var prop in fProps)
            {
                var fValue = prop.GetValue(f, null);
                PropertyInfo sProp = sType.GetProperty(prop.Name);

                if (fValue != null && sProp != null && sProp.CanWrite)
                    sProp.SetValue(s, fValue);
            }

            return s;
        }

        public virtual IList<TFirst> Map(IList<TSecond> sList)
        {
            IList<TFirst> fList = new List<TFirst>();

            foreach (TSecond s in sList)
            {
                fList.Add(Map(s));
            }

            return fList;
        }
        public virtual IList<TSecond> Map(IList<TFirst> fList)
        {
            IList<TSecond> sList = new List<TSecond>();

            foreach (TFirst f in fList)
            {
                sList.Add(Map(f));
            }

            return sList;
        }
    }
}
