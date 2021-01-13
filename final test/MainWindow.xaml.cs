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
using System;

namespace final_test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<record> coursesellist = new List<record>();
        List<student> studentlist = new List<student>();
        List<course> courselist = new List<course>();
        List<teacher> teacherlist = new List<teacher>();
        List<print> printlist = new List<print>();
        int selectedclassnum=0,selectedclasspoint=0;
        public MainWindow()
        {                  
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
            int i = -1, j = 0; ;
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
                        treeset.TeacherCourse.Add(new courses() { code=j,tea=coursedata.Teacher,coursename = coursedata.CourseName, courseclass = coursedata.CourseClass, type = coursedata.Type, point = coursedata.Point});
                        teacherlist.Add(treeset);
                        i += 1;
                        j += 1;
                    }
                    else
                    {
                        teacherlist[i].TeacherCourse.Add(new courses() { code = j,tea = coursedata.Teacher, coursename = coursedata.CourseName, courseclass = coursedata.CourseClass, type = coursedata.Type, point = coursedata.Point });
                        j += 1;
                    }

                }
                


            }
            listView1.ItemsSource = courselist;
            treeView.ItemsSource = teacherlist;
            
           
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {      
            if (treeView.SelectedItem is courses)
            {
                label1.Content = treeView.SelectedItem;
            }        
        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            if (teachernameclass.IsSelected)
            { NewMethod(); }
            if(allclass.IsSelected)
            {aaaaa();}
            
            
        }

        private void aaaaa()
        {
            if (comboBox.SelectedItem != null && listView1.SelectedItem != null)
            {
                student selstd = new student()
                {
                    ID = studentlist[comboBox.SelectedIndex].ID,
                    Name = studentlist[comboBox.SelectedIndex].Name

                };
                courses cos = new courses()
                {
                    coursename = courselist[listView1.SelectedIndex].CourseName,
                    courseclass = courselist[listView1.SelectedIndex].CourseClass,
                    tea = courselist[listView1.SelectedIndex].Teacher,
                    type = courselist[listView1.SelectedIndex].Type,
                    point = courselist[listView1.SelectedIndex].Point,
                    code = listView1.SelectedIndex


                };
                record rds = new record()
                {
                    selstudent = selstd,
                    selcourse = cos
                };

                if (coursesellist.Count > 0)
                {
                    int b = 0;
                    for (int x = 0; x < coursesellist.Count; x++)
                    {
                        if (coursesellist[x].selcourse.code == rds.selcourse.code)
                        {
                            b += 1;
                        }
                        if (coursesellist[x].selcourse.code != rds.selcourse.code && x == coursesellist.Count - 1 && b == 0)
                        {
                            coursesellist.Add(rds);
                            selectedclassnum += 1;
                            selectedclasspoint += cos.point;
                        }
                    }
                }
                else
                {
                    coursesellist.Add(rds);
                    selectedclassnum += 1;
                    selectedclasspoint += cos.point;
                }
                listView.ItemsSource = coursesellist;
                listView.Items.Refresh();
            }
        }

        private void NewMethod()
        {
            if (comboBox.SelectedItem != null && treeView.SelectedItem != null)
            {
                student selstd = new student()
                {
                    ID = studentlist[comboBox.SelectedIndex].ID,
                    Name = studentlist[comboBox.SelectedIndex].Name

                };
                record rd = new record()
                {
                    selstudent = selstd,
                    selcourse = treeView.SelectedItem as courses
                };

                if (coursesellist.Count > 0)
                {
                    int b = 0;
                    for (int x = 0; x < coursesellist.Count; x++)
                    {
                        if (coursesellist[x].selcourse.code == rd.selcourse.code)
                        {
                            b += 1;
                        }
                        if (coursesellist[x].selcourse.code != rd.selcourse.code && x == coursesellist.Count - 1 && b == 0)
                        {
                            coursesellist.Add(rd);
                            selectedclassnum += 1;
                            selectedclasspoint += rd.selcourse.point;
                        }
                    }
                }
                else
                {
                    coursesellist.Add(rd);
                    selectedclassnum += 1;
                    selectedclasspoint += rd.selcourse.point;
                }
                listView.ItemsSource = coursesellist;
                listView.Items.Refresh();
            }
            
        }

        private void deletebutton_Click(object sender, RoutedEventArgs e)
        {  
            if(listView.SelectedIndex > 0 )
            {
                selectedclasspoint -= coursesellist[listView.SelectedIndex].selcourse.point;
                coursesellist.Remove(coursesellist[listView.SelectedIndex]);
                listView.Items.Refresh();
                selectedclassnum -= 1;
            }
           
            if(listView.SelectedIndex==0)
            {
                listView.Items.Clear();
            }
            
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            coursesellist.Clear();
            listView.Items.Refresh();
            selectedclassnum = 0;
            selectedclasspoint = 0;


        }

        private void listView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
                                
            label1.Content = listView1.SelectedItem;
            
                        
        }

        private void savebutton_Click(object sender, RoutedEventArgs e)
        {
            print pn = new print()
            {
                學號 = studentlist[comboBox.SelectedIndex].ID,
                姓名 = studentlist[comboBox.SelectedIndex].Name,
                已選課數 = selectedclassnum,
                已選學分數 = selectedclasspoint
            };

            using (var writer = new StreamWriter($"C:\\Users\\Administrator\\Desktop\\{comboBox.SelectedItem}選課清單.csv",false,Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                printlist.Add(pn);
                csv.WriteRecords(printlist);
            }

            printlist.Clear();
        }
    }
    class print
    {
        public string 學號 { get; set; }
        public string 姓名 { get; set; }
        public int 已選課數{ get; set; }
        public int 已選學分數 { get; set; }
    }
    class record
    {
        public student selstudent { get; set; }
        public courses selcourse { get; set; }

    }
    class courses
    {
        public string tea { get; set; }
        public string coursename { get; set; }
        public string courseclass { get; set; }
        public int point { get; set; }
        public string type { get; set; }
        public int code { get; set; }
       
        public override string ToString()
        {
            return $"{this.tea} { this.coursename} {this.type} { this.point} {"學分"} {"開課班級:"}{ this.courseclass}";
        }

    }      

    class teacher
    {        
        public string TeacherName { get; set; }
        public ObservableCollection<courses> TeacherCourse { get; set; } 
        public teacher()
        {
            TeacherCourse = new ObservableCollection<courses>();                       
           
        }
        public override string ToString()
        {
            return $"{ this.TeacherName} ";
        }

    }
    class student
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
    class course
    {
        [Index(0)]
        public string Teacher { get; set; }
        [Index(1)]
        public string CourseName { get; set; }
        [Index(2)]
        public int Point { get; set; }
        [Index(3)]
        public string Type { get; set; }
        [Index(4)]
        public string CourseClass { get; set; }
        [Index(5)]
        public string CourseTime { get; set; }
       
        
        public override string ToString()
        {
            return $"{ this.Teacher} { this.CourseName}  { this.Point} { this.Type}" +
                $"{ this.CourseClass} { this.CourseTime}";
        }
    }

}
