# ECommerceApi

ECommerceApi is a RESTful web service designed for managing an e-commerce platform. It provides endpoints for handling products, categories, customers, and orders. The system allows you to perform CRUD operations and query specific data, such as orders by a customer or products by category.

## Features

- Manage products, categories, customers, and orders.
- Perform CRUD operations via RESTful API endpoints.
- Filtered data retrieval (e.g., orders by customer, products by category).
- Swagger UI for easy API testing.

## Technologies

- .NET 7.0
- ASP.NET Core
- Swagger (Swashbuckle)
- SQLite

## Example Data for Testing

Here are some example data entries you can use to test the API endpoints using Swagger:

### Categories
```json
{
  "name": "Electronics"
}
{
  "name": "Books"
}
{
  "name": "Clothing"
}
{
  "name": "Home & Kitchen"
}
```
### Customers
```json
{
  "name": "John Doe",
  "email": "john.doe@example.com"
}
{
  "name": "Jane Smith",
  "email": "jane.smith@example.com"
}
{
  "name": "Bob Johnson",
  "email": "bob.johnson@example.com"
}
```
### Products
```json
{
  "name": "Smartphone",
  "description": "Latest model smartphone with advanced features.",
  "price": 799.99,
  "categoryId": 1
}
{
  "name": "Mystery Novel",
  "description": "A captivating mystery novel by a bestselling author.",
  "price": 14.99,
  "categoryId": 2
}
{
  "name": "Graphic T-Shirt",
  "description": "Comfortable cotton t-shirt with a cool graphic print.",
  "price": 19.99,
  "categoryId": 3
}
{
  "name": "Kitchen Blender",
  "description": "High-speed blender perfect for smoothies and soups.",
  "price": 129.99,
  "categoryId": 4
}
{
  "name": "Laptop",
  "description": "High-performance laptop suitable for gaming and work.",
  "price": 1199.99,
  "categoryId": 1
}
{
  "name": "Science Fiction Novel",
  "description": "An exciting journey through space and time.",
  "price": 19.99,
  "categoryId": 2
}
{
  "name": "Running Sneakers",
  "description": "Lightweight sneakers ideal for daily runs.",
  "price": 69.99,
  "categoryId": 3
}
{
  "name": "Coffee Maker",
  "description": "Brews coffee quickly and keeps it hot.",
  "price": 79.99,
  "categoryId": 4
}
```
### Orders
```json
{
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 1
    },
    {
      "productId": 2,
      "quantity": 2
    }
  ]
}
{
  "customerId": 2,
  "items": [
    {
      "productId": 3,
      "quantity": 3
    }
  ]
}
{
  "customerId": 3,
  "items": [
    {
      "productId": 4,
      "quantity": 1
    }
  ]
}
{
"customerId": 3,
  "items": [
    {
      "productId": 7,
      "quantity": 2
    }
  ]
}
{
  "customerId": 2,
  "items": [
    {
      "productId": 5,
      "quantity": 1
    },
    {
      "productId": 6,
      "quantity": 1
    }
  ]
}
```
