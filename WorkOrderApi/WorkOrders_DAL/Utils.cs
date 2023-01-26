namespace WorkOrders_DAL
{
    public class Utils
    {
        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            if(t.Equals(typeof(bool)))
            {
                return Convert.ChangeType(value.ToString() == "Yes", t);
            }

            return Convert.ChangeType(value, t);
        }
    }
}
