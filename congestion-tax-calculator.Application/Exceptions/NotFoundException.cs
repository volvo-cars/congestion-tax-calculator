namespace congestion_tax_calculator.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string objectName, object id) : base($"{objectName} ({id}) was not found")
        {

        }
    }
}
