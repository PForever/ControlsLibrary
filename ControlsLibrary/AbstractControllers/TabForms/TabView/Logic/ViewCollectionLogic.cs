using System;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Logic
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

        public override void Remove(ITabContent tabPanelTabContent, bool disposing = true)
        {
            Buffer.Remove(tabPanelTabContent);
            if (Current == tabPanelTabContent) Current = null;

            Controls.Remove(tabPanelTabContent, disposing);
        }
    }
}