namespace Measurements.BusinessLogic2.Entities;

[Serializable]
public class ResultException: Exception
{
    public string Error { get; }
    public new string Message { get; }

    public int StatusCode { get; }

    public ResultException(string error, string message, int statusCode) : base(message)
    {
        Error = error;
        Message = message;
        StatusCode = statusCode;
    }
}