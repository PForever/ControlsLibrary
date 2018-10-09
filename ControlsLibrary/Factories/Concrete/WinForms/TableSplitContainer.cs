using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
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
                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); //100%
                    _table.RowStyles.Add(new RowStyle(SizeType.Absolute, RelativePosition));
                    _table.RowStyles.Add(new RowStyle(SizeType.Percent));

                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                    break;

                case Orientation.Vertical:

                    _table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, RelativePosition));
                    _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));

                    _table.RowStyles.Add(new RowStyle(SizeType.Percent));
                    break;
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.T:
                        AddNewTab.Invoke(this, new TabEventArgs(null));
                        break;
                    case Keys.W:
                        RemoveSelectedTab.Invoke(this, new TabEventArgs(null));
                        break;
                }
            }
        }

        public IControlList Controls { get; set; }

        public Orientation _orientation;
        public Orientation Orientation
        {
            get => _orientation;
            set
            {
                _orientation = 
                Panel1.Orientation = value;

                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        _table.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100F); //100%
                        _table.RowStyles[0] = new RowStyle(SizeType.Absolute, RelativePosition);
                        _table.RowStyles[1] = new RowStyle(SizeType.Percent);
                        break;

                    case Orientation.Vertical:

                        _table.RowStyles[0] = new RowStyle(SizeType.Percent, 100F);
                        _table.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, RelativePosition);
                        _table.ColumnStyles[1] = new ColumnStyle(SizeType.Percent);
                        break;
                }

                Panel2 = Panel2;
            }
        }

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
                ((Panel) value.Control).Dock = DockStyle.Fill;
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
                        break;
                    case Orientation.Vertical:
                        _table.ColumnStyles[0].Width = value;
                        break;
                }
                _relativePosition = value;
            }
        }

        private event TabEventHandler AddNewTab;
        private event TabEventHandler RemoveSelectedTab;

        event TabEventHandler ISplitContainer.RemoveSelectedTab
        {
            add { this.RemoveSelectedTab += value; }
            remove { this.RemoveSelectedTab -= value; }
        }

        event TabEventHandler ISplitContainer.AddNewTab
        {
            add { this.AddNewTab += value; }
            remove { this.AddNewTab -= value; }
        }
    }
}