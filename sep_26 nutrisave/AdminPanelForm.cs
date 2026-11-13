using System.Drawing.Drawing2D;

namespace sep_26_nutrisave;

public sealed class AdminPanelForm : Form
{
    private static readonly Color Forest = Color.FromArgb(36, 85, 58);
    private static readonly Color DarkText = Color.FromArgb(47, 76, 57);
    private static readonly Color MutedText = Color.FromArgb(101, 125, 108);
    private static readonly Color PageBackground = Color.FromArgb(244, 248, 245);
    private static readonly Color Border = Color.FromArgb(218, 231, 220);

    private readonly DataGridView approvalGrid = new();
    private readonly DataGridView recentGrid = new();
    private readonly TextBox searchBox = new();
    private readonly ComboBox categoryBox = new();
    private readonly ComboBox sortBox = new();
    private readonly Label totalUsersValue = new();
    private readonly Label pendingValue = new();
    private readonly Label approvedValue = new();
    private readonly Label rejectedValue = new();
    private readonly Dictionary<string, string> offerStatuses = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<AdminAction> actions = [];
    private AdminTab selectedTab = AdminTab.FoodOffers;

    public AdminPanelForm()
    {
        Text = "NutriSave+ - Admin Panel";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1120, 720);
        Size = new Size(1280, 850);
        BackColor = PageBackground;
        Font = new Font("Segoe UI", 9F);

        BuildLayout();
        RefreshView();
    }

    private void BuildLayout()
    {
        Controls.Add(BuildContent());
        Controls.Add(BuildSidebar());
    }

    private Control BuildSidebar()
    {
        Panel sidebar = new()
        {
            Dock = DockStyle.Left,
            Width = 220,
            BackColor = Color.FromArgb(232, 244, 233),
            Padding = new Padding(14, 18, 12, 12)
        };
        sidebar.Controls.Add(new Label
        {
            Text = "🌿  NutriSave+",
            Dock = DockStyle.Top,
            Height = 50,
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            ForeColor = Forest,
            TextAlign = ContentAlignment.MiddleCenter
        });

        FlowLayoutPanel menu = new()
        {
            Dock = DockStyle.Top,
            Height = 470,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(0, 14, 0, 0)
        };
        AddMenuButton(menu, "⌂  Dashboard", typeof(Form1));
        AddMenuButton(menu, "▣  Food Inventory", typeof(Form2));
        AddMenuButton(menu, "◷  Expiry Alerts", typeof(Form2));
        AddMenuButton(menu, "♨  Recipes", typeof(Form7));
        AddMenuButton(menu, "♜  Meal & Nutrition", typeof(Form3));
        AddMenuButton(menu, "◈  Medication", typeof(Form4));
        AddMenuButton(menu, "♡  Food Sharing", typeof(Form5));
        AddMenuButton(menu, "▥  Reports", typeof(Form6));
        menu.Controls.Add(CreateMenuButton("⚙  Admin Panel", true));
        sidebar.Controls.Add(menu);
        sidebar.Controls.Add(new Label
        {
            Text = "Good Food\r\nBrighter Days  ♥",
            Dock = DockStyle.Bottom,
            Height = 92,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = Forest,
            TextAlign = ContentAlignment.MiddleCenter
        });
        return sidebar;
    }

    private void AddMenuButton(FlowLayoutPanel menu, string text, Type formType)
    {
        Button button = CreateMenuButton(text, false);
        button.Click += (_, _) => OpenExistingForm(formType);
        menu.Controls.Add(button);
    }

    private static Button CreateMenuButton(string text, bool active)
    {
        Button button = new()
        {
            Text = text,
            Width = 188,
            Height = 40,
            Margin = new Padding(0, 3, 0, 3),
            Padding = new Padding(10, 0, 0, 0),
            TextAlign = ContentAlignment.MiddleLeft,
            FlatStyle = FlatStyle.Flat,
            BackColor = active ? Forest : Color.Transparent,
            ForeColor = active ? Color.White : Forest,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    private Control BuildContent()
    {
        Panel content = new() { Dock = DockStyle.Fill, Padding = new Padding(24, 20, 24, 18), AutoScroll = true };
        TableLayoutPanel layout = new() { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 1, RowCount = 5 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 360));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 230));
        layout.Controls.Add(BuildHeader(), 0, 0);
        layout.Controls.Add(BuildSummaryCards(), 0, 1);
        layout.Controls.Add(BuildTabs(), 0, 2);
        layout.Controls.Add(BuildOffersCard(), 0, 3);
        layout.Controls.Add(BuildBottomCards(), 0, 4);
        content.Controls.Add(layout);
        return content;
    }

    private Control BuildHeader()
    {
        TableLayoutPanel header = new() { Dock = DockStyle.Fill, ColumnCount = 2 };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
        Panel title = new() { Dock = DockStyle.Fill, Padding = new Padding(4, 8, 0, 0) };
        title.Controls.Add(new Label { Text = "🍃  Admin Panel  🍃", AutoSize = true, Font = new Font("Segoe UI", 25F, FontStyle.Bold), ForeColor = Color.FromArgb(26, 61, 43), Location = new Point(2, 8) });
        title.Controls.Add(new Label { Text = "Manage users, review food offers and claims, and keep the community safe.", AutoSize = true, Font = new Font("Segoe UI", 10F), ForeColor = MutedText, Location = new Point(8, 60) });
        header.Controls.Add(title, 0, 0);

        RoundedPanel mascot = new() { Dock = DockStyle.Fill, BackColor = Color.FromArgb(221, 241, 226), Padding = new Padding(14, 10, 8, 8), Margin = new Padding(8, 4, 0, 4) };
        mascot.Controls.Add(new Label { Text = "🌱", Dock = DockStyle.Right, Width = 64, Font = new Font("Segoe UI Emoji", 30F), TextAlign = ContentAlignment.MiddleCenter });
        mascot.Controls.Add(new Label { Text = "Hi, I'm Thistle!", Dock = DockStyle.Top, Height = 29, Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = DarkText });
        mascot.Controls.Add(new Label { Text = "Let's keep NutriSave+ a safe\nand supportive place for everyone!", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 8.5F), ForeColor = MutedText });
        header.Controls.Add(mascot, 1, 0);
        return header;
    }

    private Control BuildSummaryCards()
    {
        TableLayoutPanel cards = new() { Dock = DockStyle.Fill, ColumnCount = 4, Padding = new Padding(0, 4, 0, 4) };
        for (int i = 0; i < 4; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        cards.Controls.Add(CreateStatCard("👥", "Total Users", "User accounts are not available", totalUsersValue, Color.FromArgb(225, 243, 225)), 0, 0);
        cards.Controls.Add(CreateStatCard("▣", "Pending Approvals", "Existing food offers", pendingValue, Color.FromArgb(255, 239, 211)), 1, 0);
        cards.Controls.Add(CreateStatCard("✓", "Approved", "Admin actions this session", approvedValue, Color.FromArgb(220, 241, 224)), 2, 0);
        cards.Controls.Add(CreateStatCard("×", "Rejected", "Admin actions this session", rejectedValue, Color.FromArgb(255, 224, 224)), 3, 0);
        return cards;
    }

    private static Control CreateStatCard(string icon, string title, string subtitle, Label value, Color iconColor)
    {
        RoundedPanel card = new() { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(4), Padding = new Padding(10) };
        card.Controls.Add(new Label { Text = icon, Dock = DockStyle.Left, Width = 48, BackColor = iconColor, ForeColor = Forest, Font = new Font("Segoe UI Symbol", 20F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter });
        Panel text = new() { Dock = DockStyle.Fill, Padding = new Padding(10, 0, 0, 0) };
        value.Text = "—";
        value.Dock = DockStyle.Top;
        value.Height = 34;
        value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        value.ForeColor = DarkText;
        text.Controls.Add(value);
        text.Controls.Add(new Label { Text = title, Dock = DockStyle.Top, Height = 20, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = DarkText });
        text.Controls.Add(new Label { Text = subtitle, Dock = DockStyle.Top, Height = 20, Font = new Font("Segoe UI", 7.5F), ForeColor = MutedText });
        card.Controls.Add(text);
        return card;
    }

    private Control BuildTabs()
    {
        FlowLayoutPanel tabs = new() { Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(0, 5, 0, 5) };
        AddTab(tabs, AdminTab.FoodOffers, "▣  Food Offer Approvals");
        AddTab(tabs, AdminTab.Claims, "▤  Claim Approvals");
        AddTab(tabs, AdminTab.Users, "♙  User Management");
        AddTab(tabs, AdminTab.Logs, "◷  System Logs");
        return tabs;
    }

    private void AddTab(FlowLayoutPanel tabs, AdminTab tab, string text)
    {
        Button button = new() { Text = text, Width = 190, Height = 40, Margin = new Padding(2), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), Cursor = Cursors.Hand };
        button.FlatAppearance.BorderSize = 0;
        button.Click += (_, _) => { selectedTab = tab; RefreshView(); };
        tabs.Controls.Add(button);
    }

    private Control BuildOffersCard()
    {
        RoundedPanel card = new() { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(14, 10, 14, 12) };
        TableLayoutPanel layout = new() { Dock = DockStyle.Fill, RowCount = 3 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Panel heading = new();
        heading.Controls.Add(new Label { Text = "🌼  Pending Food Offers", AutoSize = true, Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = DarkText, Location = new Point(0, 0) });
        heading.Controls.Add(new Label { Text = "Review existing food offers submitted through Food Sharing.", AutoSize = true, Font = new Font("Segoe UI", 8F), ForeColor = MutedText, Location = new Point(25, 25) });
        layout.Controls.Add(heading, 0, 0);

        FlowLayoutPanel filters = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, Padding = new Padding(0, 4, 0, 4) };
        sortBox.Width = 130;
        sortBox.DropDownStyle = ComboBoxStyle.DropDownList;
        sortBox.Items.AddRange(["Latest first", "Oldest first"]);
        sortBox.SelectedIndex = 0;
        sortBox.SelectedIndexChanged += (_, _) => RefreshView();
        categoryBox.Width = 125;
        categoryBox.DropDownStyle = ComboBoxStyle.DropDownList;
        categoryBox.Items.AddRange(["All categories", "Produce", "Bakery", "Dairy"]);
        categoryBox.SelectedIndex = 0;
        categoryBox.SelectedIndexChanged += (_, _) => RefreshView();
        searchBox.Width = 210;
        searchBox.PlaceholderText = "Search food name or user...";
        searchBox.TextChanged += (_, _) => RefreshView();
        filters.Controls.Add(sortBox);
        filters.Controls.Add(categoryBox);
        filters.Controls.Add(searchBox);
        layout.Controls.Add(filters, 0, 1);

        ConfigureApprovalGrid();
        layout.Controls.Add(approvalGrid, 0, 2);
        card.Controls.Add(layout);
        return card;
    }

    private void ConfigureApprovalGrid()
    {
        approvalGrid.Dock = DockStyle.Fill;
        approvalGrid.ReadOnly = true;
        approvalGrid.AllowUserToAddRows = false;
        approvalGrid.RowHeadersVisible = false;
        approvalGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        approvalGrid.BackgroundColor = Color.White;
        approvalGrid.BorderStyle = BorderStyle.None;
        approvalGrid.EnableHeadersVisualStyles = false;
        approvalGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 244, 233);
        approvalGrid.ColumnHeadersDefaultCellStyle.ForeColor = Forest;
        approvalGrid.Columns.Add("Food", "Food");
        approvalGrid.Columns.Add("Quantity", "Quantity");
        approvalGrid.Columns.Add("Expiry", "Expiry Date");
        approvalGrid.Columns.Add("SubmittedBy", "Submitted By");
        approvalGrid.Columns.Add("Collection", "Collection Details");
        approvalGrid.Columns.Add("Status", "Status");
        approvalGrid.Columns.Add(new DataGridViewButtonColumn { Name = "Approve", HeaderText = "", Text = "✓ Approve", UseColumnTextForButtonValue = true });
        approvalGrid.Columns.Add(new DataGridViewButtonColumn { Name = "Reject", HeaderText = "", Text = "⊗ Reject", UseColumnTextForButtonValue = true });
        approvalGrid.CellContentClick += ApprovalGrid_CellContentClick;
    }

    private Control BuildBottomCards()
    {
        TableLayoutPanel bottom = new() { Dock = DockStyle.Fill, ColumnCount = 2 };
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        bottom.Controls.Add(BuildRecentActions(), 0, 0);
        bottom.Controls.Add(BuildQuickActions(), 1, 0);
        return bottom;
    }

    private Control BuildRecentActions()
    {
        RoundedPanel card = new() { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(14, 10, 14, 10), Margin = new Padding(0, 0, 6, 0) };
        card.Controls.Add(new Label { Text = "◷  Recent Actions", Dock = DockStyle.Top, Height = 28, Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = DarkText });
        card.Controls.Add(new Label { Text = "Actions performed in this Admin Panel session.", Dock = DockStyle.Top, Height = 24, Font = new Font("Segoe UI", 8F), ForeColor = MutedText });
        recentGrid.Dock = DockStyle.Fill;
        recentGrid.ReadOnly = true;
        recentGrid.AllowUserToAddRows = false;
        recentGrid.RowHeadersVisible = false;
        recentGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        recentGrid.BackgroundColor = Color.White;
        recentGrid.BorderStyle = BorderStyle.None;
        recentGrid.Columns.Add("DateTime", "Date & Time");
        recentGrid.Columns.Add("Action", "Action");
        recentGrid.Columns.Add("Item", "Item");
        recentGrid.Columns.Add("By", "By");
        card.Controls.Add(recentGrid);
        return card;
    }

    private Control BuildQuickActions()
    {
        RoundedPanel card = new() { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(14, 10, 14, 10), Margin = new Padding(6, 0, 0, 0) };
        card.Controls.Add(new Label { Text = "⚙  Quick Actions", Dock = DockStyle.Top, Height = 28, Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = DarkText });
        card.Controls.Add(new Label { Text = "Common admin tasks.", Dock = DockStyle.Top, Height = 24, Font = new Font("Segoe UI", 8F), ForeColor = MutedText });
        TableLayoutPanel actionsPanel = new() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Padding = new Padding(0, 6, 0, 0) };
        actionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        actionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        actionsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        actionsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        Button users = CreateQuickAction("♙  Manage Users\r\nUser data is not available", Color.FromArgb(225, 244, 231));
        Button offers = CreateQuickAction("▣  View All Offers\r\nBrowse existing offers", Color.FromArgb(225, 244, 231));
        Button claims = CreateQuickAction("▤  View All Claims\r\nClaim data is not available", Color.FromArgb(224, 239, 248));
        Button logs = CreateQuickAction("◷  System Logs\r\nSession actions only", Color.FromArgb(255, 241, 218));
        users.Click += (_, _) => ShowUnavailable("User management is unavailable because this project has no user or authentication data source.");
        offers.Click += (_, _) => { selectedTab = AdminTab.FoodOffers; RefreshView(); };
        claims.Click += (_, _) => { selectedTab = AdminTab.Claims; RefreshView(); };
        logs.Click += (_, _) => { selectedTab = AdminTab.Logs; RefreshView(); };
        actionsPanel.Controls.Add(users, 0, 0);
        actionsPanel.Controls.Add(offers, 1, 0);
        actionsPanel.Controls.Add(claims, 0, 1);
        actionsPanel.Controls.Add(logs, 1, 1);
        card.Controls.Add(actionsPanel);
        return card;
    }

    private static Button CreateQuickAction(string text, Color color)
    {
        Button button = new() { Text = text + "                         >", Dock = DockStyle.Fill, Margin = new Padding(3), Padding = new Padding(8), TextAlign = ContentAlignment.MiddleLeft, BackColor = color, ForeColor = Forest, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8F, FontStyle.Bold), Cursor = Cursors.Hand };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    private void RefreshView()
    {
        totalUsersValue.Text = "—";
        pendingValue.Text = Form5.FoodOffers.Count.ToString();
        approvedValue.Text = actions.Count(action => action.Action == "Approved").ToString();
        rejectedValue.Text = actions.Count(action => action.Action == "Rejected").ToString();
        PopulateApprovalGrid();
        PopulateRecentGrid();
    }

    private void PopulateApprovalGrid()
    {
        approvalGrid.Rows.Clear();
        approvalGrid.Columns[6].Visible = selectedTab == AdminTab.FoodOffers;
        approvalGrid.Columns[7].Visible = selectedTab == AdminTab.FoodOffers;
        if (selectedTab != AdminTab.FoodOffers)
        {
            approvalGrid.Columns[0].HeaderText = selectedTab == AdminTab.Claims ? "Claimant" : selectedTab == AdminTab.Users ? "User" : "Date & Time";
            approvalGrid.Columns[1].HeaderText = selectedTab == AdminTab.Claims ? "Food Offer" : selectedTab == AdminTab.Users ? "Email" : "Activity";
            approvalGrid.Columns[2].HeaderText = selectedTab == AdminTab.Claims ? "Quantity Requested" : selectedTab == AdminTab.Users ? "Role" : "By";
            approvalGrid.Columns[3].HeaderText = selectedTab == AdminTab.Claims ? "Claim Date" : selectedTab == AdminTab.Users ? "Status" : "";
            approvalGrid.Columns[4].Visible = false;
            approvalGrid.Columns[5].Visible = false;
            approvalGrid.Rows.Add("No data source", "This section is not supported by the current application model.", "", "", "", "");
            return;
        }

        approvalGrid.Columns[0].HeaderText = "Food";
        approvalGrid.Columns[1].HeaderText = "Quantity";
        approvalGrid.Columns[2].HeaderText = "Expiry Date";
        approvalGrid.Columns[3].HeaderText = "Submitted By";
        approvalGrid.Columns[4].HeaderText = "Collection Details";
        approvalGrid.Columns[5].HeaderText = "Status";
        approvalGrid.Columns[4].Visible = true;
        approvalGrid.Columns[5].Visible = true;
        IEnumerable<string> offers = Form5.FoodOffers.Where(MatchesOffer);
        if (sortBox.SelectedIndex == 1) offers = offers.Reverse();
        foreach (string offer in offers)
        {
            string status = offerStatuses.TryGetValue(offer, out string? savedStatus) ? savedStatus : "Pending";
            int row = approvalGrid.Rows.Add("🍅  " + offer, "—", "—", "—", "—", status);
            approvalGrid.Rows[row].Tag = offer;
            approvalGrid.Rows[row].Cells[5].Style.ForeColor = status == "Rejected" ? Color.Firebrick : status == "Approved" ? Color.SeaGreen : Color.DarkGoldenrod;
        }
    }

    private bool MatchesOffer(string offer)
    {
        bool searchMatches = string.IsNullOrWhiteSpace(searchBox.Text) || offer.Contains(searchBox.Text, StringComparison.OrdinalIgnoreCase);
        bool categoryMatches = categoryBox.SelectedIndex <= 0 || categoryBox.Text switch
        {
            "Produce" => Form2.FoodItems.Any(item => item.FoodName.Equals(offer, StringComparison.OrdinalIgnoreCase) && item.Category.Contains("produce", StringComparison.OrdinalIgnoreCase)),
            "Bakery" => Form2.FoodItems.Any(item => item.FoodName.Equals(offer, StringComparison.OrdinalIgnoreCase) && item.Category.Contains("bak", StringComparison.OrdinalIgnoreCase)),
            "Dairy" => Form2.FoodItems.Any(item => item.FoodName.Equals(offer, StringComparison.OrdinalIgnoreCase) && item.Category.Contains("dairy", StringComparison.OrdinalIgnoreCase)),
            _ => true
        };
        return searchMatches && categoryMatches;
    }

    private void PopulateRecentGrid()
    {
        recentGrid.Rows.Clear();
        foreach (AdminAction action in actions.AsEnumerable().Reverse().Take(8))
            recentGrid.Rows.Add(action.Timestamp.ToString("dd MMM yyyy, hh:mm tt"), action.Action, action.Item, "Admin");
    }

    private void ApprovalGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (selectedTab != AdminTab.FoodOffers || e.RowIndex < 0 || approvalGrid.Rows[e.RowIndex].Tag is not string offer) return;
        if (e.ColumnIndex == 7)
        {
            DialogResult result = MessageBox.Show(this, $"Reject the food offer '{offer}'?", "Confirm rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            UpdateOfferStatus(offer, "Rejected");
        }
        else if (e.ColumnIndex == 6)
        {
            UpdateOfferStatus(offer, "Approved");
        }
    }

    private void UpdateOfferStatus(string offer, string status)
    {
        offerStatuses[offer] = status;
        actions.Add(new AdminAction(DateTime.Now, status, offer));
        RefreshView();
    }

    private void ShowUnavailable(string message)
    {
        MessageBox.Show(this, message, "Not available", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OpenExistingForm(Type formType)
    {
        if (Activator.CreateInstance(formType) is not Form form) return;
        form.FormClosed += (_, _) => Show();
        Hide();
        form.Show();
    }

    private sealed record AdminAction(DateTime Timestamp, string Action, string Item);

    private enum AdminTab
    {
        FoodOffers,
        Claims,
        Users,
        Logs
    }

    private sealed class RoundedPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using GraphicsPath path = new();
            int radius = 12;
            int diameter = radius * 2;
            Rectangle bounds = new(0, 0, Width - 1, Height - 1);
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            using SolidBrush brush = new(BackColor);
            using Pen pen = new(Border);
            e.Graphics.FillPath(brush, path);
            e.Graphics.DrawPath(pen, path);
        }
    }
}
