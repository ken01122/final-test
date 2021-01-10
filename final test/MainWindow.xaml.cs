using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

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

            using (var reader = new StreamReader("D:\\course.csv", Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {                
                var records = csv.GetRecords<course>();
                foreach (var coursedata in records)
                {
                    courselist.Add(coursedata);
                  
                }
                
            }
            listView1.ItemsSource = courselist;
            treeView.ItemsSource = teacherlist;
        }

    }
    
    
    public class teacher
    {
        
        public string TeacherName { get; set; }
                
        public ObservableCollection<course> TeacherCourse { get; set; } 
        public teacher(string tn,course cn)
        {            
            
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
