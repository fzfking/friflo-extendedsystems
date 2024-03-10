using FrifloExt.Interfaces;

namespace FrifloExt
{
    public class SystemSwitch : IConditionSwitch
    {
        public bool State { get; set; }

        public SystemSwitch(bool defaultState = true)
        {
            State = defaultState;
        }
    }
}