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
	/// Summary description for FormEditAddVariable.
	/// </summary>
	public class FormEditAddVariable : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxKey;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.ComponentModel.Container components = null;

		private bool cancel = false;

		#region Constructors and Destructor

		public FormEditAddVariable()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			comboBoxKey.SelectedIndex = 1;
		}

		public FormEditAddVariable(String path)
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			
			// Fill combo box and prevent change
			if (path.IndexOf("System.") > -1)
			{
				comboBoxKey.SelectedIndex = 0;
			} 
			else 
			{
				comboBoxKey.SelectedIndex = 1;
			}
			comboBoxKey.Enabled = false;

			// Fill textbox
			int pos = path.IndexOf(".") + 1;
			textBox1.Text = path.Substring(pos, path.Length - pos);
			
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
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxKey = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Variable:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxKey
			// 
			this.comboBoxKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKey.Items.AddRange(new object[] {
															 "System",
															 "Local"});
			this.comboBoxKey.Location = new System.Drawing.Point(64, 8);
			this.comboBoxKey.Name = "comboBoxKey";
			this.comboBoxKey.Size = new System.Drawing.Size(72, 21);
			this.comboBoxKey.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(144, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(200, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_EnterPress);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(104, 40);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(184, 40);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(80, 24);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// FormEditAddVariable
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 75);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboBoxKey);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MinimizeBox = false;
			this.Name = "FormEditAddVariable";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add/Edit Variable";
			this.TopMost = true;
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

		#endregion

		/// <summary>
		/// Accessor method to retrieve variable specified in textbox
		/// </summary>
		/// <returns></returns>
		public String GetVariable()
		{
			if (cancel == true) return null;
			if (textBox1.Text == "") return null;
			return comboBoxKey.Text + "." + textBox1.Text;
		}

	}
}
