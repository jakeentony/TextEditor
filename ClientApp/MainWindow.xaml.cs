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
using iTextSharp.text.pdf;
using iTextSharp.text;

using PrintDialog = System.Windows.Controls.PrintDialog;
using DataFormats = System.Windows.DataFormats;
using Paragraph = System.Windows.Documents.Paragraph;
using MessageBox = System.Windows.MessageBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using TabItem = System.Windows.Controls.TabItem;
using Button = System.Windows.Controls.Button;
using RichTextBox = System.Windows.Controls.RichTextBox;
using iTextSharp.text.pdf.parser;
using System.Windows.Media.TextFormatting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Data//add items, path, 
    {
        public RichTextBox richTB;
        public string textSize;
        public string path;
        public Data()
        {
            richTB = new RichTextBox();
        }
        public void Bolt_Click()
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
        public void Italic_Click()
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
        public void Underline_Click()
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
       
        public void Print_Click()
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                print.PrintDocument(((IDocumentPaginatorSource)richTB.Document).DocumentPaginator, "print");
            }
        }        

        public void Copy_Click()
        {
            richTB.Copy();
        }

        public void Cut_Click()
        {
            richTB.Cut();
        }

        public void Paste_Click()
        {
            richTB.Paste();
        }

        public void SelectAll_Click()
        {
            richTB.SelectAll();
        }
        public void Font_Click()
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textSize = ((int)dlg.Font.Size).ToString();
                richTB.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, new FontFamily(dlg.Font.Name));
                richTB.Selection.ApplyPropertyValue(Inline.FontWeightProperty, dlg.Font.Bold ? FontWeights.Bold : FontWeights.Regular);
                richTB.Selection.ApplyPropertyValue(Inline.FontStyleProperty, dlg.Font.Italic ? FontStyles.Italic : FontStyles.Normal);
            }
        }
        public void Color_Click()
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
        public void Delete_Click()
        {
            if (richTB.Selection.Text == "")
            {
                richTB.SelectAll();
            }
            richTB.Selection.Text = "";
        }        

        public void SaveAs_Click()
        {
            TextRange range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
            Microsoft.Win32.SaveFileDialog save = new();
            save.Filter = "rich text file, text file, portable document format(*.rtf;*.txt;*.pdf)|*.rtf;*.txt;*.pdf|text file(*.txt)|*.txt|rich text file(*.rtf)|*.rtf|portable document format(*.pdf)|*.pdf";
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
                        Document doc = new Document();
                        PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));
                        doc.Open();
                        doc.Add(new iTextSharp.text.Paragraph(range.Text));
                        doc.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        public void Save_Click()
        {
            string text = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd).Text;
            File.WriteAllText(path, text);
        }
        public void rightTextAlign()
        {
            if (richTB.Selection.Text == "")
                richTB.SelectAll();
            richTB.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }
        public void centerTextAlign()
        {
            if (richTB.Selection.Text == "")
                richTB.SelectAll();
            richTB.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
        }
        public void leftTextAlign()
        {
            if (richTB.Selection.Text == "")
                richTB.SelectAll();
            richTB.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
        }
        public void justifyTextAlign()
        {
            if (richTB.Selection.Text == "")
                richTB.SelectAll();
            richTB.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
        }
    }

    public partial class MainWindow : System.Windows.Window
    {
        List<Data> data;
        Data data2;
        List<int> size;
        SolidColorBrush colorBrushWhite;
        SolidColorBrush colorBrushBlack;
        private List<TabItem> _tabItems;
        private TabItem _tabAdd;
        public MainWindow()
        {
            try
            {
                data = new List<Data>();
                Data data3 = new Data();
                data.Add(data3);
                data2 = data3;
                
                InitializeComponent();

                // initialize tabItem array
                _tabItems = new List<TabItem>();

                // add a tabItem with + in header 
                _tabAdd = new TabItem();
                _tabAdd.Header = "+";
                // tabAdd.MouseLeftButtonUp += new MouseButtonEventHandler(tabAdd_MouseLeftButtonUp);

                _tabItems.Add(_tabAdd);

                // add first tab
                this.AddTabItem();

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                tabDynamic.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            colorBrushWhite = new SolidColorBrush();
            colorBrushWhite.Color = Colors.White;
            colorBrushBlack = new SolidColorBrush();
            colorBrushBlack.Color = Colors.Black;
            size = new List<int>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            size_comboBox.ItemsSource = size;
            size_comboBox.SelectedValue = 11;
        }

        private void richTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = new TextRange(data2.richTB.Document.ContentStart, data2.richTB.Document.ContentEnd).Text;
            sm.Text = (str.Count(s => s != '\r' && s != '\n')).ToString();
            var tmp = str.Split(' ', '.', ',', '-', '\n', '\t', '\r');
            wr.Text = tmp.Count(s => s != "").ToString();
            tmp = str.Split('\n');
            ln.Text = (tmp.Length - 1).ToString();
        }

        private TabItem AddTabItem()
        {
            int count = _tabItems.Count;

            // create new tab item
            TabItem tab = new TabItem();

            tab.Header = string.Format("Tab {0}", count);
            tab.Name = string.Format("tab{0}", count);
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            // add controls to tab item, this case I added just a textbox
            Data data3 = new Data();
            data.Add(data3);
            data2 = data3;
            
            tab.Content = data3.richTB;

            // insert tab item right before the last (+) tab item
            _tabItems.Insert(count - 1, tab);

            return tab;
        }
        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = tabDynamic.SelectedItem as TabItem;
            if (tab == null) return;

            if (tab.Equals(_tabAdd))
            {
                // clear tab control binding
                tabDynamic.DataContext = null;

                TabItem newTab = this.AddTabItem();

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                // select newly added tab item
                tabDynamic.SelectedItem = newTab;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();

            var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItem tab = item as TabItem;

            if (tab != null)
            {
                if (_tabItems.Count < 3)
                {
                    MessageBox.Show("Cannot remove last tab.");
                }
                else if (MessageBox.Show(string.Format("Are you sure you want to remove the tab '{0}'?", tab.Header.ToString()),
                    "Remove Tab", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // get selected tab
                    TabItem selectedTab = tabDynamic.SelectedItem as TabItem;

                    // clear tab control binding
                    tabDynamic.DataContext = null;

                    _tabItems.Remove(tab);

                    // bind tab control
                    tabDynamic.DataContext = _tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = _tabItems[0];
                    }
                    tabDynamic.SelectedItem = selectedTab;
                }
            }
        } 

        public void FirstOpen()
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Filter = "rich text file, text file, portable document format(*.rtf;*.txt;*.pdf)|*.rtf;*.txt;*.pdf|text file(*.txt)|*.txt|rich text file(*.rtf)|*.rtf|portable document format(*.pdf)|*.pdf";
            if (open.ShowDialog() == true)
            {
                string text = File.ReadAllText(open.FileName);
                data2.richTB.Document.Blocks.Clear();
                data2.richTB.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(text)));
            }
            data2.path = open.FileName;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Filter = "rich text file, text file, portable document format(*.rtf;*.txt;*.pdf)|*.rtf;*.txt;*.pdf|text file(*.txt)|*.txt|rich text file(*.rtf)|*.rtf|portable document format(*.pdf)|*.pdf";
            if (open.ShowDialog() == true)
            {
                string text = File.ReadAllText(open.FileName);
                data2.richTB.Document.Blocks.Clear();
                data2.richTB.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(text)));
            }
            data2.path = open.FileName;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void darkTheme(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"dark.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushWhite;
        }
        private void lightTheme(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"light.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushBlack;
        }
        private void greenTheme(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"green.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushWhite;
        }
        private void blueTheme(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"blue.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushBlack;
        } 
        private void redTheme(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"red.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushWhite;
        }

        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"light.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushBlack;
        }

        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"dark.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = App.LoadComponent(uri) as ResourceDictionary;
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            size_comboBox.Foreground = colorBrushWhite;
        }

        private void aboutAs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developers:\nSofiia Stepaniuk\nVitalii Marchuk\nYevhenii Parkonny\n\n2022.11.07", "About as",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Bolt_Click(object sender, RoutedEventArgs e)
        {
            data2.Bolt_Click();
        }
        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            data2.Italic_Click();
        }
        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            data2.Underline_Click();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {            
            var tb = (System.Windows.Controls.TextBox)e.OriginalSource;
            data2.textSize = tb.Text;
            if (data2.textSize != "0" && data2.textSize != "" && data2.textSize != null)
            {
                if (data2.richTB.Selection.Text == "")
                {
                    data2.richTB.SelectAll();
                }
                data2.richTB.Selection.ApplyPropertyValue(Inline.FontSizeProperty, data2.textSize);
            }
        }
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            data2.Print_Click();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            data2.Copy_Click();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            data2.Cut_Click();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            data2.Paste_Click();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            data2.SelectAll_Click();
        }
        private void Font_Click(object sender, RoutedEventArgs e)
        {
            data2.Font_Click();
        }
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            data2.Color_Click();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            data2.Delete_Click();
        }



        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            data2.SaveAs_Click();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            data2.Save_Click();
        }
        private void rightTextAlign(object sender, RoutedEventArgs e)
        {
            data2.rightTextAlign();
        }
        private void centerTextAlign(object sender, RoutedEventArgs e)
        {
            data2.centerTextAlign();
        }
        private void leftTextAlign(object sender, RoutedEventArgs e)
        {
            data2.leftTextAlign();
        }
        private void justifyTextAlign(object sender, RoutedEventArgs e)
        {
            data2.justifyTextAlign();
        }
    }
}