using System.Configuration;
namespace BulletinBoard.Utils.Validation;

/// <summary>
/// Validate a given data.
/// </summary>
public class Validator : IValidator
{
    private static StringValidator? _stringValidator;

    /// <summary>
    /// Define the validation rule.
    /// </summary>
    public Validator()
    {
        _stringValidator = new StringValidator(1, 20, " \\\'\"`");
    }

    /// <summary>
    /// Validate a string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
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