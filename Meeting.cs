using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public enum Category { CodeMonkey, Hub, Short, TeamBuilding };
    public enum Type { Live, InPerson };

    public class Meeting
    {
        public string Name { get; set; }
        public Admin ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Type Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<User> Participants { get; set; }

        public Meeting(string name, Admin respPerson, string descr, 
                       Category cat, Type type, DateTime startDate, 
                       DateTime endDate, List<User> partic)
        {
            Name = name;
            ResponsiblePerson = respPerson;
            Description = descr;
            Category = cat;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Participants = partic;
        }

        /// <summary>
        /// Gets the period that the meeting lasts to find intersections between different 
        /// meeting periods
        /// </summary>
        /// <param name="startDate">start of the meeting</param>
        /// <param name="endDate">end of the meeting</param>
        /// <returns>a meeting period</returns>
        public MeetingPeriod GetMeetingPeriod(DateTime startDate, DateTime endDate)
        {
            MeetingPeriod meetingPeriod = new MeetingPeriod(startDate, endDate);
            return meetingPeriod;
        }
        /// <summary>
        /// Adds a person to the meeting
        /// </summary>
        /// <param name="meetings">meeting list</param>
        /// <param name="name">name of the user</param>
        /// <param name="whenAdded">the time of user addition to the meeting</param>
        /// <returns>a string that provides information about the result</returns>
        public string AddToMeeting(List<Meeting> meetings, string name, DateTime whenAdded)
        {
            string result;
            User person = new User(name, whenAdded);

            MeetingPeriod meetingPeriod = GetMeetingPeriod(StartDate, EndDate);
            foreach (Meeting meeting in meetings)
            {
                if (meeting == this)
                    continue;
                MeetingPeriod otherMeetingPeriod = GetMeetingPeriod(meeting.StartDate, meeting.EndDate);
                if (meetingPeriod.IntersectsWith(otherMeetingPeriod) && meeting.ContainsUser(name))
                {
                    Participants.Add(person);
                    result = "Warning! The user is registered in other meetings that intersects with the current one";
                    return result;
                }
            }
            if(!ContainsUser(name))
            {
                Participants.Add(person);
                result = "Participant added succesfully";
                return result;
            }

            result = "Participant already exists in the meeting";
            return result;
        }

        /// <summary>
        /// Removes a user from the meeting
        /// </summary>
        /// <param name="name">username</param>
        public void RemoveFromMeeting(string name)
        {
            foreach(User user in Participants)
            {
                if(user.Name == name)
                {
                    Participants.Remove(user);
                    break;
                }
            }
        }
        /// <summary>
        /// Checks if user is in the participant list
        /// of the meeting
        /// </summary>
        /// <param name="name">name of the user</param>
        /// <returns>true if user is found</returns>
        private bool ContainsUser(string name)
        {
            bool result = false;
            foreach(User user in Participants)
            {
                if(name == user.Name)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
