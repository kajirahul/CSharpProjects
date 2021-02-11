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
using System.Windows.Shapes;
using static Project_phase2.MainWindow;

namespace Project_phase2
{
    /// <summary>
    /// Interaction logic for AddInstructor.xaml
    /// </summary>
    public partial class AddInstructor : Window
    {

        public AddInstructor()
        {
            InitializeComponent();
        }
        Project22 P = new Project22();
        private void AddInstructbtn_Click(object sender, RoutedEventArgs e)
        {
            Instructor c = new Instructor();
            c.ID = Int32.Parse(InstructID.Text);
            c.FirstName = InstructFname.Text;
            c.LastName = InstrucLname.Text;
             /* ---------------------------------------
            *-----Not applicable while adding------ -
           *---------------------------------
      //string name = InstructCourse.Text;
      //var cr = P.Courses.SingleOrDefault(a => a.Name == name);
      //if (cr != null)
      //{
      //    c.Courses.Add(cr);
      //}
      //else
      //{
      //    MessageBox.Show("Course not found");
      //}
             */
      Project22 p = new Project22();

            p.Instructors.Add(c);
            p.SaveChanges();
            this.Close();
            MessageBox.Show("Instructor Successfully added");
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void update_Instruct_Click(object sender, RoutedEventArgs e)
        {
            
            int pID = Int32.Parse(InstructID.Text);
            var I = P.Instructors.SingleOrDefault(a => a.ID == pID);
            if (I != null)
            {
                I.ID = pID;
                I.FirstName = InstructFname.Text;
                I.LastName = InstrucLname.Text;

               
                string name = InstructCourse.Text;
                var cr = P.Courses.SingleOrDefault(a => a.Name == name);
                if ( cr != null)
                {
                    I.Courses.Add(cr);
                MessageBox.Show("Data successfully updated.");
                }
                else
                {
                    MessageBox.Show("Course not found");
                }
                
            }
            else
            {
                MessageBox.Show("The instructor with ID " + pID + " doesn't exists.");
            }
            P.SaveChanges();
            
            this.Close();
            MainWindow m = new MainWindow();
            m.Show();
        }

        private void delete_Instructor_Click(object sender, RoutedEventArgs e)
        {
           
            int pID = Int32.Parse(InstructID.Text);
            Instructor I = P.Instructors.SingleOrDefault(a => a.ID == pID);
            if (I != null)
            {
                P.Instructors.Remove(I);
                MessageBox.Show("Data successfully deleted.");
            }
            else
            {
                MessageBox.Show("No match found.");
            }
            P.SaveChanges();
            
            this.Close();
            MainWindow m = new MainWindow();
            m.Show();
        }
    }
}
