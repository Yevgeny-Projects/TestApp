using System;
using System.Collections.Generic;

namespace ConsoleDateOccurences
{
    class Program
    {
        public static void Main()
        {
            List<DateTime> dateList = new List<DateTime> {
                    new DateTime(2001,2,1),
                    new DateTime(2001,2,1),
                    new DateTime(2001,2,1),
                    new DateTime(2001,2,1),
                    new DateTime(2001,2,5),
                    new DateTime(2001,2,5),
                    new DateTime(2001,2,5),
                    new DateTime(2001,2,11),
                    new DateTime(2001,2,28),
                    new DateTime(2001,2,28),
                 };

            // Element to be CountOccured in dateList[]


            int n = dateList.Count;
            int startPos = 0;
            int selectedMonth = 2;
            int selectedyear = 2001;
            int numberOfDaysInMonth = 28;
            for (int day = 1; day <= numberOfDaysInMonth; day++)
            {
                var dateToSearch = new DateTime(selectedyear, selectedMonth, day);
                var result = CountOccur(dateList, dateToSearch, startPos, n);
                startPos = result.lastOccur;
                Console.WriteLine(dateToSearch.ToString() + " occurs " + result.countOccur + " times");
            }

        }

        static (int countOccur, int lastOccur) CountOccur(List<DateTime> dateList,
                                                            DateTime dateToSearch,
                                                            int startPos,
                                                            int n)
        {
            int i;
            int j;

            i = FirstOccur(dateList, startPos, n - 1, dateToSearch, n);

            if (i == -1)
                return (0, startPos);

            j = LastOccur(dateList, i, n - 1, dateToSearch, n);

            return (j - i + 1, j);
        }


        static int FirstOccur(List<DateTime> dateList, 
                                int low, 
                                int high,
                                DateTime dateToSearch, 
                                int n)
        {
            if (high >= low)
            {
                int mid = (low + high) / 2;
                if ((mid == 0 || dateToSearch > dateList[mid - 1])
                                    && dateList[mid] == dateToSearch)
                    return mid;
                else if (dateToSearch > dateList[mid])
                    return FirstOccur(dateList, (mid + 1), high, dateToSearch, n);
                else
                    return FirstOccur(dateList, low, (mid - 1), dateToSearch, n);
            }
            return -1;
        }

        static int LastOccur(List<DateTime> dateList, 
                                int low,
                                int high, 
                                DateTime dateToSearch, 
                                int n)
        {
            if (high >= low)
            {
                int mid = (low + high) / 2;
                if ((mid == n - 1 || dateToSearch < dateList[mid + 1])
                                    && dateList[mid] == dateToSearch)
                    return mid;
                else if (dateToSearch < dateList[mid])
                    return LastOccur(dateList, low, (mid - 1), dateToSearch, n);
                else
                    return LastOccur(dateList, (mid + 1), high, dateToSearch, n);
            }
            return -1;
        }

    }
}
