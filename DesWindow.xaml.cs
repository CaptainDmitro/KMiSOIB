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
using System.Windows.Shapes;

namespace KMiSOIB
{
    /// <summary>
    /// Логика взаимодействия для DesWindow.xaml
    /// </summary>
    public partial class DesWindow : Window
    {
        public DesWindow()
        {
            InitializeComponent();
        }

        private void Proceed_Button_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTB.Text;
            string key = KeyTB.Text;
            Des des = new Des(message, key);
        }
    }
}
