using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0419.동적바인딩
{
    internal class School
    {
        public bool IsChecked { get; set; }
        public string SchoolID { get; set; }
        public string SchoolName { get; set; }

        public static ObservableCollection<School> GetCollection()
        {
            ObservableCollection<School> collection =
                new ObservableCollection<School>();
            collection.Add(new School() { SchoolID = "0001", SchoolName = "학교1" });
            collection.Add(new School() { SchoolID = "0002", SchoolName = "학교2" });
            collection.Add(new School() { SchoolID = "0003", SchoolName = "학교3" });
            return collection;
        }
    }
}
