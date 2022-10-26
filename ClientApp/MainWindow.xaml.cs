using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog o = new Microsoft.Win32.OpenFileDialog();
            if (o.ShowDialog() == true)
            {
                string text = File.ReadAllText(o.FileName);
                richTB.Document.Blocks.Clear();
                richTB.Document.Blocks.Add(new Paragraph(new Run(text)));
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            richTB.Copy();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            richTB.Cut();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            richTB.Paste();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            richTB.SelectAll();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            richTB.Cut();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTB.FontFamily = new FontFamily(dlg.Font.Name);
                richTB.FontSize = dlg.Font.Size * 98.0 / 72.0;
                richTB.FontWeight = dlg.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                richTB.FontStyle = dlg.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTB.Foreground = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B)); ;
            }
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            richTB.Copy();
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            richTB.Cut();
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            richTB.Paste();
        }

        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            richTB.SelectAll();
        }

        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            richTB.Cut();
        }
    }
}

//List<int> size;
//public MainWindow()
//{
//    InitializeComponent();
//    size = new List<int>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
//    size_comboBox.ItemsSource = size;
//    size_comboBox.SelectedValue = 11;
//}

//private void Bolt(object sender, RoutedEventArgs e)
//{
//    if (richtxt.Selection.Text == "")
//    {
//        richtxt.SelectAll();
//    }
//    if ((FontWeight)richtxt.Selection.GetPropertyValue(Inline.FontWeightProperty) == FontWeights.Bold)
//    {
//        richtxt.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
//    }
//    else
//    {
//        richtxt.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
//    }
//}

//private void Italic(object sender, RoutedEventArgs e)
//{
//    if (richtxt.Selection.Text == "")
//    {
//        richtxt.SelectAll();
//    }
//    if ((FontStyle)richtxt.Selection.GetPropertyValue(Inline.FontStyleProperty) == FontStyles.Italic)
//    {
//        richtxt.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
//    }
//    else
//    {
//        richtxt.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
//    }
//}

//private void Underline(object sender, RoutedEventArgs e)
//{
//    if (richtxt.Selection.Text == "")
//    {
//        richtxt.SelectAll();
//    }

//    if (richtxt.Selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
//    {
//        richtxt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
//    }
//    else
//    {
//        richtxt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
//    }
//}
//private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
//{
//   if(size_textBox.Text!="0"&&size_textBox.Text!=""&&size_textBox.Text!=null)
//    {
//        if (richtxt.Selection.Text == "")
//        {
//            richtxt.SelectAll();
//        }
//        richtxt.Selection.ApplyPropertyValue(Inline.FontSizeProperty, size_textBox.Text);

//    }
//}