﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Linq;

namespace WpfApplication1 {
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window {
		DataContext dc;

		Table<Student> students;
		Table<Grade> grades;
        Table<Course> course;
		public Window1() {
			InitializeComponent();

			string host = "tod112.hib.no,1443";
			string database = "tod112";
			string user = "tod112";
			string pass = "tod112";

			dc = new DataContext("Data Source=" + host + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass);


			students = dc.GetTable<Student>();
			grades = dc.GetTable<Grade>();
            course = dc.GetTable<Course>();

			studentList.DataContext = students;
            courseList.DataContext = course;
            
            var stuff = students.Join(grades, st => st.id, gr => gr.studentid, (st, gr) => new {st.studentname, gr.coursecode, gr.grade });
			gradeList.DataContext = stuff;

            var piss = from grade in grades
                       select grade.grade;

            gradeBox.DataContext = piss;
		}

        private void courseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course c = (Course)courseList.SelectedItem;
            var filter = dc.GetTable<Student>().
                Join(grades, st => st.id, gr => gr.studentid, (st, gr) => new { st, gr }).
                Join(course, grr => grr.gr.coursecode, cr => cr.coursecode, (grr, cr) => new { grr, cr }).
                Where(m => m.grr.gr.coursecode.Equals(c.coursecode)).
                Select(m => new { m.grr.st.id, m.grr.st.studentname, m.grr.st.studentage });

            studentList.DataContext = filter;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            char grad = ((string)((ComboBoxItem)gradeBox.SelectedItem).Content).ToCharArray().First();
            if (grad == 'F')
            {
               var filter = dc.GetTable<Student>().
               Join(grades, st => st.id, gr => gr.studentid, (st, gr) => new { st, gr }).
               Join(course, grr => grr.gr.coursecode, cr => cr.coursecode, (grr, cr) => new { grr, cr }).
               Where(m => m.grr.gr.grade.Equals(grad)).
               Select(m => new { m.grr.st.studentname, m.cr.coursecode, m.grr.gr.grade });

                /* Comments/suggestions from Remy Monsen
               var filter2 = dc.GetTable<Student>()
                   .Join(grades, s => s.id, g => g.studentid, (s, g) => new { s.studentname, g.coursecode, g.grade })
                   .Where(m => m.grade.Equals(grad));


               DataClasses1DataContext dx = new DataClasses1DataContext();

               var filter3 = dx.grades.Where(x => x.grade1.Equals(grad)).Select(x => new { x.student.id, x.coursecode, x.grade1 });
                */
               gradeList.DataContext = filter;
            }
            else
            {
               var filter = dc.GetTable<Student>().
               Join(grades, st => st.id, gr => gr.studentid, (st, gr) => new { st, gr }).
               Join(course, grr => grr.gr.coursecode, cr => cr.coursecode, (grr, cr) => new { grr, cr }).
               Where(m => (m.grr.gr.grade.CompareTo(grad)) <= 0).
               Select(m => new { m.grr.st.studentname, m.cr.coursecode, m.grr.gr.grade });

               gradeList.DataContext = filter;
            }

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            var filter = dc.GetTable<Student>().Where(st => st.studentname.Contains(nameSearch.Text));
            studentList.DataContext = filter;
        }

        private void search_Click2(object sender, KeyEventArgs e)
        {
            var filter = dc.GetTable<Student>().Where(st => st.studentname.Contains(nameSearch.Text));
            studentList.DataContext = filter;
        }

	}
}
