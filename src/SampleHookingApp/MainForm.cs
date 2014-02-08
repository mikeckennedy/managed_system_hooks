#region File Header
// /////////////////////////////////////////////////////////////
// Date: 2/25/2004			Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

#region using ...
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
#endregion
// EXAMPLE CODE SECTION
using Kennedy.ManagedHooks;

namespace Kennedy.ManagedHooks.SampleHookingApp
{
	public class MainForm : System.Windows.Forms.Form
	{
		#region Member Variables

		private System.Windows.Forms.Button buttonInstall;
		private System.Windows.Forms.Button buttonUninstall;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.TextBox textBoxMessages;
		private System.Windows.Forms.Button buttonAbout;

		#endregion

		// EXAMPLE CODE SECTION
		private IMouseHookLite mouseHook = null;
		private IKeyboardHookExt keyboardHook = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Add any constructor code after InitializeComponent call
			//
			SetInitialMessage();

			// EXAMPLE CODE SECTION
			keyboardHook = HookFactory.CreateKeyboardHookExt();
			keyboardHook.KeyDown += new KeyboardEventHandlerExt( keyboardHook_KeyDown );

			//
			// Note: Use IMouseHookExt for mouse move events.
			//
			mouseHook = HookFactory.CreateMouseHookLite();
			mouseHook.LeftButtonDown += new MouseEventHandlerExt( mouseHook_LeftButtonDown );
			mouseHook.LeftButtonUp += new MouseEventHandlerExt( mouseHook_LeftButtonUp );
			mouseHook.MouseWheel += new MouseEventHandlerExt( mouseHook_MouseWheel );

			UpdateEnabledStates();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// EXAMPLE CODE SECTION
				using (mouseHook) { mouseHook = null; }
				using (keyboardHook) { keyboardHook = null; }

				using (components) { components = null; }
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonInstall = new System.Windows.Forms.Button();
			this.buttonUninstall = new System.Windows.Forms.Button();
			this.textBoxMessages = new System.Windows.Forms.TextBox();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.buttonAbout = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// buttonInstall
			//
			this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonInstall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonInstall.Location = new System.Drawing.Point( 8, 8 );
			this.buttonInstall.Name = "buttonInstall";
			this.buttonInstall.Size = new System.Drawing.Size( 376, 32 );
			this.buttonInstall.TabIndex = 0;
			this.buttonInstall.Text = "&Install Mouse and Keyboard Hooks";
			this.buttonInstall.Click += new System.EventHandler( this.buttonInstall_Click );
			//
			// buttonUninstall
			//
			this.buttonUninstall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUninstall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonUninstall.Location = new System.Drawing.Point( 8, 44 );
			this.buttonUninstall.Name = "buttonUninstall";
			this.buttonUninstall.Size = new System.Drawing.Size( 376, 32 );
			this.buttonUninstall.TabIndex = 1;
			this.buttonUninstall.Text = "&Uninstall Mouse and Keyboard Hooks";
			this.buttonUninstall.Click += new System.EventHandler( this.buttonUninstall_Click );
			//
			// textBoxMessages
			//
			this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				| System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessages.BackColor = System.Drawing.Color.White;
			this.textBoxMessages.Location = new System.Drawing.Point( 0, 84 );
			this.textBoxMessages.Multiline = true;
			this.textBoxMessages.Name = "textBoxMessages";
			this.textBoxMessages.ReadOnly = true;
			this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxMessages.Size = new System.Drawing.Size( 456, 320 );
			this.textBoxMessages.TabIndex = 2;
			this.textBoxMessages.TabStop = false;
			this.textBoxMessages.Text = "";
			//
			// statusBar1
			//
			this.statusBar1.Location = new System.Drawing.Point( 0, 404 );
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size( 456, 22 );
			this.statusBar1.TabIndex = 3;
			//
			// buttonAbout
			//
			this.buttonAbout.Anchor = ( (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)) );
			this.buttonAbout.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAbout.Location = new System.Drawing.Point( 392, 24 );
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size( 56, 36 );
			this.buttonAbout.TabIndex = 4;
			this.buttonAbout.Text = "&About";
			this.buttonAbout.Click += new System.EventHandler( this.buttonAbout_Click );
			//
			// MainForm
			//
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 456, 426 );
			this.Controls.Add( this.buttonAbout );
			this.Controls.Add( this.statusBar1 );
			this.Controls.Add( this.textBoxMessages );
			this.Controls.Add( this.buttonUninstall );
			this.Controls.Add( this.buttonInstall );
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "System Hook Test Application";
			this.ResumeLayout( false );

		}
		#endregion

		#region Main Method
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//
			// Give our app an XP theme look.
			//
			Application.EnableVisualStyles();
			Application.DoEvents();

			Application.Run( new MainForm() );
		}

		#endregion

		private void buttonInstall_Click(object sender, System.EventArgs e)
		{
			// EXAMPLE CODE SECTION

			AddText( "Adding mouse hook." );
			mouseHook.InstallHook();

			AddText( "Adding keyboard hook." );
			keyboardHook.InstallHook();

			UpdateEnabledStates();
		}

		private void buttonUninstall_Click(object sender, System.EventArgs e)
		{
			// EXAMPLE CODE SECTION

			mouseHook.UninstallHook();
			AddText( "Mouse hook removed." );

			keyboardHook.UninstallHook();
			AddText( "Keyboard hook removed." );

			UpdateEnabledStates();
		}

		// EXAMPLE CODE SECTION
		private void mouseHook_LeftButtonDown(MouseHookEventArgs mea)
		{
			ReportMouseEvent( MouseEvents.LeftButtonDown, mea );
		}

		private void mouseHook_LeftButtonUp(MouseHookEventArgs mea)
		{
			ReportMouseEvent( MouseEvents.LeftButtonUp, mea );
		}

		private void mouseHook_MouseWheel(MouseHookEventArgs mea)
		{
			ReportMouseEvent( MouseEvents.MouseWheel, mea );
		}

		private void ReportMouseEvent(MouseEvents mEvent, MouseHookEventArgs mea)
		{
			string msg = string.Format( "Mouse event: {0}: {1}.", mEvent.ToString(), mea.ToString() );
			AddText( msg );
		}

		// EXAMPLE CODE SECTION
		private void keyboardHook_KeyDown(object sender, KeyboardHookEventArgs kea)
		{
			string msg = string.Format( "KeyDown event: {0}.", kea.ToString() );

			//
			// Prevent the 'A' key from reaching any applications whatsoever
            // except this callback and other system hook applications.
			//
			if ( kea.Key == Keys.A )
			{
				kea.Cancel = true;
				msg += " [BLOCKED]";
			}
			AddText( msg );
		}

		#region Utility Methods

		private void AddText(string message)
		{
			if (message == null)
			{
				return;
			}

			int length = textBoxMessages.Text.Length + message.Length;
			if (length >= textBoxMessages.MaxLength)
			{
				textBoxMessages.Text = "";
			}

			if (!message.EndsWith("\r\n"))
			{
				message += "\r\n";
			}

			textBoxMessages.Text = message + textBoxMessages.Text;
		}

		private void buttonAbout_Click(object sender, System.EventArgs e)
		{
			new AboutForm().ShowDialog();
		}

		private void SetInitialMessage()
		{
			string msg =
				"Press Install Hooks button to begin.\r\n" +
				"\r\n" +
				"The mouse hook will track the left button events as " +
				"well as the mouse wheel events. Other mouse events can " +
				"be tracked but are not in this sample.\r\n" +
				"\r\n" +
				"All the keyboard events are tracked, as well as the " +
				"status of the modifier keys. The sample application will " +
				"prevent any 'A' character from reaching other applications. " +
				"To test this, install the hooks, then start Notepad and type:\r\n" +
				"\r\n" +
				"The cat ran over the hill as usual.\r\n" +
				"\r\n" +
				"You should see the following text appear:\r\n" +
				"\r\n" +
				"The ct rn over the hill s usul.\r\n" +
				"\r\n";

			textBoxMessages.Text = msg;
		}

		private void UpdateEnabledStates()
		{
			buttonInstall.Enabled = !mouseHook.IsHooked;
			buttonUninstall.Enabled = mouseHook.IsHooked;
		}

		#endregion

	}
}
