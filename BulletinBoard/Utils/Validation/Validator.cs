using System.Configuration;
namespace BulletinBoard.Utils.Validation;

public class Validator : IValidator
{
    private static StringValidator? _stringValidator;

    public Validator()
    {
        _stringValidator = new StringValidator(1, 20, " \\\'\"`");
    }

    public bool IsValidString(string? str)
    {
        try
        {
            _stringValidator!.Validate(str);
            return true;
        }
        catch (ArgumentException) { }

        return false;
    }
}