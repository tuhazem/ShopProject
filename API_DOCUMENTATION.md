# 📖 ShopApp API Detailed Documentation

This document provides a detailed technical specification for all available API endpoints in the ShopApp project.

---

## 🔐 1. Authentication (`/api/Auth`)
Handles user registration and JWT-based authentication.

### **Register User**
- **Endpoint**: `POST /api/Auth/Register`
- **Description**: Creates a new user account.
- **Request Body (`RegisterModel`)**:
  ```json
  {
    "fullName": "John Doe",
    "username": "johndoe",
    "email": "john@example.com",
    "password": "Password123!"
  }
  ```
- **Success Response (200 OK)**:
  ```json
  {
    "message": "User registered successfully",
    "isAuthenticated": true,
    "username": "johndoe",
    "email": "john@example.com",
    "roles": ["User"],
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI...",
    "expiresOn": "2026-05-18T10:00:00Z"
  }
  ```

### **Login**
- **Endpoint**: `POST /api/Auth/Login`
- **Description**: Authenticates a user and returns a JWT token.
- **Request Body (`LoginModel`)**:
  ```json
  {
    "email": "john@example.com",
    "password": "Password123!"
  }
  ```
- **Success Response (200 OK)**: Same as Register response.

---

## 📦 2. Products (`/api/Product`)
Requires `Authorization: Bearer <token>`.

### **Get All Products (Paginated)**
- **Endpoint**: `GET /api/Product`
- **Query Parameters**:
  - `PageNumber`: `int` (default: 1)
  - `PageSize`: `int` (default: 10)
  - `SearchTerm`: `string` (Optional)
  - `MinPrice`: `decimal` (Optional)
  - `MaxPrice`: `decimal` (Optional)
- **Response**:
  ```json
  {
    "items": [
      {
        "id": 1,
        "name": "Laptop",
        "description": "High-end gaming laptop",
        "price": 1500.00,
        "categoryName": "Electronics",
        "stock": 10
      }
    ],
    "pageNumber": 1,
    "totalPages": 5,
    "totalCount": 48,
    "hasPreviousPage": false,
    "hasNextPage": true
  }
  ```

### **Create Product**
- **Endpoint**: `POST /api/Product`
- **Requirement**: `Admin` role.
- **Request Body**:
  ```json
  {
    "name": "Smartphone",
    "description": "Latest model",
    "price": 800.00,
    "categoryId": 2,
    "stock": 50
  }
  ```

---

## 📂 3. Categories (`/api/Category`)

### **Get All Categories**
- **Endpoint**: `GET /api/Category`
- **Response**:
  ```json
  [
    {
      "id": 1,
      "name": "Electronics",
      "products": [
        { "id": 1, "name": "Laptop", "price": 1500.0, "stock": 10 }
      ]
    }
  ]
  ```

---

## 👥 4. Customers (`/api/Customer`)

### **Search Customer**
- **Endpoint**: `GET /api/Customer/search`
- **Parameters**: `id` (optional), `name` (optional)
- **Example**: `/api/Customer/search?name=John`
- **Response**:
  ```json
  {
    "name": "John Doe",
    "phone": "0123456789",
    "email": "john@example.com",
    "balance": 500.00
  }
  ```

### **Restore Customer**
- **Endpoint**: `POST /api/Customer/{id}/restore`
- **Description**: Reverses a soft-delete operation.

---

## 🛒 5. Orders (`/api/Order`)

### **Create Order**
- **Endpoint**: `POST /api/Order`
- **Description**: Processes a new order and updates product stock.
- **Request Body**:
  ```json
  {
    "customerId": 1,
    "items": [
      { "productId": 1, "quantity": 2 },
      { "productId": 3, "quantity": 1 }
    ]
  }
  ```
- **Success Response**:
  ```json
  {
    "message": "Order created successfully!",
    "orderId": 123
  }
  ```

---

## 📊 6. Admin & Reports (`/api/Admin` & `/api/Action`)

### **Dashboard Status**
- **Endpoint**: `GET /api/Admin/dashboard-status`
- **Requirement**: `Admin` role.
- **Response**:
  ```json
  {
    "totalSalaryToday": 4500.00,
    "totalOrderToday": 15,
    "topSellingProducts": [
      { "productName": "Laptop", "timesSold": 8 }
    ],
    "lowStockAlert": [
      { "productName": "Mouse", "currentStock": 2 }
    ]
  }
  ```

### **Download Invoice (PDF)**
- **Endpoint**: `GET /api/Action/order-invoice/{orderId}`
- **Response**: Binary stream of a PDF file.

### **Download Inventory (Excel)**
- **Endpoint**: `GET /api/Action/download-inventory`
- **Response**: Binary stream of an XLSX file.

---

## ⚠️ Error Handling
All errors return a standard problem detail format:
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Name": ["The Name field is required."]
  }
}
```
