using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionFeeApp.Models.Methods
{
    public class ConsoleReader
    {
        public DateTime ParseTime()
        {
            var timeInput = Console.ReadLine();
            DateTime timeOutput;
            while (!DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out timeOutput))
            {
                Console.WriteLine("Invalid date/time, please retry");
                timeInput = Console.ReadLine();
            }
            return timeOutput;
        }

        public VehicleType ParseVehicleType()
        {
            return (VehicleType)Convert.ToInt32(Console.ReadLine());
        }

        public Selection ParseCommandSelection()
        {
            return (Selection)Convert.ToInt32(Console.ReadLine());
        }
    }
}
