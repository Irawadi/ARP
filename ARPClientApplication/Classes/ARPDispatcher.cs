using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ARPClientApplication
{
    public static class ARPDispatcher
    {
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }

                    if (child is Expander)
                    {
                        Expander expander = (Expander)child;
                        StackPanel stackPanel = expander.Content as StackPanel;
                        foreach (T childOfChild in FindVisualChildren<T>(stackPanel))
                        {
                            yield return childOfChild;
                        }
                    }

                    if (child is GroupBox)
                    {
                        GroupBox groupBox = (GroupBox)child;
                        StackPanel stackPanel = groupBox.Content as StackPanel;
                        foreach (T childOfChild in FindVisualChildren<T>(stackPanel))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }
        public static ListView FindListViewByName(DependencyObject depObj, string name)
        {
            ListView listView = new ListView();
            foreach (ListView lv in FindVisualChildren<ListView>(depObj))
            {
                if (lv.Name == name) { listView = lv; }
            }
            return listView;
        }
        public static Label FindLabelByName(DependencyObject depObj, string name)
        {
            return FindVisualChildren<Label>(depObj).Where(l => l.Name==name).First();
        }
        public static TextBox FindTextBoxByName(DependencyObject depObj, string name)
        {
            TextBox textBox = new TextBox();
            foreach (TextBox tb in FindVisualChildren<TextBox>(depObj))
            {
                if (tb.Name == name) { textBox = tb; }
            }
            return textBox;
        }
        public static Button FindButtonByName(DependencyObject depObj, string name)
        {
            Button button = new Button();
            foreach (Button b in FindVisualChildren<Button>(depObj))
            {
                if (b.Name == name) { button = b; }
            }
            return button;
        }
        public static StackPanel FindStackPanelByName(DependencyObject depObj, string name)
        {
            StackPanel stackPanel = new StackPanel();
            foreach (StackPanel s in FindVisualChildren<StackPanel>(depObj))
            {
                if (s.Name == name) { stackPanel = s; }
            }
            return stackPanel;
        }
        public static RichTextBox FindRichTextBoxByName(DependencyObject depObj, string name)
        {
            RichTextBox richTextBox = new RichTextBox();
            foreach (RichTextBox r in FindVisualChildren<RichTextBox>(depObj))
            {
                if (r.Name == name) { richTextBox = r; }
            }
            return richTextBox;
        }
        public static DatePicker FindDatePickerByName(DependencyObject depObj, string name)
        {
            DatePicker datePicker = new DatePicker();
            foreach (DatePicker dp in FindVisualChildren<DatePicker>(depObj))
            {
                if (dp.Name == name) { datePicker = dp; }
            }
            return datePicker;
        }
        public static RadioButton FindRadioButtonByGroupNameAndContent(DependencyObject depObj, string groupName, string content)
        {
            RadioButton radioButton = new RadioButton();
            foreach (RadioButton rb in FindVisualChildren<RadioButton>(depObj))
            {
                if (rb.GroupName == groupName && rb.Content.ToString() == content) { radioButton = rb; }
            }
            return radioButton;
        }
        public static BasicColumn FindBasicColumnByName(DependencyObject depObj, string name)
        {
            BasicColumn basicColumn = new BasicColumn();
            foreach (BasicColumn bc in FindVisualChildren<BasicColumn>(depObj))
            {
                if (bc.Name == name) { basicColumn = bc; }
            }
            return basicColumn;
        }
        public static void SetDatePickersLanguage(DependencyObject depObj)
        {
            CultureInfo cultureInfo = new CultureInfo("ru-RU");
            foreach (DatePicker d in FindVisualChildren<DatePicker>(depObj))
            {
                d.Language = XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag);
            }
        }
        public static void AssignEHButtonClick(DependencyObject depObj, RoutedEventHandler del) { foreach (Button b in FindVisualChildren<Button>(depObj)) { b.Click += del; } }
        public static void AssignEHRadioButtonClick(DependencyObject depObj, RoutedEventHandler del) { foreach (RadioButton rb in FindVisualChildren<RadioButton>(depObj)) { rb.Click += del; } }
        public static void AssignEHTextBoxLostFocus(DependencyObject depObj, RoutedEventHandler del) { foreach (TextBox t in FindVisualChildren<TextBox>(depObj)) { t.LostFocus += del; } }
        public static void AssignEHPasswordBoxLostFocus(DependencyObject depObj, RoutedEventHandler del) { foreach (PasswordBox p in FindVisualChildren<PasswordBox>(depObj)) { p.LostFocus += del; } }
        public static void AssignEHTextBoxTextChanged(DependencyObject depObj, TextChangedEventHandler del) { foreach (TextBox t in FindVisualChildren<TextBox>(depObj)) { t.TextChanged += del; } }
        public static void AssignEHPasswordBoxPasswordChanged(DependencyObject depObj, RoutedEventHandler del) { foreach (PasswordBox p in FindVisualChildren<PasswordBox>(depObj)) { p.PasswordChanged += del; } }
        public static void AssignEHListViewSelectionChanged(DependencyObject depObj, SelectionChangedEventHandler del) { foreach (ListView l in FindVisualChildren<ListView>(depObj)) { l.SelectionChanged += del; } }
        public static void AssignEHDatePickerSelectedDateChanged(DependencyObject depObj, EventHandler<SelectionChangedEventArgs> del) { foreach (DatePicker d in FindVisualChildren<DatePicker>(depObj)) { d.SelectedDateChanged += del; } }
    }
}
