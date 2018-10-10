using System.Drawing;
using System.Linq;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    partial class TabCollectionBase
    {
        protected sealed class TabCollectionState
        {
            public Orientation Orientation { get; set; }

            public TabCollectionState(Orientation orientation)
            {
                Orientation = orientation;
            }

            public void OnSurfacing(IControlList controls, int from, int to, int value)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        for (int i = from; i <= to; i++)
                        {
                            ChangeLocationWidth(controls[i], value);
                        }
                        break;
                    case Orientation.Vertical:
                        for (int i = from; i <= to; i++)
                        {
                            ChangeLocationHeight(controls[i], value);
                        }
                        break;
                }
            }

            public void OnSetPosition(int index, ITabPanel item, int currentTabLent)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        ChangeLocationWidth(item, index * currentTabLent);
                        break;
                    case Orientation.Vertical:
                        ChangeLocationHeight(item, index * currentTabLent);
                        break;
                }
            }

            private void ChangeLocationWidth(IControl control, int width)
            {
                control.Location = new Point(control.Location.X + width, control.Location.Y);
            }
            private void ChangeLocationHeight(IControl control, int height)
            {
                control.Location = new Point(control.Location.X, control.Location.Y + height);
            }

            public int ControllerLen(int width, int height)
            {
                return Orientation == Orientation.Horizontal ? width : height; 
            }

            public void OnInsert(int index, ITabPanel item, int currentTabLen)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        item.Width = currentTabLen;
                        break;
                    case Orientation.Vertical:
                        item.Height = currentTabLen;
                        break;
                }
            }

            public void OnRender(IControlList controls, int currentTabLen, int intend)
            {
                int curPosition = 0;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        foreach (ITabPanel panel in controls.Cast<ITabPanel>())
                        {
                            panel.Location = new Point(curPosition, panel.Location.Y);
                            curPosition += (panel.Width = currentTabLen) + intend;
                        }

                        break;
                    case Orientation.Vertical:
                        foreach (ITabPanel panel in controls.Cast<ITabPanel>())
                        {
                            panel.Location = new Point(panel.Location.X, curPosition);
                            curPosition += (panel.Height = currentTabLen) + intend;
                        }

                        break;
                }
            }
            public void OnCalcNewPosition(int argsX, int argsY, ITabPanel tab)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        tab.Location = new Point(tab.Location.X + (argsX - tab.ClickPosition.X), tab.Location.Y); ;
                        break;
                    case Orientation.Vertical:
                        tab.Location = new Point(tab.Location.X, tab.Location.Y + (argsY - tab.ClickPosition.Y));
                        break;
                }
            }

            public double GetPosition(Point location)
            {
                return Orientation == Orientation.Horizontal
                    ? location.X
                    : location.Y;
            }
        }
    }
}
