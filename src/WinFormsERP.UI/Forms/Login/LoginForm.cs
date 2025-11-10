using WinFormsERP.Core.Interfaces.Services;

namespace WinFormsERP.UI.Forms.Login;

public partial class LoginForm : Form
{
    private readonly IAuthenticationService _authService;
    private TextBox txtUsername = null!;
    private TextBox txtPassword = null!;
    private Button btnLogin = null!;
    private Label lblTitle = null!;
    private Label lblUsername = null!;
    private Label lblPassword = null!;

    public LoginForm(IAuthenticationService authService)
    {
        _authService = authService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "ERP System - Login";
        this.Size = new Size(400, 300);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        // Title
        lblTitle = new Label
        {
            Text = "WinForms ERP System",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(100, 30)
        };
        this.Controls.Add(lblTitle);

        // Username Label
        lblUsername = new Label
        {
            Text = "Username:",
            Location = new Point(50, 90),
            Size = new Size(80, 20)
        };
        this.Controls.Add(lblUsername);

        // Username TextBox
        txtUsername = new TextBox
        {
            Location = new Point(140, 87),
            Size = new Size(200, 25)
        };
        this.Controls.Add(txtUsername);

        // Password Label
        lblPassword = new Label
        {
            Text = "Password:",
            Location = new Point(50, 130),
            Size = new Size(80, 20)
        };
        this.Controls.Add(lblPassword);

        // Password TextBox
        txtPassword = new TextBox
        {
            Location = new Point(140, 127),
            Size = new Size(200, 25),
            UseSystemPasswordChar = true
        };
        this.Controls.Add(txtPassword);

        // Login Button
        btnLogin = new Button
        {
            Text = "Login",
            Location = new Point(140, 180),
            Size = new Size(200, 35),
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnLogin.Click += BtnLogin_Click;
        this.Controls.Add(btnLogin);

        // Enter key support
        this.AcceptButton = btnLogin;
    }

    private async void BtnLogin_Click(object? sender, EventArgs e)
    {
        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter username and password.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        btnLogin.Enabled = false;
        btnLogin.Text = "Logging in...";

        try
        {
            var user = await _authService.AuthenticateAsync(username, password);
            
            if (user != null)
            {
                MessageBox.Show($"Welcome, {user.FullName}!", "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Open main form
                var mainForm = Program.ServiceProvider?.GetService(typeof(Main.MainForm)) as Main.MainForm;
                if (mainForm != null)
                {
                    this.Hide();
                    mainForm.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLogin.Enabled = true;
            btnLogin.Text = "Login";
            txtPassword.Clear();
        }
    }
}
