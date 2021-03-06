using Contracts.Enums;
using Contracts.Models;
using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IChargeService
    {
        List<PeriodTotalCharge> CalculateCharges(TimeRange range, VehicleType type);

        List<ChargePeriod> CalculateChargePeriods(TimeRange range);

        double CalculateTotalCharge(List<PeriodTotalCharge> periodTotalCharges);

        void GetDefaultChargeValues();
    }
}