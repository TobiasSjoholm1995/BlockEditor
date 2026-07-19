using BlockEditor.Helpers;
using BlockEditor.Models;
using BlockEditor.Utils;
using BlockEditor.Views.Controls;
using LevelModel.Models.Components;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace BlockEditor.Views.Windows
{
    public partial class BlockOptionWindow : ToolWindow
    {

        private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private SimpleBlock _block;
        private readonly Map _map;
        private readonly string _mapItemOptions;
        private Action _refreshGui;

        public bool StartNavigation { get; set; }
        public bool OKButtonPressed { get; set; } = false;

        public string GetBlockOption => _block.Options;

        public BlockOptionWindow(Map map, MyPoint? index, Action refreshGui)
        {
            InitializeComponent();

            if (map == null || index == null)
                return;

            _refreshGui = refreshGui;
            _map = map;
            _block = GetBlock(index.Value);
            _mapItemOptions = ItemBlockOptionsControl.GetOptions(map.Level.Items);

            Init(index.Value);
            MyUtil.SetPopUpWindowPosition(this);
        }

        public BlockOptionWindow(Map map, int blockID, Action refreshGui)
        {
            InitializeComponent();

            if (map == null)
                return;

            _refreshGui = refreshGui;
            _map = map;
            _block = new SimpleBlock(blockID);
            _mapItemOptions = ItemBlockOptionsControl.GetOptions(map.Level.Items);

            Init(null);
            MyUtil.SetPopUpWindowPosition(this);
        }

        private SimpleBlock GetBlock(MyPoint index)
        {
            var block = _map.Blocks.StartBlocks.GetBlock(index);

            if (block.IsEmpty())
                block = _map.Blocks.GetBlock(index);

            return block;
        }

        private void Init(MyPoint? index)
        {
            if (_block.IsEmpty())
            {
                if (index != null)
                {
                    lblPosX.Text = index.Value.X.ToString(_culture);
                    lblPosY.Text = index.Value.Y.ToString(_culture);
                    okButtonPanel.Visibility = Visibility.Collapsed;

                }
                else
                {   
                    okButtonPanel.Visibility = Visibility.Visible;
                    gridPosition.Visibility = Visibility.Collapsed;
                }

                tbBlockOption.Text = String.Empty;
            }
            else
            {
                BlockImage.Source = BlockImages.GetImageBlock(BlockImages.BlockSize.Zoom160, _block.ID)?.Image;

                if (_block.Position == null)
                {
                    gridPosition.Visibility = Visibility.Collapsed;
                    okButtonPanel.Visibility = Visibility.Visible;

                }
                else
                {
                    lblPosX.Text = _block.Position.Value.X.ToString(_culture);
                    lblPosY.Text = _block.Position.Value.Y.ToString(_culture);
                    okButtonPanel.Visibility = Visibility.Collapsed;
                }


                if (_block.IsItem())
                {
                    var c = new ItemBlockOptionsControl();
                    c.AllowCustomItemCount = true;

                    if (string.IsNullOrWhiteSpace(_block.Options))
                        c.UpdateCheckboxItems(_map.Level.Items);
                    else
                        c.SetBlockOptions(_block.Options);

                    c.Margin = new Thickness(5, 0, 10, 20);
                    c.OnBlockOptionChanged += OnOptionsChanged;
                    OptionPanel.Children.Add(c);
                }
                else if (_block.ID == Block.TELEPORT)
                {
                    var panel = new StackPanel();
                    panel.Orientation = Orientation.Horizontal;
                    panel.VerticalAlignment = VerticalAlignment.Center;
                    panel.Margin = new Thickness(10, 10, 10, 5);

                    var label = new TextBlock();
                    label.Text = "Color: ";
                    label.VerticalAlignment = VerticalAlignment.Center;
                    label.FontSize = 14;
                    label.Margin = new Thickness(0, 0, 0, 15);
                    label.FontWeight = FontWeights.SemiBold;

                    var c = new ColorPickerControl();
                    c.VerticalAlignment = VerticalAlignment.Center;
                    c.SetColor(_block.Options);
                    c.Margin = new Thickness(5, 0, 5, 15);
                    c.OnNewColor += OnNewColor;
                    c.Height = 30;

                    var b = new WhiteButton("Navigate to connected blocks");
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.VerticalAlignment = VerticalAlignment.Center;
                    b.OnClick += btnNavigate_Click;
                    b.Width = 210;
                    b.Height = 26;
                    b.Margin = new Thickness(10, 0, 0, 10);

                    panel.Children.Add(label);
                    panel.Children.Add(c);
                    OptionPanel.Children.Add(panel);
                    OptionPanel.Children.Add(b);
                }
                else if (_block.ID == Block.HAPPY_BLOCK || _block.ID == Block.SAD_BLOCK)
                {
                    tbBlockOption.Text = "Stats Change:";
                    var c = new StatsBlockControl(_block.ID == Block.SAD_BLOCK);
                    c.SetBlockOptions(_block.Options);
                    c.Margin = new Thickness(10, 0, 10, 20);
                    c.OnBlockOptionChanged += OnOptionsChanged;
                    OptionPanel.Children.Add(c);
                }
                else if (_block.ID == Block.CUSTOM_STATS)
                {
                    tbBlockOption.Text = string.Empty;
                    var c = new CustomStatsControl();
                    c.SetBlockOptions(_block.Options);
                    c.Margin = new Thickness(10, 0, 10, 20);
                    c.OnBlockOptionChanged += OnOptionsChanged;
                    OptionPanel.Children.Add(c);
                }
                else if(_block.ID == Block.BASIC_PILLAR)
                {
                    var split = _block.Options?.Split(':');
                    var style = split.FirstOrDefault();
                    var color = split.Length > 1 ? split.LastOrDefault() : "";

                    var panel1 = new StackPanel();
                    panel1.Orientation = Orientation.Horizontal;
                    panel1.VerticalAlignment = VerticalAlignment.Center;
                    panel1.Margin = new Thickness(10, 10, 10, 5);

                    var label1 = new TextBlock();
                    label1.Text = "Style: ";
                    label1.VerticalAlignment = VerticalAlignment.Center;
                    label1.FontSize = 14;
                    label1.FontWeight = FontWeights.SemiBold;
                    label1.Margin = new Thickness(0,0,9,0);

                    var cb = new ComboBox();
                    cb.Items.Add("Pillar 1");
                    cb.Items.Add("Pillar 2");
                    cb.Items.Add("Pillar 3");
                    cb.SelectedIndex = int.TryParse(style, out var pi) && pi >= 1 && pi <= 3 ? pi - 1 : 0;
                    cb.VerticalAlignment = VerticalAlignment.Center;
                    cb.SelectionChanged += PillarStyleChanged;
                    cb.Width = 100;
                    cb.BorderThickness = new Thickness(0);

                    panel1.Children.Add(label1);
                    panel1.Children.Add(cb);
                    OptionPanel.Children.Add(panel1);

                    var panel2 = new StackPanel();
                    panel2.Orientation = Orientation.Horizontal;
                    panel2.VerticalAlignment = VerticalAlignment.Center;
                    panel2.Margin = new Thickness(10, 0, 10, 0);

                    var label2 = new TextBlock();
                    label2.Text = "Color: ";
                    label2.VerticalAlignment = VerticalAlignment.Center;
                    label2.FontSize = 14;
                    label2.Margin = new Thickness(0, 0, 0, 15);
                    label2.FontWeight = FontWeights.SemiBold;

                    var c = new ColorPickerControl();
                    c.VerticalAlignment = VerticalAlignment.Center;
                    c.SetColor(color);
                    c.Margin = new Thickness(5, 0, 5, 15);
                    c.OnNewColor += OnNewColor;
                    c.Height = 30;

                    panel2.Children.Add(label2);
                    panel2.Children.Add(c);
                    OptionPanel.Children.Add(panel2);
                }
                else if(_block.ID == Block.MOVE_BLOCK)
                {
                    tbBlockOption.Text = string.Empty;
                    var c = new MoveBLockOptions();
                    c.SetBlockOptions(_block.Options);
                    c.Margin = new Thickness(10, 0, 10, 20);
                    c.OnBlockOptionChanged += OnOptionsChanged;
                    OptionPanel.Children.Add(c);
                }
                else
                {
                    var c = new BlockOptionsControl();
                    c.SetBlockOptions(_block.Options);
                    c.Margin = new Thickness(10, 0, 10, 20);
                    c.OnBlockOptionChanged += OnOptionsChanged;
                    OptionPanel.Children.Add(c);
                }
            }
        }

        private void PillarStyleChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;

            if(cb == null)
                return;

            var splits = _block.Options?.Split(':');
            var style  = (cb.SelectedIndex + 1).ToString(CultureInfo.InvariantCulture);
            var color  = splits.Length > 1 ? splits.LastOrDefault() : "15592939";
            var text   = style + ":" + color;

            OnOptionsChanged(text);
            _refreshGui?.Invoke();
        }

        private void btnConnected_Click()
        {
            var count = 0;

            using (new TempCursor(Cursors.Wait))
            {
                foreach (var b in _map.Blocks.GetBlocks())
                {
                    if (b.ID != Block.TELEPORT)
                        continue;

                    if (!string.Equals(b.Options, _block.Options, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    count++;
                }
            }

            if (count <= 1)
                MessageUtil.ShowInfo($"There is no connected teleport blocks.");
            else if (count == 2)
                MessageUtil.ShowInfo($"There is 1 connected teleport block.");
            else
                MessageUtil.ShowInfo($"There are {count - 1} connected teleport blocks.");
        }

        private void btnNavigate_Click()
        {
            StartNavigation = true;
            Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void OnOptionsChanged(string text)
        {
            if (text == null || _block.IsEmpty())
                return;

            var ignoreOption = _block.IsItem() && string.Equals(text, _mapItemOptions, StringComparison.CurrentCultureIgnoreCase);
            _block.Options   = ignoreOption ? string.Empty : text;;

            if(_block.Position == null)
                return;

            using (new TempOverwrite(_map.Blocks, true))
                _map.Blocks.Add(_block);
        }

        private void OnNewColor(string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(text))
                    text = Convert.ToInt32(text, 16).ToString();
            }
            catch
            {
                MessageUtil.ShowError("Failed to convert color to PR2 block option format.");
            }

            if(_block.ID == Block.BASIC_PILLAR)
            {
                var style = _block.Options?.Split(':').FirstOrDefault() ?? "1";
                text      = style + ":" + text;
            }

            OnOptionsChanged(text);
            _refreshGui?.Invoke();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            OKButtonPressed = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
