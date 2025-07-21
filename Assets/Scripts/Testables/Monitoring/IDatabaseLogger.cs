using System;

namespace EthanKennerly.PoorLife
{
    public interface IDatabaseLogger : IDisposable
    {
        void Log(step_log stepLog);
        void Flush();
    }
}
