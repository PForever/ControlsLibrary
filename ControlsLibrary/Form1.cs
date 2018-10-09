using System;
using System.Windows.Forms;
using ControlsLibrary.Factories.Concrete.WinForms.WinHelp;

namespace ControlsLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabView1.BubblingFromParent();
            ControlExtensions.BindingConcreteEvents(this, tabView1);
            this.MouseMove += OnMouseMove;
            tabView1.MouseMove += OnTabViewMouseMove;
            tabView1.Controls[0].MouseMove += OnTabCollectionMouseMove;
            tabView1.Controls[1].MouseMove += OnViewCollectionMouseMove;
            tabView1.Controls[1].Controls[0].MouseMove += OnTabContentMouseMove;
            this.MouseMove += OnMouseMove2;
            this.KeyUp += OnKeyUp;
            //this.TunnelingFromParent();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            GC.Collect();
        }

        private void OnMouseMove2(object sender, MouseEventArgs e)
        {
        }

        private void OnTabContentMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnViewCollectionMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnTabCollectionMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnTabViewMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
