using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using _0419_바인딩.데이터바인딩;

namespace _0419_바인딩
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public void Application_Statup(object sender, StartupEventArgs e)
        {
            Window1 Window = new Window1();
            Window.Show();
        }
    }
}
