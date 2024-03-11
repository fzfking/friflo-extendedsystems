# Friflo Extended Systems
A different look at systems for the Friflo ecs. This extension is oriented for unity, but can be used anywhere.
## Caution: WIP. There can be bugs. Api may change.
# Installation
Before all it need installed Friflo ecs.
## Via upm
### By git url
```https://github.com/fzfking/friflo-extendedsystems.git```
### By editing Packages/manifest.json
```"com.fzfking.friflo-extendedsystems": "https://github.com/fzfking/friflo-extendedsystems.git"```
## Via sources
You can download it directly as zip archive.
# Features
## Systems separation through implemented interfaces
```c#
namespace FrifloExt.examples
{
    public class Example1 : MonoBehaviour
    {
        private ExtendedSystems _systems;

        private void Awake()
        {
            var entityStore = new EntityStore();
            _systems = new ExtendedSystems(entityStore);
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
}
```
## System groups
```c#
namespace FrifloExt.examples
{
    public class Example2 : MonoBehaviour
    {
        private ExtendedSystems _systems;

        private void Awake()
        {
            var entityStore = new EntityStore();
            _systems = new ExtendedSystems(entityStore);
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
```
## System switching
```c#
namespace FrifloExt.examples
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
```
## DelHere systems
Can remove tags and components in needed point of systems order.
```c#
namespace FrifloExt.examples
{
    public class Example3 : MonoBehaviour
    {
        private FrifloExt.ExtendedSystems _systems;
        private SystemSwitch _group1Switch;
        private SystemSwitch _group2Switch;
        private int _frameCounter;

        private void Awake()
        {
            var entityStore = new EntityStore();
            _systems = new FrifloExt.ExtendedSystems(entityStore);
            
            _systems
                .AddSystem(systemGroup1)
                .AddSystem(systemGroup2)
                .AddGroup(new ExampleTransformSystem(), new ExampleTransformSystem2())
                // Position will be removed after ExampleTransformSystem 
                // and ExampleTransformSystem2 end update
                .DelHere<Position>()
                //After that disabled tag will be removed
                .DelTagHere<Disabled>()
                .Init();

            _group1Switch.State = false;
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
```