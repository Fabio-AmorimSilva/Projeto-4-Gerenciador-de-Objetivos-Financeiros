using Color = ScottPlot.Color;

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
                                bars.Add(new Bar
                                {
                                    Position = barIndex,
                                    Value = (double)financialGoal.Total,
                                    FillColor = financialGoal.Status == GoalStatus.Success
                                        ? Color.FromARGB(Colors.Green.Medium.Hex)
                                        : Color.FromARGB(Colors.Red.Medium.Hex),
                                    LineWidth = 0,
                                    Size = 0.15
                                });

                                barIndex++;
                            }
                            
                            plot.Add.Bars(bars);

                            var ticks = new List<Tick>();
                            var tickIndex = 0;

                            foreach (var financialGoal in financialGoals)
                            {
                                ticks.Add(new Tick(position: tickIndex,
                                    label: $"{financialGoal.Month}/{financialGoal.Year} - {financialGoal.FinancialGoalName}"));

                                tickIndex++;
                            }

                            LegendItem success = new()
                            {
                                LineColor = Color.FromARGB(Colors.Green.Medium),
                                MarkerFillColor = Color.FromARGB(Colors.Green.Medium),
                                LineWidth = 2,
                                LabelText = "Success"
                            };

                            LegendItem failed = new()
                            {
                                LineColor = Color.FromARGB(Colors.Red.Medium),
                                MarkerFillColor = Color.FromARGB(Colors.Red.Medium),
                                LineWidth = 2,
                                LabelText = "Failed"
                            };

                            plot.YLabel("-- BRL - R$ -- ");
                            plot.XLabel(" -- Period --");
                            plot.ShowLegend([success, failed]);
                            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
                            plot.Axes.Bottom.MajorTickStyle.Length = 0;
                            plot.Axes.Bottom.TickLabelStyle.FontName = "Lato";
                            plot.Axes.Bottom.TickLabelStyle.FontSize = 8;
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
}