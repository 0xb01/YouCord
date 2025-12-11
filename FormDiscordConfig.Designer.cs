namespace YouCord
{
    partial class FormDiscordConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiscordConfig));
            groupDiscordConfig = new GroupBox();
            ButtonApplyConfig = new Button();
            TextWebhookUrl = new TextBox();
            groupDiscordConfig.SuspendLayout();
            SuspendLayout();
            // 
            // groupDiscordConfig
            // 
            groupDiscordConfig.Controls.Add(ButtonApplyConfig);
            groupDiscordConfig.Controls.Add(TextWebhookUrl);
            groupDiscordConfig.Location = new Point(12, 12);
            groupDiscordConfig.Name = "groupDiscordConfig";
            groupDiscordConfig.Size = new Size(352, 76);
            groupDiscordConfig.TabIndex = 0;
            groupDiscordConfig.TabStop = false;
            groupDiscordConfig.Text = "Please enter your Discord Webhook URL (Channel):";
            // 
            // ButtonApplyConfig
            // 
            ButtonApplyConfig.Location = new Point(6, 47);
            ButtonApplyConfig.Name = "ButtonApplyConfig";
            ButtonApplyConfig.Size = new Size(340, 23);
            ButtonApplyConfig.TabIndex = 1;
            ButtonApplyConfig.Text = "Apply";
            ButtonApplyConfig.UseVisualStyleBackColor = true;
            ButtonApplyConfig.Click += ButtonApplyConfig_Click;
            // 
            // TextWebhookUrl
            // 
            TextWebhookUrl.Location = new Point(6, 19);
            TextWebhookUrl.Name = "TextWebhookUrl";
            TextWebhookUrl.Size = new Size(340, 22);
            TextWebhookUrl.TabIndex = 0;
            // 
            // FormDiscordConfig
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 100);
            Controls.Add(groupDiscordConfig);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormDiscordConfig";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YouCord: Discord Configuration";
            groupDiscordConfig.ResumeLayout(false);
            groupDiscordConfig.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupDiscordConfig;
        private Button ButtonApplyConfig;
        private TextBox TextWebhookUrl;
    }
}