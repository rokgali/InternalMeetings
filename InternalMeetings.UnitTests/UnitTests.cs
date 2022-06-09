using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace InternalMeetings.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        static string TEST_FILENAME = "..\\..\\TestMeetingData.json";
        [TestMethod]
        public void Deleting_Meeting_From_Dataset()
        {
            //correct password is 123
            List<Meeting> meetingList = InOutUtil.Convert_json_to_meeting(TEST_FILENAME);

            Meeting meetingToBeDeleted = meetingList[0];
            Meeting meetingToBeLeft = meetingList[1];

            Program.DeleteMeeting(meetingList, meetingToBeDeleted, "123");
            Program.DeleteMeeting(meetingList, meetingToBeLeft, "124");

            Assert.IsTrue(!meetingList.Contains(meetingToBeDeleted));
            Assert.IsTrue(meetingList.Contains(meetingToBeLeft));
        }
        [TestMethod]
        public void Add_Person_To_Meeting_If_Person_Exists()
        {
            List<Meeting> meetingList = InOutUtil.Convert_json_to_meeting(TEST_FILENAME);

            Meeting meetingToBeAddedTo = meetingList[0];

            meetingToBeAddedTo.AddToMeeting(meetingList, "user", DateTime.Now);
            meetingToBeAddedTo.AddToMeeting(meetingList, "user", DateTime.Now);

            Assert.IsTrue(meetingToBeAddedTo.Participants.Count == 2);
        }
        [TestMethod]
        public void Added_Person_Intersects_With_Other_Meeting()
        {
            List<Meeting> meetingList = InOutUtil.Convert_json_to_meeting(TEST_FILENAME);

            meetingList[0].AddToMeeting(meetingList, "User", DateTime.Now);
            string result1 = meetingList[1].AddToMeeting(meetingList, "User", DateTime.Now);
            string warning = "Warning! The user is registered in other meetings that intersects with the current one";

            Assert.IsTrue(result1 == warning);
        }
        [TestMethod]
        public void Remove_Participant_From_Meeting()
        {
            List<Meeting> meetingList = InOutUtil.Convert_json_to_meeting(TEST_FILENAME);
            Meeting meetingToBeRemovedFrom = meetingList[0];

            meetingToBeRemovedFrom.RemoveFromMeeting("user");

            Assert.IsTrue(meetingToBeRemovedFrom.Participants.Count == 1);
        }
        [TestMethod]
        public void Filter_Data_By_Requirements()
        {
            List<Meeting> meetingList = InOutUtil.Convert_json_to_meeting(TEST_FILENAME);

            DateTime fromDate = new DateTime(2022, 6, 10);
            DateTime toDate = new DateTime(2022, 6, 12);
            List<Meeting> filteredList = Program.MeetingListFilter(meetingList, "description", "admin1",
                                                                   Category.TeamBuilding, Type.InPerson, 
                                                                   fromDate, toDate, 1);
            Assert.IsTrue(filteredList.Count == 1);
        }
    }
}
