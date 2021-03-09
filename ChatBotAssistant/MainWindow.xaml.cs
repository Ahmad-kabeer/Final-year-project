using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace ChatBotAssistant
{
    public partial class MainWindow : Window
    {
        string SameSession = "";
        bool a =true;

        public MainWindow()
        {
            InitializeComponent();
            BotReply("hi! My name is kabeer bot. I can answer about banking.i.e.balance enquiry. How can I help you ?");
        }

        private void InputMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void InputMessage_KeyDown(object sender, KeyEventArgs e)
        {

            if (Keyboard.IsKeyDown(Key.Enter))
            {
                MainWork();
            }
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWork();
        }
        
        private void MainWork()
        {
            if (String.IsNullOrEmpty(InputMessage.Text))
            {

            }
            else
            {
                string input_message = "Me:- " + InputMessage.Text + "        " + "(" + DateTime.Now.ToString() + ")";
                CustomerReply(input_message);
            }
        }

        private void BotReply(string sms)
        {
            ScrollViewer myScrollViewer = new ScrollViewer();
            myScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel sp = new StackPanel();
            var textBlock = new TextBlock();
            var bc = new BrushConverter();
            textBlock.Text = sms ;
            textBlock.FontWeight = FontWeights.UltraBold;
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.VerticalAlignment = VerticalAlignment.Bottom;
            textBlock.FontSize = 12;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.Background = (Brush)bc.ConvertFrom("#ffffff");
            textBlock.Foreground = Brushes.Navy;
            textBlock.FontFamily = new FontFamily("Helvetica Neue");
            textBlock.FontStretch = FontStretches.UltraExpanded;
            textBlock.FontStyle = FontStyles.Normal;
            textBlock.FontWeight = FontWeights.Normal;
            textBlock.LineHeight = Double.NaN;
            textBlock.Padding = new Thickness(7, 7, 7, 7);
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Typography.NumeralStyle = FontNumeralStyle.OldStyle;
            textBlock.Typography.SlashedZero = true;
            sp.Children.Add(textBlock);
            Responce.Children.Add(sp);
            myScrollViewer.Content = textBlock;
            InputMessage.Clear();
        }

        private void CustomerReply(string sms)
        {
            StackPanel sp = new StackPanel();
            var textBlock = new TextBlock();
            var bc = new BrushConverter();
            textBlock.Text = sms;
            textBlock.FontWeight = FontWeights.UltraBold;
            textBlock.HorizontalAlignment = HorizontalAlignment.Right;
            textBlock.VerticalAlignment = VerticalAlignment.Bottom;
            textBlock.FontSize = 12;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.Background = (Brush)bc.ConvertFrom("#e0fcc4");
            textBlock.Foreground = Brushes.Navy;
            textBlock.FontFamily = new FontFamily("Helvetica Neue");
            textBlock.FontStretch = FontStretches.UltraExpanded;
            textBlock.FontStyle = FontStyles.Normal;
            textBlock.FontWeight = FontWeights.Normal;
            textBlock.LineHeight = Double.NaN;
            textBlock.Padding = new Thickness(7, 7, 7, 7);
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Typography.NumeralStyle = FontNumeralStyle.OldStyle;
            textBlock.Typography.SlashedZero = true;
            sp.Children.Add(textBlock);
            Responce.Children.Add(sp);
            var a = InputMessage.Text;
            Communicate(a);
        }

        void Communicate(string str)
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: "GXXg-PHXZXyExBoDgjQlOjtR1_wDUBAm7Fwc2E3Z2tth");
            var service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("https://api.us-south.assistant.watson.cloud.ibm.com/instances/23faa32e-b756-4462-b9b1-c8557bf82fd4");
            string AssId = "98ffc80e-8caa-457b-8fc3-84bde0c20ac7";
            var session = service.CreateSession(AssId);
            if (a == true)
            {
                SameSession = session.Result.SessionId;
                a = false;
            }
            MessageInput mi = new MessageInput();
            mi.Text = str;
            var _response = service.Message(AssId, SameSession, mi, null);
            foreach (IBM.Watson.Assistant.v2.Model.RuntimeResponseGeneric reply in _response.Result.Output.Generic)
            {
                BotReply(reply.Text);
            }
            InputMessage.Clear();
        }
    }
}
