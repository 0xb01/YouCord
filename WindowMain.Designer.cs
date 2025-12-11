namespace YouCord
{
    partial class WindowMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowMain));
            timer1 = new System.Windows.Forms.Timer(components);
            listView1 = new ListView();
            toolTip1 = new ToolTip(components);
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            TextChannelAdd = new ToolStripTextBox();
            toolStripLabel2 = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            channelToolStripMenuItem = new ToolStripMenuItem();
            deleteSelectedToolStripMenuItem = new ToolStripMenuItem();
            discordToolStripMenuItem = new ToolStripMenuItem();
            configurationToolStripMenuItem = new ToolStripMenuItem();
            testSendToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            TextLogs = new RichTextBox();
            statusStrip1 = new StatusStrip();
            LabelCountdown = new ToolStripStatusLabel();
            ProgressCountdown = new ToolStripProgressBar();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            openYouCordToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.BorderStyle = BorderStyle.FixedSingle;
            listView1.Location = new Point(8, 34);
            listView1.Name = "listView1";
            listView1.Size = new Size(649, 270);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.DrawColumnHeader += listView1_DrawColumnHeader;
            listView1.DrawItem += listView1_DrawItem;
            listView1.DrawSubItem += listView1_DrawSubItem;
            listView1.MouseClick += listView1_MouseClick;
            listView1.MouseMove += listView1_MouseMove;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, TextChannelAdd, toolStripLabel2, toolStripSeparator2, toolStripDropDownButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(669, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(60, 22);
            toolStripLabel1.Text = "YouCord";
            // 
            // TextChannelAdd
            // 
            TextChannelAdd.Alignment = ToolStripItemAlignment.Right;
            TextChannelAdd.BorderStyle = BorderStyle.FixedSingle;
            TextChannelAdd.Name = "TextChannelAdd";
            TextChannelAdd.Size = new Size(172, 25);
            TextChannelAdd.KeyDown += TextChannelAdd_KeyDown;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripLabel2.Enabled = false;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(111, 22);
            toolStripLabel2.Text = "Add Channel (URL):";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.Alignment = ToolStripItemAlignment.Right;
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { channelToolStripMenuItem, discordToolStripMenuItem, aboutToolStripMenuItem });
            toolStripDropDownButton1.Image = Properties.Resources.icon_24_cog;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(29, 22);
            // 
            // channelToolStripMenuItem
            // 
            channelToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { deleteSelectedToolStripMenuItem });
            channelToolStripMenuItem.Image = Properties.Resources.icons8_youtube_24;
            channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            channelToolStripMenuItem.Size = new Size(118, 22);
            channelToolStripMenuItem.Text = "Channel";
            // 
            // deleteSelectedToolStripMenuItem
            // 
            deleteSelectedToolStripMenuItem.Image = Properties.Resources.icon_24_trash;
            deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            deleteSelectedToolStripMenuItem.Size = new Size(164, 22);
            deleteSelectedToolStripMenuItem.Text = "Remove Selected";
            deleteSelectedToolStripMenuItem.Click += deleteSelectedToolStripMenuItem_Click;
            // 
            // discordToolStripMenuItem
            // 
            discordToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { configurationToolStripMenuItem, testSendToolStripMenuItem });
            discordToolStripMenuItem.Image = Properties.Resources.icons8_discord_24;
            discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            discordToolStripMenuItem.Size = new Size(118, 22);
            discordToolStripMenuItem.Text = "Discord";
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.Image = Properties.Resources.icon_24_cog;
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.Size = new Size(148, 22);
            configurationToolStripMenuItem.Text = "Configuration";
            configurationToolStripMenuItem.Click += configurationToolStripMenuItem_Click;
            // 
            // testSendToolStripMenuItem
            // 
            testSendToolStripMenuItem.Image = Properties.Resources.icons8_check_24;
            testSendToolStripMenuItem.Name = "testSendToolStripMenuItem";
            testSendToolStripMenuItem.Size = new Size(148, 22);
            testSendToolStripMenuItem.Text = "Test Send";
            testSendToolStripMenuItem.Click += testSendToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Image = Properties.Resources.glr;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(118, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // TextLogs
            // 
            TextLogs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextLogs.BorderStyle = BorderStyle.FixedSingle;
            TextLogs.Location = new Point(8, 307);
            TextLogs.Name = "TextLogs";
            TextLogs.Size = new Size(649, 55);
            TextLogs.TabIndex = 4;
            TextLogs.Text = "";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { LabelCountdown, ProgressCountdown });
            statusStrip1.Location = new Point(0, 368);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(669, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // LabelCountdown
            // 
            LabelCountdown.Name = "LabelCountdown";
            LabelCountdown.Size = new Size(22, 17);
            LabelCountdown.Text = "-/-";
            // 
            // ProgressCountdown
            // 
            ProgressCountdown.Name = "ProgressCountdown";
            ProgressCountdown.Size = new Size(100, 16);
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "YouCord";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { openYouCordToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(153, 48);
            // 
            // openYouCordToolStripMenuItem
            // 
            openYouCordToolStripMenuItem.Image = Properties.Resources.glr;
            openYouCordToolStripMenuItem.Name = "openYouCordToolStripMenuItem";
            openYouCordToolStripMenuItem.Size = new Size(152, 22);
            openYouCordToolStripMenuItem.Text = "Open YouCord";
            openYouCordToolStripMenuItem.Click += openYouCordToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.exit;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(152, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // WindowMain
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(669, 390);
            Controls.Add(statusStrip1);
            Controls.Add(TextLogs);
            Controls.Add(toolStrip1);
            Controls.Add(listView1);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "WindowMain";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YouCord";
            Resize += WindowMain_Resize;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private ListView listView1;
        private ToolTip toolTip1;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox TextChannelAdd;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem discordToolStripMenuItem;
        private ToolStripMenuItem configurationToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem channelToolStripMenuItem;
        private ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private RichTextBox TextLogs;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel LabelCountdown;
        private ToolStripProgressBar ProgressCountdown;
        private ToolStripMenuItem testSendToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem openYouCordToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}
