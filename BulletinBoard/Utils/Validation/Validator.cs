using System.Configuration;
namespace BulletinBoard.Utils.Validation;

public class Validator : IValidator
{
    private static StringValidator? _stringValidator;

    public Validator()
    {
        _stringValidator = new StringValidator(1, 20, "\\\'\"`");
    }


    public bool isValidName(string? name)
    {
        return isValidString(name);
    }

    public bool isValidDisplayName(string? displayName)
    {
        return isValidString(displayName);
    }

    public bool isValidPassword(string? password)
    {
        return isValidString(password);
    }

    public bool isValidString(string? str)
    {
        try
        {
            _stringValidator!.Validate(str);
            return true;
        }
        catch (ArgumentException)
        {
        }
        return false;
    }
}