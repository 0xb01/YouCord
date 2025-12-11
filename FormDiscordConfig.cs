namespace YouCord
{
    public partial class FormDiscordConfig : Form
    {
        public FormDiscordConfig()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            TextWebhookUrl.Text = SettingsManager.LoadWebhook();
        }

        private void ButtonApplyConfig_Click(object sender, EventArgs e)
        {
            string url = TextWebhookUrl.Text.Trim();
            SettingsManager.SaveWebhook(url);
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
