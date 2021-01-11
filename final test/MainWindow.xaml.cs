using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime;

namespace final_test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            List<student> studentlist = new List<student>();
            List<course> courselist = new List<course>();
            List<teacher> teacherlist = new List<teacher>();
            

            InitializeComponent();
            using (var reader = new StreamReader("D:\\2B.csv",Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<student>();
                foreach(var studentdata in records)
                {
                    studentlist.Add(studentdata);
                }
            }
            comboBox.ItemsSource = studentlist;
            string sat= " ";
            int i = -1;
            using (var reader = new StreamReader("D:\\course.csv", Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {                
                var records = csv.GetRecords<course>();
                foreach (var coursedata in records)
                {
                    courselist.Add(coursedata);
                    teacher treeset = new teacher();
                    if(sat!=coursedata.Teacher)
                    {
                        sat = coursedata.Teacher;
                        treeset.TeacherName = coursedata.Teacher;
                        treeset.TeacherCourse.Add(new courses() { coursename = coursedata.CourseName, courseclass = coursedata.CourseClass, type = coursedata.Type, point = coursedata.Point});
                        teacherlist.Add(treeset);
                        i += 1;
                    }
                    else
                    {
                        teacherlist[i].TeacherCourse.Add(new courses() { coursename = coursedata.CourseName, courseclass = coursedata.CourseClass, type = coursedata.Type, point = coursedata.Point });
                    }

                }
                


            }
            listView1.ItemsSource = courselist;
            treeView.ItemsSource = teacherlist;

           
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {

        }
    }


    public class courses
    {
        public string coursename { get; set; }
        public string courseclass { get; set; }
        public string point { get; set; }
        public string type { get; set; }
        
        
       
    }
        

    public class teacher
    {
        
        public string TeacherName { get; set; }
        public ObservableCollection<courses> TeacherCourse { get; set; } 
        public teacher()
        {
            TeacherCourse = new ObservableCollection<courses>();                       
           
        }


    }
    public class student
    {
        [Index(0)]
        public string ID { get; set; }
        [Index(1)]
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{ this.ID} { this.Name}";
        }
    }
    public class course
    {
        [Index(0)]
        public string Teacher { get; set; }
        [Index(1)]
        public string CourseName { get; set; }
        [Index(2)]
        public string Point { get; set; }
        [Index(3)]
        public string Type { get; set; }
        [Index(4)]
        public string CourseClass { get; set; }
        [Index(5)]
        public string CourseTime { get; set; }
        public override string ToString()
        {
            return $"{ this.Teacher} { this.CourseName} { this.Point} { this.Type}" +
                $"{ this.CourseClass} { this.CourseTime}";
        }
    }
}
