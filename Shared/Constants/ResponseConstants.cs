namespace Shared.Constants;

public class ResponseConstants
{
    #region Success

    public const string S0000 = "S0000";
    
    #endregion

    #region Information

    public const string I0000 = "I0000"; //404
    public const string I0001 = "I0001"; //403

    #endregion

    #region Warning

    public const string W0000 = "W0000"; //Data is nothing
    public const string W0001 = "W0001"; //Duplicate data
    public const string W0002 = "W0002"; //Users not found
    public const string W0003 = "W0003"; //Email duplicate
    public const string W0004 = "W0004"; //Otp code expire

    #endregion

    #region Error

    public const string E0000 = "E0000"; //Unexpected error
    public const string E0001 = "E0001"; //Fail email send


    #endregion
}