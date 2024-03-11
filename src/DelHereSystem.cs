using Friflo.Engine.ECS;
using FrifloExt.Interfaces;

namespace FrifloExt
{
    public class DelHereSystem<T> : IInitSystem, IUpdateSystem where T: struct, IComponent
    {
        private EntityStore _store;
        private EntityBatch _batch;
        
        public void OnInit(EntityStore store)
        {
            _store = store;
            _batch = new EntityBatch()
                .Remove<T>();
        }
        
        public void OnUpdate()
        {
            _store.Entities.ApplyBatch(_batch);
        }
    }
}