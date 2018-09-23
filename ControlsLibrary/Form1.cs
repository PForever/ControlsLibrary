using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlsLibrary.Factories.Concrete.WinForms.WinHelp;

namespace ControlsLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BubblingFromParent();

            this.MouseMove += OnMouseMove;
            tabView1.MouseMove += OnTabViewMouseMove;
            tabView1.Controls[0].MouseMove += OnTabCollectionMouseMove;
            tabView1.Controls[1].MouseMove += OnViewCollectionMouseMove;
            tabView1.Controls[1].Controls[0].MouseMove += OnTabContentMouseMove;
            this.MouseMove += OnMouseMove2;
            //this.TunnelingFromParent();
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
