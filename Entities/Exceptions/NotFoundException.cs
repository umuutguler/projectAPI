namespace Entities.Exceptions
{
    public abstract partial class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

    }
}