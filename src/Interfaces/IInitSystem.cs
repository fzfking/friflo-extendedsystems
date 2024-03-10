using Friflo.Engine.ECS;

namespace FrifloExt.Interfaces
{
    public interface IInitSystem : ISystem
    {
        public void OnInit(EntityStore store);
    }
}