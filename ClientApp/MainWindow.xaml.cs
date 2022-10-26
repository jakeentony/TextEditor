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

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> size;
        public MainWindow()
        {
            InitializeComponent();
            size = new List<int>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            size_comboBox.ItemsSource = size;
            size_comboBox.SelectedValue = 11;
        }

        private void Bolt(object sender, RoutedEventArgs e)
        {
            if (richtxt.Selection.Text == "")
            {
                richtxt.SelectAll();
            }
            if ((FontWeight)richtxt.Selection.GetPropertyValue(Inline.FontWeightProperty) == FontWeights.Bold)
            {
                richtxt.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                richtxt.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Italic(object sender, RoutedEventArgs e)
        {
            if (richtxt.Selection.Text == "")
            {
                richtxt.SelectAll();
            }
            if ((FontStyle)richtxt.Selection.GetPropertyValue(Inline.FontStyleProperty) == FontStyles.Italic)
            {
                richtxt.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                richtxt.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void Underline(object sender, RoutedEventArgs e)
        {
            if (richtxt.Selection.Text == "")
            {
                richtxt.SelectAll();
            }

            if (richtxt.Selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
            {
                richtxt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                richtxt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           if(size_textBox.Text!="0"&&size_textBox.Text!=""&&size_textBox.Text!=null)
            {
                if (richtxt.Selection.Text == "")
                {
                    richtxt.SelectAll();
                }
                richtxt.Selection.ApplyPropertyValue(Inline.FontSizeProperty, size_textBox.Text);

            }
        }
    }
}
