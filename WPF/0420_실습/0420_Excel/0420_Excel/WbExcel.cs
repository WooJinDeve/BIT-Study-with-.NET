using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0420_Excel
{
    class WbExcel
    {
        public WbExcel() { }

        #region 엑셀 불러오기
        public void ExcelLoad()
        {
            Microsoft.Office.Interop.Excel.Application application = null;
            Workbook workBook = null;

			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				if (dlg.ShowDialog() != DialogResult.OK)
					return;

				//Excel 프로그램 실행
				application = new Microsoft.Office.Interop.Excel.Application();
				//Excel 화면 띄우기 옵션
				application.Visible = true;
				//파일로부터 불러오기
				workBook = application.Workbooks.Open(dlg.FileName);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
			finally
			{
				//변경점 저장하면서 닫기
				workBook.Close(true);
				//Excel 프로그램 종료
				application.Quit();
				//오브젝트 해제
				ReleaseExcelObject(workBook);
				ReleaseExcelObject(application);
			}
		}
		private void ReleaseExcelObject(object obj)
		{
			try
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
					obj = null;
				}
			}
			catch (Exception ex)
			{
				obj = null;
				throw ex;
			}
			finally
			{
				GC.Collect();
			}
		}
        #endregion

        #region 엑셀 저장
		public void ExcelSave()
        {
			Microsoft.Office.Interop.Excel.Application application = null;
			Workbook workBook = null;

			try
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "xlsx files|*.xlsx";
				if (dlg.ShowDialog() != DialogResult.OK)
					return;

				application = new Microsoft.Office.Interop.Excel.Application();
				application.Visible = true;

				//Workbook 생성
				workBook = application.Workbooks.Add();
				//특정 경로에 엑셀 파일로 저장
				workBook.SaveAs(dlg.FileName, XlFileFormat.xlWorkbookDefault);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
			finally
			{
				workBook.Close(true);
				application.Quit();

				ReleaseExcelObject(workBook);
				ReleaseExcelObject(application);
			}
		}
		#endregion

		//변경함
		#region 엑셀 값 변경
		public void ExcelToSeatChanged(string msg, int column, int row)
        {
			Microsoft.Office.Interop.Excel.Application application = null;
			Workbook excel = null;
			Worksheet sheet = null;

			try
			{
				//엑셀 오픈
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "xlsx files|*.xlsx";
				if (dlg.ShowDialog() != DialogResult.OK)
					return;

				//엑셀 정보 변경 
				application = new Microsoft.Office.Interop.Excel.Application();
				application.Visible = true; //??

				//오픈한 엑셀 정보 저장
				excel = application.Workbooks.Open(dlg.FileName);

				//첫번째 시트정보 저장
				sheet = excel.Worksheets.Item[1];

				// 행 열에 맞는 msg 저장
				Range cell1 = sheet.Cells[column, row];
				cell1.Value = msg;

				//엑셀 저장 
				excel.SaveAs(dlg.FileName, XlFileFormat.xlWorkbookDefault);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
			finally
			{
				excel.Close(true);
				application.Quit();

				ReleaseExcelObject(excel);
				ReleaseExcelObject(application);
			}
		}

		#endregion
	}
}
