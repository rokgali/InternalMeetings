using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public class MeetingPeriod
    {
        DateTime Start;
        DateTime End;
        public MeetingPeriod(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        /// <summary>
        /// Checks if the meeting time intersects with another meeting
        /// </summary>
        /// <param name="otherPeriod">The meeting that is being 
        /// checked for time intersection with</param>
        /// <returns>true if the meeting times intersect, false if not</returns>
        public bool IntersectsWith(MeetingPeriod otherPeriod)
        {
            return !(Start >= otherPeriod.End || End <= otherPeriod.Start);
        }
    }
}
