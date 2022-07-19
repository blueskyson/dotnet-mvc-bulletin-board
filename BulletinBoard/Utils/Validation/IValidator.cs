namespace BulletinBoard.Utils.Validation;

public interface IValidator {
    public bool IsValidName(string? name);
    public bool IsValidDisplayName(string? name);
    public bool IsValidPassword(string? password);
}