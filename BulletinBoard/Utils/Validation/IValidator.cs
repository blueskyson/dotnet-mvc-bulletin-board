namespace BulletinBoard.Utils.Validation;

public interface IValidator {
    public bool isValidName(string? name);
    public bool isValidDisplayName(string? name);
    public bool isValidPassword(string? password);
}