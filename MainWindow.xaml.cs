using System;
using System.Windows;
using WPFAN_Access.dsStudentsTableAdapters;
namespace WPFAN_Access
{
	
	public partial class MainWindow : Window
	{
		dsStudents dsStudents=new dsStudents();
		taAssignments taAssignments=new taAssignments();
		taGrades taGrades=new taGrades();
		TableAdapterManager tam;

		public MainWindow()
		{
			InitializeComponent();
			tam = new TableAdapterManager { taAssignments=taAssignments,taGrades=taGrades };
			try
			{
				taGrades.Fill(dsStudents.dtGrades);
				taAssignments.Fill(dsStudents.dtAssignments);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void miMarks_Click(object sender, RoutedEventArgs e)
		{
			ShowData();
		}
		void ShowData()
		{
			string s = "";
			foreach (dsStudents.dtAssignmentsRow x in dsStudents.dtAssignments.Rows)
			{
				s += x.Student + ": " + x.Topic + ", " + x.Grade + " (" + x.dtGradesRow.GradeDescription + ")\r\n";
			}
			s += "\r\n";
			tbData.Text += s;
		}


		private void miSave_Click(object sender, RoutedEventArgs e)
		{
			tam.UpdateAll(dsStudents);
		}

		private void miNew_Click(object sender, RoutedEventArgs e)
		{
			var st = "John Smith";
			var js = dsStudents.dtAssignments.FindByStudent(st);
			var gr = dsStudents.dtGrades.FindByGrade(5);
			if (js == null && gr != null)
				dsStudents.dtAssignments.AdddtAssignmentsRow(st, "How to develop Razor apps", gr);
		}

		private void miModify_Click(object sender, RoutedEventArgs e)
		{
			var res = dsStudents.dtAssignments.FindByStudent("John Doe");
			res.BeginEdit();
			res.Grade = 2;
			res.EndEdit();
		}

		private void miDelete_Click(object sender, RoutedEventArgs e)
		{
			var res = dsStudents.dtAssignments.FindByStudent("Jane Doe");
			if (res != null)
				res.Delete();
			res.AcceptChanges();
		}
	}
}
