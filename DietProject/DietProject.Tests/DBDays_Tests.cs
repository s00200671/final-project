using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DietProject;

namespace DietProject.Tests
{
    [TestClass]
    public class DBDays_Tests
    {
        [TestMethod]
        public void GenerateDBTime_1()
        {
            // Converting 12AM or 00:00 to the DB time format
            int hour_input = 12;
            int minute_input = 00;
            string AM_PM_input = "am";

            string expected_result = "0000";

            string result = DBDays.generateDBTime(AM_PM_input, hour_input, minute_input);

            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void GenerateDBTime_2()
        {
            // Converting 12PM or 12:00 to the DB time format
            int hour_input = 12;
            int minute_input = 00;
            string AM_PM_input = "pm";

            string expected_result = "1200";

            string result = DBDays.generateDBTime(AM_PM_input, hour_input, minute_input);

            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void GenerateDBTime_3()
        {
            // hour must be between 0 and 12
            int hour_input = 14;
            int minute_input = 00;
            string AM_PM_input = "pm";

            Assert.ThrowsException<Exception>(() => DBDays.generateDBTime(AM_PM_input, hour_input, minute_input));
        }
        [TestMethod]
        public void GenerateDBTime_4()
        {
            // Minutes must be between 0 and 60
            int hour_input = 10;
            int minute_input = 60;
            string AM_PM_input = "pm";

            Assert.ThrowsException<Exception>(() => DBDays.generateDBTime(AM_PM_input, hour_input, minute_input));
        }
    }
}
