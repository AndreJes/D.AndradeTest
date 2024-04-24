using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class SessionEvent : Lecture
    {
        public TimeSpan MinStartTime { get; private set; }
        public TimeSpan MaxStartTime { get; private set; }

        public uint EventAddedTime{ get; private set; }

        public SessionEvent(uint duration, string description, TimeSpan minStartTime, TimeSpan maxStartTime) : base(duration, description)
        {
            MinStartTime = minStartTime;
            MaxStartTime = maxStartTime;
            EventAddedTime = (uint)(MaxStartTime.TotalMinutes - MinStartTime.TotalMinutes);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
