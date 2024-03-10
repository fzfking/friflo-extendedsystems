using System.Collections.Generic;
using System.Linq;
using ExtendedSystems.Interfaces;
using Friflo.Engine.ECS;

namespace ExtendedSystems
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class ExtendedSystems
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EntityStore Store;
        private readonly List<ISystem> _systems;
        private readonly List<IUpdateSystem> _updateSystems;

        public ExtendedSystems(EntityStore store, int initCapacity = 32)
        {
            Store = store;
            _systems = new(initCapacity);
            _updateSystems = new(initCapacity);
        }

        public virtual ExtendedSystems AddSystem<T>(T system) where T: class, ISystem
        {
            _systems.Add(system);
            if (system is IUpdateSystem updateSystem) 
                _updateSystems.Add(updateSystem);
            return this;
        }
    
        public void Init()
        {
            foreach (var initSystem in _systems.OfType<IInitSystem>()) 
                initSystem.OnInit(Store);
        }

        public void Update()
        {
            foreach (var updateSystem in _updateSystems) 
                updateSystem.OnUpdate();
        }
    
        public void Destroy()
        {
            foreach (var destroySystem in _systems.OfType<IDestroySystem>()) 
                destroySystem.OnDestroy();
        }
    }
}