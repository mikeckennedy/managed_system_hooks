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
using System.Diagnostics;
using System.Windows.Forms;
#endregion

namespace Kennedy.ManagedHooks.SampleHookingApp
{
	public class AboutForm : System.Windows.Forms.Form
	{
		#region Member Variables

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelWeb;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelEmail;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Construction and Cleanup

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelEmail = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelWeb = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// label1
			//
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)) );
			this.label1.Location = new System.Drawing.Point( 4, 8 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 428, 32 );
			this.label1.TabIndex = 0;
			this.label1.Text = "System Hook Test Application";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// label2
			//
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point( 8, 240 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 428, 16 );
			this.label2.TabIndex = 1;
			this.label2.Text = "Copyright (C) 2004-2005 Michael Kennedy. All rights reserved.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// labelEmail
			//
			this.labelEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelEmail.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelEmail.ForeColor = System.Drawing.Color.Blue;
			this.labelEmail.Location = new System.Drawing.Point( 4, 68 );
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size( 428, 16 );
			this.labelEmail.TabIndex = 2;
			this.labelEmail.Text = "mkennedy@unitedbinary.com";
			this.labelEmail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelEmail.Click += new System.EventHandler( this.labelEmail_Click );
			//
			// label4
			//
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)) );
			this.label4.Location = new System.Drawing.Point( 4, 44 );
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size( 428, 20 );
			this.label4.TabIndex = 3;
			this.label4.Text = "Michael Kennedy";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// labelWeb
			//
			this.labelWeb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelWeb.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelWeb.ForeColor = System.Drawing.Color.Blue;
			this.labelWeb.Location = new System.Drawing.Point( 4, 88 );
			this.labelWeb.Name = "labelWeb";
			this.labelWeb.Size = new System.Drawing.Size( 428, 16 );
			this.labelWeb.TabIndex = 8;
			this.labelWeb.Text = "http://www.unitedbinary.com";
			this.labelWeb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelWeb.Click += new System.EventHandler( this.labelWeb_Click );
			//
			// label6
			//
			this.label6.Location = new System.Drawing.Point( 44, 112 );
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size( 360, 40 );
			this.label6.TabIndex = 5;
			this.label6.Text = "This application demonstrates how to use the SystemHook class in the ManagedHooks" +
				" library by creating a mouse event hook and a keyboard event hook.";
			//
			// label7
			//
			this.label7.Location = new System.Drawing.Point( 44, 160 );
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size( 360, 40 );
			this.label7.TabIndex = 6;
			this.label7.Text = "This sample differs significantly from other .NET system hook samples in that it " +
				"creates *global* system hooks whereas  most (all?) others create local thread le" +
				"vel system event hooks.";
			//
			// buttonOK
			//
			this.buttonOK.Anchor = ( (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)) );
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOK.Location = new System.Drawing.Point( 176, 208 );
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "&OK";
			//
			// AboutForm
			//
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.CancelButton = this.buttonOK;
			this.ClientSize = new System.Drawing.Size( 436, 262 );
			this.Controls.Add( this.buttonOK );
			this.Controls.Add( this.label7 );
			this.Controls.Add( this.label6 );
			this.Controls.Add( this.labelWeb );
			this.Controls.Add( this.label4 );
			this.Controls.Add( this.labelEmail );
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About System Hook Test Application";
			this.ResumeLayout( false );

		}
		#endregion

		#region Link Navigation Code

		private void Navigate(string URL)
		{
			string path = Environment.GetFolderPath( System.Environment.SpecialFolder.System );
			string app = System.IO.Path.Combine( path, "rundll32.exe" );
			string arguments = "url.dll,FileProtocolHandler \"" + URL + "\"";

			ProcessStartInfo startInfo = new ProcessStartInfo( app, arguments );
			Process.Start( startInfo );
		}

		private void labelEmail_Click(object sender, System.EventArgs e)
		{
			Navigate( "mailto:mkennedy@unitedbinary.com" );
		}

		private void labelWeb_Click(object sender, System.EventArgs e)
		{
			Navigate( labelWeb.Text );
		}

		#endregion

	}
}
