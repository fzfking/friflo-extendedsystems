#if UNITY_EDITOR
using ExtendedSystems.Interfaces;
using Friflo.Engine.ECS;

namespace ExtendedSystems.examples
{
    public class ExampleTransformSystem : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private ArchetypeQuery<Transform> _transformQuery;

        public void OnInit(EntityStore store)
        {
            _transformQuery = store.Query<Transform>();
            //Init queries logic
        }

        public void OnUpdate()
        {
            foreach (var (transforms, entities) in _transformQuery.Chunks)
            {
                //Do whatever you want with entities
            }
        }

        public void OnDestroy()
        {
            //Dispose logic
        }
    }

    public class ExampleTransformSystem2 : IUpdateSystem, IInitSystem
    {
        private ArchetypeQuery<Transform> _transformQuery;

        public void OnInit(EntityStore store)
        {
            _transformQuery = store.Query<Transform>();
        }

        public void OnUpdate()
        {
            foreach (var (transforms, entities) in _transformQuery.Chunks)
            {
                //Do whatever you want with entities
            }
        }
    }
}
#endif