# Testing Guide for Shop Management System

## Testing the API Endpoints

### 1. Using Swagger UI (Recommended for Beginners)

1. **Start the application**:
   ```bash
   cd ShopManagement.API
   dotnet run
   ```

2. **Open browser** and navigate to: `https://localhost:5001`

3. **Test Products API**:
   - Click on "Products" section
   - Try "GET /api/products" to see all products
   - Try "GET /api/products/1" to get a specific product
   - Try "POST /api/products" to create a new product

### 2. Using curl Commands

#### Get All Products
```bash
curl -X GET "https://localhost:5001/api/products" -H "accept: application/json"
```

#### Get Product by ID
```bash
curl -X GET "https://localhost:5001/api/products/1" -H "accept: application/json"
```

#### Create New Product
```bash
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -H "accept: application/json" \
  -d '{
    "name": "Gaming Mouse",
    "description": "High-precision gaming mouse",
    "price": 79.99,
    "stockQuantity": 25,
    "categoryId": 1
  }'
```

#### Update Product
```bash
curl -X PUT "https://localhost:5001/api/products/1" \
  -H "Content-Type: application/json" \
  -H "accept: application/json" \
  -d '{
    "id": 1,
    "name": "Updated Laptop",
    "description": "Updated high-performance laptop",
    "price": 1099.99,
    "stockQuantity": 45,
    "categoryId": 1
  }'
```

#### Delete Product
```bash
curl -X DELETE "https://localhost:5001/api/products/1" -H "accept: application/json"
```

#### Search Products
```bash
curl -X GET "https://localhost:5001/api/products/search?searchTerm=laptop" -H "accept: application/json"
```

#### Get Low Stock Products
```bash
curl -X GET "https://localhost:5001/api/products/low-stock?threshold=20" -H "accept: application/json"
```

#### Update Product Stock
```bash
curl -X PATCH "https://localhost:5001/api/products/1/stock" \
  -H "Content-Type: application/json" \
  -H "accept: application/json" \
  -d '10'
```

### 3. Using Postman

1. **Import Collection**: Create a new collection in Postman
2. **Set Base URL**: `https://localhost:5001`
3. **Add Requests**: Create requests for each endpoint

#### Sample Postman Requests:

**GET All Products**
- Method: GET
- URL: `{{baseUrl}}/api/products`

**POST Create Product**
- Method: POST
- URL: `{{baseUrl}}/api/products`
- Body (JSON):
```json
{
  "name": "Wireless Headphones",
  "description": "Noise-cancelling wireless headphones",
  "price": 199.99,
  "stockQuantity": 30,
  "categoryId": 1
}
```

### 4. Testing Categories API

#### Get All Categories
```bash
curl -X GET "https://localhost:5001/api/categories" -H "accept: application/json"
```

#### Create New Category
```bash
curl -X POST "https://localhost:5001/api/categories" \
  -H "Content-Type: application/json" \
  -H "accept: application/json" \
  -d '{
    "name": "Home & Garden",
    "description": "Home and garden products"
  }'
```

## Expected Responses

### Successful Product Creation (201 Created)
```json
{
  "id": 5,
  "name": "Gaming Mouse",
  "description": "High-precision gaming mouse",
  "price": 79.99,
  "stockQuantity": 25,
  "categoryId": 1,
  "category": null,
  "createdAt": "2024-01-03T10:30:00Z",
  "updatedAt": null,
  "isDeleted": false
}
```

### Error Response (400 Bad Request)
```json
{
  "statusCode": 400,
  "message": "Price cannot be negative",
  "details": "Invalid argument provided",
  "timestamp": "2024-01-03T10:30:00Z"
}
```

### Not Found Response (404 Not Found)
```json
{
  "statusCode": 404,
  "message": "Product with ID 999 not found",
  "details": "Resource not found",
  "timestamp": "2024-01-03T10:30:00Z"
}
```

## Testing Middleware

### 1. Exception Handling Middleware
Try creating a product with invalid data to see the middleware in action:

```bash
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "",
    "price": -10,
    "stockQuantity": -5
  }'
```

### 2. Request Logging Middleware
Check the console output when making requests to see the logging middleware working.

## Testing Business Logic

### 1. Stock Management
```bash
# Try to reduce stock below zero
curl -X PATCH "https://localhost:5001/api/products/1/stock" \
  -H "Content-Type: application/json" \
  -d '-1000'
```

### 2. Validation Testing
```bash
# Try to create product with negative price
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Product",
    "price": -50,
    "stockQuantity": 10,
    "categoryId": 1
  }'
```

## Common Testing Scenarios

### 1. Happy Path Testing
- Create, read, update, delete operations with valid data
- Search and filter operations
- Stock management operations

### 2. Error Path Testing
- Invalid input data
- Non-existent resource IDs
- Business rule violations
- Boundary value testing

### 3. Edge Cases
- Empty search terms
- Zero stock quantities
- Maximum/minimum values
- Special characters in names

## Troubleshooting

### Common Issues:

1. **SSL Certificate Errors**:
   - Use `curl -k` to ignore SSL certificates in development
   - Or use `http://localhost:5000` instead of `https://localhost:5001`

2. **Port Already in Use**:
   - Check if another instance is running
   - Use `netstat -an | findstr :5001` to check port usage

3. **JSON Parsing Errors**:
   - Ensure proper JSON formatting
   - Check Content-Type headers

4. **404 Errors**:
   - Verify the URL path
   - Check if the application is running
   - Ensure correct HTTP method

## Assignment Testing Checklist

When completing the assignment, test:

- [ ] All CRUD operations work correctly
- [ ] Validation rules are enforced
- [ ] Error handling works properly
- [ ] Business logic is implemented correctly
- [ ] Middleware functions as expected
- [ ] API returns appropriate HTTP status codes
- [ ] Response formats are consistent
- [ ] Logging is working
- [ ] Database operations are successful

## Performance Testing

For advanced students, consider:

1. **Load Testing**: Use tools like Apache Bench or k6
2. **Concurrent Requests**: Test multiple simultaneous requests
3. **Memory Usage**: Monitor application memory consumption
4. **Response Times**: Measure API response times

Remember: Testing is crucial for ensuring your implementation works correctly and handles edge cases properly!