using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ControlsLibrary
{
    public partial class DefaultPage
    {
        private SplitContainer _browseBox;
        private Button _browseButton;
        private TextBox _browseString;
        private void InitializeComponent()
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            Dock = DockStyle.Bottom;
            Width = 200;
            Height = 50;
            _browseBox = new SplitContainer { Name = "BrowseBox", Orientation = Orientation.Vertical, Dock = DockStyle.Fill};
            _browseBox.SplitterDistance = 100;
            _browseButton = new Button{Name = "BrowseButton", Dock = DockStyle.Fill};
            _browseString = new TextBox{Name = "BrowseString", Dock = DockStyle.Fill, Multiline = true};
            _browseButton.Click += OnBrowseButtonClick;
            _browseBox.Panel1.Controls.Add(_browseString);
            _browseBox.Panel2.Controls.Add(_browseButton);
            Controls.Add(_browseBox);
        }

        private Image _image;

        private void OnBrowseButtonClick(object sender, System.EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (!String.IsNullOrEmpty(_browseString.Text))
            {
                dialog.InitialDirectory = new FileInfo(_browseString.Text).DirectoryName;
            }

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _image = new Bitmap(dialog.FileName);
                _browseString.Text = dialog.FileName;
                Parent.BackgroundImageLayout = ImageLayout.None;
                Parent.BackgroundImage = _image;
            }
        }
    }
}