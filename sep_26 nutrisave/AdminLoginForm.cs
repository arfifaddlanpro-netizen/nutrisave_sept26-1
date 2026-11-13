namespace sep_26_nutrisave;

public sealed class AdminLoginForm : Form
{
    private const string DemoUsername = "admin";
    private const string DemoPassword = "admin123";

    private readonly TextBox usernameBox = new();
    private readonly TextBox passwordBox = new();
    private readonly Label errorLabel = new();

    public AdminLoginForm()
    {
        Text = "NutriSave+ - Admin Login";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(430, 360);
        BackColor = Color.FromArgb(244, 248, 245);
        Font = new Font("Segoe UI", 9F);

        BuildLayout();
    }

    private void BuildLayout()
    {
        Panel card = new()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(34, 24, 34, 24)
        };

        Label logo = new()
        {
            Text = "🌿  NutriSave+",
            Dock = DockStyle.Top,
            Height = 38,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(36, 85, 58),
            TextAlign = ContentAlignment.MiddleCenter
        };
        card.Controls.Add(logo);

        Label title = new()
        {
            Text = "Admin Panel Login",
            Dock = DockStyle.Top,
            Height = 34,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            ForeColor = Color.FromArgb(47, 76, 57),
            TextAlign = ContentAlignment.MiddleCenter
        };
        card.Controls.Add(title);

        Label subtitle = new()
        {
            Text = "Sign in to manage community activity safely.",
            Dock = DockStyle.Top,
            Height = 34,
            ForeColor = Color.FromArgb(101, 125, 108),
            TextAlign = ContentAlignment.MiddleCenter
        };
        card.Controls.Add(subtitle);

        TableLayoutPanel fields = new()
        {
            Dock = DockStyle.Top,
            Height = 104,
            ColumnCount = 2,
            RowCount = 2,
            Padding = new Padding(16, 8, 16, 0)
        };
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

        fields.Controls.Add(new Label { Text = "Username", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.FromArgb(47, 76, 57) }, 0, 0);
        usernameBox.Dock = DockStyle.Fill;
        usernameBox.PlaceholderText = "Enter username";
        fields.Controls.Add(usernameBox, 1, 0);
        fields.Controls.Add(new Label { Text = "Password", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.FromArgb(47, 76, 57) }, 0, 1);
        passwordBox.Dock = DockStyle.Fill;
        passwordBox.UseSystemPasswordChar = true;
        passwordBox.PlaceholderText = "Enter password";
        passwordBox.KeyDown += PasswordBox_KeyDown;
        fields.Controls.Add(passwordBox, 1, 1);
        card.Controls.Add(fields);

        errorLabel.Text = string.Empty;
        errorLabel.Dock = DockStyle.Top;
        errorLabel.Height = 30;
        errorLabel.ForeColor = Color.Firebrick;
        errorLabel.TextAlign = ContentAlignment.MiddleCenter;
        card.Controls.Add(errorLabel);

        FlowLayoutPanel buttons = new()
        {
            Dock = DockStyle.Top,
            Height = 48,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false,
            Padding = new Padding(16, 6, 16, 0)
        };
        Button loginButton = CreateButton("Login", Color.FromArgb(36, 85, 58), Color.White);
        loginButton.Click += LoginButton_Click;
        Button cancelButton = CreateButton("Cancel", Color.FromArgb(236, 243, 237), Color.FromArgb(36, 85, 58));
        cancelButton.DialogResult = DialogResult.Cancel;
        buttons.Controls.Add(loginButton);
        buttons.Controls.Add(cancelButton);
        card.Controls.Add(buttons);

        AcceptButton = loginButton;
        CancelButton = cancelButton;
        Controls.Add(card);
    }

    private static Button CreateButton(string text, Color backColor, Color foreColor)
    {
        Button button = new()
        {
            Text = text,
            Width = 100,
            Height = 32,
            BackColor = backColor,
            ForeColor = foreColor,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Margin = new Padding(5, 0, 0, 0)
        };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    private void PasswordBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            LoginButton_Click(this, EventArgs.Empty);
            e.SuppressKeyPress = true;
        }
    }

    private void LoginButton_Click(object? sender, EventArgs e)
    {
        if (string.Equals(usernameBox.Text.Trim(), DemoUsername, StringComparison.OrdinalIgnoreCase) && passwordBox.Text == DemoPassword)
        {
            DialogResult = DialogResult.OK;
            return;
        }

        errorLabel.Text = "Invalid admin username or password.";
        passwordBox.Clear();
        passwordBox.Focus();
    }
}
