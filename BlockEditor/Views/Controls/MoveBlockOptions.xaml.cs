using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlockEditor.Views.Controls
{
    public partial class MoveBLockOptions : UserControl
    {

        public event Action<string> OnBlockOptionChanged;
        private const string _default =  "::1";

        public MoveBLockOptions()
        {
            InitializeComponent();
        }

        public void SetBlockOptions(string input)
        {
            var split = input?.Split(":");

            if (split == null || split.Length != 3)
            {
                tbPattern.Text    = "";
                tbDelay.Text      = "";
                cbLoop.IsChecked  = true;
                cbRigid.IsChecked = false;
            }
            else
            {
                tbPattern.Text = split[0];
                tbDelay.Text = int.TryParse(split[1], out _) ? split[1] : "";

                if (int.TryParse(split[2], out var data))
                {
                    cbLoop.IsChecked  = data % 2 == 1;
                    cbRigid.IsChecked = data % 2 == 0;
                }
                else
                {
                    cbLoop.IsChecked = true;
                    cbRigid.IsChecked = false;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FireOnBlockOptionChanged();
        }

        private void FireOnBlockOptionChanged()
        {
            var options = "";
            var data    = 0;

            options += (tbPattern.Text ?? "") + ":";
            options += (tbDelay.Text   ?? "") + ":";

            if(cbLoop.IsChecked == true) 
                data += 1;

            if(cbRigid.IsChecked == true) 
                data += 2;

            options += data.ToString(CultureInfo.InvariantCulture);

            if(string.Equals(options, _default, StringComparison.InvariantCultureIgnoreCase))
                options = "";

            OnBlockOptionChanged?.Invoke(options);
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            FireOnBlockOptionChanged();
        }
    }
}
