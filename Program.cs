using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public class Program
    {
        static string FILENAME = "..\\..\\MeetingData.json"; //The file that the meeting data is stored in
        static string FILTERED_DATA = "..\\..\\FilteredMeetingData.json"; //The file that the filtered data is stored in

        static void Main(string[] args)
        {
            //File.WriteAllText(FILENAME, null);
            List<Meeting> meetingList = InOutUtil.Convert_json_to_meeting(FILENAME);
            List<Meeting> filteredList = new List<Meeting>();

            Console.WriteLine("Do you wish to create a new meeting? Type y to proceed");
            string ans = Console.ReadLine();
            if (string.Equals(ans, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                bool valid = false;
                Console.WriteLine("Enter the name of the meeting");
                string meetingName = Console.ReadLine();
                Console.WriteLine("Who will be responsible?");
                Console.WriteLine("Add the username");
                string adminUsername = Console.ReadLine();
                Console.WriteLine("Add a password");
                string passwd = Console.ReadLine();
                Admin responsiblePerson = new Admin(passwd, adminUsername);
                Console.WriteLine("Add a desciption");
                string description = Console.ReadLine();
                Category selectedCategory = Category.TeamBuilding;
                Type selectedType = Type.Live;
                DateTime startDateTime = DateTime.Now;
                DateTime endDateTime = DateTime.Now;

                Console.WriteLine("What category of meeting will this be?");
                while (!valid)
                {
                    selectedCategory = SelectedCategory();
                    if (selectedCategory == 0)
                    {
                        Console.WriteLine("Select an input within bounds");
                        valid = false;
                    }
                    else
                    {
                        valid = true;
                    }
                }
                valid = false;
                while (!valid)
                {
                    Console.WriteLine("What type of meeting will this be?");
                    selectedType = SelectedType();
                    if (selectedType == 0)
                    {
                        Console.WriteLine("Select an input within bounds");
                        valid = false;
                    }
                    else
                        valid = true;
                }
                valid = false;
                while (!valid)
                {
                    Console.WriteLine("Add a starting date and time");
                    Console.WriteLine("Enter the year");
                    int startYear = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the month 1-12");
                    int startMonth = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the day");
                    int startDay = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the hours");
                    int startHour = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the minutes");
                    int startMinute = int.Parse(Console.ReadLine());
                    startDateTime = DateTime.Now;
                    try
                    {
                        startDateTime = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a reasonable date and time");
                        valid = false;
                    }
                    if (startDateTime < DateTime.Now)
                    {
                        Console.WriteLine("Can't travel back in time");
                        valid = false;
                    }
                }
                valid = false;
                while (!valid)
                {
                    Console.WriteLine("Add an ending date and time");
                    Console.WriteLine("Enter the year");
                    int endYear = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the month 1-12");
                    int endMonth = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the day");
                    int endDay = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the hour");
                    int endHour = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the minute");
                    int endMinute = int.Parse(Console.ReadLine());
                    endDateTime = DateTime.Now;
                    try
                    {
                        endDateTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a reasonable date and time");
                        valid = false;
                    }
                    if(startDateTime >= endDateTime)
                    {
                        Console.WriteLine("The end needs to be later than the start");
                        valid = false;
                    }
                }
                List<User> userList = new List<User>();
                Meeting newMeeting = new Meeting(meetingName, responsiblePerson, description, selectedCategory,
                                                 selectedType, startDateTime, endDateTime, userList);
                meetingList.Add(newMeeting);
                InOutUtil.Convert_meetings_to_json(FILENAME, meetingList);
            }
            Console.WriteLine("Do you wish to delete a meeting? type y to proceed");
            ans = Console.ReadLine();
            if (string.Equals(ans, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                int i = 0;
                Console.WriteLine("Which meeting should be deleted?");
                foreach (Meeting meeting in meetingList)
                {
                    i++;
                    Console.WriteLine("{0}. {1} {2} {3} {4} {5}", i, meeting.Name, meeting.ResponsiblePerson.Name, meeting.Description,
                                                             meeting.StartDate, meeting.EndDate);
                }
                bool valid = false;
                Console.WriteLine("Enter an index");
                while (!valid)
                {
                    int input = int.Parse(Console.ReadLine()) - 1;
                    try
                    {
                        Console.WriteLine("Enter password");
                        string password = Console.ReadLine();
                        DeleteMeeting(meetingList, meetingList[input], password);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Write an index within bounds");
                        valid = false;
                    }
                }
            }
            Console.WriteLine("Do you wish to add a participant to a meeting? Type y to proceed");
            ans = Console.ReadLine();
            if (string.Equals(ans, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                int i = 0;
                Console.WriteLine("Which meeting do you wish to add an user to?");
                foreach (Meeting meeting in meetingList)
                {
                    i++;
                    Console.WriteLine("{0}. {1} {2} {3} {4} {5}", i, meeting.Name, meeting.ResponsiblePerson.Name, meeting.Description,
                                                             meeting.StartDate, meeting.EndDate);
                }
                bool valid = false;
                while (!valid)
                {
                    int input = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine("What is the username?");
                    string username = Console.ReadLine();
                    try
                    {
                        meetingList[input].AddToMeeting(meetingList, username, DateTime.Now);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Write an index within bounds");
                        valid = false;
                    }
                }
            }
            Console.WriteLine("Do you wish to remove a person from a meeting? Type y to proceed");
            ans = Console.ReadLine();
            if (string.Equals(ans, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                int i = 0;
                Console.WriteLine("Which meeting do you wish to remove a user from?");
                foreach (Meeting meeting in meetingList)
                {
                    i++;
                    Console.WriteLine("{0}. {1} {2} {3} {4} {5}", i, meeting.Name, meeting.ResponsiblePerson, meeting.Description,
                                                             meeting.StartDate, meeting.EndDate);
                }
                bool valid = false;
                while (!valid)
                {
                    int input = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine("What is the username?");
                    string username = Console.ReadLine();
                    try
                    {
                        meetingList[input].RemoveFromMeeting(username);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Write an index within bounds");
                        valid = false;
                    }
                }
            }
            Console.WriteLine("Do you wish to filter the data? Type y to proceed");
            ans = Console.ReadLine();
            if (string.Equals(ans, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                string description;
                string respPerson;
                Category selectedCategory = Category.TeamBuilding;
                Type selectedType = Type.Live;
                DateTime startDateTime = DateTime.Now;
                DateTime endDateTime = DateTime.Now;
                int participantCount;

                Console.WriteLine("Type a description to filter by");
                description = Console.ReadLine();
                Console.WriteLine("Type the responsible persons username");
                respPerson = Console.ReadLine();

                bool valid = false;
                while (!valid)
                {
                    Console.WriteLine("Select category to filter by");
                    selectedCategory = SelectedCategory();
                    if (selectedCategory == 0)
                    {
                        Console.WriteLine("Select an input within bounds");
                        valid = false;
                    }
                    else
                    {
                        valid = true;
                    }
                }
                valid = false;
                while (!valid)
                {
                    Console.WriteLine("Select type to filter by");
                    selectedType = SelectedType();
                    if (selectedType == 0)
                    {
                        Console.WriteLine("Select an input within bounds");
                        valid = false;
                    }
                    else
                        valid = true;
                }
                valid = false;
                while (!valid)
                {
                    Console.WriteLine("Add a starting date and time");
                    Console.WriteLine("Enter the year");
                    int startYear = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the month 1-12");
                    int startMonth = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the day");
                    int startDay = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the hours");
                    int startHour = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the minutes");
                    int startMinute = int.Parse(Console.ReadLine());
                    try
                    {
                        startDateTime = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a reasonable date and time");
                        valid = false;
                    }
                }

                valid = false;
                while (!valid)
                {
                    Console.WriteLine("Add an ending date and time");
                    Console.WriteLine("Enter the year");
                    int endYear = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the month 1-12");
                    int endMonth = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the day");
                    int endDay = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the hour");
                    int endHour = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the minute");
                    int endMinute = int.Parse(Console.ReadLine());
                    endDateTime = DateTime.Now;
                    try
                    {
                        endDateTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a reasonable date and time");
                        valid = false;
                    }
                    if (startDateTime >= endDateTime)
                    {
                        Console.WriteLine("The end needs to be later than the start");
                        valid = false;
                    }
                }
                Console.WriteLine("Insert the preferred participant count");
                participantCount = int.Parse(Console.ReadLine());
                MeetingListFilter(filteredList, description, respPerson, selectedCategory, selectedType,
                                  startDateTime, endDateTime, participantCount);
            }
            InOutUtil.Convert_meetings_to_json(FILENAME, meetingList);
            InOutUtil.Convert_meetings_to_json(FILTERED_DATA, filteredList);
        }
        /// <summary>
        /// Checks if user input for category selection is valid
        /// </summary>
        /// <returns> 0 if not valid selected category if valid</returns>
        static Category SelectedCategory()
        {
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine("{0}). {1}", (int)category + 1, category.ToString());
            }
            if (int.TryParse(Console.ReadLine(), out int categoryInput))
            {
                if (Enum.IsDefined(typeof(Category), categoryInput - 1))
                {
                    Category selectedCategory = (Category)categoryInput;
                    return selectedCategory;
                }
            }
            return 0;
        }
        /// <summary>
        /// Checks if the user input for type selection is valid
        /// </summary>
        /// <returns> returns 0 if not valid, the selected type if valid</returns>
        static Type SelectedType()
        {
            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                Console.WriteLine("{0}). {1}", (int)type + 1, type.ToString());
            }
            if (int.TryParse(Console.ReadLine(), out int typeInput))
            {
                if (Enum.IsDefined(typeof(Type), typeInput - 1))
                {
                    Type selectedType = (Type)typeInput;
                    return selectedType;
                }
            }
            return 0; 
        }
        /// <summary>
        /// Filters the meeting data by provided criteria
        /// </summary>
        /// <param name="meetings">the meeting list that is being filtered</param>
        /// <param name="description">the description of the meeting to filter by</param>
        /// <param name="respPerson">the responsible person username of the meeting</param>
        /// <param name="category">the category of the meeting</param>
        /// <param name="type">the type of the meeting</param>
        /// <param name="fromDate">the start date and time of the meeting</param>
        /// <param name="toDate">the end date and time of the meeting</param>
        /// <param name="participantCount">number of participants</param>
        /// <returns>a list with the filtered data</returns>
        public static List<Meeting> MeetingListFilter(List<Meeting> meetings, string description, string respPerson, Category category, 
                                               Type type, DateTime fromDate, DateTime toDate, int participantCount)
        {
            List<Meeting> filteredList = new List<Meeting>();

            foreach(Meeting meeting in from meeting in meetings
                                    where meeting.Description.Contains(description)
                                    && meeting.ResponsiblePerson.Name == respPerson
                                    && meeting.Category == category
                                    && meeting.Type == type
                                    && (fromDate <= meeting.StartDate && 
                                    meeting.EndDate <= toDate)
                                    && meeting.Participants.Count > participantCount
                                    select meeting)
            {
                filteredList.Add(meeting);
            }

            return filteredList;
        }
        /// <summary>
        /// Deletes a meeting from the meeting list
        /// </summary>
        /// <param name="meetings">the list of meetings</param>
        /// <param name="meeting">the meeting that is selected for deletion</param>
        /// <param name="password">to check if the person is an admin</param>
        public static void DeleteMeeting(List<Meeting> meetings, Meeting meeting, string password)
        {
            if (meeting.ResponsiblePerson.Password == password)
            {
                meetings.Remove(meeting);
            }
            else
                Console.WriteLine("Incorrect password");
        }
    }
}
