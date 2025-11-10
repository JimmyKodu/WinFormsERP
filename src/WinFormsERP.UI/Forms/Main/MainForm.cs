namespace WinFormsERP.UI.Forms.Main;

public partial class MainForm : Form
{
    private MenuStrip menuStrip = null!;
    private StatusStrip statusStrip = null!;
    private ToolStripStatusLabel statusLabel = null!;

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "WinForms ERP System - Main";
        this.Size = new Size(1200, 800);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.WindowState = FormWindowState.Maximized;

        // Menu Strip
        menuStrip = new MenuStrip();
        
        // Sales Menu
        var salesMenu = new ToolStripMenuItem("&Sales");
        salesMenu.DropDownItems.Add("Customers", null, (s, e) => OpenCustomers());
        salesMenu.DropDownItems.Add("Sales Orders", null, (s, e) => OpenSalesOrders());
        salesMenu.DropDownItems.Add("Sales Analytics", null, (s, e) => OpenSalesAnalytics());
        menuStrip.Items.Add(salesMenu);

        // Purchasing Menu
        var purchasingMenu = new ToolStripMenuItem("&Purchasing");
        purchasingMenu.DropDownItems.Add("Suppliers", null, (s, e) => OpenSuppliers());
        purchasingMenu.DropDownItems.Add("Purchase Orders", null, (s, e) => OpenPurchaseOrders());
        purchasingMenu.DropDownItems.Add("Supplier Evaluation", null, (s, e) => OpenSupplierEvaluation());
        menuStrip.Items.Add(purchasingMenu);

        // Inventory Menu
        var inventoryMenu = new ToolStripMenuItem("&Inventory");
        inventoryMenu.DropDownItems.Add("Products", null, (s, e) => OpenProducts());
        inventoryMenu.DropDownItems.Add("Warehouses", null, (s, e) => OpenWarehouses());
        inventoryMenu.DropDownItems.Add("Stock Transfers", null, (s, e) => OpenStockTransfers());
        inventoryMenu.DropDownItems.Add("Stock Alerts", null, (s, e) => OpenStockAlerts());
        menuStrip.Items.Add(inventoryMenu);

        // Finance Menu
        var financeMenu = new ToolStripMenuItem("&Finance");
        financeMenu.DropDownItems.Add("Chart of Accounts", null, (s, e) => OpenChartOfAccounts());
        financeMenu.DropDownItems.Add("Journal Entries", null, (s, e) => OpenJournalEntries());
        financeMenu.DropDownItems.Add("Accounts Receivable", null, (s, e) => OpenAccountsReceivable());
        financeMenu.DropDownItems.Add("Accounts Payable", null, (s, e) => OpenAccountsPayable());
        financeMenu.DropDownItems.Add("Financial Reports", null, (s, e) => OpenFinancialReports());
        menuStrip.Items.Add(financeMenu);

        // HR Menu
        var hrMenu = new ToolStripMenuItem("&Human Resources");
        hrMenu.DropDownItems.Add("Employees", null, (s, e) => OpenEmployees());
        hrMenu.DropDownItems.Add("Attendance", null, (s, e) => OpenAttendance());
        hrMenu.DropDownItems.Add("Payroll", null, (s, e) => OpenPayroll());
        menuStrip.Items.Add(hrMenu);

        // System Menu
        var systemMenu = new ToolStripMenuItem("S&ystem");
        systemMenu.DropDownItems.Add("Users", null, (s, e) => OpenUsers());
        systemMenu.DropDownItems.Add("Roles", null, (s, e) => OpenRoles());
        systemMenu.DropDownItems.Add("Permissions", null, (s, e) => OpenPermissions());
        systemMenu.DropDownItems.Add(new ToolStripSeparator());
        systemMenu.DropDownItems.Add("Settings", null, (s, e) => OpenSettings());
        systemMenu.DropDownItems.Add("About", null, (s, e) => OpenAbout());
        systemMenu.DropDownItems.Add(new ToolStripSeparator());
        systemMenu.DropDownItems.Add("Logout", null, (s, e) => Logout());
        systemMenu.DropDownItems.Add("Exit", null, (s, e) => this.Close());
        menuStrip.Items.Add(systemMenu);

        this.MainMenuStrip = menuStrip;
        this.Controls.Add(menuStrip);

        // Status Strip
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel("Ready");
        statusStrip.Items.Add(statusLabel);
        this.Controls.Add(statusStrip);

        // Welcome label
        var welcomeLabel = new Label
        {
            Text = "Welcome to WinForms ERP System\n\nSelect a module from the menu to begin.",
            Font = new Font("Segoe UI", 14),
            AutoSize = false,
            Size = new Size(600, 100),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };
        this.Controls.Add(welcomeLabel);
    }

    // Placeholder methods for menu items
    private void OpenCustomers() => ShowNotImplemented("Customers");
    private void OpenSalesOrders() => ShowNotImplemented("Sales Orders");
    private void OpenSalesAnalytics() => ShowNotImplemented("Sales Analytics");
    private void OpenSuppliers() => ShowNotImplemented("Suppliers");
    private void OpenPurchaseOrders() => ShowNotImplemented("Purchase Orders");
    private void OpenSupplierEvaluation() => ShowNotImplemented("Supplier Evaluation");
    private void OpenProducts() => ShowNotImplemented("Products");
    private void OpenWarehouses() => ShowNotImplemented("Warehouses");
    private void OpenStockTransfers() => ShowNotImplemented("Stock Transfers");
    private void OpenStockAlerts() => ShowNotImplemented("Stock Alerts");
    private void OpenChartOfAccounts() => ShowNotImplemented("Chart of Accounts");
    private void OpenJournalEntries() => ShowNotImplemented("Journal Entries");
    private void OpenAccountsReceivable() => ShowNotImplemented("Accounts Receivable");
    private void OpenAccountsPayable() => ShowNotImplemented("Accounts Payable");
    private void OpenFinancialReports() => ShowNotImplemented("Financial Reports");
    private void OpenEmployees() => ShowNotImplemented("Employees");
    private void OpenAttendance() => ShowNotImplemented("Attendance");
    private void OpenPayroll() => ShowNotImplemented("Payroll");
    private void OpenUsers() => ShowNotImplemented("Users");
    private void OpenRoles() => ShowNotImplemented("Roles");
    private void OpenPermissions() => ShowNotImplemented("Permissions");
    private void OpenSettings() => ShowNotImplemented("Settings");

    private void OpenAbout()
    {
        MessageBox.Show(
            "WinForms ERP System v1.0\n\n" +
            "A comprehensive enterprise resource planning system\n" +
            "for small and medium-sized businesses.\n\n" +
            "© 2025 All Rights Reserved",
            "About",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void Logout()
    {
        var result = MessageBox.Show(
            "Are you sure you want to logout?",
            "Logout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            this.Close();
        }
    }

    private void ShowNotImplemented(string feature)
    {
        MessageBox.Show(
            $"The {feature} module is under development.\n\n" +
            "This feature will be available in a future release.",
            "Feature Coming Soon",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}
