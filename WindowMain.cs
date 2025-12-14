using System.Diagnostics;
using System.Threading;

namespace YouCord
{
    public partial class WindowMain : Form
    {
        private List<ChannelConfig> _watchedChannels = new List<ChannelConfig>();
        private Font _boldFont;

        private string _currentWebhookUrl = "";

        private System.Windows.Forms.Timer _uiTimer;
        private DateTime _nextUpdate;
        private const int UPDATE_INTERVAL_MINUTES = 30;

        public WindowMain()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _currentWebhookUrl = SettingsManager.LoadWebhook();

            SetupList();
            SetupTimer();
            LoadChannels();
            CheckForUpdates();
        }

        private void SetupList()
        {
            listView1.View = View.Details;
            listView1.OwnerDraw = true;
            listView1.FullRowSelect = true;
            listView1.CheckBoxes = true;

            _boldFont = new Font(listView1.Font, FontStyle.Bold);

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 60);
            listView1.SmallImageList = imgList;

            listView1.Columns.Clear();
            listView1.Columns.Add("Channel", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("Latest Video", 320, HorizontalAlignment.Center);
            listView1.Columns.Add("Uploaded", 120, HorizontalAlignment.Center);

            System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp?.SetValue(listView1, true, null);
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(e.Header.Text, listView1.Font, Brushes.Black, e.Bounds, sf);
            }
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (!(e.Item.Tag is ChannelConfig data)) return;

            Brush bgBrush = e.Item.Selected ? SystemBrushes.Highlight : SystemBrushes.Window;
            Brush textBrush = e.Item.Selected ? SystemBrushes.HighlightText : Brushes.Black;
            Brush secBrush = e.Item.Selected ? SystemBrushes.HighlightText : Brushes.Gray;

            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            var sfLeft = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter };
            var sfCenter = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

            if (e.ColumnIndex == 0)
            {
                Point checkPos = new Point(e.Bounds.X + 2, e.Bounds.Y + (e.Bounds.Height - 14) / 2);
                CheckBoxRenderer.DrawCheckBox(e.Graphics, checkPos, e.Item.Checked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                if (data.ChannelAvatarImg != null)
                {
                    e.Graphics.DrawImage(data.ChannelAvatarImg, e.Bounds.X + 25, e.Bounds.Y + 10, 40, 40);
                }
                Rectangle textRect = new Rectangle(e.Bounds.X + 70, e.Bounds.Y, e.Bounds.Width - 70, e.Bounds.Height);
                e.Graphics.DrawString(data.ChannelName, _boldFont, textBrush, textRect, sfLeft);
            }
            else if (e.ColumnIndex == 1)
            {
                if (data.VideoThumbnailImg != null) e.Graphics.DrawImage(data.VideoThumbnailImg, e.Bounds.X + 5, e.Bounds.Y + 5, 89, 50);
                Rectangle textRect = new Rectangle(e.Bounds.X + 100, e.Bounds.Y, e.Bounds.Width - 100, e.Bounds.Height);
                e.Graphics.DrawString(data.VideoTitle, _boldFont, textBrush, textRect, sfLeft);
            }
            else
            {
                DateTime date = DateTimeOffset.FromUnixTimeSeconds(data.UploadTimestamp).LocalDateTime;
                e.Graphics.DrawString(date.ToString("yyyy-MM-dd HH:mm"), _boldFont, secBrush, e.Bounds, sfCenter);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            var info = listView1.HitTest(e.X, e.Y);
            var item = info.Item;

            if (item != null)
            {
                if (e.X < 24)
                {
                    info.Item.Checked = !info.Item.Checked;
                    listView1.Invalidate();
                    return;
                }

                int currentX = 0;
                int colIndex = -1;

                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    if (e.X >= currentX && e.X < currentX + listView1.Columns[i].Width)
                    {
                        colIndex = i;
                        break;
                    }
                    currentX += listView1.Columns[i].Width;
                }

                if (item.Tag is ChannelConfig data)
                {
                    try
                    {
                        switch (colIndex)
                        {
                            case 0: // Channel
                                string channel = $"https://www.youtube.com/channel/{data.ChannelId}";
                                Process.Start(new ProcessStartInfo { FileName = channel, UseShellExecute = true });
                                break;
                            case 1: // Video
                                Process.Start(new ProcessStartInfo { FileName = data.VideoUrl, UseShellExecute = true });
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not open browser.");
                    }
                }
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            var info = listView1.HitTest(e.X, e.Y);
            if (info.Item != null && info.Item.Tag is ChannelConfig data)
            {
                int colIndex = info.Item.SubItems.IndexOf(info.SubItem);
                string tip = (colIndex == 1) ? data.VideoTitle : data.ChannelName;

                if (listView1.Tag?.ToString() != tip)
                {
                    toolTip1.SetToolTip(listView1, tip);
                    listView1.Tag = tip;
                }
            }
            else
            {
                toolTip1.SetToolTip(listView1, null);
                listView1.Tag = null;
            }
        }

        private async void LoadChannels()
        {
            Log("Loading saved channels...");
            _watchedChannels = YouTubeManager.LoadConfig();

            foreach (var ch in _watchedChannels)
            {
                await YouTubeManager.ReloadImagesAsync(ch);
                AddItemToUI(ch);
            }
            Log($"Loaded!");
        }

        private void AddItemToUI(ChannelConfig config)
        {
            var item = new ListViewItem();
            item.Tag = config;
            item.SubItems.Add("");
            item.SubItems.Add("");
            listView1.Items.Add(item);
        }

        private void SetupTimer()
        {
            _nextUpdate = DateTime.Now.AddMinutes(UPDATE_INTERVAL_MINUTES);

            ProgressCountdown.Minimum = 0;
            ProgressCountdown.Maximum = UPDATE_INTERVAL_MINUTES * 60;
            ProgressCountdown.Value = ProgressCountdown.Maximum;

            _uiTimer = new System.Windows.Forms.Timer();
            _uiTimer.Interval = 1000; // 1 second
            _uiTimer.Tick += UiTimer_Tick;
            _uiTimer.Start();
        }

        private async Task CheckForUpdates()
        {
            bool anyUpdates = false;

            for (int i = 0; i < _watchedChannels.Count; i++)
            {
                var current = _watchedChannels[i];
                var latest = await YouTubeManager.GetChannelInfoAsync(current.ChannelId);

                if (latest != null && latest.VideoId != current.VideoId)
                {
                    Log($"New Upload: {latest.ChannelName} - '{latest.VideoTitle}'");

                    if (!string.IsNullOrEmpty(_currentWebhookUrl))
                    {
                        Log($"Sending Discord notification for {latest.ChannelName}...");
                        await YouTubeManager.SendDiscordNotificationAsync(latest);
                    }
                    else
                    {
                        Log("No Discord webhook set. Skipping notification.");
                    }

                    _watchedChannels[i] = latest;

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Tag is ChannelConfig c && c.ChannelId == current.ChannelId)
                        {
                            item.Tag = latest;
                            break;
                        }
                    }
                    anyUpdates = true;
                }
            }

            if (anyUpdates)
            {
                YouTubeManager.SaveConfig(_watchedChannels);
                listView1.Invalidate();
            }
        }

        private async void UiTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan remaining = _nextUpdate - DateTime.Now;

            if (remaining.TotalSeconds > 0)
            {
                LabelCountdown.Text = $"Next update in: {remaining:mm\\:ss}";

                int secondsLeft = (int)remaining.TotalSeconds;
                if (secondsLeft >= 0 && secondsLeft <= ProgressCountdown.Maximum)
                {
                    ProgressCountdown.Value = secondsLeft;
                }
            }
            else
            {
                _uiTimer.Stop();
                LabelCountdown.Text = "Checking for new videos...";

                await CheckForUpdates();

                _nextUpdate = DateTime.Now.AddMinutes(UPDATE_INTERVAL_MINUTES);
                ProgressCountdown.Value = ProgressCountdown.Maximum;
                _uiTimer.Start();
            }
        }

        private void Log(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            TextLogs.AppendText($"[{time}] {message}\n");

            TextLogs.SelectionStart = TextLogs.Text.Length;
            TextLogs.ScrollToCaret();
        }

        private async void TextChannelAdd_KeyDown(object sender, KeyEventArgs e)
        {
            string input = TextChannelAdd.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            Log($"Adding channel: {input}...");

            try
            {
                string channelId = await YouTubeManager.GetChannelIdFromUrlAsync(input);

                if (string.IsNullOrEmpty(channelId))
                {
                    Log("Error: Could not extract Channel ID.");
                    MessageBox.Show("Invalid URL.");
                    return;
                }

                if (_watchedChannels.Any(c => c.ChannelId == channelId))
                {
                    Log("Error: Channel already exists in list.");
                    MessageBox.Show("Channel already added.");
                    return;
                }

                var newConfig = await YouTubeManager.GetChannelInfoAsync(channelId);
                if (newConfig != null)
                {
                    _watchedChannels.Add(newConfig);
                    YouTubeManager.SaveConfig(_watchedChannels);
                    AddItemToUI(newConfig);

                    Log($"Channel added: {newConfig.ChannelName}");
                    TextChannelAdd.Clear();
                }
                else
                {
                    Log("Error: Failed to fetch channel details.");
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
            }
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var itemsToDelete = listView1.CheckedItems.Cast<ListViewItem>().ToList();

            if (itemsToDelete.Count == 0 && listView1.SelectedItems.Count > 0)
            {
                itemsToDelete = listView1.SelectedItems.Cast<ListViewItem>().ToList();
            }

            if (itemsToDelete.Count == 0)
            {
                MessageBox.Show("Please check or select channels to delete.");
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete {itemsToDelete.Count} channel(s)?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                foreach (var item in itemsToDelete)
                {
                    if (item.Tag is ChannelConfig config)
                    {
                        _watchedChannels.RemoveAll(x => x.ChannelId == config.ChannelId);
                        listView1.Items.Remove(item);
                        Log($"Deleted Channel: {config.ChannelName}");
                    }
                }
                YouTubeManager.SaveConfig(_watchedChannels);
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDiscordConfig f = new FormDiscordConfig();
            if (f.ShowDialog() == DialogResult.OK)
            {
                _currentWebhookUrl = SettingsManager.LoadWebhook();
                Log("Global Webhook updated.");
            }
        }

        private async void testSendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                Log("Please select a channel from the list first.");
                return;
            }

            if (string.IsNullOrEmpty(_currentWebhookUrl))
            {
                Log("Test Failed: No Discord Webhook found.");
                return;
            }

            var item = listView1.SelectedItems[0];
            if (item.Tag is ChannelConfig config)
            {
                Log($"Testing Discord trigger for '{config.ChannelName}'...");

                try
                {
                    await YouTubeManager.SendDiscordNotificationAsync(config);
                    Log("Test notification sent successfully.");
                }
                catch (Exception ex)
                {
                    Log($"Test Error: {ex.Message}");
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("YouCord\n\nA simple YouTube to Discord notifier.\n\nDeveloped by Banikaz.", "About YouCord", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WindowMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void openYouCordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
