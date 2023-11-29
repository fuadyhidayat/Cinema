using Logic.Common.Exceptions;

namespace Logic.Common.Extensions;

public static class ExceptionExtensions
{
    public static IReadOnlyCollection<string> GetAllErrorMessages(this Exception exception)
    {
        var errorMessages = new List<string>();

        if (exception is AggregateException ae)
        {
            foreach (var innerException in ae.Flatten().InnerExceptions)
            {
                errorMessages.AddRange(innerException.GetAllErrorMessages());
            }
        }
        else if (exception is AppValidationException ave)
        {
            errorMessages.AddRange(ave.ErrorMessages);
        }
        else
        {
            errorMessages.Add(exception.Message);
        }

        return errorMessages;
    }
}
