using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Project_phase2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Create();
            var cRegister = new Project22();
            cRegister.Courses.Load();
            dgrid.ItemsSource = cRegister.Courses.Local;

           
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
        public class Course
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int ID { get; set; }
            public string Department { get; set; }
            public int Number { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Prerequisite { get; set; }
            public virtual Instructor Instructors { get; set; }
            public string Status { get; set; }
            public int ClassSize { get; set; }
            public int Enrolled { get; set; }
            public string Semester { get; set; }
            public int Year { get; set; }

            public string Section { get; set; }
            public int CreditHrs { get; set; }
            public string ClassDays { get; set; }
            public string Time { get; set; }

            public string RoomNum { get; set; }
        }
        public class Instructor
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public virtual List<Course> Courses { get; set; }
            public override string ToString()
            {
                return string.Format("{0}, {1}", LastName, FirstName);
            }
            
        }

        static void Create()
        {
            int[] refNums = { 11,22,33,44,55,66 };

            string[] instructorNames = { "Jacob jackson","Amy Johnson","Kaji Prasad" };

            string[] departments = { "CSC","MATH","SCE" };

            int[] courseNumbers = { 285, 460, 221, 222, 110, 120};

           

            string[] courseName = { "Programming","Mathematics","Algebra","Statistics","Biology","Astronomy"};

            string[] description = {"Intro to programming","Problem Solving","Algebra issues","Numbers game","Plant life","Space life"};

            string[] prereq = { "CSC 120", "CSC 220", "Math 112", "Math 222", "Bio 110", "Bio 120", "Ast 223", "Ast344" };

            string[] status = { "Open", "Closed", "Open", "Open", "Closed", "Open", "Open", "Open" };
            int[] size = { 12, 10, 13, 14, 15, 16, 17, 18 };
            int[] enrolled = { 09, 10, 9, 9, 15, 9, 9, 9 };
            string[] sem = { "Spring", "Summer", "Fall"};
            int[] year = { 2020, 2021, 2019, 2020, 2020, 2021, 2019, 2020 };
            string[] section = { "1", "A1", "1C" };
            int[] credit = { 3, 3, 3};
            string[] days = { "MW", "MWF", "TR" };
            string[] time = { "8:00am - 9:15am", "9:30am - 10:15am", "2:30pm-3:15pm" };
            string[] room = { "CSC 201", "CSC 222", "BH 263"};


            Instructor[] instructors = new Instructor[3];
            for (int i = 0; i < 3; ++i)
                instructors[i] = new Instructor()
                {
                    FirstName = instructorNames[i].Split(new char[] { ' ' })[0],
                    LastName = instructorNames[i].Split(new char[] { ' ' })[1],
                    Courses = new List<Course>()
                };

            Course[] courses = new Course[6];
            for (int i = 0; i < 6; ++i)
                courses[i] = new Course()
                {
                    ID = refNums[i],
                    Department = departments[i / 2],
                    Instructors = instructors[i / 2],
                    Number = courseNumbers[i],
                    Name = courseName[i],
                    Description = description[i],
                    Prerequisite = prereq[i],
                    Status = status[i],
                    ClassSize = size[i],
                    Enrolled = enrolled[i],
                    Semester = sem[i/2],
                    Year = year[i],
                    Section = section[i/2],
                    CreditHrs = credit[i/2],
                    ClassDays = days[i/2],
                    Time = time[i/2],
                    RoomNum = room[i/2],

                };



            using (var cRegister = new Project22())
            {
                cRegister.Instructors.AddRange(instructors);
                cRegister.Courses.AddRange(courses);
                
                cRegister.SaveChanges();
            }   
        }
        public class Project22 : DbContext
        {
            public Project22() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pande\OneDrive\Documents\Project22.mdf;Integrated Security=True;Connect Timeout=30")
            {

            }
            public DbSet<Course> Courses { get; set; }
            public DbSet<Instructor> Instructors { get; set; }

        }

        // *******------Adding new Course button------********
        private void addCourse1_Click(object sender, RoutedEventArgs e)
        {
            AddCourse a = new AddCourse();
            a.Show();
        }

        // *******------Adding new Instructor button------********
        private void addInstructor_Click(object sender, RoutedEventArgs e)
        {
            AddInstructor I = new AddInstructor();
            I.Show();
        }

        private void dgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            AddCourse ac= new AddCourse();
            Course c = (Course)dgrid.SelectedItem;
            ac.CourseClassDays.Text = c.ClassDays.ToString();
            ac.CourseClassSize.Text = c.ClassSize.ToString();
            ac.CourseCreditHrs.Text = c.CreditHrs.ToString();
            ac.CourseDpt.Text = c.Department.ToString();
            ac.CourseDscrpt.Text = c.Description.ToString();
            ac.CourseEnrolled.Text = c.Enrolled.ToString();
            ac.CourseID.Text = c.ID.ToString();
            ac.CourseLastname.Text = c.Instructors.LastName.ToString();
            ac.CourseInstID.Text = c.Instructors.FirstName.ToString();
            ac.CourseName.Text = c.Name.ToString();
            ac.CourseNum.Text = c.Number.ToString();
            ac.CoursePre.Text = c.Prerequisite.ToString();
            ac.CourseRoomNum.Text = c.RoomNum.ToString();
            ac.CourseSection.Text = c.Section.ToString();
            ac.CourseSem.Text = c.Semester.ToString();
            ac.CourseStatus.Text = c.Status.ToString();
            ac.CourseClassTime.Text = c.Time.ToString();
            ac.CourseYear.Text = c.Year.ToString();
            ac.Show();
        }

        private void dgrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddCourse ac = new AddCourse();
                Course c = (Course)dgrid.SelectedItem;
                ac.CourseClassDays.Text = c.ClassDays.ToString();
                ac.CourseClassSize.Text = c.ClassSize.ToString();
                ac.CourseCreditHrs.Text = c.CreditHrs.ToString();
                ac.CourseDpt.Text = c.Department.ToString();
                ac.CourseDscrpt.Text = c.Description.ToString();
                ac.CourseEnrolled.Text = c.Enrolled.ToString();
                ac.CourseID.Text = c.ID.ToString();
                ac.CourseLastname.Text = c.Instructors.LastName.ToString();
                ac.CourseInstID.Text = c.Instructors.FirstName.ToString();
                ac.CourseName.Text = c.Name.ToString();
                ac.CourseNum.Text = c.Number.ToString();
                ac.CoursePre.Text = c.Prerequisite.ToString();
                ac.CourseRoomNum.Text = c.RoomNum.ToString();
                ac.CourseSection.Text = c.Section.ToString();
                ac.CourseSem.Text = c.Semester.ToString();
                ac.CourseStatus.Text = c.Status.ToString();
                ac.CourseClassTime.Text = c.Time.ToString();
                ac.CourseYear.Text = c.Year.ToString();
                
                ac.Show();

            }
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            Project22 project = new Project22();
            
            dgrid.ItemsSource = project.Courses.Where(a => a.Semester.Contains(searchtxt.Text) ||
                                                      a.Year.ToString().Contains(searchtxt.Text) ||
                                                      a.Instructors.FirstName.Contains(searchtxt.Text)||
                                                      a.Instructors.LastName.Contains(searchtxt.Text)||
                                                      a.Department.Contains(searchtxt.Text) ||
                                                      a.Number.ToString().Contains(searchtxt.Text)).ToList();
        }
    }
}
