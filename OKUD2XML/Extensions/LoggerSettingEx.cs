using das.Logger;
using das.Logger.Extensions;

namespace OKUD2XML.Extensions
{
    public static class LoggerSettingEx
    {
        public static LoggerSetting UseDefaultSetting(this LoggerSetting setting)
        {
            return setting
                    .AddConsole(format: "{DateTime:HH:mm:ss}|{Level}|{Source}|{Message}")
                    .AddEveryDayFile(format: "{DateTime:dd.MM.yyyy HH:mm:ss}|{Level}|{Source}|{Message}");
        }
    }
}
