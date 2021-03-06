﻿using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events.Handlers;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories.Concrete.WinForms.Containers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms
{
    public partial class TableSplitContainer : ISplitContainer
    {
        private readonly TableLayoutPanel _table;
        private int _relativePosition;
        private SplitContainerOrientationState _stateManager;
        private readonly IFactory _factory;

        public TableSplitContainer(TableLayoutPanel table, IFactory factory)
        {
            _factory = factory;
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
            Controls = new ControlList(_table.Controls, _factory);
            _stateManager = new SplitContainerOrientationState(Orientation);
            _stateManager.OnInitializeComponent(_table, RelativePosition);
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Control)
            //{
            //    switch (e.KeyCode)
            //    {
            //        case Keys.T:
            //            AddNewTab.Invoke(this, new TabEventArgs(null));
            //            break;
            //        case Keys.W:
            //            RemoveSelectedTab.Invoke(this, new TabEventArgs(null));
            //            break;
            //    }
            //}
        }

        public void RemoveSelected()
        {
            RemoveSelectedTab.Invoke(this, new TabEventArgs(null));
        }
        public void AddNew()
        {
            AddNewTab.Invoke(this, new TabEventArgs(null));
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
                _stateManager.Orientation = value;
                _stateManager.OnOrientationChanged(_table, RelativePosition);
                Reload();
            }
        }

        private void Reload()
        {
            Panel2 = Panel2;
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
                _stateManager.OnPanel2Changed(_table, value);
                ((Panel) value.Control).Dock = DockStyle.Fill;
            }
        }

        public int RelativePosition
        {
            get => _relativePosition;
            set
            {
                _stateManager.OnRelatePositionChanged(_table, value);
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