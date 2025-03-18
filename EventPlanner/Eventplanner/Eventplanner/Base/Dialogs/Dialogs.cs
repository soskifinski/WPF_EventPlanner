using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Eventplanner.GUI.Base.Dialogs
{
    static class Dialogs
    {
        public enum LZMessageBoxButton
        {
            OK,
            OKCancel,
            YesNo,
            SaveCancel,
            DeleteCancel,
            SwapTabCancel,
            CloseWindowCancel,
        }

        public static MessageBoxResult ShowMessageBox(string message, string title, LZMessageBoxButton button)
        {
            var win = new MessageBoxWindow();
            win.Title = title;
            win.MessageContent = message;
            win.BackgroudColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF8497B0")); //DefaultDarkBrush


            if (button == LZMessageBoxButton.YesNo)
            {
                win.AcceptButtonText = "Ja";
                win.DeclineButtonText = "Nein";
                win.CancelButton.Visibility = Visibility.Hidden;
                win.DeleteButton.Visibility = Visibility.Hidden;
                win.SaveButton.Visibility = Visibility.Hidden;
            }
            else if (button == LZMessageBoxButton.OK)
            {
                win.AcceptButtonText = "OK";
                win.DeclineButton.Visibility = Visibility.Hidden;
                win.CancelButton.Visibility = Visibility.Hidden;
                win.DeleteButton.Visibility = Visibility.Hidden;
                win.SaveButton.Visibility = Visibility.Hidden;
            }
            else if (button == LZMessageBoxButton.OKCancel)
            {
                win.CancelButtonText = "Abbrechen";
                win.AcceptButtonText = "OK";
                win.DeclineButton.Visibility = Visibility.Hidden;
                win.DeleteButton.Visibility = Visibility.Hidden;
                win.SaveButton.Visibility = Visibility.Hidden;
            }
            else if (button == LZMessageBoxButton.SaveCancel)
            {
                win.AcceptButtonText = "Speichern";
                win.CancelButtonText = "Abbrechen";
                win.AcceptButton.Visibility = Visibility.Hidden;
                win.DeclineButton.Visibility = Visibility.Hidden;
                win.DeleteButton.Visibility = Visibility.Hidden;
            }
            else if (button == LZMessageBoxButton.DeleteCancel)
            {
                win.DeleteButtonText = "Löschen";
                win.CancelButtonText = "Abbrechen";
                win.DeclineButton.Visibility = Visibility.Hidden;
                win.AcceptButton.Visibility = Visibility.Hidden;
                win.SaveButton.Visibility = Visibility.Hidden;
            }
            else if (button == LZMessageBoxButton.SwapTabCancel)
            {
                win.AcceptButtonText = "Wechseln";
                win.CancelButtonText = "Abbrechen";
                win.DeclineButton.Visibility = Visibility.Hidden;
                win.DeleteButton.Visibility = Visibility.Hidden;
                win.SaveButton.Visibility = Visibility.Hidden;
            }
            else if (button == LZMessageBoxButton.CloseWindowCancel)
            {
                win.AcceptButtonText = "Beenden";
                win.CancelButtonText = "Abbrechen";
                win.DeclineButton.Visibility = Visibility.Hidden;
                win.DeleteButton.Visibility = Visibility.Hidden;
                win.SaveButton.Visibility = Visibility.Hidden;
            }
            win.ShowDialog();
            return win.Result;
        }
    }
}
