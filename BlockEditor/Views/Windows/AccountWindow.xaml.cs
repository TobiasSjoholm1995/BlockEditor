using BlockEditor.Helpers;
using BlockEditor.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BlockEditor.Views.Windows
{

    public partial class AccountWindow : Window
    {

        private string UserName { get; set; }
        private string Password { get; set; }
        private string UserDomain { get; set; }

        public AccountWindow()
        {
            InitializeComponent();
            SetComboBoxUsers();
            UpdateButtons();

            OpenWindows.Add(this);
        }

        private void SetComboBoxUsers()
        {
            cbUsers.Items.Clear();
            cbDomain.Items.Clear();

            cbDomain.Items.Add(Domain.Pr2Hub);
            cbDomain.Items.Add(Domain.Trapwork);
            cbDomain.SelectedItem = null;

            foreach(var user in Users.AllUsers.OrderBy(u => u.Name))
            {
                if(user == null || !user.IsValid())
                    continue;

                var item = new ComboBoxItem();
                item.Content = user.Name + " (" + user.Domain +")";
                item.Tag = user;

                cbUsers.Items.Add(item);
            }

            var index = GetCurrentUserIndex();

            if (cbUsers.SelectedIndex != index)
            {
                cbUsers.SelectedIndex = index;
            }
        }

        private int GetCurrentUserIndex()
        {
            ComboBoxItem item = null;
            int notFound = -1;

            if(Users.Current == null || !Users.Current.IsValid())
                return notFound;

            foreach(var x in cbUsers.Items)
            {
                if(!(x is ComboBoxItem i))
                    continue;

                if(!(i.Tag is User user))
                        continue;

                if(string.Equals(user.Name, Users.Current.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(user.Domain, Users.Current.Domain, StringComparison.InvariantCultureIgnoreCase))
                    item = i;
            }

            if(item == null)
                return notFound;

            return cbUsers.Items.IndexOf(item);
        }

        private void UpdateButtons()
        {
            BtnOk.IsEnabled = IsOK();
            btnLogout.IsEnabled = !string.IsNullOrWhiteSpace(cbUsers.Text);
            ErrorTextbox.Text = string.Empty;

            if(Users.IsLoggedIn() || Users.AllUsers.Any())
            {
                CurrentUserPanel.Visibility = Visibility.Visible;
            }
            else
            {
                CurrentUserPanel.Visibility = Visibility.Collapsed;
            }
        }

        private bool IsOK()
        {
            return !string.IsNullOrWhiteSpace(UserName)
               && !string.IsNullOrEmpty(Password)
               && !string.IsNullOrWhiteSpace(UserDomain);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (new TempCursor(Cursors.Wait))
                {
                    Mouse.OverrideCursor = Cursors.Wait;

                    var success = Users.Login(UserName, Password, UserDomain, out var errorMsg);

                    UpdateButtons();

                    if (success)
                    {
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        ErrorTextbox.Text = errorMsg;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Password_TextChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordTextbox.Password;
            UpdateButtons();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserName = NameTextbox.Text;
            UpdateButtons();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Users.Remove(Users.Current);
            SetComboBoxUsers();
            Users.Logout();
            UpdateButtons();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextbox.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenWindows.Remove(this);
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbUsers.SelectedItem as ComboBoxItem;

            if(item?.Tag == null)
                return;

            var user = item.Tag as User;

            if(user == null || !user.IsValid())
                return;

            Users.Add(user.Name, user.Token, user.Domain);
            UpdateButtons();
            btnLogout.IsEnabled = true;
        }

        private void cbDomain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbDomain.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(item))
                return;
            
            UserDomain = item;
            UpdateButtons();
        }
    }
}
