using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class RegisterLogic : IRegisterLogic
{
    private IUnitOfWork _unitOfWork;
    private IHasher _hasher;

    public RegisterLogic(IUnitOfWork unitOfWork, IHasher hasher)
    {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }

    public bool UserNameExists(string name)
    {
        IQueryable<User>? userQuery =
            from u in _unitOfWork.UserRepository.GetDbSet()
            where u.Name == name
            select u;

        return (userQuery.FirstOrDefault() != null);
    }

    public async Task<int> AddUserAsync(User user)
    {
        try
        {
            ProcessSaltAndHash(user);
            _unitOfWork.UserRepository.Add(user);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }

        return -1;
    }

    private void ProcessSaltAndHash(User user)
    {
        user.Salt = _hasher.GenerateSaltBase64();
        user.Password = _hasher.GenerateHashBase64(user.Password!, user.Salt);
    }

}