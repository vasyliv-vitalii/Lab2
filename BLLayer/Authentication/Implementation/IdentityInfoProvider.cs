using BLLayer.Authentication.Interfaces;

namespace BLLayer.Authentication.Implementation;

public class IdentityInfoProvider : IIdentityInfoGetter, IIdentityInfoSetter
{
    private int _userId;
    private string _userRole;
    
    public int UserId
    {
        get => ValidateId(_userId);
        set => _userId = value;
    }

    public string UserRole
    {
        get => ValidateRole(_userRole);
        set => _userRole = value;
    }

    private int ValidateId(int? id)
    {
        if (id == null || id == 0)
        {
            throw new UnauthorizedAccessException();
        }

        return id.Value;
    }

    private string ValidateRole(string role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new UnauthorizedAccessException();
        }

        return role;
    }
}