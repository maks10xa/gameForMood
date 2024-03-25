using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Diagnostics;

namespace gameForMood.Services.Interceptors
{
    public class LogSqlQueryInterceptor : DbCommandInterceptor
    {
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            if (eventData.Duration.TotalMilliseconds > 500)
            {
                Debug.WriteLine($"Very slow query ${command.CommandText}, MS:${eventData.Duration.TotalMilliseconds}" + Environment.NewLine + Environment.NewLine);
            }

            return base.ReaderExecuted(command, eventData, result);
        }
    }
}
