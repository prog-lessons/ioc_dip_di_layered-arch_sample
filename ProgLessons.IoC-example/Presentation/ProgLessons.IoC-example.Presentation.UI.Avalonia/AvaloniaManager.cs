using System;
using System.Collections.Generic;
using Aval = Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProgLessons.IoC_example.App.Services.UI;

namespace ProgLessons.IoC_example.Presentation.UI.Avalonia
{
    public class AvaloniaManager : UserInterfaceManager
    {
        static public Window Window { get; set; }

        StackPanel mainPanel = Window.Find<StackPanel>("panel");
        Aval.Layout.HorizontalAlignment defHorizAlign = Aval.Layout.HorizontalAlignment.Center;
        Aval.Layout.VerticalAlignment defVertAlign = Aval.Layout.VerticalAlignment.Top;

        Control FocusedControl;
        TextBlock txtFixedInfo;

        public AvaloniaManager()
        {
            Window.Activated += WindowActivated;
            this.RequestAppStop = delegate { Window.IsEnabled = false; };
            this.SingleThreaded = false;
        }

        public override void BreakLine()
        {
            var txtBlock = new TextBlock();
            var x = txtBlock;
            x.Height = 10;
            x.HorizontalAlignment = Aval.Layout.HorizontalAlignment.Stretch;
            mainPanel.Children.Add(x);
        }

        public override void OutputInfo(string text, InfoCategory infoCategory)
        {
            if (infoCategory == InfoCategory.Title) return;

            TextBlock x = null;
            var useSameTxtInfo = false;

            if (infoCategory == InfoCategory.FixedResponse)
            {
                if (this.txtFixedInfo == null)
                    this.txtFixedInfo = new TextBlock();
                else
                    useSameTxtInfo = true;

                x = this.txtFixedInfo;
            }
            else
            {
                var txtBlock = new TextBlock();
                x = txtBlock;
            }

            if (infoCategory == InfoCategory.SubTitle)
            {
                x.FontSize = 18;
                x.Foreground = Aval.Media.Brushes.DarkCyan;
            }

            if (infoCategory == InfoCategory.Default)
            {
                x.FontSize = 13;
                x.Foreground = Aval.Media.Brushes.Black;
            }

            if (infoCategory == InfoCategory.Response || infoCategory == InfoCategory.FixedResponse)
            {
                x.FontSize = 14;
                x.Foreground = Aval.Media.Brushes.Black;
                x.FontWeight = Aval.Media.FontWeight.Bold;
            }

            x.Text = text;
            x.HorizontalAlignment = defHorizAlign;
            x.VerticalAlignment = defVertAlign;

            if (!useSameTxtInfo) mainPanel.Children.Add(x);
        }

        public override void WaitUserInput(List<UIField> inputFields, Action inputPerformedFunc)
        {
            if (this.inputFields != null) return;

            base.WaitUserInput(inputFields, inputPerformedFunc);

            BreakLine();

            var firstElement = true;

            foreach (UIField f in inputFields)
            {
                BreakLine();
                BreakLine();
                OutputInfo(f.Label + ": ", InfoCategory.Default);

                var txbUIField = new TextBox();
                {
                    var x = txbUIField;
                    x.Name = "txb" + f.Label;
                    x.HorizontalAlignment = Aval.Layout.HorizontalAlignment.Center;
                    x.VerticalAlignment = Aval.Layout.VerticalAlignment.Top;
                    x.Width = 100;
                    x.Margin = new Aval.Thickness(5, 15, 0, 0);
                    x.Text = "";
                    if (f.HideChars) x.PasswordChar = '*';
                    x.LostFocus += InputFieldLostFocus;
                    mainPanel.Children.Add(x);

                    if (firstElement)
                    {
                        FocusedControl = x;
                        firstElement = false;
                    }
                }
            }

            var btnSumitFields = new Button();
            {
                var x = btnSumitFields;
                x.HorizontalAlignment = defHorizAlign;
                x.VerticalAlignment = defVertAlign;
                x.Width = 160;
                x.Height = 50;
                x.FontWeight = Aval.Media.FontWeight.Bold;
                x.Margin = new Aval.Thickness(5, 35, 0, 15);
                x.BorderThickness = new Aval.Thickness(5, 5, 5, 5);
                x.Content = "Submit";
                x.Click += btnSubmit_Click;
                mainPanel.Children.Add(x);
            }
        }

        private void WindowActivated(object sender, EventArgs e)
        {
            if (Window.IsEnabled) (FocusedControl as TextBox).Focus();
        }

        private void InputFieldLostFocus(object sender, RoutedEventArgs e)
        {
            var uiField = inputFields.Find(f => (sender as TextBox).Name == "txb" + f.Label);
            uiField.Value = (sender as TextBox).Text;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIField f in base.inputFields)
            {
                if (String.IsNullOrEmpty(f.Value.Trim()))
                {
                    BreakLine();
                    OutputInfo("All fields required.", InfoCategory.FixedResponse);
                    return;
                }
            }

            inputPerformedFunc.Invoke();
            (sender as Button).Focus();
        }
    }

}