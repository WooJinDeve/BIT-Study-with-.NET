using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _0419.동적바인딩
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        #region Field 

        private PropertyDescriptorCollection propertyDescriptorCollection = null;
        private PropertyDescriptor isCheckedPropertyDescriptor = null; 
        private PropertyDescriptor textPropertyDescriptor = null; 

        #endregion


        public Window1()
        {
            InitializeComponent();
        }

        private void schoolButton_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<School> collection = School.GetCollection(); 
            this.listBox.ItemsSource = collection;             
            this.listBox.ItemTemplate = (DataTemplate)FindResource("schoolTemplate");
            //GetDataTemplate(typeof(School), "IsChecked", "SchoolID", "SchoolName");

            //this.propertyDescriptorCollection =
            //    TypeDescriptor.GetProperties(collection[0]);
            //this.isCheckedPropertyDescriptor =
            //    this.propertyDescriptorCollection["IsChecked"];
            //this.textPropertyDescriptor =
            //    this.propertyDescriptorCollection["SchoolName"];
        }

        private void studentButton_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Student> collection = Student.GetCollection();
            this.listBox.ItemsSource = collection;
            this.listBox.ItemTemplate = (DataTemplate)FindResource("studentTemplate");
             //   GetDataTemplate(typeof(School), "IsChecked","StudentID","StudentName");

            //this.propertyDescriptorCollection =
            //    TypeDescriptor.GetProperties(collection[0]);
            //this.isCheckedPropertyDescriptor =
            //    this.propertyDescriptorCollection["IsChecked"];
            //this.textPropertyDescriptor =
            //    this.propertyDescriptorCollection["StudentName"];
            
        }

        private void currentItemButton_Click(object sender, RoutedEventArgs e)
        {
            object item = this.listBox.SelectedItem; if (item != null) 
            { 
                bool isChecked = (bool)this.isCheckedPropertyDescriptor.GetValue(item); 
                string text = (string)this.textPropertyDescriptor.GetValue(item); 
                MessageBox.Show(string.Format("{0}, {1}", isChecked, text)); 
            }
        }

        /*
         <DataTemplate DataType="{x:Type local:Person}">
            <TextBlock>
                    <TextBlock Text="{Binding Path=Name}" />
                    <TextBlock Text="{Binding Path=Phone}" />
                    <TextBlock Text="{Binding Path=Age}" />
                    <TextBlock Text="{Binding Path=Male, Converter={StaticResource maleStringConverter}}" />                       
            </TextBlock>
        </DataTemplate>
        //--------------------------------------------------------
        <DataTemplate DataType="{x:Type local:School}">
            <StackPanel Orientation="Orientation.Horizontal">
                    <CheckBox Width="20d" VerticalAlignment="Center"  
                                IsChecked="{Binding Path=IsChecked}" /> 
                    <TextBlock Text = {Binding Path="StudentName"}/>
            </StackPanel>
        </DataTemplate>
         */
        private DataTemplate GetDataTemplate(Type itemType, string isCheckedPropertyName, string PropertyId, string textPropertyName) 
        {
            DataTemplate dataTemplate = new DataTemplate();
            
            dataTemplate.DataType = itemType;  //클래스명
            
            FrameworkElementFactory stackPanelFrameworkElementFactory = new FrameworkElementFactory(typeof(StackPanel)); 
            stackPanelFrameworkElementFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory checkBoxFrameworkElementFactory = new FrameworkElementFactory(typeof(CheckBox));
            checkBoxFrameworkElementFactory.SetValue(CheckBox.WidthProperty, 20d);
            checkBoxFrameworkElementFactory.SetValue(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center);
            checkBoxFrameworkElementFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding(isCheckedPropertyName));
            stackPanelFrameworkElementFactory.AppendChild(checkBoxFrameworkElementFactory);

            FrameworkElementFactory textBlockIdFrameworkElementFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockIdFrameworkElementFactory.SetBinding(TextBlock.TextProperty, new Binding(PropertyId));
            stackPanelFrameworkElementFactory.AppendChild(textBlockIdFrameworkElementFactory);

            FrameworkElementFactory textBlockFrameworkElementFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFrameworkElementFactory.SetBinding(TextBlock.TextProperty, new Binding(textPropertyName));
            stackPanelFrameworkElementFactory.AppendChild(textBlockFrameworkElementFactory);

            dataTemplate.VisualTree = stackPanelFrameworkElementFactory;

            return dataTemplate;
        }        
    }
}
