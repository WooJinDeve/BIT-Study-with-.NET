using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0419.동적바인딩
{
    internal class Student
    {
        public bool IsChecked { get; set; }
        public string StudentID { get; set; }
        public string StudentName { get; set; }

        public static ObservableCollection<Student> GetCollection() 
        { 
            ObservableCollection<Student> collection = 
                new ObservableCollection<Student>(); 
            
            collection.Add(new Student() { StudentID = "0001", StudentName = "학생1" }); 
            collection.Add(new Student() { StudentID = "0002", StudentName = "학생2" }); 
            collection.Add(new Student() { StudentID = "0003", StudentName = "학생3" }); 
            return collection; 
        }
    }
}


