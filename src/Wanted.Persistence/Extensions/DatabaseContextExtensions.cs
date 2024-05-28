namespace Wanted.Persistence.Extensions;

using EntityFramework.Exceptions.Common;
using ErrorOr;

public static class DatabaseContextExtensions
{
    public static async Task<ErrorOr<Success>> SaveChangesAsyncExt(
        this DatabaseContext context,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var isSuccess = (await context.SaveChangesAsync(cancellationToken)) > 0;
            return isSuccess ? Result.Success : Error.Failure("Database is not accessible");
        }
        catch (UniqueConstraintException e)
        {
            return Error.Failure($"UniqueConstraint {e.ConstraintName} {e.Message}");
        }
        catch (CannotInsertNullException e)
        {
            return Error.Failure($"CannotInsertNull {e.Message}");
        }
        catch (MaxLengthExceededException e)
        {
            return Error.Failure($"MaxLengthExceeded {e.Message}");
        }
        catch (NumericOverflowException e)
        {
            return Error.Failure($"NumericOverflow {e.Message}");
        }
        catch (ReferenceConstraintException e)
        {
            return Error.Failure($"ReferenceConstraint {e.ConstraintName}  {e.Message}");
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }
}
