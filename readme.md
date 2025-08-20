# BasicStockApp - Installation and Deployment Guide

---

## 1. Download and Extract Package

- Download the `API.zip` file from the [BasicStockApp Releases](https://github.com/Y-E-Projects/BasicStockApp/releases) page.
- Extract the archive to a directory of your choice (e.g., `C:\Publish`).

---

## 2. IIS (Internet Information Services) Installation and Configuration

- Open **“Turn Windows features on or off”** from the Start menu search.  
- Enable **Internet Information Services (IIS)** components.  
- Ensure IIS is properly installed and configured.

---

## 3. MySQL Server Installation and Configuration

- Download the latest `mysql-installer-community-x.x.x.x.msi` from the [MySQL Community Installer](https://dev.mysql.com/downloads/installer/) page.  
- Follow the installation steps by referring to this [video tutorial](https://www.youtube.com/watch?v=v8i2NgiM5pE&ab_channel=GeekyScript).  
  - Important: You may create a dedicated database user or use the root credentials for database access.  
- After installation, restart your system.

---

## 4. Define Website in IIS

- Launch **Internet Information Services (IIS) Manager** from the Start menu.  
- Right-click on the **Sites** node in the left panel and select **Add Website**.  
- In the dialog:  
  - Enter a **Site Name** using English characters only, without spaces.  
  - Set the **Physical Path** to the extracted publish directory (e.g., `C:\Publish`).  
- Verify the folder permissions: right-click the directory, navigate to **Properties > Security**, and ensure the **IIS_IUSRS** user has appropriate access; add it if necessary.  
- Configure the binding type to **https**.  
- Leave the IP address field empty and confirm the setup.

---

## 5. Application Configuration (`appsettings.json`)

- Open the `appsettings.json` file located in the publish directory with a text editor.  
- Update the `Database` field under `DefaultConnection` with the database name, using English letters without spaces or special characters.  
- Input your MySQL credentials in the `User` and `Password` fields:  
  - If you have not created a custom user, use `"root"` for `User` and your root password for `Password`.  
- Set the `DefaultCulture` parameter according to your preferred response language:  
  - Turkish: `"tr-TR"`  
  - English: `"en-US"`  
- Save the changes.

---

## 6. Application Startup and Initial Database Migration

- Execute `API.exe` within the publish directory.  
- Monitor the console for Entity Framework Core migration logs similar to the following:

Executed DbCommand (6ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
CREATE DATABASE DenemeStockAppDb
...
Applying migration '20250812141150_mig1'.
...
Application started. Press Ctrl+C to shut down.

- These logs confirm successful database creation and schema application.

---

## 7. Final Steps and Testing

- After setup, verify application availability by navigating to `https://localhost` in your web browser.  
- For changes in response language or database settings:  
  - Restart the `API.exe` application.  
  - Restart the IIS site hosting the application.  
  - For language changes, restarting IIS multiple times (2-3) may be required for the update to take effect.

---

### Important Notes:

- Changes to database connection strings and application language require restarting both IIS and the application for changes to take effect.  
- Proper configuration of IIS user permissions is essential for application access to necessary files and folders.  
- It is strongly recommended to configure HTTPS with a valid SSL certificate for secure communications.

---

This guide is designed to facilitate a smooth and professional deployment and initial execution of the BasicStockApp application.  
For support or further inquiries, please contact the development team.
