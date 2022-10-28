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
using DataFormats = System.Windows.DataFormats;
using Path = System.IO.Path;
using PrintDialog = System.Windows.Controls.PrintDialog;

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

        private void Bolt_Click(object sender, RoutedEventArgs e)
        {
            if (richTB.Selection.Text == "")
            {
                richTB.SelectAll();
            }
            if ((FontWeight)richTB.Selection.GetPropertyValue(Inline.FontWeightProperty) == FontWeights.Bold)
            {
                richTB.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                richTB.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            if (richTB.Selection.Text == "")
            {
                richTB.SelectAll();
            }
            if ((FontStyle)richTB.Selection.GetPropertyValue(Inline.FontStyleProperty) == FontStyles.Italic)
            {
                richTB.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                richTB.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            if (richTB.Selection.Text == "")
            {
                richTB.SelectAll();
            }

            if (richTB.Selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
            {
                richTB.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                richTB.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (size_textBox.Text != "0" && size_textBox.Text != "" && size_textBox.Text != null)
            {
                if (richTB.Selection.Text == "")
                {
                    richTB.SelectAll();
                }
                richTB.Selection.ApplyPropertyValue(Inline.FontSizeProperty, size_textBox.Text);

            }
        }
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                print.PrintDocument(((IDocumentPaginatorSource)richTB.Document).DocumentPaginator, "print");
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Filter = "rich text file, text file(*.rtf;*.txt)|*.rtf;*.txt|text file(*.txt)|*.txt|rich text file(*.rtf)|*.rtf";
            if (open.ShowDialog() == true)
            {
                richTB.Document.Blocks.Clear();
                switch (System.IO.Path.GetExtension(open.FileName))
                {
                    case ".txt":
                        string text = File.ReadAllText(open.FileName);
                        richTB.Document.Blocks.Add(new Paragraph(new Run(text)));
                        break;
                    case ".rtf":
                        richTB.Selection.Load(new FileStream(open.FileName, FileMode.Open), DataFormats.Rtf);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            richTB.Copy();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            richTB.Cut();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            richTB.Paste();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            richTB.SelectAll();
        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                size_textBox.Text = ((int)dlg.Font.Size).ToString();
                richTB.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, new FontFamily(dlg.Font.Name));
                richTB.Selection.ApplyPropertyValue(Inline.FontWeightProperty, dlg.Font.Bold ? FontWeights.Bold : FontWeights.Regular);
                richTB.Selection.ApplyPropertyValue(Inline.FontStyleProperty, dlg.Font.Italic ? FontStyles.Italic : FontStyles.Normal);
            }
        }
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (richTB.Selection.Text != "")
                {
                    richTB.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B)));
                }
                else
                {
                    richTB.Foreground = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (richTB.Selection.Text == "")
            {
                richTB.SelectAll();
            }
            richTB.Selection.Text = "";
        }

        private void richTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd).Text;
            sm.Text = (str.Count(s=>s!='\r'&&s!='\n')).ToString();
            var tmp = str.Split(' ', '.', ',', '-', '\n', '\t', '\r');
            wr.Text=tmp.Count(s=>s!="").ToString();
            tmp = str.Split('\n');
            ln.Text = (tmp.Length - 1).ToString();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
            Microsoft.Win32.SaveFileDialog save = new();
            save.Filter = "rich text file, text file(*.rtf;*.txt)|*.rtf;*.txt|text file(*.txt)|*.txt|rich text file(*.rtf)|*.rtf|(*.pdf)|*.pdf";
            if (save.ShowDialog() == true)
            {
                switch (System.IO.Path.GetExtension(save.FileName))
                {
                    case ".txt":
                        using (var file = new FileStream(save.FileName, FileMode.OpenOrCreate))
                        {
                            range.Save(file, DataFormats.Text);
                        }
                        break;
                    case ".rtf":
                        using (var file = new FileStream(save.FileName, FileMode.OpenOrCreate))
                        {
                            range.Save(file, DataFormats.Rtf);
                        }
                        break;
                    case ".pdf":
                        File.WriteAllText(Path.Combine(save.FileName), range.Text);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
