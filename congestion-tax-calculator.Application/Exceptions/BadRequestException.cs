namespace congestion_tax_calculator.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string objectName, object id) : base($"{objectName} ({id}) was not found")
        {
        }
    }
}
