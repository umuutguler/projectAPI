namespace Entities.Exceptions
{
    public abstract partial class NotFoundException
    {
        public sealed class ProductNotFoundException : NotFoundException
        {
            public ProductNotFoundException(int id)
                : base($"The Product with id : {id} could not found")
            {

            }
        }

    }
}