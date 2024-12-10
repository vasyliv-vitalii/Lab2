namespace BLLayer.Authentication.Interfaces;

public interface IIdentityInfoGetter
{
    public int UserId { get; }
    public string UserRole { get; }
}