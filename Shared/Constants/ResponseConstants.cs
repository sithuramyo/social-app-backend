namespace Shared.Constants;

public class ResponseConstants
{
    #region Success

    public const string S0000 = "S0000";
    
    #endregion

    #region Information

    public const string I0000 = "I0000"; //401
    public const string I0001 = "I0001"; //403
    public const string I0002 = "I0002"; //404
    public const string I0003 = "I0003"; //405
    public const string I0004 = "I0004"; //502
    public const string I0005 = "I0005"; //503
    public const string I0006 = "I0006"; //500

    #endregion

    #region Warning

    public const string W0000 = "W0000"; //Request data is nothing
    public const string W0001 = "W0001"; //Duplicate data
    public const string W0002 = "W0002"; //Not found
    public const string W0003 = "W0003"; //Email duplicate
    public const string W0004 = "W0004"; //Otp code expire
    public const string W0005 = "W0005"; //Invalid base64
    public const string W0006 = "W0006"; //Friend request is already sent
    public const string W0007 = "W0007"; //Friend already
    public const string W0008 = "W0008"; //You can't sent request yourself
    public const string W0009 = "W0009"; //You can't sent request blocked user
    public const string W0010 = "W0010"; //You can't unfriend blocked user
    public const string W0011 = "W0011"; //You can't block blocked user
    public const string W0012 = "W0012"; //You missing post access type

    #endregion

    #region Error

    public const string E0000 = "E0000"; //Unexpected error
    public const string E0001 = "E0001"; //Fail email send


    #endregion
}