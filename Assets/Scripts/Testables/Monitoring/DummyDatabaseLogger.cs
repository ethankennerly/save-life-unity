using System.Collections.Generic;

namespace EthanKennerly.PoorLife
{
    public class DummyDatabaseLogger : IDatabaseLogger
    {
        private readonly List<step_log> _unflushedLogs;
        private int _currentIndex;

        public DummyDatabaseLogger()
        {
            _unflushedLogs = new List<step_log>();
        }

        public void Log(step_log stepLog)
        {
            _currentIndex++;
            stepLog.index = _currentIndex;
            _unflushedLogs.Add(stepLog);
        }

        public void Flush()
        {
            _unflushedLogs.Clear();
        }

        public void Dispose()
        {
        }
    }
}
