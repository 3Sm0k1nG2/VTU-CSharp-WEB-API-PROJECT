using WorkOrders_BAL.Interfaces;

namespace WorkOrders_BAL.Dtos.Base
{
    public class DtoBase : IDto
    {
        private Guid _id;

        public Guid Id
        {
            get => _id;
        }

        protected DtoBase() { }
        protected DtoBase(Guid id)
        {
            _id = id;
        }

        public void SetInitId(Guid id)
        {
            if (_id == Guid.Empty)
                _id = id;
        }
    }
}
