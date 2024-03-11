using Friflo.Engine.ECS;
using FrifloExt.Interfaces;

namespace FrifloExt
{
    public static class SystemsExtension
    {
        public static ExtendedSystems DelHere<T>(this ExtendedSystems systems) where T : struct, IComponent
        {
            systems.AddSystem(new DelHereSystem<T>());
            return systems;
        }
        
        public static ExtendedSystems DelTagHere<T>(this ExtendedSystems systems) where T : struct, ITag
        {
            systems.AddSystem(new DelTagHereSystem<T>());
            return systems;
        }
        
        public static ISystemGroup DelHere<T>(this ISystemGroup systems) where T : struct, IComponent
        {
            systems.AddSystem(new DelHereSystem<T>());
            return systems;
        }
        
        public static ISystemGroup DelTagHere<T>(this ISystemGroup systems) where T : struct, ITag
        {
            systems.AddSystem(new DelTagHereSystem<T>());
            return systems;
        }

        public static ISystemGroup AddGroup(this ExtendedSystems systems, params ISystem[] childSystems)
        {
            var group = new SystemGroup(childSystems);
            systems.AddSystem(group);
            return group;
        }
        
        public static ISystemGroup AddSwitchGroup(this ExtendedSystems systems, IConditionSwitch conditionSwitch, params ISystem[] childSystems)
        {
            var group = new ConditionalSystemGroup(conditionSwitch, childSystems);
            systems.AddSystem(group);
            return group;
        }
    }
}