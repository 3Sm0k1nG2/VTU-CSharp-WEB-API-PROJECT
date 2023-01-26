using System.ComponentModel.DataAnnotations.Schema;
using WorkOrders_BAL.Interfaces;

namespace WorkOrders_BAL.Entities.Base
{
    public class EntityBase : IEntity
    {
        private Guid _id;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id == Guid.Empty)
                    _id = value;
            }
        }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        protected EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
