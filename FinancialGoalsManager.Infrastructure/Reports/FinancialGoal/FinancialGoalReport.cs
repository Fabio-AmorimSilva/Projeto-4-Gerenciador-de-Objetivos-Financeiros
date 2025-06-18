namespace FinancialGoalsManager.Infrastructure.Reports.FinancialGoal;

public static class FinancialGoalReport
{
    public static byte[] GeneratePdf(IEnumerable<FinancialGoalReportModel> financialGoals)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(ts => ts.FontSize(12));

                page
                    .Header()
                    .Text("Financial Goal Tracking Report")
                    .WordSpacing()
                    .FontSize(24)
                    .Bold()
                    .AlignCenter();
                
                page
                    .Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Table(t =>
                    {
                        t.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(500);
                            cd.RelativeColumn(500);
                            cd.RelativeColumn(500);
                        });
                        
                        t.Header(th =>
                        {
                            th.Cell().Row(1).Column(1).Element(Block).Text("Month");
                            th.Cell().Row(1).Column(1).Element(Block).Text("Year");
                            th.Cell().Row(1).Column(1).Element(Block).Text("Total");
                        });

                        uint rowIndex = 2;

                        foreach (var financialGoal in financialGoals)
                        {
                            t.Cell().Row(rowIndex).Column(1).Element(Entry).Text(financialGoal.Month.ToString());
                            t.Cell().Row(rowIndex).Column(1).Element(Entry).Text(financialGoal.Year.ToString());
                            t.Cell().Row(rowIndex).Column(1).Element(Entry).Text(financialGoal.Total.ToString(CultureInfo.CurrentCulture));
                            
                            rowIndex++;
                        }

                    });
            });
        });

        return document.GeneratePdf();
    }
    
    private static IContainer Entry(IContainer container)
        => container
            .BorderBottom(2)
            .PaddingBottom(2)
            .PaddingVertical(1)
            .PaddingHorizontal(6)
            .ShowOnce()
            .AlignCenter()
            .AlignMiddle();

    private static IContainer Block(IContainer container)
        => container
            .BorderBottom(2)
            .Background(Colors.Grey.Lighten3)
            .ShowOnce()
            .MinWidth(50)
            .MinWidth(20)
            .AlignCenter()
            .AlignMiddle();
    
}