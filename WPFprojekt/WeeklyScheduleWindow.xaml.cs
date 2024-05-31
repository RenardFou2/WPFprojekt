using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFprojekt
{
    public partial class WeeklyScheduleWindow : Window
    {
        public WeeklyScheduleWindow(ObservableCollection<Seans> seanse, DateTime startOfWeek)
        {
            InitializeComponent();
            DataContext = this;

            var endOfWeek = startOfWeek.AddDays(7);
            var weeklySeanse = seanse.Where(s => s.Czas >= startOfWeek && s.Czas < endOfWeek).ToList();

            WeeklyScheduleListView.ItemsSource = weeklySeanse;
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

                TableRow headerRow = new TableRow();
                table.RowGroups.Add(new TableRowGroup());
                table.RowGroups[0].Rows.Add(headerRow);

                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tytuł"))));
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Data"))));
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Sala"))));
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Cena biletu"))));
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Czas"))));
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Gatunek"))));

                foreach (Seans seans in WeeklyScheduleListView.Items)
                {
                    TableRow row = new TableRow();
                    table.RowGroups[0].Rows.Add(row);

                    row.Cells.Add(new TableCell(new Paragraph(new Run(seans.film.Nazwa))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run(seans.Czas.ToString()))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run(seans.Sala))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run(seans.Cena_biletu.ToString("C")))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run(seans.film.Dlugosc.ToString()))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run(seans.film.Gatunek))));
                }

                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Harmonogram");
            }
        }
    }
}
