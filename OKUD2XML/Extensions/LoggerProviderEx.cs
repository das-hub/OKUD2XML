using das.Extensions.Logger;
using das.Extensions.Logger.Extensions;

namespace OKUD2XML.Extensions
{
    public static class LoggerProviderEx
    {
        public static LoggerProvider UseDefaultWriters(this LoggerProvider provider)
        {
            return provider
                    .AddConsole(format: "{DateTime:HH:mm:ss}|{Level}|{Source}|{Message}")
                    .AddEveryDayFile(format: "{DateTime:dd.MM.yyyy HH:mm:ss}|{Level}|{Source}|{Message}");
        }
    }
}
