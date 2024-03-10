using ExtendedSystems.Interfaces;

namespace ExtendedSystems
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