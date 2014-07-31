/*
 * This file is part of PathEdit.
 * 
 * PathEdit is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PathEdit
{
	/// <summary>
	/// Form to edit a path variable
	/// </summary>
	public class FormEditAddPath : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelPath;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button buttonBrowse;

		private bool cancel = false;

		#region Constructors and Destructor

		public FormEditAddPath()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		public FormEditAddPath(String path)
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			textBox1.Text = path;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelPath = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelPath
			// 
			this.labelPath.Location = new System.Drawing.Point(8, 8);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(72, 16);
			this.labelPath.TabIndex = 0;
			this.labelPath.Tag = "";
			this.labelPath.Text = "Path Entry:";
			this.labelPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(72, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(256, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_EnterPress);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(112, 40);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(192, 40);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(328, 8);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(24, 20);
			this.buttonBrowse.TabIndex = 2;
			this.buttonBrowse.Text = "...";
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// FormEditAddPath
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 75);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.labelPath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditAddPath";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add/Edit Path";
			this.ResumeLayout(false);

		}
		#endregion

		#region Form Menber Functions

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			cancel = true;
			this.Close();
		}

		/// <summary>
		/// Close the form on enter key
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBox1_EnterPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13 || e.KeyChar == 27) this.Close();
		}

		/// <summary>
		/// Browse button click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBrowse_Click(object sender, System.EventArgs e)
		{
			folderBrowserDialog1.Description = "Select Folder to Add to Path";
			
			if (textBox1.Text != "") folderBrowserDialog1.SelectedPath = textBox1.Text;

			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = folderBrowserDialog1.SelectedPath;
			}
			textBox1.Focus();
		}

		#endregion

		/// <summary>
		/// Accessor method to retrieve path specified in textbox
		/// </summary>
		/// <returns></returns>
		public String GetPath()
		{
			if (cancel == true) return null;
			if (textBox1.Text == "") return null;
			return textBox1.Text;
		}

	}
}
