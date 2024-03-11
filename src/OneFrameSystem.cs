using System.Runtime.CompilerServices;
using FrifloExt.Interfaces;

namespace FrifloExt
{
    public abstract class OneFrameSystem : IUpdateSystem
    {
        private readonly SystemSwitch _conditionSwitch;

        public OneFrameSystem(SystemSwitch conditionSwitch)
        {
            _conditionSwitch = conditionSwitch;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnUpdate()
        {
            if (!_conditionSwitch.State)
                return;
            Process();
            _conditionSwitch.State = false;
        }

        /// <summary>
        /// Implement it with your custom logic
        /// </summary>
        protected abstract void Process();
    }
}