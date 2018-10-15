using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories.Concrete.WinForms.WinHelp;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    partial class TabCollectionBase
    {
        protected sealed class TabCollectionState
        {
            private const int FixedPoint = 0;

            public Orientation Orientation { get; set; }

            public TabCollectionState(Orientation orientation)
            {
                Orientation = orientation;
            }

            public void OnSurfacing(IControlList controls, int from, int to, int currentTabLent, int indent)
            {
                int distention = (currentTabLent + indent) * from;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        for (int i = from; i <= to; i++, distention += currentTabLent + indent)
                        {
                            ChangeLocationWidth((ITabPanel)controls[i], distention);
                        }
                        break;
                    case Orientation.Vertical:
                        for (int i = from; i <= to; i++, distention += currentTabLent + indent)
                        {
                            ChangeLocationHeight((ITabPanel)controls[i], distention);
                        }
                        break;
                }
            }

            public void OnSetStartPosition(int index, ITabPanel item, int currentTabLent, int indent)
            {
                int distention = (currentTabLent + indent) * index;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        SetStartLocationWidth(item, distention);
                        break;
                    case Orientation.Vertical:
                        SetStartLocationHeight(item, distention);
                        break;
                }
            }


            private void SetStartLocationWidth(ITabPanel control, int distention)
            {
                control.Location = new Point(distention, FixedPoint);
            }
            private void SetStartLocationHeight(ITabPanel control, int distention)
            {
                control.Location = new Point(FixedPoint, distention);
            }

            public void OnSetPosition(int index, ITabPanel item, int currentTabLent, int indent)
            {
                int distention = (currentTabLent + indent) * index;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        ChangeLocationWidth(item, distention);
                        break;
                    case Orientation.Vertical:
                        ChangeLocationHeight(item, distention);
                        break;
                }
            }

            private void ChangeLocationWidth(ITabPanel control, int distention)
            {
                Point point = new Point(distention, FixedPoint);
                control.ChangeLocation(point);
            }
            private void ChangeLocationHeight(ITabPanel control, int distention)
            {
                Point point = new Point(FixedPoint, distention);
                control.ChangeLocation(point);
            }

            public int ControllerLen(int width, int height)
            {
                return Orientation == Orientation.Horizontal ? width : height; 
            }

            internal void OnRender(ITabPanel panel, int currentTabLen, int indent, int index)
            {
                Point point;
                int curPosition = index * (currentTabLen + indent);
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        point = new Point(curPosition, FixedPoint);
                        panel.ChangeLocation(point);
                        break;
                    case Orientation.Vertical:
                        point = new Point(FixedPoint, curPosition);
                        panel.ChangeLocation(point);
                        break;
                }
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
                            Point point = new Point(curPosition, FixedPoint);
                            panel.ChangeLocation(point);
                            curPosition += (panel.Width = currentTabLen) + intend;
                        }

                        break;
                    case Orientation.Vertical:
                        foreach (ITabPanel panel in controls.Cast<ITabPanel>())
                        {
                            Point point = new Point(FixedPoint, curPosition);
                            panel.ChangeLocation(point);
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
                        tab.Location = new Point(tab.Location.X + (argsX - tab.ClickPosition.X), FixedPoint); ;
                        break;
                    case Orientation.Vertical:
                        tab.Location = new Point(FixedPoint, tab.Location.Y + (argsY - tab.ClickPosition.Y));
                        break;
                }
            }

            public double GetPosition(Point location)
            {
                return Orientation == Orientation.Horizontal
                    ? location.X
                    : location.Y;
            }

            public int OnUpdateThreshold(int width, int height, int maxTabWidth, float percent)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        return (int) (percent * width / maxTabWidth);
                    case Orientation.Vertical:
                        return (int) (percent * height / maxTabWidth);
                }
                throw new WtfException();
            }
        }
    }
}
