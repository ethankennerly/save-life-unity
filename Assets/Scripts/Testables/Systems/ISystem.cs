using System.Collections.Generic;

namespace EthanKennerly.PoorLife
{
    public interface ISystem
    {
        void Update(float deltaTime, List<step_log> commands);
    }
}
