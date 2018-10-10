using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class ViewCollectionLogic : ViewCollectionBase
    {
        private ITabContent _current;
        protected override BufferedPage Buffer { get; set; }
        protected override IPanel Panel { get; }

        public ViewCollectionLogic(IPanel pnl)
        {
            Panel = pnl;
            InitializeComponent();
        }

        protected override void InitializeComponent()
        {
            Buffer = new BufferedPage();
        }

        public override TimeSpan TimeOut { get; set; }
        public override int Capacity { get; set; }

        public override ITabContent Current
        {
            get => _current;
            set
            {
                if (value == null)
                {
                    _current = null;
                    return;
                }
                if (!Buffer.Pages.Contains(value))
                {
                    value.Fetch = true;
                    Buffer.Add(value);
                    Controls.Add(value);
                }
                value.Visible = true;
                if (_current != null)
                {
                    Buffer.Start(_current);
                    _current.Visible = false;
                }
                _current = value;

            }
        }

        public override void Remove(ITabContent tabPanelTabContent)
        {
            Controls.Remove(tabPanelTabContent);
            Buffer.Remove(tabPanelTabContent);
            if (Current == tabPanelTabContent) Current = null;
        }
    }
}