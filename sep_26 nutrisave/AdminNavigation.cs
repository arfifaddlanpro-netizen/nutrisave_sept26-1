namespace sep_26_nutrisave;

internal static class AdminNavigation
{
    public static void Attach(Form owner, Panel sidebar)
    {
        if (sidebar.Controls.OfType<Button>().Any(button => button.Name == "btnAdminPanelFromPage"))
        {
            return;
        }

        Button adminButton = new()
        {
            Name = "btnAdminPanelFromPage",
            Text = "    Admin Panel",
            Location = new Point(3, 590),
            Size = new Size(Math.Min(218, sidebar.ClientSize.Width - 6), 48),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(221, 233, 212),
            ForeColor = Color.FromArgb(36, 95, 61),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(12, 0, 0, 0),
            UseVisualStyleBackColor = false,
            Anchor = AnchorStyles.Left | AnchorStyles.Bottom
        };
        adminButton.FlatAppearance.BorderSize = 0;
        adminButton.Click += (_, _) => OpenAdminPanel(owner);
        sidebar.Controls.Add(adminButton);
        adminButton.BringToFront();
    }

    private static void OpenAdminPanel(Form owner)
    {
        using AdminLoginForm login = new();
        if (login.ShowDialog(owner) != DialogResult.OK)
        {
            return;
        }

        AdminPanelForm adminPanel = new();
        adminPanel.FormClosed += (_, _) => owner.Show();
        owner.Hide();
        adminPanel.Show();
    }
}
