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
using System.Data;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace PathEdit
{
	/// <summary>
	/// PathEdit's main form...contains the combobox of enviromental variables to
	/// edit, provides the menus, context menus, systray icon, get/set of env vars. 
	/// </summary>
	public class FormPathEdit : System.Windows.Forms.Form
	{
		#region Control Definitions

		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton toolBarButtonOpen;
		private System.Windows.Forms.ToolBarButton toolBarButtonSave;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep1;
		private System.Windows.Forms.ToolBarButton toolBarButtonRefresh;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep3;
		private System.Windows.Forms.ToolBarButton toolBarButtonExit;
		private System.Windows.Forms.ToolBarButton toolBarButtonUndo;
		private System.Windows.Forms.ToolBarButton toolBarButtonRedo;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep2;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep0;
		private System.Windows.Forms.ToolBarButton toolBarButtonUp;
		private System.Windows.Forms.ToolBarButton toolBarButtonDown;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBarButton toolBarButtonNewVar;
		private System.Windows.Forms.ToolBarButton toolBarButtonEditVar;
		private System.Windows.Forms.ToolBarButton toolBarButtonEditEntry;
		private System.Windows.Forms.ToolBarButton toolBarButtonAddEntry;
		private System.Windows.Forms.ToolBarButton toolBarButtonDelVar;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep4;
		private System.Windows.Forms.ToolBarButton toolBarButtonDelEntry;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemRefresh;
		private System.Windows.Forms.MenuItem menuItemAdd;
		private System.Windows.Forms.MenuItem menuItemExplore;
		private System.Windows.Forms.MenuItem menuItemShow;
		private System.Windows.Forms.MenuItem menuItemNotifyExit;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemFileSep;
		private System.Windows.Forms.MenuItem menuItemEdit;
		private System.Windows.Forms.MenuItem menuItemListSep;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemLicense;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemOpenAll;
		private System.Windows.Forms.MenuItem menuItemSaveAll;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.ContextMenu contextMenuNotify;
		private System.Windows.Forms.ContextMenu contextMenuPathList;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.CheckedListBox listBoxPath;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Button buttonHide;
		private System.Windows.Forms.ListBox listBoxVariables;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components;
		
		#endregion

		// Member variables
		private bool local_clean = true;
		private bool system_clean = true;
		private VariableMap system = null;
		private VariableMap local = null;

		#region Contruction and Distruction

		public FormPathEdit()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			InitializePathEdit();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPathEdit));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotify = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemShow = new System.Windows.Forms.MenuItem();
            this.menuItemNotifyExit = new System.Windows.Forms.MenuItem();
            this.contextMenuPathList = new System.Windows.Forms.ContextMenu();
            this.menuItemEdit = new System.Windows.Forms.MenuItem();
            this.menuItemExplore = new System.Windows.Forms.MenuItem();
            this.menuItemListSep = new System.Windows.Forms.MenuItem();
            this.menuItemAdd = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemOpenAll = new System.Windows.Forms.MenuItem();
            this.menuItemSaveAll = new System.Windows.Forms.MenuItem();
            this.menuItemRefresh = new System.Windows.Forms.MenuItem();
            this.menuItemFileSep = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemLicense = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.listBoxPath = new System.Windows.Forms.CheckedListBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonHide = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.toolBarButtonSep0 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSave = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonUndo = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonRedo = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNewVar = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonEditVar = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDelVar = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep3 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonAddEntry = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonEditEntry = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonUp = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDown = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDelEntry = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep4 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonExit = new System.Windows.Forms.ToolBarButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenu = this.contextMenuNotify;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PathEdit";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuNotify
            // 
            this.contextMenuNotify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItemShow,
            this.menuItemNotifyExit});
            // 
            // menuItem1
            // 
            this.menuItem1.Enabled = false;
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "PathEdit";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "-";
            // 
            // menuItemShow
            // 
            this.menuItemShow.Index = 2;
            this.menuItemShow.Text = "&Show";
            this.menuItemShow.Click += new System.EventHandler(this.menuItemShow_Click);
            // 
            // menuItemNotifyExit
            // 
            this.menuItemNotifyExit.Index = 3;
            this.menuItemNotifyExit.Text = "E&xit";
            this.menuItemNotifyExit.Click += new System.EventHandler(this.menuItemNotifyExit_Click);
            // 
            // contextMenuPathList
            // 
            this.contextMenuPathList.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEdit,
            this.menuItemExplore,
            this.menuItemListSep,
            this.menuItemAdd,
            this.menuItemOpen,
            this.menuItemSave});
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Index = 0;
            this.menuItemEdit.Text = "&Edit";
            this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
            // 
            // menuItemExplore
            // 
            this.menuItemExplore.Index = 1;
            this.menuItemExplore.Text = "&Explore";
            this.menuItemExplore.Click += new System.EventHandler(this.menuItemExplore_Click);
            // 
            // menuItemListSep
            // 
            this.menuItemListSep.Index = 2;
            this.menuItemListSep.Text = "-";
            // 
            // menuItemAdd
            // 
            this.menuItemAdd.Index = 3;
            this.menuItemAdd.Text = "&Add";
            this.menuItemAdd.Click += new System.EventHandler(this.menuItemAdd_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 4;
            this.menuItemOpen.Text = "&Open";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Index = 5;
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpenAll,
            this.menuItemSaveAll,
            this.menuItemRefresh,
            this.menuItemFileSep,
            this.menuItemExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemOpenAll
            // 
            this.menuItemOpenAll.Index = 0;
            this.menuItemOpenAll.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpenAll.Text = "&Open All";
            this.menuItemOpenAll.Click += new System.EventHandler(this.menuItemOpenAll_Click);
            // 
            // menuItemSaveAll
            // 
            this.menuItemSaveAll.Index = 1;
            this.menuItemSaveAll.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItemSaveAll.Text = "&Save All";
            this.menuItemSaveAll.Click += new System.EventHandler(this.menuItemSaveAll_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Index = 2;
            this.menuItemRefresh.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.menuItemRefresh.Text = "&Refresh";
            // 
            // menuItemFileSep
            // 
            this.menuItemFileSep.Index = 3;
            this.menuItemFileSep.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 4;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 1;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemLicense,
            this.menuItemAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemLicense
            // 
            this.menuItemLicense.Index = 0;
            this.menuItemLicense.Text = "&License";
            this.menuItemLicense.Click += new System.EventHandler(this.menuItemLicense_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 1;
            this.menuItemAbout.Text = "&About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // listBoxVariables
            // 
            this.listBoxVariables.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxVariables.IntegralHeight = false;
            this.listBoxVariables.Location = new System.Drawing.Point(0, 0);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(200, 144);
            this.listBoxVariables.Sorted = true;
            this.listBoxVariables.TabIndex = 0;
            this.listBoxVariables.SelectedIndexChanged += new System.EventHandler(this.listBoxVariables_SelectedIndexChanged);
            this.listBoxVariables.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxVariables_KeyPress);
            // 
            // listBoxPath
            // 
            this.listBoxPath.AllowDrop = true;
            this.listBoxPath.ContextMenu = this.contextMenuPathList;
            this.listBoxPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPath.HorizontalScrollbar = true;
            this.listBoxPath.IntegralHeight = false;
            this.listBoxPath.Location = new System.Drawing.Point(0, 0);
            this.listBoxPath.Name = "listBoxPath";
            this.listBoxPath.ScrollAlwaysVisible = true;
            this.listBoxPath.Size = new System.Drawing.Size(144, 144);
            this.listBoxPath.TabIndex = 1;
            this.listBoxPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxPath_DragDrop);
            this.listBoxPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxPath_DragEnter);
            this.listBoxPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxPath_KeyPress);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(168, 200);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(248, 200);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(75, 23);
            this.buttonHide.TabIndex = 3;
            this.buttonHide.Text = "Hide";
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 8);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelTitle.Location = new System.Drawing.Point(8, 200);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(88, 23);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "PathEdit v0.3";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonSep0,
            this.toolBarButtonOpen,
            this.toolBarButtonSave,
            this.toolBarButtonRefresh,
            this.toolBarButtonSep1,
            this.toolBarButtonUndo,
            this.toolBarButtonRedo,
            this.toolBarButtonSep2,
            this.toolBarButtonNewVar,
            this.toolBarButtonEditVar,
            this.toolBarButtonDelVar,
            this.toolBarButtonSep3,
            this.toolBarButtonAddEntry,
            this.toolBarButtonEditEntry,
            this.toolBarButtonUp,
            this.toolBarButtonDown,
            this.toolBarButtonDelEntry,
            this.toolBarButtonSep4,
            this.toolBarButtonExit});
            this.toolBar.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(369, 28);
            this.toolBar.TabIndex = 8;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // toolBarButtonSep0
            // 
            this.toolBarButtonSep0.Name = "toolBarButtonSep0";
            this.toolBarButtonSep0.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonOpen
            // 
            this.toolBarButtonOpen.ImageIndex = 0;
            this.toolBarButtonOpen.Name = "toolBarButtonOpen";
            this.toolBarButtonOpen.ToolTipText = "Open Path";
            // 
            // toolBarButtonSave
            // 
            this.toolBarButtonSave.ImageIndex = 1;
            this.toolBarButtonSave.Name = "toolBarButtonSave";
            this.toolBarButtonSave.ToolTipText = "Save Path";
            // 
            // toolBarButtonRefresh
            // 
            this.toolBarButtonRefresh.ImageIndex = 2;
            this.toolBarButtonRefresh.Name = "toolBarButtonRefresh";
            this.toolBarButtonRefresh.ToolTipText = "Refresh Paths";
            // 
            // toolBarButtonSep1
            // 
            this.toolBarButtonSep1.Name = "toolBarButtonSep1";
            this.toolBarButtonSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonUndo
            // 
            this.toolBarButtonUndo.ImageIndex = 3;
            this.toolBarButtonUndo.Name = "toolBarButtonUndo";
            this.toolBarButtonUndo.ToolTipText = "Undo";
            // 
            // toolBarButtonRedo
            // 
            this.toolBarButtonRedo.ImageIndex = 4;
            this.toolBarButtonRedo.Name = "toolBarButtonRedo";
            this.toolBarButtonRedo.ToolTipText = "Redo";
            // 
            // toolBarButtonSep2
            // 
            this.toolBarButtonSep2.Name = "toolBarButtonSep2";
            this.toolBarButtonSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonNewVar
            // 
            this.toolBarButtonNewVar.ImageIndex = 5;
            this.toolBarButtonNewVar.Name = "toolBarButtonNewVar";
            this.toolBarButtonNewVar.ToolTipText = "New Path Variable";
            // 
            // toolBarButtonEditVar
            // 
            this.toolBarButtonEditVar.ImageIndex = 6;
            this.toolBarButtonEditVar.Name = "toolBarButtonEditVar";
            this.toolBarButtonEditVar.ToolTipText = "Edit Path Variable";
            // 
            // toolBarButtonDelVar
            // 
            this.toolBarButtonDelVar.ImageIndex = 7;
            this.toolBarButtonDelVar.Name = "toolBarButtonDelVar";
            this.toolBarButtonDelVar.ToolTipText = "Remove Path Variable";
            // 
            // toolBarButtonSep3
            // 
            this.toolBarButtonSep3.Name = "toolBarButtonSep3";
            this.toolBarButtonSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonAddEntry
            // 
            this.toolBarButtonAddEntry.ImageIndex = 8;
            this.toolBarButtonAddEntry.Name = "toolBarButtonAddEntry";
            this.toolBarButtonAddEntry.ToolTipText = "Add Entry";
            // 
            // toolBarButtonEditEntry
            // 
            this.toolBarButtonEditEntry.ImageIndex = 9;
            this.toolBarButtonEditEntry.Name = "toolBarButtonEditEntry";
            this.toolBarButtonEditEntry.ToolTipText = "Edit Entry";
            // 
            // toolBarButtonUp
            // 
            this.toolBarButtonUp.ImageIndex = 10;
            this.toolBarButtonUp.Name = "toolBarButtonUp";
            this.toolBarButtonUp.ToolTipText = "Move Entry Up";
            // 
            // toolBarButtonDown
            // 
            this.toolBarButtonDown.ImageIndex = 11;
            this.toolBarButtonDown.Name = "toolBarButtonDown";
            this.toolBarButtonDown.ToolTipText = "Move Entry Down";
            // 
            // toolBarButtonDelEntry
            // 
            this.toolBarButtonDelEntry.ImageIndex = 12;
            this.toolBarButtonDelEntry.Name = "toolBarButtonDelEntry";
            this.toolBarButtonDelEntry.ToolTipText = "Remove Entry";
            // 
            // toolBarButtonSep4
            // 
            this.toolBarButtonSep4.Name = "toolBarButtonSep4";
            this.toolBarButtonSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonExit
            // 
            this.toolBarButtonExit.ImageIndex = 13;
            this.toolBarButtonExit.Name = "toolBarButtonExit";
            this.toolBarButtonExit.ToolTipText = "Exit PathEdit";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.splitter1);
            this.panelMain.Controls.Add(this.listBoxVariables);
            this.panelMain.Location = new System.Drawing.Point(8, 32);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(352, 144);
            this.panelMain.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBoxPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(208, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 144);
            this.panel1.TabIndex = 13;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 144);
            this.splitter1.TabIndex = 12;
            this.splitter1.TabStop = false;
            // 
            // FormPathEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(376, 230);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonHide);
            this.Controls.Add(this.buttonApply);
            this.Menu = this.mainMenu1;
            this.Name = "FormPathEdit";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "PathEdit";
            this.Load += new System.EventHandler(this.FormPathEdit_Load);
            this.Resize += new System.EventHandler(this.FormPathEdit_Resize);
            this.panelMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Form Member Functions

		private void InitializePathEdit()
		{
			// VariableMaps
			system = new VariableMap(VariableMap.ENVIROMENT.SYSTEM);
			local = new VariableMap(VariableMap.ENVIROMENT.LOCAL);
			RefreshAll();

			// Set Size
			this.Size = new Size(600, 300);

			// Change combobox to default
			listBoxVariables.SelectedIndex = 0;
		}

		private void FormPathEdit_Resize(object sender, System.EventArgs e)
		{
			toolBar.Width = this.Size.Width;

			groupBox1.Width = this.Size.Width - 24;
			groupBox1.Top = this.Size.Height - 92;
			
			buttonHide.Left = this.Size.Width - buttonHide.Width - 24;
			buttonHide.Top = groupBox1.Top + 12;

			buttonApply.Left = buttonHide.Left - buttonApply.Width - 12;
			buttonApply.Top = buttonHide.Top;

			labelTitle.Top = buttonHide.Top;

			panelMain.Width = this.Size.Width - 24;
			panelMain.Height = groupBox1.Top - panelMain.Top;
		}

		private void FormPathEdit_Load(object sender, System.EventArgs e)
		{
			listBoxVariables.Select();
		}

		#endregion

		#region Control Member Functions

		private void listBoxVariables_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
			// Make sure there is a selection
			if (listBoxVariables.SelectedItem == null) return;

			// Prevent user mucking
			listBoxVariables.Enabled = false;
			buttonApply.Enabled = false;
			if (listBoxVariables.SelectedItem.ToString().IndexOf("System.") > -1)
			{
				UpdateSystemPath();
			} 
			else 
			{
				UpdateLocalPath();
			}
			buttonApply.Enabled = true;
			listBoxVariables.Enabled = true;
			listBoxVariables.Focus();
		}

		private void listBoxVariables_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) 
			{
				listBoxPath.Focus();
			}

		}

		private void listBoxPath_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			//string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			//foreach(string file in files)
			//{
			//	MessageBox.Show(file);
			//}
		}

		private void listBoxPath_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void listBoxPath_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) EditPath();
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch (toolBar.Buttons.IndexOf(e.Button))
			{
				case 1:
					OpenAll();
					break;
				case 2:
					SaveAll();
					break;
				case 3:
					RefreshAll();
					break;
				case 8:
					AddVariable();
					break;
				case 9:
					RenameVariable();
					break;
				case 10:
					DeleteVariable();
					break;
				case 12:
					AddPath();
					break;
				case 13:
					EditPath();
					break;
				case 14:
					MovePathUp();
					break;
				case 15:
					MovePathDown();
					break;
				case 16:
					DeletePath();
					break;
				case 18:
					Exit();
					break;
				default:
					Console.WriteLine(e.Button);
					break;
			}
		}

		private void buttonHide_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		private void buttonApply_Click(object sender, System.EventArgs e)
		{
			Apply();
		}

		#endregion

		#region Main Menu Member Functions
	
		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			ShowHelp();
		}

		private void menuItemLicense_Click(object sender, System.EventArgs e)
		{
			OpenLicense();
		}

		private void menuItemOpenAll_Click(object sender, System.EventArgs e)
		{
			OpenAll();
		}

		private void menuItemSaveAll_Click(object sender, System.EventArgs e)
		{
			SaveAll();
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			Exit();
		}

		#endregion

		#region Listbox Rightclick Menu Member Functions

		private void menuItemAdd_Click(object sender, System.EventArgs e)
		{
			AddPath();
		}

		private void menuItemEdit_Click(object sender, System.EventArgs e)
		{
			EditPath();
		}

		private void menuItemExplore_Click(object sender, System.EventArgs e)
		{
			ExplorePath();
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
			Open();
		}

		private void menuItemSave_Click(object sender, System.EventArgs e)
		{
			Save();
		}

		#endregion

		#region SystemTray Member Functions

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			menuItemShow_Click(sender, e);
		}

		private void menuItemShow_Click(object sender, System.EventArgs e)
		{
			this.Show();
		}

		private void menuItemNotifyExit_Click(object sender, System.EventArgs e)
		{
			menuItemExit_Click(sender, e);
		}

		#endregion

		#region PathEdit Member Functions

		private void UpdateSystemPath()
		{
			if (!system_clean) 
			{
				system.SetPath(listBoxVariables.Text, GetPaths());
			}
			SetPaths(system.GetPath(listBoxVariables.Text));
		}

		private void UpdateLocalPath()
		{
			if (!local_clean) 
			{
				local.SetPath(listBoxVariables.Text, GetPaths());
			}
			SetPaths(local.GetPath(listBoxVariables.Text));
		}

		private void SetSystemPath()
		{
			listBoxVariables.Enabled = false;
			buttonApply.Enabled = false;

			system.SetPath(listBoxVariables.Text, GetPaths());
			system.apply();

			Win32Calls.refreshEnviroment();

			listBoxVariables.Enabled = true;
			buttonApply.Enabled = true;
		}

		private void SetLocalPath()
		{
			listBoxVariables.Enabled = false;
			buttonApply.Enabled = false;

			local.SetPath(listBoxVariables.SelectedItem.ToString(), GetPaths());
			local.apply();

			Win32Calls.refreshEnviroment();

			listBoxVariables.Enabled = true;
			buttonApply.Enabled = true;
		}

		private void RefreshAll()
		{
			// Prevent user mucking
			listBoxVariables.Enabled = false;
			buttonApply.Enabled = false;
			
			listBoxVariables.Items.Clear();
			listBoxVariables.Items.AddRange(local.GetKeys());
			listBoxVariables.Items.AddRange(system.GetKeys());
			listBoxVariables.SelectedIndex = 0;

			buttonApply.Enabled = true;
			listBoxVariables.Enabled = true;
		}

		private void Apply()
		{
			// Prevent user mucking
			listBoxVariables.Enabled = false;
			if (listBoxVariables.SelectedItem.ToString().IndexOf("System.") > -1)
			{
				SetSystemPath();
			} 
			else
			{
				SetLocalPath();
			}	
			listBoxVariables.Enabled = true;
		}

		private void Open()
		{
			if (listBoxVariables.SelectedItem == null) return;
			string var = listBoxVariables.SelectedItem.ToString();

			openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			openFileDialog1.Multiselect = false;
			if (openFileDialog1.ShowDialog() == DialogResult.OK) 
			{
				StreamReader r = new StreamReader(openFileDialog1.FileName);
				String path = r.ReadLine();
				r.Close();

				listBoxVariables.Enabled = false;
                SetPaths(path.Split(';'));
				listBoxVariables.Enabled = true;
			}
		}

		private void OpenAll()
		{
            MessageBox.Show("This feature is disabled due to the hazard of overwrighting all system variables, " + 
                "and the inability to selectively apply changes.  If you wish to save and restore an individual " +
                "variable, select the variable in the left listbox and right click in the right listbox and " + 
                "specify save or open.");
		}

		private void Save()
		{
			saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			saveFileDialog1.OverwritePrompt = true;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) 
			{
				StreamWriter w = new StreamWriter(saveFileDialog1.FileName);
				w.WriteLine(GetPaths());
				w.Close();				
			}
		}
		
		private void SaveAll()
		{
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.OverwritePrompt = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter w = new StreamWriter(saveFileDialog1.FileName);
                
                string[] variables = local.GetKeys();
                foreach (string variable in variables)
                {
                    string temp = "";
                    string[] entries;
                    entries = local.GetPath(variable);
                    
                    for (int i = 0; i < entries.Length; i++)
                    {
                        if (i > 0) temp += ";";
                        temp += entries[i];
                    }
                    w.WriteLine(variable);
                    w.WriteLine(temp);
                }
                
                variables = system.GetKeys();
                foreach (string variable in variables)
                {
                    string temp = "";
                    string[] entries;
                    entries = system.GetPath(variable);
                    
                    for (int i = 0; i < entries.Length; i++)
                    {
                        if (i > 0) temp += ";";
                        temp += entries[i];
                    }
                    w.WriteLine(variable);
                    w.WriteLine(temp);
                }
                    
                w.Close();
            }
		}

		private void ShowHelp()
		{
			FormHelp help = new FormHelp();
			help.ShowDialog(this);
		}

		private void OpenLicense() 
		{
			Win32Calls.open(Application.StartupPath + @"\gpl.txt");
		}

		private void Exit()
		{
			this.Close();
		}

		#endregion

		#region Varable Member Fnctions

		private void AddVariable()
		{
			FormEditAddVariable form = new FormEditAddVariable();
			form.ShowDialog();
			string var = form.GetVariable();
			if (var != null)
			{
				bool result = false;
				if (var.IndexOf("System.") > -1)
				{
					result = system.AddVariable(var);
				}
				else
				{
					result = local.AddVariable(var);
				}

				if (result)
				{
					
					listBoxVariables.SelectedIndex = listBoxVariables.Items.Add(var);
				}
			}
		}

		private void RenameVariable()
		{
			// Make sure there is a selection and save it
			if (listBoxVariables.SelectedIndex == -1) return;
			string old = listBoxVariables.SelectedItem.ToString();

			// Launch dialog
			FormEditAddVariable form = new FormEditAddVariable(listBoxVariables.SelectedItem.ToString());
			form.ShowDialog();
			string var = form.GetVariable();

			if (var != null)
			{
				// Check for existing item
				ListBox.ObjectCollection col = listBoxVariables.Items;
				foreach (string item in col)
				{
					if (item == var) 
					{
						// TODO prompt user
						return;
					}
				}

				// Rename
				if (var.IndexOf("System.") > -1)
				{
					system.RenameVariable(old, var);
				}
				else
				{
					local.RenameVariable(old, var);
				}
				listBoxVariables.Items.Remove(old);
				listBoxVariables.SelectedIndex = listBoxVariables.Items.Add(var);
			}
		}

		private void DeleteVariable()
		{
			// Make sure there is a selection and save it
			if (listBoxVariables.SelectedIndex == -1) return;
			string old = listBoxVariables.SelectedItem.ToString();

			// TODO prompt user
			
			// Rename
			if (old.IndexOf("System.") > -1)
			{
				system.DeleteVariable(old);
			}
			else
			{
				local.DeleteVariable(old);
			}
			listBoxVariables.Items.Remove(old);
			listBoxVariables.SelectedIndex = 0;
		}

		#endregion

		#region Path Member Fnctions
		private void AddPath()
		{
			FormEditAddPath form = new FormEditAddPath();
			form.ShowDialog();
			if (form.GetPath() != null)
			{
				listBoxPath.Items.Add(form.GetPath(), true);
			}
		}
		
		private void EditPath()
		{
			if (listBoxPath.SelectedIndex == -1) return;
			FormEditAddPath form = new FormEditAddPath(listBoxPath.SelectedItem.ToString());
			form.ShowDialog();
			if (form.GetPath() != null)
			{
				listBoxPath.Items[listBoxPath.SelectedIndex] = form.GetPath();
			}
		}

		private void MovePathUp()
		{
			int i = listBoxPath.SelectedIndex;
			bool selected = listBoxPath.GetItemChecked(i);
			Object o = listBoxPath.SelectedItem;
			if (i > 0)
			{

				listBoxPath.Items.RemoveAt(i);
				listBoxPath.Items.Insert(i-1, o);
				listBoxPath.SelectedIndex = i-1;
				listBoxPath.SetItemChecked(i-1, true);
			}
		}

		private void MovePathDown()
		{
			int i = listBoxPath.SelectedIndex;
			bool selected = listBoxPath.GetItemChecked(i);
			Object o = listBoxPath.SelectedItem;
			if (i < listBoxPath.Items.Count-1) 
			{
				listBoxPath.Items.RemoveAt(i);
				listBoxPath.Items.Insert(i+1, o);
				listBoxPath.SelectedIndex = i+1;
				listBoxPath.SetItemChecked(i+1, true);
			}
		}

		private void DeletePath()
		{
			if (listBoxPath.SelectedIndex != -1)
			{
				listBoxPath.SetItemCheckState(listBoxPath.SelectedIndex, CheckState.Unchecked);
			}
		}

		private void ExplorePath()
		{
			if (listBoxPath.SelectedIndex == -1) return;
			Win32Calls.open(listBoxPath.SelectedItem.ToString());
		}

		private void SetPaths(String[] paths)
		{
			if (paths == null) return;

			listBoxPath.Items.Clear();
			for (int i=0; i < paths.Length; i++)
			{
				if (paths[i] == "") continue;
				listBoxPath.Items.Add(paths[i], true);
			}
		}

		private string GetPaths()
		{
			string temp = "";

			System.Windows.Forms.CheckedListBox.CheckedItemCollection items = listBoxPath.CheckedItems;

			for (int i=0; i < items.Count; i++)
			{
				if (i>0) temp += ";";
				temp += items[i];
			}
			return temp;
		}

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormPathEdit());
		}
	}
}
