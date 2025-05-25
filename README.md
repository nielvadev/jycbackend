# 🧩 JYC Backend API

API desarrollada en .NET cómo soporte de prueba técnica para Misión Empresarial

---

## ✅ Requisitos previos

Asegúrate de tener instalado:

-   [.NET 6 SDK o superior](https://dotnet.microsoft.com/en-us/download)
-   Visual Studio 2022 o Visual Studio Code
-   SQL Server

---

## ⚙️ Configuración local

### 1. Clona el repositorio

```bash
git clone https://github.com/nielvadev/jycbackend.git
cd jycbackend
```

### 2. Restaura los paquetes NuGet

```bash
dotnet restore
```

### 3. Configura el entorno

Crea un archivo llamado appsettings.json en la raíz del proyecto (este archivo está ignorado por Git por seguridad):

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=JyCDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      },
      "Https": {
        "Url": "https://localhost:5001"
      }
    }
  }
}
```

### 4. Ejecuta la aplicación

```bash
dotnet run
```

Por defecto estará disponible en:

https://localhost:5001

http://localhost:5000

### 5. Ver la documentación Swagger

```bash
https://localhost:5001/swagger
```

---

## Estructura del proyecto

```bash
│   Program.cs
│   Startup.cs
│
├───Controllers
│       ClientsController.cs
│       DeliveriesController.cs
│       OrdersController.cs
│       ProductsController.cs
│
├───DTOs
|       ApiResponse.cs
│       ClientDto.cs
│       DeliveryDto.cs
│       OrderDto.cs
│       ProductDto.cs
├───Models
│       Client.cs
│       Delivery.cs
│       Order.cs
│       OrderDetail.cs
│       Product.cs
│
├───Data
│       AppDbContext.cs
│
```

---

## 📜 Base de datos

El proyecto utiliza SQL Server como base de datos.

### 1. Crea la base de datos

```sql


CREATE DATABASE JyCDB;
GO

USE JyCDB;
GO

-- Tabla Clients
CREATE TABLE Clients (
Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
ClientDoc NVARCHAR(50) NOT NULL,
FirstName NVARCHAR(100) NOT NULL,
LastName NVARCHAR(100) NOT NULL,
Email NVARCHAR(150) UNIQUE NOT NULL,
Phone NVARCHAR(20),
Address NVARCHAR(200),
CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Tabla Products
CREATE TABLE Products (
Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
Name NVARCHAR(100) NOT NULL,
Description NVARCHAR(500),
Price DECIMAL(18,2) NOT NULL,
Stock INT NOT NULL DEFAULT 0,
CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Tabla Orders
CREATE TABLE Orders (
Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
ClientId UNIQUEIDENTIFIER NOT NULL,
OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
EstimatedDeliveryDate DATETIME NULL,
Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
FOREIGN KEY (ClientId) REFERENCES Clients(Id)
);
GO

-- Tabla OrderDetails
CREATE TABLE OrderDetails (
Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
OrderId UNIQUEIDENTIFIER NOT NULL,
ProductId UNIQUEIDENTIFIER NOT NULL,
Quantity INT NOT NULL,
UnitPrice DECIMAL(18,2) NOT NULL,
FOREIGN KEY (OrderId) REFERENCES Orders(Id),
FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
GO

-- Tabla Deliveries
CREATE TABLE Deliveries (
Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
OrderId UNIQUEIDENTIFIER NOT NULL UNIQUE,
DeliveryDate DATETIME NULL,
Delivered BIT NOT NULL DEFAULT 0,
Observations NVARCHAR(500),
FOREIGN KEY (OrderId) REFERENCES Orders(Id)
);
GO
```

