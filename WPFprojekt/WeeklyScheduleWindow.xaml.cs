using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFprojekt
{
	public partial class WeeklyScheduleWindow : Window
	{
		private ObservableCollection<Seans> seanse;
		private DateTime startOfWeek;
		private Dictionary<DateTime, int> dailyCount;
		private bool isLoaded = false;

		public WeeklyScheduleWindow(ObservableCollection<Seans> seanse, DateTime startOfWeek)
		{
			InitializeComponent();
			DataContext = this;

			this.seanse = seanse;
			this.startOfWeek = startOfWeek;

			var endOfWeek = startOfWeek.AddDays(7);
			var weeklySeanse = seanse.Where(s => s.Czas >= startOfWeek && s.Czas < endOfWeek).ToList();

			WeeklyScheduleListView.ItemsSource = weeklySeanse;

			PopulateDailyCount();
			PopulateSeansTable();

			// Subskrypcja zdarzenia Loaded okna
			Loaded += WeeklyScheduleWindow_Loaded;
		}

		private void WeeklyScheduleWindow_Loaded(object sender, RoutedEventArgs e)
		{
			isLoaded = true;
			DrawChart();
		}

		private void PopulateDailyCount()
		{
			dailyCount = new Dictionary<DateTime, int>();

			for (DateTime date = startOfWeek; date < startOfWeek.AddDays(7); date = date.AddDays(1))
			{
				dailyCount[date.Date] = 0;
			}

			foreach (Seans seans in seanse)
			{
				DateTime dateKey = seans.Czas.Date;
				if (dailyCount.ContainsKey(dateKey))
				{
					dailyCount[dateKey]++;
				}
			}
		}

		private void DrawChart()
		{
			if (!isLoaded || dailyCount == null || chartCanvas.ActualWidth == 0 || chartCanvas.ActualHeight == 0)
				return;

			double maxCount = dailyCount.Values.Max();
			double barWidth = chartCanvas.ActualWidth / dailyCount.Count;
			double maxBarHeight = chartCanvas.ActualHeight;

			chartCanvas.Children.Clear();

			int index = 0;
			foreach (var kvp in dailyCount)
			{
				double barHeight = (kvp.Value / maxCount) * maxBarHeight;

				Rectangle rect = new Rectangle();
				rect.Width = barWidth;
				rect.Height = barHeight;
				rect.Fill = Brushes.DarkBlue;

				Canvas.SetLeft(rect, index * barWidth + 10);
				Canvas.SetTop(rect, maxBarHeight - barHeight);

				chartCanvas.Children.Add(rect);

				TextBlock textBlock = new TextBlock();
				textBlock.Text = kvp.Value.ToString();
				textBlock.TextAlignment = TextAlignment.Center;
				textBlock.FontSize = 12;


				Canvas.SetLeft(textBlock, index * barWidth + barWidth / 2);
				Canvas.SetTop(textBlock, maxBarHeight - barHeight - 20);

				chartCanvas.Children.Add(textBlock);


				TextBlock dateTextBlock = new TextBlock();
				dateTextBlock.Text = kvp.Key.ToShortDateString();
				dateTextBlock.TextAlignment = TextAlignment.Center;
				dateTextBlock.FontSize = 12;


				Canvas.SetLeft(dateTextBlock, index * barWidth + 10);
				Canvas.SetTop(dateTextBlock, maxBarHeight + 5);

				chartCanvas.Children.Add(dateTextBlock);

				index++;
			}
		}

		private void PopulateSeansTable()
		{
			if (WeeklyScheduleListView.Items == null)
				return;

			var seansList = WeeklyScheduleListView.Items.Cast<Seans>().Select(seans => new
			{
				seans.film.Nazwa,
				seans.Czas,
				seans.Sala,
				Cena_biletu = seans.Cena_biletu.ToString("C"),
				seans.film.Dlugosc,
				seans.film.Gatunek
			}).ToList();

		}

		private void PrintSchedule_Click(object sender, RoutedEventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{
				FlowDocument doc = new FlowDocument();
				doc.PagePadding = new Thickness(50);
				doc.PageWidth = 800;

				Table table = new Table();
				doc.Blocks.Add(table);

				for (int i = 0; i < 6; i++)
				{
					table.Columns.Add(new TableColumn());
				}

				TableRowGroup group = new TableRowGroup();
				table.RowGroups.Add(group);

				TableRow headerRow = new TableRow();
				group.Rows.Add(headerRow);
				headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tytuł"))));
				headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Data"))));
				headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Sala"))));
				headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Cena biletu"))));
				headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Długość seansu"))));
				headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Gatunek"))));

				foreach (Seans seans in seanse)
				{
					TableRow row = new TableRow();
					group.Rows.Add(row);

					row.Cells.Add(new TableCell(new Paragraph(new Run(seans.film.Nazwa))));
					row.Cells.Add(new TableCell(new Paragraph(new Run(seans.Czas.ToString()))));
					row.Cells.Add(new TableCell(new Paragraph(new Run(seans.Sala.ToString()))));
					row.Cells.Add(new TableCell(new Paragraph(new Run(seans.Cena_biletu.ToString("C")))));
					row.Cells.Add(new TableCell(new Paragraph(new Run(seans.film.Dlugosc.ToString()))));
					row.Cells.Add(new TableCell(new Paragraph(new Run(seans.film.Gatunek))));
				}

				IDocumentPaginatorSource idocument = doc as IDocumentPaginatorSource;
				printDialog.PrintDocument(idocument.DocumentPaginator, "Harmonogram seansów");
			}
		}
	}
}
