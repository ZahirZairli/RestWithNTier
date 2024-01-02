namespace Core.Utilities.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string message): base(message){}
    public NotFoundException():base("The entity is not exist in the database!"){}
}
