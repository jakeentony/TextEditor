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
            if (ri.Selection.Text == "")
            {
                ri.SelectAll();
            }
            if ((FontWeight)ri.Selection.GetPropertyValue(Inline.FontWeightProperty) == FontWeights.Bold)
            {
                ri.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                ri.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Italic(object sender, RoutedEventArgs e)
        {
            if (ri.Selection.Text == "")
            {
                ri.SelectAll();
            }
            if ((FontStyle)ri.Selection.GetPropertyValue(Inline.FontStyleProperty) == FontStyles.Italic)
            {
                ri.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                ri.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void Underline(object sender, RoutedEventArgs e)
        {
            if (ri.Selection.Text == "")
            {
                ri.SelectAll();
            }

            if (ri.Selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
            {
                ri.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                ri.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           if(size_textBox.Text!="0"&&size_textBox.Text!=""&&size_textBox.Text!=null)
            {
                if (ri.Selection.Text == "")
                {
                    ri.SelectAll();
                }
                ri.Selection.ApplyPropertyValue(Inline.FontSizeProperty, size_textBox.Text);

            }
        }
    }
}
