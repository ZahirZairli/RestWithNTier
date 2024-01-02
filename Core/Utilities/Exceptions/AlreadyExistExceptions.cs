namespace Core.Utilities.Exceptions;

public class AlreadyExistExceptions:Exception
{
    public AlreadyExistExceptions(string message):base(message){}
    public AlreadyExistExceptions():base("Entity already exist!"){}
}
