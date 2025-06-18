namespace FinancialGoalsManager.Domain.Common.Interfaces;

public interface IFinancialGoalReportService
{
    Task<byte[]> GeneratePdf();
}