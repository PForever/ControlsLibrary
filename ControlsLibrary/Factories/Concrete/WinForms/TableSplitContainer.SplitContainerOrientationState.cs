using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms
{
    public partial class TableSplitContainer
    {
        protected sealed class SplitContainerOrientationState
        {
            public Orientation Orientation { get; set; }

            public SplitContainerOrientationState(Orientation orientation)
            {
                Orientation = orientation;
            }
            public void OnInitializeComponent(TableLayoutPanel table, int relativePosition)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); //100%
                        table.RowStyles.Add(new RowStyle(SizeType.Absolute, relativePosition));
                        table.RowStyles.Add(new RowStyle(SizeType.Percent));

                        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                        break;

                    case Orientation.Vertical:

                        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, relativePosition));
                        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));

                        table.RowStyles.Add(new RowStyle(SizeType.Percent));
                        break;
                }
            }

            public void OnOrientationChanged(TableLayoutPanel table, int relativePosition)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        table.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100F); //100%
                        table.RowStyles[0] = new RowStyle(SizeType.Absolute, relativePosition);
                        table.RowStyles[1] = new RowStyle(SizeType.Percent);
                        break;

                    case Orientation.Vertical:

                        table.RowStyles[0] = new RowStyle(SizeType.Percent, 100F);
                        table.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, relativePosition);
                        table.ColumnStyles[1] = new ColumnStyle(SizeType.Percent);
                        break;
                }
            }

            public void OnPanel2Changed(TableLayoutPanel table, IPanel value)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        table.Controls.Add((Panel)value.Control, 0, 1);
                        break;
                    case Orientation.Vertical:
                        table.Controls.Add((Panel)value.Control, 1, 0);
                        break;
                }
            }

            public void OnRelatePositionChanged(TableLayoutPanel table, int value)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        table.RowStyles[0].Height = value;
                        break;
                    case Orientation.Vertical:
                        table.ColumnStyles[0].Width = value;
                        break;
                }
            }
        }
    }
}