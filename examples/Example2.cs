#if UNITY_EDITOR
using Friflo.Engine.ECS;
using UnityEngine;

namespace FrifloExt.examples
{
    public class Example2 : MonoBehaviour
    {
        private FrifloExt.ExtendedSystems _systems;

        private void Awake()
        {
            var entityStore = new EntityStore();
            _systems = new FrifloExt.ExtendedSystems(entityStore);
            var systemGroup1 = new SystemGroup();
            systemGroup1.AddSystems(new ExampleTransformSystem(), new ExampleTransformSystem2());
        
            var systemGroup2 = new SystemGroup(new ExampleTransformSystem(), new ExampleTransformSystem2());
            _systems
                .AddSystem(systemGroup1)
                .AddSystem(systemGroup2)
                .Init();
        }

        private void Update()
        {
            _systems.Update();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
        }
    }
}
#endif