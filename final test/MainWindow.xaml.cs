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
        }

    }
    
    public class treenode 
    {
        public string titel { get; set; }
        public ObservableCollection<treenode> Item { get; set; }
    
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
        public string CourseNum { get; set; }
        [Index(3)]
        public string CourseMoN { get; set; }
        [Index(4)]
        public string CourseClass { get; set; }
        [Index(5)]
        public string CourseTime { get; set; }
       

        public override string ToString()
        {
            return $"{ this.Teacher} { this.CourseName} { this.CourseNum} { this.CourseMoN}" +
                $"{ this.CourseClass} { this.CourseTime}";
        }
    }
}
