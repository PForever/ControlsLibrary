using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.Factories.Concrete.WinForms.Containers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms
{
    public class TableSplitContainer : ISplitContainer
    {
        private TableLayoutPanel _table;
        private int _relativePosition;

        public TableSplitContainer(TableLayoutPanel table)
        {
            _table = table;
            InitializeComponent();
        }
        public void Dispose()
        {
            _table?.Dispose();
        }

        public object Control => _table;
        public string Name
        {
            get => _table.Name;
            set => _table.Name = value;
        }
        public Point Location { get => _table.Location; set => _table.Location = value; }
        public bool Visible { get => _table.Visible; set => _table.Visible = value; }
        public int Width { get => _table.Width; set => _table.Width = value; }
        public int Height { get => _table.Height; set => _table.Height = value; }
        public void InitializeComponent()
        {
            Controls = new ControlList(_table.Controls);
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    _table.LayoutSettings.ColumnCount = 2;
                    _table.LayoutSettings.RowCount = 1;

                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                    _table.RowStyles.Add(new RowStyle(SizeType.Percent));
                    _table.RowStyles.Add(new RowStyle(SizeType.Percent));
                    break;

                case Orientation.Vertical:

                    _table.LayoutSettings.RowCount = 2;
                    _table.LayoutSettings.ColumnCount = 1;
                    _table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                    break;
            }
        }

        public IControlList Controls { get; set; }
        public Orientation Orientation { get; set; }
        public IPanel Panel1
        {
            get => (IPanel) (Controls.Count == 0 ? null : Controls[0]);
            set
            {
                if (Panel1 != null) Controls.Remove(Panel1);
                Controls.Add(value);
                _table.Controls.Add((Panel)value.Control, 0, 0);

            }
        }

        public IPanel Panel2
        {
            get => (IPanel)(Controls.Count <= 1 ? null : Controls[1]);
            set
            {

                if (Panel2 != null) Controls.Remove(Panel2);
                Controls.Add(value);
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        _table.Controls.Add((Panel)value.Control, 0, 1);
                    break;
                    case Orientation.Vertical:
                        _table.Controls.Add((Panel)value.Control, 1, 0);
                    break;
                }
            }
        }

        public int RelativePosition
        {
            get => _relativePosition;
            set
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        _table.RowStyles[0].Height = value;
                        _table.RowStyles[1].Height = 100 - value;
                        break;
                    case Orientation.Vertical:
                        _table.ColumnStyles[0].Width = value;
                        _table.ColumnStyles[1].Width = 100 - value;
                        break;
                }
                _relativePosition = value;
            }
        }
    }
}