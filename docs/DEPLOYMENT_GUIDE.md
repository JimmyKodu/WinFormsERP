# WinFormsERP Deployment Guide

## Table of Contents

1. [System Requirements](#system-requirements)
2. [Pre-Deployment Checklist](#pre-deployment-checklist)
3. [Development Build](#development-build)
4. [Production Deployment](#production-deployment)
5. [Database Setup](#database-setup)
6. [Post-Deployment Tasks](#post-deployment-tasks)
7. [Troubleshooting](#troubleshooting)

## System Requirements

### Server Requirements

**Minimum:**
- Operating System: Windows 10 / Windows Server 2016 or later
- Processor: 2 GHz dual-core
- RAM: 2 GB
- Storage: 500 MB available space
- .NET Runtime: .NET 8.0 or later

**Recommended:**
- Operating System: Windows 11 / Windows Server 2022
- Processor: 3 GHz quad-core or higher
- RAM: 4 GB or more
- Storage: 1 GB available space on SSD
- .NET Runtime: .NET 8.0 or later

### Client Workstation Requirements

**Minimum:**
- Operating System: Windows 10
- Processor: 1.6 GHz dual-core
- RAM: 2 GB
- Storage: 200 MB available space
- Display: 1024x768 resolution
- .NET Runtime: .NET 8.0 or later

**Recommended:**
- Operating System: Windows 11
- Processor: 2.4 GHz quad-core
- RAM: 4 GB or more
- Storage: 500 MB available space
- Display: 1920x1080 resolution or higher
- .NET Runtime: .NET 8.0 or later

### Network Requirements

- Local network for multi-user deployments
- Network share for centralized database (optional)
- Backup network location for automated backups

## Pre-Deployment Checklist

Before deploying the application, ensure:

- [ ] .NET 8.0 Runtime is installed on all target machines
- [ ] User accounts are created with appropriate permissions
- [ ] Database backup strategy is defined
- [ ] Network connectivity is tested
- [ ] Antivirus software is configured to allow the application
- [ ] System administrator credentials are available
- [ ] User training schedule is planned

## Development Build

### Building from Source

1. **Clone the Repository**
   ```bash
   git clone https://github.com/JimmyKodu/WinFormsERP.git
   cd WinFormsERP
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build Solution**
   ```bash
   dotnet build --configuration Debug
   ```

4. **Run Tests**
   ```bash
   dotnet test
   ```

5. **Run Application**
   ```bash
   cd src/WinFormsERP.UI
   dotnet run
   ```

### Development Environment Setup

1. Install Visual Studio 2022 or later with:
   - .NET desktop development workload
   - .NET 8.0 SDK

2. Open `WinFormsERP.sln` in Visual Studio

3. Set `WinFormsERP.UI` as startup project

4. Press F5 to run with debugging

## Production Deployment

### Option 1: Framework-Dependent Deployment (Smaller Size)

Requires .NET 8.0 Runtime on target machines.

```bash
dotnet publish src/WinFormsERP.UI/WinFormsERP.UI.csproj \
  -c Release \
  -o ./publish/framework-dependent \
  --no-self-contained
```

**Advantages:**
- Smaller deployment package (~5-10 MB)
- Faster deployment
- Automatic runtime updates

**Disadvantages:**
- Requires .NET 8.0 Runtime installation
- Runtime version compatibility needed

### Option 2: Self-Contained Deployment (Recommended)

Includes .NET Runtime in the package.

```bash
dotnet publish src/WinFormsERP.UI/WinFormsERP.UI.csproj \
  -c Release \
  -r win-x64 \
  -o ./publish/self-contained \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true
```

**Advantages:**
- No runtime installation required
- Guaranteed compatibility
- Standalone executable

**Disadvantages:**
- Larger package size (~80-100 MB)
- Manual updates for runtime fixes

### Creating Installation Package

#### Using 7-Zip

```bash
# Create deployment archive
7z a WinFormsERP-v1.0.0.zip ./publish/self-contained/*
```

#### Using Windows Installer (Advanced)

1. Install WiX Toolset v4
2. Create installer project
3. Include application files
4. Add desktop shortcut
5. Register file associations

Example WiX configuration:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<Wix xmlns="http://schemas.microsoft.com/wix/2006/wi">
  <Product Id="*" Name="WinFormsERP" Version="1.0.0" 
           Manufacturer="Your Company" UpgradeCode="PUT-GUID-HERE">
    <Package InstallerVersion="200" Compressed="yes" />
    
    <Directory Id="TARGETDIR" Name="SourceDir">
      <Directory Id="ProgramFilesFolder">
        <Directory Id="INSTALLFOLDER" Name="WinFormsERP" />
      </Directory>
    </Directory>
    
    <Feature Id="ProductFeature" Title="WinFormsERP" Level="1">
      <ComponentGroupRef Id="ProductComponents" />
    </Feature>
  </Product>
</Wix>
```

## Database Setup

### First-Time Setup

The database is automatically created on first run. Default location:
```
<ApplicationDirectory>/erp.db
```

### Centralized Database Setup

For multi-user environments:

1. **Choose Database Location**
   ```
   \\FileServer\Shared\ERPData\erp.db
   ```

2. **Update Connection String**
   
   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=\\\\FileServer\\Shared\\ERPData\\erp.db"
     }
   }
   ```

3. **Set Permissions**
   - Grant users read/write access to the database directory
   - Ensure network path is accessible

### Database Initialization

On first run, the system will:
1. Create database schema
2. Create default administrator account:
   - Username: `admin`
   - Password: `admin123`

**Security Alert:** Change the default password immediately!

### Manual Database Creation

If needed, manually initialize the database:

```bash
cd src/WinFormsERP.Data
dotnet ef database update --connection "Data Source=C:\ERPData\erp.db"
```

## Installation Steps

### Single-User Installation

1. **Extract Files**
   - Extract deployment package to: `C:\Program Files\WinFormsERP`

2. **Configure Application**
   - Review and edit `appsettings.json` if needed
   - Set database connection string

3. **Create Shortcut**
   - Right-click `WinFormsERP.UI.exe`
   - Select "Create shortcut"
   - Move shortcut to Desktop

4. **First Run**
   - Launch application
   - Login with admin credentials
   - Change default password

### Multi-User Installation

1. **Prepare Network Share**
   ```
   \\FileServer\WinFormsERP\
   ├── App\              (Application files)
   ├── Database\         (Centralized database)
   └── Backups\          (Automatic backups)
   ```

2. **Deploy Application**
   - Copy application files to `\\FileServer\WinFormsERP\App`

3. **Configure Database**
   - Update `appsettings.json` to point to network database
   - Test connection from one workstation

4. **Deploy to Workstations**
   - Create batch script for easy deployment:
   
   ```batch
   @echo off
   echo Installing WinFormsERP...
   
   REM Create local directory
   mkdir "C:\Program Files\WinFormsERP"
   
   REM Copy files from network
   xcopy "\\FileServer\WinFormsERP\App\*" "C:\Program Files\WinFormsERP\" /E /I /Y
   
   REM Create shortcut
   powershell "$s=(New-Object -COM WScript.Shell).CreateShortcut('%userprofile%\Desktop\WinFormsERP.lnk');$s.TargetPath='C:\Program Files\WinFormsERP\WinFormsERP.UI.exe';$s.Save()"
   
   echo Installation complete!
   pause
   ```

5. **Test Each Workstation**
   - Launch application
   - Verify database connectivity
   - Test basic operations

## Post-Deployment Tasks

### 1. Change Default Password

First task after installation:
1. Login as admin
2. Navigate to **System → Change Password**
3. Enter new secure password
4. Confirm and save

### 2. Create User Accounts

1. Navigate to **System → Users**
2. Create accounts for each user
3. Assign appropriate roles
4. Provide credentials to users securely

### 3. Configure Backup Schedule

**Automated Backup Script:**

```batch
@echo off
REM Daily database backup script

set SOURCE=\\FileServer\WinFormsERP\Database\erp.db
set DEST=\\FileServer\WinFormsERP\Backups\erp_%date:~-4,4%%date:~-10,2%%date:~-7,2%.db

copy "%SOURCE%" "%DEST%"

REM Delete backups older than 30 days
forfiles /p "\\FileServer\WinFormsERP\Backups" /m *.db /d -30 /c "cmd /c del @path"

echo Backup completed successfully
```

Schedule using Windows Task Scheduler:
```bash
schtasks /create /tn "ERP Daily Backup" /tr "C:\Scripts\backup_erp.bat" /sc daily /st 23:00
```

### 4. System Configuration

1. **Company Settings**
   - Navigate to **System → Settings**
   - Configure company information
   - Set currency and tax rates
   - Configure regional settings

2. **Initial Data**
   - Import or create chart of accounts
   - Set up warehouses
   - Configure departments

### 5. User Training

Schedule training sessions:
- Basic navigation and login
- Module-specific training (Sales, Inventory, etc.)
- Reporting and analytics
- Best practices

Provide documentation:
- User Manual (see `docs/USER_MANUAL.md`)
- Quick reference guides
- Video tutorials

### 6. Performance Monitoring

Monitor system performance:
- Database file size
- Response times
- User concurrency
- Error logs

Set up alerts for:
- Database size > 1 GB
- Failed login attempts
- System errors

## Troubleshooting

### Application Won't Start

**Problem:** Double-clicking executable does nothing

**Solutions:**
1. Check if .NET 8.0 Runtime is installed:
   ```bash
   dotnet --version
   ```
   If not installed, download from: https://dotnet.microsoft.com/download/dotnet/8.0

2. Check Windows Event Log for errors
3. Run from command prompt to see error messages:
   ```bash
   cd "C:\Program Files\WinFormsERP"
   WinFormsERP.UI.exe
   ```

### Database Connection Errors

**Problem:** "Unable to open database file"

**Solutions:**
1. Check database file path in `appsettings.json`
2. Verify file permissions (read/write access)
3. Ensure database directory exists
4. Check network connectivity (for network database)

**Problem:** "Database is locked"

**Solutions:**
1. Close all instances of the application
2. Check for crashed processes in Task Manager
3. Enable WAL mode (see Technical Documentation)
4. Ensure database is not open in SQLite browser

### Performance Issues

**Problem:** Application is slow

**Solutions:**
1. Check database size - consider archiving old data
2. Run database maintenance:
   ```sql
   VACUUM;
   ANALYZE;
   ```
3. Check available disk space
4. Monitor network speed (for network database)
5. Review concurrent user count

### Login Issues

**Problem:** Cannot login with admin credentials

**Solutions:**
1. Verify database was initialized properly
2. Check for typos (username is case-sensitive)
3. Reset admin password manually (contact support)

## Rollback Procedures

If deployment fails:

1. **Stop Application**
   - Close all running instances
   - Kill any hung processes

2. **Restore Previous Version**
   ```bash
   # Backup current version
   move "C:\Program Files\WinFormsERP" "C:\Program Files\WinFormsERP.failed"
   
   # Restore backup
   move "C:\Program Files\WinFormsERP.backup" "C:\Program Files\WinFormsERP"
   ```

3. **Restore Database**
   ```bash
   copy "\\FileServer\WinFormsERP\Backups\erp_YYYYMMDD.db" "\\FileServer\WinFormsERP\Database\erp.db"
   ```

4. **Test System**
   - Launch application
   - Verify functionality
   - Check data integrity

## Version Upgrades

### Planning an Upgrade

1. **Review Release Notes**
   - Check breaking changes
   - Review new features
   - Note database migrations

2. **Backup Everything**
   - Create full database backup
   - Archive current application version
   - Export critical reports

3. **Test in Development**
   - Deploy to test environment
   - Verify functionality
   - Test with sample data

### Upgrade Process

1. **Schedule Downtime**
   - Notify all users
   - Choose low-activity time
   - Allow 2-4 hours for upgrade

2. **Backup Database**
   ```bash
   copy erp.db erp_backup_pre_upgrade.db
   ```

3. **Deploy New Version**
   - Follow deployment steps above
   - Keep old version as backup

4. **Update Database**
   - Application will run migrations automatically on first start
   - Or manually run:
   ```bash
   dotnet ef database update
   ```

5. **Test System**
   - Test critical functions
   - Verify data integrity
   - Check reports

6. **Open for Users**
   - Notify users of completion
   - Monitor for issues
   - Be ready for quick rollback

## Support and Maintenance

### Regular Maintenance Tasks

**Daily:**
- Monitor system performance
- Check backup completion
- Review error logs

**Weekly:**
- Verify database integrity
- Review user activity logs
- Clear temporary files

**Monthly:**
- Update passwords (recommended)
- Review user access rights
- Archive old data
- System health check

**Quarterly:**
- Full system backup to external media
- Disaster recovery test
- Performance optimization
- Security audit

### Getting Support

**Technical Support:**
- Email: support@erp.com
- Phone: +1 (555) 123-4567
- Hours: Monday-Friday, 8 AM - 6 PM

**Documentation:**
- User Manual: `/docs/USER_MANUAL.md`
- Technical Documentation: `/docs/TECHNICAL_DOCUMENTATION.md`
- Online Help: Press F1 in application

**Community:**
- GitHub Issues: https://github.com/JimmyKodu/WinFormsERP/issues
- Discussion Forum: (Coming soon)

---

## Appendix

### A. Checklist for Go-Live

- [ ] Application deployed to all workstations
- [ ] Database accessible from all workstations
- [ ] Default admin password changed
- [ ] All user accounts created
- [ ] Roles and permissions configured
- [ ] Initial data loaded (chart of accounts, etc.)
- [ ] Backup system tested and automated
- [ ] Users trained on system
- [ ] Support contacts distributed
- [ ] Rollback plan documented
- [ ] Go-live date communicated
- [ ] Post go-live support scheduled

### B. System Architecture Diagram

```
┌─────────────────────────────────────────────────────┐
│                 Client Workstations                  │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐          │
│  │ Desktop  │  │ Desktop  │  │ Desktop  │  ...     │
│  │ Client 1 │  │ Client 2 │  │ Client 3 │          │
│  └─────┬────┘  └─────┬────┘  └─────┬────┘          │
│        │             │             │                 │
└────────┼─────────────┼─────────────┼─────────────────┘
         │             │             │
         └─────────────┴─────────────┘
                       │
                       │ Local Network
                       │
         ┌─────────────▼──────────────┐
         │    File Server / NAS        │
         │                             │
         │  ┌─────────────────────┐   │
         │  │   SQLite Database   │   │
         │  │      erp.db         │   │
         │  └─────────────────────┘   │
         │                             │
         │  ┌─────────────────────┐   │
         │  │   Backup Storage    │   │
         │  └─────────────────────┘   │
         └─────────────────────────────┘
```

### C. Default Port and Network Settings

- **Application**: Desktop application (no ports required)
- **Database**: File-based SQLite (no network ports)
- **Backup Location**: Windows file share (SMB ports 445, 139)

### D. Minimum Bandwidth Requirements

- Single User: No network required (local database)
- Multi-User: 100 Mbps LAN minimum
- Recommended: 1 Gbps LAN for 50+ users

---

© 2025 WinFormsERP. All Rights Reserved.

For questions or concerns about this deployment guide, contact the development team.
