using ASA.Communication;
using ASA.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;

namespace ARPClientApplication
{
    public static class ARPDispatcherRealizer
    {
        public static void ARPButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender; string n = b.Name; string ev = "Click";
            string result = ReportEvent(n, ev);
            ExecuteInterfaceActions(result);
        }
        public static async void ARPRadioButtonClick(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender; string n = rb.GroupName; string v = rb.Content.ToString();
            await Task.Run(() =>
            {
                WriteSessionVariable(n, v);
            });
        }
        public static async void ARPTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender; string n = t.Name; string v = t.Text;
            await Task.Run(() =>
            {
                WriteSessionVariable(n, v);
            });
        }
        public static async void ARPPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox p = (PasswordBox)sender; string n = p.Name; string v = ASAEncoder.Encrypt(p.Password);
            await Task.Run(() =>
            {
                WriteSessionVariable(n, v);
            });
        }
        public static void ARPListViewSelectionChanged(object sender, RoutedEventArgs e)
        {
            ListView l = (ListView)sender; string n = l.Name; string v = l.SelectedIndex.ToString();
            WriteSessionVariable(n, v);
            string result = ReportEvent(n, "SelectionChanged");
            ExecuteInterfaceActions(result);
        }
        public static async void ARPDatePickerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker d = (DatePicker)sender; string n = d.Name; string v = d.Text;
            await Task.Run(() =>
            {
                WriteSessionVariable(n, v);
            });
        }
        public static void WriteSessionVariable(string n, string v)
        {
            string query;
            string result;
            ARPMessageEAV.A a = new ARPMessageEAV.A() { N = n, V = v };
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ARPMessageEAV.A));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, a);
                    query = sww.ToString(); // Your XML
                }
            }
            try
            {
                using (ServiceSpeaker serviceSpeaker = new ServiceSpeaker(ServiceSpeaker.ConstructionMode.fromString, Properties.Settings.Default.ARPServiceAddress))
                {
                    result = serviceSpeaker.Do(query);
                }
            }
            catch (Exception)
            {
                result = "Nothing";
            }
        }
        public static string ReportEvent(string n, string ev)
        {
            string query;
            string result;
            ARPMessageEAA.A a = new ARPMessageEAA.A() { N = n, E = ev };
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ARPMessageEAA.A));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, a);
                    query = sww.ToString(); // Your XML
                }
            }
            try
            {
                using (ServiceSpeaker serviceSpeaker = new ServiceSpeaker(ServiceSpeaker.ConstructionMode.fromString, Properties.Settings.Default.ARPServiceAddress))
                {
                    result = serviceSpeaker.Do(query);
                }
            }
            catch (Exception)
            {
                result = "Nothing";
            }
            return result;
        }
        public static void ExecuteInterfaceActions(string result)
        {
            try
            {
                if (result != "Nothing" && result != "")
                {
                    Interface.R r;
                    XmlSerializer formatter = new XmlSerializer(typeof(Interface.R));
                    using (TextReader textReader = new StringReader(result))
                    {
                        r = (Interface.R)formatter.Deserialize(textReader);
                    }
                    Interface.IEx.ExecuteInstructions(r);
                }
            }
            catch (Exception) { }
        }
    }
}
