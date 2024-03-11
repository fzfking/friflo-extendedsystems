using Friflo.Engine.ECS;
using FrifloExt.Interfaces;

namespace FrifloExt
{
    public class DelTagHereSystem<T> : IInitSystem, IUpdateSystem where T: struct, ITag
    {
        private EntityStore _store;
        private EntityBatch _batch;
        
        public void OnInit(EntityStore store)
        {
            _store = store;
            _batch = new EntityBatch()
                .RemoveTag<T>();
        }
        
        public void OnUpdate()
        {
            _store.Entities.ApplyBatch(_batch);
        }
    }
}