using System.Text;

namespace Logic.Common.Exceptions;

public class AppValidationException(IEnumerable<string> errorMessages)
    : Exception("One or more validation failures have occurred.")
{
    public IEnumerable<string> ErrorMessages { get; } = errorMessages;

    public override string ToString()
    {
        var summary = new StringBuilder();

        foreach (var errorMessage in ErrorMessages)
        {
            _ = summary.AppendLine(errorMessage);
        }

        return summary.ToString();
    }
}
