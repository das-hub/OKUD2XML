using System;
using das.Extensions.Logger;
using OKUD2XML.Engine;

namespace OKUD2XML.Core
{
    public interface ICommand
    {
        void Invoke();
    }

    public class Commands
    {
        public static ICommand Extract => new ExtractCommand(App.Log, App.Paths.GetFilesFromSource("*.rtf"), App.Paths.RESULT);
        public static ICommand Start => new StartCommand();
    }

    public class StartCommand : ICommand
    {
        public void Invoke()
        {
            Console.WriteLine($@"Для обработки документов формы по ОКУД 0406009, разместите файлы в каталог [{App.Paths.SOURCE}].{Environment.NewLine}" +
                              $"Файлы берутся по маске [*.rtf].{Environment.NewLine}" +
                              $"Результат работы программы в [{App.Paths.RESULT}].{Environment.NewLine + Environment.NewLine}" +
                              "Для начала работы нажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    public class ExtractCommand : ICommand
    {
        private readonly ILogger _log;
        private readonly string[] _files;
        private readonly string _target;

        public ExtractCommand(ILogger log, string[] files, string target)
        {
            _log = log;
            _files = files;
            _target = target;
        }

        public void Invoke()
        {
            foreach (string file in _files)
            {
                _log.Info($"Начинаю загрузку [{file}]...");

                try
                {
                    foreach (Okud okud in OkudPackage.OpenRtf(file))
                    {
                        _log.Debug($" Извлечен ОКУД [{okud.Name}], сохраняю его...");

                        string result = okud.SaveAs(_target);

                        _log.Info($" ОКУД [{okud.Name}] успешно сохранен как [{result}].");
                    }
                }
                catch (Exception e)
                {
                    _log.Error($" Произошла ошибка [{e.GetType()}]: {e.Message}");
                    throw;
                }

                _log.Info($"[{file}] обработан успешно.");
            }

            _log.Info("Выполнение завершено.");

            Console.ReadKey();
        }
    }
}
