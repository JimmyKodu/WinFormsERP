# WinFormsERP User Manual

## Table of Contents

1. [Getting Started](#getting-started)
2. [User Management](#user-management)
3. [Sales Module](#sales-module)
4. [Purchasing Module](#purchasing-module)
5. [Inventory Module](#inventory-module)
6. [Finance Module](#finance-module)
7. [Human Resources Module](#human-resources-module)
8. [Reports](#reports)
9. [System Settings](#system-settings)
10. [FAQ](#faq)

## Getting Started

### System Login

1. Launch the WinFormsERP application
2. Enter your username and password
3. Click the "Login" button

**Default Administrator Credentials:**
- Username: `admin`
- Password: `admin123`

⚠️ **Important Security Notice**: Change the default administrator password immediately after first login!

### Main Dashboard

After successful login, you'll see the main dashboard with the following menu options:

- **Sales**: Customer and order management
- **Purchasing**: Supplier and purchase order management
- **Inventory**: Product and warehouse management
- **Finance**: Accounting and financial management
- **Human Resources**: Employee management and payroll
- **System**: User management and system settings

## User Management

### Managing Users

#### Creating a New User

1. Navigate to **System → Users**
2. Click **New User** button
3. Fill in the required information:
   - Username (unique)
   - Full Name
   - Email Address
   - Phone Number
   - Initial Password
4. Select user status (Active/Inactive)
5. Click **Save**

#### Assigning Roles

1. Open the user record
2. Navigate to the **Roles** tab
3. Click **Assign Role**
4. Select one or more roles from the list
5. Click **Save**

#### Changing User Password

**For Administrators:**
1. Open the user record
2. Click **Reset Password**
3. Enter new password
4. Confirm new password
5. Click **Save**

**For Your Own Password:**
1. Navigate to **System → Change Password**
2. Enter current password
3. Enter new password
4. Confirm new password
5. Click **Change Password**

### Role Management

Roles define what users can do in the system.

#### Default Roles

- **Administrator**: Full system access
- **Sales Manager**: Sales module access
- **Purchase Manager**: Purchasing module access
- **Warehouse Manager**: Inventory module access
- **Accountant**: Finance module access
- **HR Manager**: HR module access

#### Creating Custom Roles

1. Navigate to **System → Roles**
2. Click **New Role**
3. Enter role name and description
4. Assign permissions
5. Click **Save**

## Sales Module

### Customer Management

#### Adding a New Customer

1. Navigate to **Sales → Customers**
2. Click **New Customer**
3. Fill in customer information:
   - Customer Code (auto-generated or manual)
   - Company Name
   - Contact Person
   - Phone, Email
   - Address
   - Tax ID
   - Credit Limit
4. Click **Save**

#### Viewing Customer History

1. Open customer record
2. Navigate to **Sales History** tab
3. View all orders, invoices, and payments

### Sales Order Management

#### Creating a Sales Order

1. Navigate to **Sales → Sales Orders**
2. Click **New Order**
3. Select customer from dropdown
4. Set order date and expected delivery date
5. Add products:
   - Click **Add Product**
   - Select product
   - Enter quantity
   - Set unit price
   - Apply discount if applicable
6. Review totals (Subtotal, Tax, Total)
7. Add notes if needed
8. Click **Save as Draft** or **Confirm Order**

#### Order Status Flow

- **Draft**: Order is being prepared
- **Confirmed**: Order is confirmed and ready for processing
- **Processing**: Order is being fulfilled
- **Completed**: Order has been delivered
- **Cancelled**: Order has been cancelled

#### Modifying a Sales Order

- Only orders in **Draft** status can be modified
- To change confirmed orders, contact your administrator

### Sales Analytics

View sales performance through:

1. Navigate to **Sales → Sales Analytics**
2. Select date range
3. View reports:
   - Total sales by period
   - Sales by customer
   - Sales by product
   - Sales trends

## Purchasing Module

### Supplier Management

#### Adding a New Supplier

1. Navigate to **Purchasing → Suppliers**
2. Click **New Supplier**
3. Fill in supplier information:
   - Supplier Code
   - Company Name
   - Contact Person
   - Contact Details
   - Payment Terms (days)
4. Click **Save**

### Purchase Order Management

#### Creating a Purchase Order

1. Navigate to **Purchasing → Purchase Orders**
2. Click **New Order**
3. Select supplier
4. Set order date and expected delivery date
5. Add products and quantities
6. Review totals
7. Click **Save** or **Send to Supplier**

## Inventory Module

### Product Management

#### Adding a New Product

1. Navigate to **Inventory → Products**
2. Click **New Product**
3. Fill in product details:
   - Product Code
   - Product Name
   - Description
   - Category
   - Unit of Measure (pcs, kg, m, etc.)
   - Unit Price
   - Cost Price
   - Reorder Level
   - Reorder Quantity
4. Click **Save**

### Warehouse Management

#### Managing Warehouses

1. Navigate to **Inventory → Warehouses**
2. View existing warehouses or create new ones
3. Each warehouse tracks:
   - Available stock
   - Reserved stock
   - In-transit stock

### Stock Transfers

#### Transferring Stock Between Warehouses

1. Navigate to **Inventory → Stock Transfers**
2. Click **New Transfer**
3. Select source warehouse
4. Select destination warehouse
5. Add products and quantities to transfer
6. Set transfer date
7. Click **Create Transfer**
8. Status updates:
   - **Pending**: Transfer created
   - **In Transit**: Goods are being moved
   - **Completed**: Goods received at destination

### Stock Alerts

View low stock items:

1. Navigate to **Inventory → Stock Alerts**
2. View products below reorder level
3. Click on product to create purchase order

## Finance Module

### Chart of Accounts

View and manage account structure:

1. Navigate to **Finance → Chart of Accounts**
2. View accounts by type:
   - Assets
   - Liabilities
   - Equity
   - Revenue
   - Expenses

### Journal Entries

#### Creating a Journal Entry

1. Navigate to **Finance → Journal Entries**
2. Click **New Entry**
3. Enter entry date and description
4. Add entry lines:
   - Select account
   - Enter debit or credit amount
   - Add line description
5. Ensure debits equal credits
6. Click **Save as Draft** or **Post**

### Accounts Receivable

Track customer invoices and payments:

1. Navigate to **Finance → Accounts Receivable**
2. View outstanding invoices
3. Record payments:
   - Select invoice
   - Click **Record Payment**
   - Enter payment amount and date
   - Select payment method
   - Click **Save**

### Accounts Payable

Track supplier invoices and payments:

1. Navigate to **Finance → Accounts Payable**
2. View outstanding bills
3. Process payments similarly to AR

### Financial Reports

Generate reports:

1. Navigate to **Finance → Financial Reports**
2. Select report type:
   - Balance Sheet
   - Income Statement
   - Cash Flow Statement
   - Trial Balance
3. Select date range
4. Click **Generate Report**
5. Export to PDF or Excel

## Human Resources Module

### Employee Management

#### Adding a New Employee

1. Navigate to **Human Resources → Employees**
2. Click **New Employee**
3. Fill in employee information:
   - Employee Number
   - First Name, Last Name
   - Date of Birth
   - Contact Information
   - Hire Date
   - Department
   - Position
   - Salary
4. Click **Save**

### Attendance Tracking

#### Recording Attendance

1. Navigate to **Human Resources → Attendance**
2. Select date
3. For each employee:
   - Mark status (Present, Absent, Late, Leave)
   - Record check-in time
   - Record check-out time
4. Click **Save**

#### Viewing Attendance Reports

1. Navigate to **Human Resources → Attendance**
2. Click **Reports**
3. Select employee and date range
4. View attendance summary

### Payroll Processing

#### Running Payroll

1. Navigate to **Human Resources → Payroll**
2. Click **New Payroll**
3. Select pay period (start and end date)
4. System automatically calculates:
   - Basic salary
   - Allowances
   - Deductions
   - Net salary
5. Review payroll
6. Click **Approve** to process
7. Click **Generate Pay Slips**

## Reports

### Generating Reports

Most modules include report functionality:

1. Navigate to desired module
2. Click **Reports**
3. Select report type
4. Set parameters (date range, filters, etc.)
5. Click **Generate**
6. View on screen or export

### Export Options

Reports can be exported in:
- PDF format
- Excel format
- CSV format

## System Settings

### General Settings

1. Navigate to **System → Settings**
2. Configure:
   - Company Information
   - Currency Settings
   - Tax Rates
   - Date and Time Format
   - Language Preferences

### Database Backup

1. Navigate to **System → Settings → Backup**
2. Click **Create Backup**
3. Select backup location
4. Click **Backup Now**

**Recommendation**: Schedule automatic daily backups

### System Logs

View system activity:

1. Navigate to **System → Logs**
2. Filter by:
   - Date range
   - User
   - Module
   - Action type

## FAQ

### General Questions

**Q: I forgot my password. What should I do?**
A: Contact your system administrator to reset your password.

**Q: Can I access the system from multiple computers?**
A: Yes, but only one active session per user is recommended.

**Q: How do I change my user interface language?**
A: Go to **System → Settings → General** and select your preferred language.

### Sales Questions

**Q: Can I edit a confirmed sales order?**
A: No, once confirmed, orders cannot be edited. Cancel and create a new order if needed.

**Q: How do I apply a discount to an order?**
A: Discounts can be applied at the line item level or order level when creating/editing an order.

### Inventory Questions

**Q: What happens when stock goes below reorder level?**
A: The system will flag the item in **Stock Alerts** for your attention.

**Q: Can I transfer partial quantities?**
A: Yes, you can transfer any quantity as long as it doesn't exceed available stock.

### Finance Questions

**Q: How do I correct a posted journal entry?**
A: Post a reversing entry with opposite debits and credits, then create the correct entry.

**Q: When should I run the month-end close?**
A: Run month-end close after all transactions for the month are entered and reconciled.

## Support and Training

### Getting Help

For technical support:
- Email: support@erp.com
- Phone: +1 (555) 123-4567
- Online Help: Press F1 in any screen

### Training Resources

- User Manual (this document)
- Video Tutorials: Available on company portal
- Training Sessions: Contact HR for schedule

### Tips for Best Results

1. **Regular Backups**: Backup your database daily
2. **Consistent Data Entry**: Enter transactions promptly
3. **Review Reports**: Regularly review system reports for accuracy
4. **User Training**: Ensure all users are properly trained
5. **Security**: Keep passwords confidential and change them regularly

---

© 2025 WinFormsERP. All Rights Reserved.

For the latest version of this manual, visit: http://erp.com/support/manuals
