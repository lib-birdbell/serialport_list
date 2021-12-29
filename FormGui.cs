// ���� FormGui.cs
using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication
{
    public partial class FormGui : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        [DllImport("user32.dll")]       
        static  extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		private RS232Interface  rs232i = new RS232Interface();
		
        public FormGui()
        {
			// Hide console window
			IntPtr hWnd = FindWindow(null, Console.Title);
			if(hWnd != IntPtr.Zero){
				ShowWindow(hWnd, 0/*SW_HIDE*/);
			}

            InitializeComponent();

			this.MinimizeBox = false;
			this.MaximizeBox = false;
        }

		private void buttonRefresh_Click(object sender, EventArgs e){
			int nLength;

			rs232i.RefreshComList();

			nLength = rs232i.GetComListCount();
			if(nLength > 0){
				combobox.Items.Clear();
				combobox.Items.AddRange(rs232i.GetComPorts());
			}
			
			/*
			//using (StreamWriter outputFile = new StreamWrite(Application.StartupPath + "\\nand-usb.qml"){
			//StreamWriter outputfile = new StreamWriter(new FileStream(Application.StartupPath + "\\program.bat"), fileMode.Create);
			//StreamWriter outputFile = new StreamWriter("program.bat");
			StreamWriter outputFile = new StreamWriter(Application.StartupPath + "\\program.bat");
			outputFile.WriteLine(executeText);
			outputFile.Close();

			Process.Start(batFile);
			//MessageBox.Show("PROGRAM!");
			*/
		}
    }
}


// File FormGui.Designer.cs
namespace WindowsFormsApplication
{
    partial class FormGui
    {
        private System.ComponentModel.IContainer components = null;
		private Button buttonLoad;
		private ComboBox combobox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
			//System.Windows.Forms.Button buttonLoad;
			this.buttonLoad = new System.Windows.Forms.Button();
			Button buttonRefresh = new Button();
			Label labelName = new Label();
			this.combobox = new ComboBox();

			this.SuspendLayout();

			// FormGui
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Serial List";
			this.ClientSize = new System.Drawing.Size(80, 70);
			this.Controls.Add(buttonRefresh);
			this.Controls.Add(labelName);
			this.Controls.Add(combobox);

			// Button [REFLESH]
			buttonRefresh.Location = new System.Drawing.Point(15, 10);
			buttonRefresh.Name = "buttonRefresh";
			buttonRefresh.Size = new System.Drawing.Size(75, 23);
			buttonRefresh.TabIndex = 1;
			buttonRefresh.Text = "REFLESH";
			buttonRefresh.UseVisualStyleBackColor = true;
			buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);

			// ComboBox
			combobox.Location = new System.Drawing.Point(15, 40);
			combobox.Size = new System.Drawing.Size(75, 30);

			// Label
			labelName.Location = new System.Drawing.Point(30, 60);
			labelName.Name = "labelName";
			labelName.Size = new System.Drawing.Size(55, 20);
			labelName.Text = "NAME";

			
			this.ResumeLayout(false);
        }

        #endregion
    }
}

