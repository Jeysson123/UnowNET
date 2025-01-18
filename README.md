# 🚀 Unow

Welcome to **Unow**! This project is designed to be a task manager. It leverages cutting-edge technologies to achieve its goals! 🎉

## 📦 Modules

### 1. **Backend** 💻
   - **Description**: The backend module is responsible for handling all business logic, managing user authentication, database operations, and providing APIs for frontend interaction. It is built with [ ASP.NET Core Web Api].
   - **Key Features**:
     - User authentication
     - Database management
     - API creation
     - API testing
       
### 2. **Frontend** 🌐
   - **Description**: The frontend module is designed to offer an intuitive and responsive user interface. It communicates with the backend to provide a seamless user experience. Built with [React].
   - **Key Features**:
     - Interactive UI
     - Real-time data updates
     - Mobile-first design
     - UI testing
---

## 🚀 Versions & Technologies Used

Here are the technologies and versions used in this project:

- **Backend**: 
  - AspNetCore.HealthChecks.SqlServe 5.0.0
  - AspNetCore.HealthChecks.UI.Client 5.0.0
  - Microsoft.AspNetCore.Mvc.Versioning 5.1.0
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets 1.11.1
  - NLog.Web.AspNetCore 5.3.15
  - Swashbuckle.AspNetCore 5.6.3
  - FluentValidation 11.11.0
  - Microsoft.EntityFrameworkCore 5.0.10
  - Microsoft.EntityFrameworkCore.SqlServer 5.0.10
  - Microsoft.EntityFrameworkCore.Design 5.0.10
  - Microsoft.EntityFrameworkCore.Tools 5.0.10
  - jose-jwt 2.4.0
  - NUnit 3.13.1
  - Docker
- **Frontend**: 
  - axios 1.7.9
  - axios-mock-adapter 2.1.0
  - jest 29.7.0
  - react 18.3.1
  - react-dom 18.3.1
  - react-redux 9.2.0
  - redux 5.0.1
  - tailwindcss 3.4.17
  - vite 6.0.5
  - Docker
    
---

## 🛠️ Installation & Setup

To set up the project locally:

1. Clone the repository:

   ```bash
   git clone https://github.com/Jeysson123/UnowNET.git
   cd UnowNET
   
2. Create docker container "sqlserver", optionally you can create your own SQL Server Instance:
   
    ```bash
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Root809!" -p 1433:1433 -d --name sqlserver mcr.microsoft.com/mssql/server:2019-latest 
    ```
3. Rebuild and install dependencies locally

   - **Backend**:
     - Open the solution with VS or VS CODE  
     - Right-click in Backend and rebuild  
   
   - **Frontend**:
     - Open the project with your favorite IDE or code editor, and execute:
   
   ```bash
   npm install
   ```

4. Run

   - **Backend**:
     - Open the solution with VS or VS CODE  
     - Start solution with IIES server
   
   - **Frontend**:
     - Open the project with your favorite IDE or code editor, and execute:
   
   ```bash
   npm run dev
   ```
   
5. Docker
   
     - **App Dockerized (Optionally)**:
         - Start sqlserver container
         - From docker desktop you can run directly unownet container
