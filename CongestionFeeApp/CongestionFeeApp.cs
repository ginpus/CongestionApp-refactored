using CongestionFeeApp.Models.Methods;
using Contracts.Enums;
using Contracts.Models;
using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CongestionFeeApp
{
    public class CongestionFeeApp
    {
        public ConsoleLogger Logger { get; set; } = new ConsoleLogger();
        public ConsoleReader Reader { get; set; } = new ConsoleReader();
        public InputValidator Validator { get; set; } = new InputValidator();

        private readonly IChargeService _chargeService;

        public CongestionFeeApp(IChargeService chargeService)
        {
            _chargeService = chargeService;
        }

        public Task Start()
        {
            TimeRange timeRange = new TimeRange { };
            List<PeriodTotalCharge> charges;
            double totalCharge;

            while (true)
            {
                Logger.PrintAllSelections();
                var chosenCommand = Reader.ParseCommandSelection();
                switch (chosenCommand)
                {
                    case Selection.Calculate_Congestion_Fee:
                        Logger.PrintVechicleTypes();
                        var vehicleType = Reader.ParseVehicleType();
                        Logger.LogExpectedTimeFormat(TimeGate.Start);
                        var startTime = Reader.ParseTime();
                        DateTime endTime;
                        do
                        {
                            Logger.LogExpectedTimeFormat(TimeGate.End);
                            endTime = Reader.ParseTime();
                        }
                        while (!Validator.IsEndTimeValid(startTime, endTime));
                        timeRange.Start = startTime;
                        timeRange.End = endTime;
                        charges = _chargeService.CalculateCharges(timeRange, vehicleType);
                        Logger.PrintIndividualCharges(charges);
                        totalCharge = _chargeService.CalculateTotalCharge(charges);
                        Logger.PrintTotalCharge(totalCharge);
                        Logger.Log("-----------------------------");
                        break;

                    case Selection.Demo_Congestion_Fee_Calculation:
                        bool demoMode = true;
                        while (demoMode)
                        {
                            Logger.Log("\nAvailable demos:");
                            Logger.Log("1 - Car: 24/04/2008 11:32 - 24/04/2008 14:42");
                            Logger.Log("2 - Motorbike: 24/04/2008 17:00 - 24/04/2008 22:11"); ;
                            Logger.Log("3 - Van: 25/04/2008 10:23 - 28/04/2008 09:02");
                            Logger.Log("9 - Exit Demo");
                            var chosenDemo = Console.ReadLine();
                            switch (chosenDemo)
                            {
                                case "1":
                                    timeRange.Start = new DateTime(2008, 4, 24, 11, 32, 0);
                                    timeRange.End = new DateTime(2008, 4, 24, 14, 42, 0);
                                    charges = _chargeService.CalculateCharges(timeRange, VehicleType.Car);
                                    foreach (var entry in charges)
                                    {
                                        Logger.Log(entry);
                                    }
                                    totalCharge = _chargeService.CalculateTotalCharge(charges);
                                    Logger.Log($"Total charge: £{ totalCharge.ToString("0.00")}");
                                    break;

                                case "2":
                                    timeRange.Start = new DateTime(2008, 4, 24, 17, 0, 0);
                                    timeRange.End = new DateTime(2008, 4, 24, 22, 11, 0);
                                    charges = _chargeService.CalculateCharges(timeRange, VehicleType.Motorbike);
                                    foreach (var entry in charges)
                                    {
                                        Logger.Log(entry);
                                    }
                                    totalCharge = _chargeService.CalculateTotalCharge(charges);
                                    Logger.Log($"Total charge: £{ totalCharge.ToString("0.00")}");
                                    break;

                                case "3":
                                    timeRange.Start = new DateTime(2008, 4, 25, 10, 23, 0);
                                    timeRange.End = new DateTime(2008, 4, 28, 9, 2, 0);
                                    charges = _chargeService.CalculateCharges(timeRange, VehicleType.Car);
                                    foreach (var entry in charges)
                                    {
                                        Logger.Log(entry);
                                    }
                                    totalCharge = _chargeService.CalculateTotalCharge(charges);
                                    Logger.Log($"Total charge: £{ totalCharge.ToString("0.00")}");
                                    break;

                                case "9":
                                    demoMode = false;
                                    break;
                                default:
                                    Logger.Log("No such selection. Try again");
                                    break;
                            }
                        }
                        Logger.Log("--------------Exiting demo mode---------------");
                        break;

                    case Selection.Exit:
                        Logger.Log("The program ended");
                        return Task.CompletedTask;

                    default:
                        Logger.Log("No such selection. Try again");
                        break;
                }
            }
        }
    }
}