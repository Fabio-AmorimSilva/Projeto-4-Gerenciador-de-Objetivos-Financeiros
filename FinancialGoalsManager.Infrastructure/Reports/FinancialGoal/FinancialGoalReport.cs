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
                page.Content().Column(column =>
                {
                    column
                        .Spacing(10);

                    column
                        .Item()
                        .Text("Financial Goal Tracking")
                        .AlignCenter()
                        .Bold();

                    column
                        .Item()
                        .AspectRatio(2)
                        .Svg(size =>
                        {
                            Plot plot = new();

                            var bars = new List<Bar>();
                            var barIndex = 0;

                            foreach (var financialGoal in financialGoals)
                            {
                                bars.Add(new Bar { Position = barIndex, Value = (double)financialGoal.Total } );

                                barIndex++;
                            }

                            foreach (var bar in bars)
                            {
                                bar.FillColor = new ScottPlot.Color(Colors.Blue.Medium.Hex);
                                bar.LineWidth = 0;
                                bar.Size = 0.15;
                            }

                            plot.Add.Bars(bars);

                            var ticks = new List<Tick>();
                            var tickIndex = 0;

                            foreach (var financialGoal in financialGoals)
                            {
                                ticks.Add(new Tick(position: tickIndex, label: $"{financialGoal.Month}/{financialGoal.Year}"));

                                tickIndex++;
                            }

                            plot.Legend = new Legend(plot);
                            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
                            plot.Axes.Bottom.MajorTickStyle.Length = 0;
                            plot.Axes.Bottom.TickLabelStyle.FontName = "Lato";
                            plot.Axes.Bottom.TickLabelStyle.FontSize = 16;
                            plot.Axes.Bottom.TickLabelStyle.OffsetY = 8;
                            plot.Grid.XAxisStyle.IsVisible = false;

                            plot.Axes.Margins(bottom: 0, top: 0.25f);

                            return plot.GetSvgXml((int)size.Width, (int)size.Height);
                        });
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