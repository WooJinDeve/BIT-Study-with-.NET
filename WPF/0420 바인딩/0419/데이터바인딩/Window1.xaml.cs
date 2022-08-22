using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace _0419.데이터바인딩
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        //private Person per = new Person("홍길동", "010-1111-1111", 10);
        //private Person per;
        //People people = new People();

        public Window1()
        {
            InitializeComponent();

            //per = (Person)FindResource("per");
            //panel.DataContext = people;

            //panel.DataContext = per; //데이터 바인딩
            Validation.AddErrorHandler(age, age_ValidationError);
        }

        void age_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            age.ToolTip = (string)e.Error.ErrorContent;
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            Person person = (Person)view.CurrentItem;
            person.Name = "";
            person.Phone = "";
            person.Age = null;
            person.Male = null;
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = 
                CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast();
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            People people = (People)FindResource("people");
            if (listbox.SelectedIndex >= 0)
                people.RemoveAt(listbox.SelectedIndex);
        }
    }
}
