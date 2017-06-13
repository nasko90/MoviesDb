using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using MoviesDatabase.Models.Models;
using DataGrid = System.Windows.Controls.DataGrid;
using DataGridColumn = System.Windows.Controls.DataGridColumn;

namespace MoviesDatabaseWPF.Classes
{
    public class PdfExoirter
    {
        public void ExportGridToPdf(System.Web.UI.WebControls.GridView grid, User curentUser)
        {
            PdfPTable pdfPTable = new PdfPTable(grid.HeaderRow.Cells.Count);

            foreach (TableCell header in grid.HeaderRow.Cells)
            {
                pdfPTable.AddCell(header.Text);
            }
            foreach (GridViewRow tablerRow in grid.Rows)
            {
                foreach (TableCell tableCell in tablerRow.Cells)
                {
                    PdfPCell pdfPCell = new PdfPCell(new Phrase(tableCell.Text));
                    pdfPTable.AddCell(pdfPCell);
                }
            }

            Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, new FileStream("Test Movie collection.pdf", FileMode.Create, FileAccess.Write));      
            pdfDocument.Open();
            pdfDocument.Add(pdfPTable);
            pdfDocument.Close();
        }
    }
}
