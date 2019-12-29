using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electonnic_Journal.Controls
{
    class ExcelTabelsController
    {
        private string currentFileName { get; set; }
        private XLWorkbook workbook;
        private IXLWorksheet worksheet;

        public int columnsCount { get; }
        public int rowsCount { get; }

        public ExcelTabelsController() { }
        public ExcelTabelsController(string fileName, int numberOfWorksheet = 1)
        {
            if (fileName.Length != 0)
            {
                currentFileName = fileName;

                workbook = new XLWorkbook(currentFileName);
                worksheet = workbook.Worksheets.Worksheet(numberOfWorksheet);

                columnsCount = worksheet.FirstCell().CurrentRegion.ColumnCount();
                rowsCount = worksheet.ActiveCell.CurrentRegion.LastRow().RowNumber();
            }
        }

        public TabelItems GetHeader()
        {
            return GetFromId(0);
        }

        public TabelItems GetFromId(int id)
        {
            if (++id < rowsCount)
            {
                TabelItems tabelItems = new TabelItems
                {
                    id_item = worksheet.Cell(id, 1).Value.ToString(),
                    FIO = worksheet.Cell(id, 2).Value.ToString(),
                    marks = new string[columnsCount - 2]
                };

                for (int i = 0; i < tabelItems.marks.Length; i++)
                {
                    string buff = worksheet.Cell(id, i + 3).Value.ToString();
                    tabelItems.marks[i] = buff;
                }
                return tabelItems;
            }
            return null;
        }


    }
}
