using System;
using System.Media;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatbot
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot = new Chatbot();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SoundPlayer player =
                    new SoundPlayer("welcome.wav");

                player.Play();
            }
            catch
            {
                MessageBox.Show("Voice file not found.");
            }

            AddMessage("Bot",
                "Hello! Welcome to the Cybersecurity Awareness Bot.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessMessage();
            }
        }

        private void ProcessMessage()
        {
            string userMessage = UserInput.Text;

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return;
            }

            AddMessage("You", userMessage);

            string response =
                chatbot.GetResponse(userMessage);

            AddMessage("Bot", response);

            UserInput.Clear();
        }

        private void AddMessage(string sender, string message)
        {
            Paragraph paragraph = new Paragraph();

            Run senderRun = new Run(sender + ": ");

            senderRun.FontWeight = FontWeights.Bold;

            if (sender == "Bot")
            {
                senderRun.Foreground = Brushes.DarkGreen;
            }
            else
            {
                senderRun.Foreground = Brushes.DarkBlue;
            }

            Run messageRun = new Run(message);

            paragraph.Inlines.Add(senderRun);
            paragraph.Inlines.Add(messageRun);

            ChatBox.Document.Blocks.Add(paragraph);

            ChatBox.ScrollToEnd();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ChatBox.Document.Blocks.Clear();
        }
    }
}
