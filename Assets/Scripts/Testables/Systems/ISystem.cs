using System.Collections.Generic;

namespace EthanKennerly.SaveLife
{
    public interface ISystem
    {
        void Update(float deltaTime, List<IComponent> commands);
    }
}
