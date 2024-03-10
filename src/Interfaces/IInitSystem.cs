using Friflo.Engine.ECS;

namespace ExtendedSystems.Interfaces
{
    public interface IInitSystem : ISystem
    {
        public void OnInit(EntityStore store);
    }
}