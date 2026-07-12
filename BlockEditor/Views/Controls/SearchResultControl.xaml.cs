using BlockEditor.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BlockEditor.Views.Controls
{

    public partial class SearchResultControl : UserControl
    {

        public event Action<string, int> OnSelectedLevel;

        private int _id;
        private string _domain;


        public SearchResultControl(SearchResult result)
        {
            InitializeComponent();

            if (result == null)
                throw new ArgumentNullException("level");

            _id = result.ID;
            _domain = result.Domain;
            btnTitle.Content = result.Title;
            btnTitle.ToolTip = result.GetToolTip();
        }

        private void btnTitle_Click(object sender, RoutedEventArgs e)
        {
            InvokeSelectedLevel();
        }

        public void InvokeSelectedLevel()
        {
            OnSelectedLevel?.Invoke(_domain, _id);
        }

    }
}
