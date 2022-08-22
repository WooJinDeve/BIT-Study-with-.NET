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

namespace _0419_바인딩
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private People people = new People();
        private Person per = null;
        public MainWindow()
        {
            InitializeComponent();

            per = people[0];
            UpdateFirstLineUI();
            UpdateListBox();

        }

        #region DATA -> UI
        private void UpdateFirstLineUI()
        {
            if (per == null)
            {
                nameLabel.Content = name.Text = "";
                phoneLabel.Content = phone.Text = "";
                ageLabel.Content = age.Text = "";
                return;
            }
            nameLabel.Content    = name.Text    = per.Name;
            phoneLabel.Content   = phone.Text   = per.Phone;
            ageLabel.Content     = age.Text     = per.Age.ToString();
        }

        private void UpdateListBox()
        {
            listbox.Items.Clear();
            foreach (Person per in people)
                listbox.Items.Add(per);
        }
        #endregion

        #region UI -> DATA
        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (per == null)
                return;

            per.Name = name.Text;
            UpdateFirstLineUI();

            //nameLabel.Content = name.Text;
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (per == null)
                return;

            per.Phone = phone.Text;
            UpdateFirstLineUI();

            //phoneLabel.Content = phone.Text;
        }

        private void age_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (per == null)
                return;

            per.Age = int.Parse(age.Text);
            UpdateFirstLineUI();

            //ageLabel.Content = age.Text;
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                per = people[listbox.SelectedIndex];
                UpdateFirstLineUI();
            }
        }

        #endregion

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;

            //foreach (Person person in people)
            //{
            //    if (name.Text == person.Name && phone.Text == person.Phone)
            //        return;
            //}

            people.Add(new Person(name.Text, phone.Text, int.Parse(age.Text)));
            // 리스트 박스의 아이템을 갱신한다.
            UpdateListBox();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                people.RemoveAt(listbox.SelectedIndex);
                // 컬렉션에 원소가 없다면 리슽의 현재 아이템이 없도록(per =null) 한다.
                if (people.Count == 0)
                    per = null;
                else
                    per = people[0];
                // 모든 UI 컨트롤을 갱신한다.
                UpdateFirstLineUI();
                UpdateListBox();
            }

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;

            per.Name = name.Text;
            per.Phone = phone.Text;
            per.Age = int.Parse(age.Text);

            UpdateFirstLineUI();
            UpdateListBox();
        }
    }
}
