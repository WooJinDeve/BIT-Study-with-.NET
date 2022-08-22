using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace _0419.데이터바인딩
{
    internal class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }

        private int? age;
        public int? Age
        {
            get { return age; }
            set
            {
                age = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));
            }
        }

        private bool? male;
        public bool? Male
        {
            get { return male; }
            set
            {
                male = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Male"));
            }
        }

        public Person() { }
        public Person(string name, string phone, int? age, bool? male)
        {
            Name = name;
            Phone = phone;
            Age = age;
            Male = male;
        }

        public override string ToString()
        {
            return Name + ", " + Phone + ", " + Age + ", " + male;
        }
    }

    //형식변환기
    //UI:string,  Data:bool?
    [ValueConversion(/* 원본 형식 */ typeof(bool), /* 대상 형식 */ typeof(bool))]
    public class MaleToStringConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            bool? male = (bool?)value;
            if (male == null)
                return "";
            else if (male == true)
                return "남자";
            else
                return "여자";
        }

        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                    parameter, System.Globalization.CultureInfo culture)
        {
            string male = (string)value;
            if (male == "")
                return null;
            else if (male == "남자")
                return true;
            else
                return false;
        }
    }

    //형식변환기
    //UI : boo?,  Data:bool?
    [ValueConversion(/* 원본 형식 */ typeof(bool), /* 대상 형식 */ typeof(bool))]
    public class MaleToFemaleConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            bool? male = (bool?)value;
            if (male == null)
                return null;
            else
                return !(bool?)value;
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                parameter, System.Globalization.CultureInfo culture)
        {
            bool? male = (bool?)value;
            if (male == null)
                return null;
            else
                return !(bool?)value;
        }
    }

    //나이 사용자 정의 예외 클래스
    public class AgeValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value,
                    System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "정수를 입력하세요.");
            }
            if (Min <= number && number <= Max)
            {
                // new ValidationResult(true, null) 같다
                return ValidationResult.ValidResult;
            }
            else
            {
                string msg =
                string.Format("나이는 {0}에서 {1}까지 입력 가능합니다.", Min, Max);
                return new ValidationResult(false, msg);
            }
        }
    }
}

