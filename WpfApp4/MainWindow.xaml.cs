using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        // Создание команды для "Масштаб" и "Строка состояния"
        public static readonly RoutedCommand ZoomCommand = new RoutedCommand();
        public static readonly RoutedCommand StatusBarCommand = new RoutedCommand();

        private string currentFilePath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            // Привязка горячих клавиш для меню
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.New, Key.N, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.Open, Key.O, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.Save, Key.S, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.SaveAs, Key.F12, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.Close, Key.Q, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.Copy, Key.C, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.Paste, Key.V, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(ApplicationCommands.Cut, Key.X, ModifierKeys.Control));

            // Привязка команд
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewFile_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenFile_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveFile_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsFile_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, Exit_Click)); // Закрытие
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, Copy_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, Paste_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, Cut_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, About_Click)); // О программе

            // Привязка собственных команд для "Масштаб" и "Строка состояния"
            CommandBindings.Add(new CommandBinding(ZoomCommand, ToggleZoom_Click));
            CommandBindings.Add(new CommandBinding(StatusBarCommand, ToggleStatusBar_Click));
        }

        // Обработчики команд
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            TextBoxEditor.Clear();
            currentFilePath = string.Empty;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                currentFilePath = openFileDialog.FileName;
                string text = File.ReadAllText(currentFilePath);
                TextBoxEditor.Text = text;
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAsFile_Click(sender, e);
            }
            else
            {
                File.WriteAllText(currentFilePath, TextBoxEditor.Text);
            }
        }

        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                currentFilePath = saveFileDialog.FileName;
                File.WriteAllText(currentFilePath, TextBoxEditor.Text);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Правка
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxEditor.SelectedText))
            {
                Clipboard.SetText(TextBoxEditor.SelectedText);
            }
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                TextBoxEditor.Paste();
            }
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxEditor.SelectedText))
            {
                Clipboard.SetText(TextBoxEditor.SelectedText);
                TextBoxEditor.SelectedText = string.Empty;
            }
        }

        // Формат
        private void ToggleWordWrap_Click(object sender, RoutedEventArgs e)
        {
            TextBoxEditor.TextWrapping = TextBoxEditor.TextWrapping == TextWrapping.Wrap ? TextWrapping.NoWrap : TextWrapping.Wrap;
        }

        // Вид
        private void ToggleZoom_Click(object sender, RoutedEventArgs e)
        {
            // Реализация изменения масштаба
            MessageBox.Show("Масштаб изменен");
        }

        private void ToggleStatusBar_Click(object sender, RoutedEventArgs e)
        {
            StatusBar.Visibility = StatusBar.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        // Справка
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Приложение 'Блокнот',\nРазработал Ямчук Тимур.", "О программе");
        }

        private void TextBoxEditor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
