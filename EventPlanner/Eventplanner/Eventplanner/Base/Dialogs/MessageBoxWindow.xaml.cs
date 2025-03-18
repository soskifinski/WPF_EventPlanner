using System.Windows;
using System.Windows.Media;

namespace Eventplanner.GUI.Base.Dialogs
{
    /// <summary>
    /// Interaktionslogik für MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty IconTemplateProperty =
           DependencyProperty.Register(nameof(IconTemplate), typeof(DataTemplate), typeof(MessageBoxWindow), new PropertyMetadata(null));

        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        public static readonly DependencyProperty BackgroudColorProperty =
        DependencyProperty.Register("BackgroudColor", typeof(SolidColorBrush), typeof(MessageBoxWindow), new PropertyMetadata(null));
        public SolidColorBrush BackgroudColor
        {
            get { return (SolidColorBrush)GetValue(BackgroudColorProperty); }
            set { SetValue(BackgroudColorProperty, value); }
        }

        public static readonly DependencyProperty MessageContentProperty =
           DependencyProperty.Register("MessageContent", typeof(object), typeof(MessageBoxWindow));

        public object MessageContent
        {
            get { return (object)GetValue(MessageContentProperty); }
            set { SetValue(MessageContentProperty, value); }
        }

        private MessageBoxResult _messageBoxResult;
        public MessageBoxResult Result
        {
            get { return _messageBoxResult; }
            set { _messageBoxResult = value; }
        }

        public string AcceptButtonText
        {
            get { return AcceptButton.Content.ToString(); }
            set { AcceptButton.Content = value; }
        }

        public string DeclineButtonText
        {
            get { return DeclineButton.Content.ToString(); }
            set { DeclineButton.Content = value; }
        }
        public string CancelButtonText
        {
            get { return CancelButton.Content.ToString(); }
            set { CancelButton.Content = value; }
        }

        public string DeleteButtonText
        {
            get { return DeleteButton.Content.ToString(); }
            set { DeleteButton.Content = value; }
        }

        public string SaveButtonText
        {
            get { return SaveButton.Content.ToString(); }
            set { SaveButton.Content = value; }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Result = MessageBoxResult.Yes;
            Close();
        }
        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Result = MessageBoxResult.No;
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = null;
            Result = MessageBoxResult.Cancel;
            Close();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Result = MessageBoxResult.Yes;
            Close();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Result = MessageBoxResult.Yes;
            Close();
        }
    }
}
