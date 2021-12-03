using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionFeeApp.Models.Methods
{
    public class InputValidator
    {
        public bool IsEndTimeValid(DateTime start, DateTime end)
        {
            bool result = true;
            if (start > end)
            {
                result = false;
                Console.WriteLine("Invalid time expression - end time cannot be earlier than start time");
            }
            return result;
        }
    }
}
