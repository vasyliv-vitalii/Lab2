namespace BLLayer.Authentication.Interfaces;

public interface IIdentityInfoSetter
{
    public int UserId { set; }
    public string UserRole { set; }
}