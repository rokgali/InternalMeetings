using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace InternalMeetings
{
    public class InOutUtil
    {
        /// <summary>
        /// Converts the json file to a list of meetings
        /// </summary>
        /// <param name="filename">file name to read from</param>
        /// <returns>a list of meetings</returns>
        public static List<Meeting> Convert_json_to_meeting(string filename)
        {
            StreamReader read = new StreamReader(filename);
            string jsonString = read.ReadToEnd();
            List<Meeting> meeting = JsonConvert.DeserializeObject<List<Meeting>>(jsonString);
            read.Close();

            if(meeting != null)
            {
                return meeting;
            }

            meeting = new List<Meeting>();

            return meeting;
        }
        /// <summary>
        /// Converts a meeting list to json file
        /// </summary>
        /// <param name="filename">name of file to write to</param>
        /// <param name="meetingList">list of meetings that should be converted</param>
        public static void Convert_meetings_to_json(string filename, List<Meeting> meetingList)
        {
            File.WriteAllText(filename, null);
            using(StreamWriter write = File.AppendText(filename))
            {
                string jsonFormattedText = JsonConvert.SerializeObject(meetingList, Formatting.Indented);
                write.WriteLine(jsonFormattedText);
            }
        }
    }
}
