namespace BulletinBoard.Utils.Validation;

/// <summary>
/// Validate a given data.
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Validate a string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsValidString(string? str);
}