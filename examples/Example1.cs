#if UNITY_EDITOR
using Friflo.Engine.ECS;
using UnityEngine;

namespace FrifloExt.examples
{
    public class Example1 : MonoBehaviour
    {
        private FrifloExt.ExtendedSystems _systems;

        private void Awake()
        {
            var entityStore = new EntityStore();
            _systems = new FrifloExt.ExtendedSystems(entityStore);
            _systems
                .AddSystem(new ExampleTransformSystem())
                .AddSystem(new ExampleTransformSystem2())
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