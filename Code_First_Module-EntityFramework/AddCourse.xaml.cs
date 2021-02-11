using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
using static Project_phase2.MainWindow;

namespace Project_phase2
{
    /// <summary>
    /// Interaction logic for AddCourse.xaml
    /// </summary>
    public partial class AddCourse : Window
    {
        Course c = new Course();
        public AddCourse()
        {
            InitializeComponent();


        }
        public enum Statuss
        {
            open, closed, Open, Closed
        }
        //public enum section
        //{
        //    Ap1, AP2, _1, _1C, _1D
        //}
        public enum ClassDays
        {
            M, T, W, R, F, MW, MWF, TR
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Project22 r = new Project22();
            c.ID = Int32.Parse(CourseID.Text);
            c.Department = CourseDpt.Text;
            c.Number = Int32.Parse(CourseNum.Text);
            c.Name = CourseName.Text;
            var instruct = r.Instructors.SingleOrDefault(a => a.FirstName == CourseInstID.Text && a.LastName == CourseLastname.Text);
            if (instruct != null)
            {
                c.Instructors = (Instructor)instruct;
            }
            else
            {
                Instructor i = new Instructor();
                i.FirstName = CourseInstID.Text;
                i.LastName = CourseLastname.Text;
                r.Instructors.Add(i);
                c.Instructors = i;
            }
            c.Description = CourseDscrpt.Text;
            c.Prerequisite = CoursePre.Text;
            if (Enum.IsDefined(typeof(Statuss), CourseStatus.Text))
            {
                c.Status = CourseStatus.Text;

            }
            else
            {
                MessageBox.Show("Input Valid Value for status.");

            }
            c.ClassSize = Int32.Parse(CourseClassSize.Text);
            c.Enrolled = Int32.Parse(CourseEnrolled.Text);
            c.Semester = CourseSem.Text;
            c.Year = Int32.Parse(CourseYear.Text);
            c.Section = CourseSection.Text;
            c.CreditHrs = Int32.Parse(CourseCreditHrs.Text);
            if (Enum.IsDefined(typeof(ClassDays), CourseClassDays.Text))
            {
                c.ClassDays = CourseClassDays.Text;
            }
            c.Time = CourseClassTime.Text;
            c.RoomNum = CourseRoomNum.Text;
            r.Courses.Add(c);
            r.SaveChanges();
            MessageBox.Show("Data successfully inserted.");
            this.Close();
            MainWindow m = new MainWindow();
            m.Show();

        }

        private void saveCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            Project22 r = new Project22();
            int cID = Int32.Parse(CourseID.Text);
            Course cr = r.Courses.SingleOrDefault(a => a.ID == cID);
            if (cr != null)
            {
                cr.ID = Int32.Parse(CourseID.Text);
                cr.Department = CourseDpt.Text;
                cr.Number = Int32.Parse(CourseNum.Text);
                cr.Name = CourseName.Text;
                cr.Instructors.FirstName = CourseInstID.Text;
                cr.Instructors.LastName = CourseLastname.Text;
                cr.Description = CourseDscrpt.Text;
                cr.Prerequisite = CoursePre.Text;
                if (Enum.IsDefined(typeof(Statuss), CourseStatus.Text))
                {
                    cr.Status = CourseStatus.Text;

                }
                else
                {
                    MessageBox.Show("Input Valid Value for status.");

                }
                cr.ClassSize = Int32.Parse(CourseClassSize.Text);
                cr.Enrolled = Int32.Parse(CourseEnrolled.Text);
                cr.Semester = CourseSem.Text;
                cr.Year = Int32.Parse(CourseYear.Text);
                cr.Section = CourseSection.Text;
                cr.CreditHrs = Int32.Parse(CourseCreditHrs.Text);
                if (Enum.IsDefined(typeof(ClassDays), CourseClassDays.Text))
                {
                    cr.ClassDays = CourseClassDays.Text;
                }
                cr.Time = CourseClassTime.Text;
                cr.RoomNum = CourseRoomNum.Text;
                r.SaveChanges();
                MessageBox.Show("Data successfully updated.");

                this.Close();
                MainWindow m = new MainWindow();
                m.Show();

            }
            else
            {
                MessageBox.Show("Please make sure the course with course ID " + cr + "exists");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Project22 r = new Project22();
            int cID = Int32.Parse(CourseID.Text);
            Course cr = r.Courses.SingleOrDefault(a => a.ID == cID);
            if (cr != null)
            {
                r.Courses.Remove(cr);
                MessageBox.Show("Data successfully deleted.");
            }
            else
            {
                MessageBox.Show("No matching record found.");
            }
             r.SaveChanges();
                

                this.Close();
                MainWindow m = new MainWindow();
                m.Show();
        }

    }   
    
}
