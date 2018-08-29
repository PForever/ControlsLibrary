using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLibrary
{
    public partial class TabForm : Form
    {
        private BackgroundWorker backgroundWorker;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar progressBar;

        Button _button { get; set; }

        public TabForm()
        {
            InitializeComponent();
            backgroundWorker.RunWorkerAsync("someInfo");
        }

        #region BackgroundWorker
        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            BeginInvoke((Action) setButton);
            _button = new Button { Text = "0", Size = new Size(100, 200) };
            int i = 0;
            while(true)
            {
                Task.Delay(100).Wait();
                lock (this)
                {
                    if (i > 100) break;
                    BeginInvoke((Action)(() => { lock (this) { if (i <= 100) _button.Text = i.ToString(); } }));
                    backgroundWorker.ReportProgress(i);
                    i++;
                }
            }
        }

        private void setButton()
        {
            _button = new Button();
            this._button.AutoEllipsis = true;
            this._button.AutoSize = true;
            this._button.Location = new System.Drawing.Point(273, 204);
            this._button.Name = "0";
            this._button.Size = new System.Drawing.Size(75, 23);
            this._button.TabIndex = 1;
            this._button.Text = "0";
            this._button.UseMnemonic = false;
            this._button.UseVisualStyleBackColor = true;
            this.Controls.Add(this._button);
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBar.Value = e.ProgressPercentage;
            }
            catch { }
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = progressBar.Maximum;
            backgroundWorker.Dispose();
            statusLabel.Text = "Ready";
            progressBar.Visible = false;
        }
        #endregion
        #region Init
        private void InitializeComponent()
        {
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnRunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 424);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(845, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(50, 17);
            this.statusLabel.Text = "Loading";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // TabForm
            // 
            this.ClientSize = new System.Drawing.Size(845, 446);
            this.Controls.Add(this.statusStrip);
            this.Name = "TabForm";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}
