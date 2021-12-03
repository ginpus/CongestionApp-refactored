using Contracts.Enums;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionFeeApp.Models.Methods
{
    public class ConsoleLogger
    {
        public void Log(object message)
        {
            Console.WriteLine(message);
        }

        public void LogExpectedTimeFormat(TimeGate timeGate)
        {
            Console.WriteLine($"Enter {timeGate.ToString().ToLower()} time in `dd/MM/yyyy HH:mm` format (no ticks): ");
        }

        public void PrintAllSelections()
        {
            Log("Available commands:");
            var count = 0;
            foreach (var name in Enum.GetNames(typeof(Selection)))
            {
                Console.WriteLine($"{++count}: {name}");
            }
        }

        public void PrintVechicleTypes()
        {
            Log("Available vechicle types:");
            var count = 0;
            foreach (var name in Enum.GetNames(typeof(VehicleType)))
            {
                Console.WriteLine($"{++count}: {name}");
            }
        }

        public void PrintIndividualCharges(List<PeriodTotalCharge> charges)
        {
            foreach (var entry in charges)
            {
                Log(entry);
            }
        }

        public void PrintTotalCharge(double totalCharge)
        {
            Log($"Total charge: £{ totalCharge.ToString("0.00")}");
        }
    }
}
