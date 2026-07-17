using BlockEditor.Utils;
using BlockEditor.Views.Windows;
using LevelModel.Models.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.Design.AxImporter;

namespace BlockEditor.Views.Controls
{

    public partial class ItemBlockOptionsControl : UserControl
    {

        private readonly List<CheckBox> _checkboxes = new List<CheckBox>();

        public event Action<string> OnBlockOptionChanged;
        public event Action<List<Item>> OnItemChanged;


        public ItemBlockOptionsControl()
        {
            InitializeComponent();
            Init();
        }


        public void SetColumnCount(int count)
        {
            ItemGrid.Columns = count;
        }

        public void UpdateCheckboxItems(List<Item> items)
        {
            foreach (var cb in _checkboxes)
            {
                if (cb == null)
                    continue;

                if (!(cb.Tag is Item item))
                    continue;

                cb.IsChecked = items.Any(i => i.ID == item.ID);
            }
        }

        public void SetBlockOptions(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            var separators = new[] { ',', '-' };

            var items = input.Split(separators);

            foreach(var s in items)
            {
                if (string.IsNullOrWhiteSpace(s))
                    continue;

                var options    = s.Split(':');
                var id_text    = options.FirstOrDefault();
                var count_text = options.LastOrDefault();

                if(string.IsNullOrWhiteSpace(id_text))
                    continue;

                if(!int.TryParse(id_text.Trim(), out var id))
                    continue;

                var cb = _checkboxes.Where(x => x.Tag is Item tag && tag.ID == id).FirstOrDefault();

                if (cb == null)
                    continue;

                cb.IsChecked = true; 

                if(cb.Tag is Item item && !string.IsNullOrWhiteSpace(count_text) && int.TryParse(count_text, out var count))
                    item.Count = count;
            }
        }

        private void Init()
        {
            ItemGrid.Columns = 3;

            foreach (var item in GetDefualtItems())
            {
                var cb = new CheckBox();
                cb.Content = item.Name;
                cb.Tag = item;
                cb.FontSize = 14;
                cb.HorizontalAlignment = HorizontalAlignment.Left;
                cb.Margin = new Thickness(5);
                cb.Checked   += Item_CheckedChange;
                cb.Unchecked += Item_CheckedChange;

                _checkboxes.Add(cb);
                ItemGrid.Children.Add(cb);
            }
        }

        private void CheckItemCount(CheckBox cb)
        {
            if(cb == null)
                return;

            if(cb.IsChecked == false)
                return;

            if(!cb.IsLoaded)
                return;

            var item = cb.Tag as Item;

            if(item == null)
                return;

            if(!Item.SupportCustomCount(item.ID))
                return;

            var input = UserInputWindow.Show("Item Count: (leave empty to apply default) ", item.Name, item.Count == null ? "" : item.Count.ToString(), true);
            var ok    = int.TryParse(input, out var count);

            if(ok)
                item.Count = count;
        }

        public static string GetOptions(IEnumerable<Item> items)
        {
            var builder   = new StringBuilder();
            var separator = "-";
            var first     = true;

            foreach (var item in items)
            {
                if (first)
                    first = false;
                else
                    builder.Append(separator);

                builder.Append(item.ID.ToString(CultureInfo.InvariantCulture));

                if(item.Count != null)
                    builder.Append("-" + item.Count.Value.ToString(CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }
        private void Item_CheckedChange(object sender, RoutedEventArgs e)
        {
            CheckItemCount(sender as CheckBox);

            var items = new List<Item>();

            foreach(var cb in _checkboxes)
            {
                if (cb?.IsChecked == null || cb.IsChecked == false)
                    continue;

                if(!(cb.Tag is Item item))
                    continue;

                items.Add(item);
            }

            OnBlockOptionChanged?.Invoke(GetOptions(items));
            OnItemChanged?.Invoke(items);
        }

        public IEnumerable<Item> GetDefualtItems()
        {
            var minId = 1;
            var maxId = 9;

            for (int i = minId; i <= maxId; i++)
            {
                var item = new Item(i, null);
                
                if (string.IsNullOrWhiteSpace(item.Name))
                    continue;

                yield return item;
            }
        }

    }
}
