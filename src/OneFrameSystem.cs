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
        
        /// <summary>
        /// Override it with your custom logic and invoke base.OnUpdate at end
        /// </summary>
        public virtual void OnUpdate()
        {
            _conditionSwitch.State = false;
        }
    }
}