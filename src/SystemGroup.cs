using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Friflo.Engine.ECS;
using FrifloExt.Interfaces;

namespace FrifloExt
{
    public class SystemGroup : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private readonly List<ISystem> _systems;
        private readonly List<IUpdateSystem> _updateSystems;
    
        public SystemGroup(int systemCapacity = 32)
        {
            _systems = new(systemCapacity);
            _updateSystems = new(systemCapacity);
        }

        public SystemGroup(params ISystem[] systems)
        {
            _systems = new(systems.Length);
            _updateSystems = new(systems.Length);
            AddSystems(systems);
        }

        public void AddSystems(params ISystem[] systems)
        {
            foreach (var system in systems) 
                AddSystem(system);
        }

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
            if (system is IUpdateSystem updateSystem) 
                _updateSystems.Add(updateSystem);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void OnUpdate()
        {
            foreach (var system in _updateSystems) 
                system.OnUpdate();
        }
    
        public virtual void OnInit(EntityStore store)
        {
            foreach (var initSystem in _systems.OfType<IInitSystem>()) 
                initSystem.OnInit(store);
        }

        public void OnDestroy()
        {
            foreach (var destroySystem in _systems.OfType<IDestroySystem>()) 
                destroySystem.OnDestroy();
        }
    }
}