using System.Runtime.CompilerServices;
using FrifloExt.Interfaces;

namespace FrifloExt
{
    public class ConditionalSystemGroup : SystemGroup
    {
        private readonly IConditionSwitch _conditionSwitch;

        public ConditionalSystemGroup(IConditionSwitch conditionSwitch, int systemCapacity = 32) : base(systemCapacity)
        {
            _conditionSwitch = conditionSwitch;
        }

        public ConditionalSystemGroup(IConditionSwitch conditionSwitch, params ISystem[] systems) : base(systems)
        {
            _conditionSwitch = conditionSwitch;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnUpdate()
        {
            if (!_conditionSwitch.State)
                return;
            base.OnUpdate();
        }
    }
}