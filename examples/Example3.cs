#if UNITY_EDITOR
using Friflo.Engine.ECS;
using UnityEngine;

namespace ExtendedSystems.examples
{
    public class Example3 : MonoBehaviour
    {
        private ExtendedSystems _systems;
        private SystemSwitch _group1Switch;
        private SystemSwitch _group2Switch;
        private int _frameCounter;

        private void Awake()
        {
            var entityStore = new EntityStore();
            _systems = new ExtendedSystems(entityStore);
            _group1Switch = new();
            _group2Switch = new();
            var systemGroup1 = new ConditionalSystemGroup(_group1Switch, 
                new ExampleTransformSystem(), 
                new ExampleTransformSystem2());
            var systemGroup2 = new ConditionalSystemGroup(_group2Switch, 
                new ExampleTransformSystem(), 
                new ExampleTransformSystem2());
            
            _systems
                .AddSystem(systemGroup1)
                .AddSystem(systemGroup2)
                .Init();

            _group1Switch.State = false;
        }

        private void Update()
        {
            //Group 1 will not work until we enable it back
            _systems.Update();

            //After 5 frame group 1 will work too
            _frameCounter++;
            if (_frameCounter >= 5 && !_group1Switch.State) 
                _group1Switch.State = true;
        }

        private void OnDestroy()
        {
            _systems.Destroy();
        }
    }
}
#endif