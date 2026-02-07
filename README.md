# Shop Management System - .NET Core 8

A comprehensive Shop Management System demonstrating advanced .NET Core 8 concepts including Object-Oriented Programming, Middleware, Repository Pattern, Service Layer, and Dependency Injection.

## 🎯 Purpose
This project serves as a complete educational example for .NET Core 8 development, showcasing:
- **OOP Concepts**: Encapsulation, Inheritance, Polymorphism, Abstraction
- **Design Patterns**: Repository, Service Layer, Middleware
- **Dependency Injection**: Constructor injection, service registration
- **CRUD Operations**: Complete Create, Read, Update, Delete functionality
- **RESTful API**: Well-structured API endpoints with proper HTTP methods

## 🏗️ Architecture

### Clean Architecture Layers
```
┌─────────────────────────────────────┐
│           Presentation Layer        │
│         (ShopManagement.API)        │
│    Controllers, Middleware, DTOs    │
├─────────────────────────────────────┤
│          Application Layer          │
│    (ShopManagement.Infrastructure)  │
│      Services, Repositories         │
├─────────────────────────────────────┤
│            Domain Layer             │
│        (ShopManagement.Core)        │
│     Entities, Interfaces, Enums     │
└─────────────────────────────────────┘
```

## 🔧 Technologies Used
- **.NET Core 8**: Latest framework features
- **Entity Framework Core 8**: ORM for data access
- **ASP.NET Core Web API**: RESTful API development
- **Swagger/OpenAPI**: API documentation
- **In-Memory Database**: For demonstration purposes
- **Dependency Injection**: Built-in DI container

## 📁 Project Structure
```
ShopManagement/
├── ShopManagement.Core/
│   ├── Entities/                 # Domain models
│   │   ├── BaseEntity.cs        # Base class (Inheritance)
│   │   ├── Product.cs           # Product entity (Encapsulation)
│   │   ├── Category.cs          # Category entity
│   │   ├── Customer.cs          # Customer entity
│   │   ├── Order.cs             # Order entity (Composition)
│   │   └── OrderItem.cs         # Order item entity
│   └── Interfaces/              # Contracts (Abstraction)
│       ├── IRepository.cs       # Generic repository interface
│       ├── IProductRepository.cs
│       ├── ICustomerRepository.cs
│       ├── IOrderRepository.cs
│       └── IProductService.cs
├── ShopManagement.Infrastructure/
│   ├── Data/
│   │   └── ShopDbContext.cs     # EF Core context
│   ├── Repositories/            # Data access (Repository Pattern)
│   │   ├── Repository.cs        # Generic repository
│   │   ├── ProductRepository.cs
│   │   ├── CustomerRepository.cs
│   │   └── OrderRepository.cs
│   └── Services/                # Business logic (Service Layer)
│       └── ProductService.cs
└── ShopManagement.API/
    ├── Controllers/             # API endpoints
    │   ├── ProductsController.cs
    │   └── CategoriesController.cs
    ├── Middleware/              # Custom middleware
    │   ├── ExceptionHandlingMiddleware.cs
    │   └── RequestLoggingMiddleware.cs
    └── Program.cs               # Application startup
```

## 🎓 OOP Concepts Demonstrated

### 1. Encapsulation
**File**: `Product.cs`
```csharp
private decimal _price;
public decimal Price 
{ 
    get => _price;
    set
    {
        if (value < 0)
            throw new ArgumentException("Price cannot be negative");
        _price = value;
    }
}
```

### 2. Inheritance
**File**: `BaseEntity.cs` → All entities inherit common properties
```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual string GetDisplayName() { ... }
}
```

### 3. Polymorphism
**Example**: Each entity overrides `GetDisplayName()` method
```csharp
// Product.cs
public override string GetDisplayName() => $"{Name} - ${Price:F2}";

// Customer.cs  
public override string GetDisplayName() => $"{FullName} ({Email})";
```

### 4. Abstraction
**File**: Interface definitions abstract implementation details
```csharp
public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    // ... other methods
}
```

## 🔄 Design Patterns

### Repository Pattern
Abstracts data access logic, making the application testable and maintainable.

### Service Layer Pattern
Contains business logic and orchestrates repository operations.

### Middleware Pattern
Custom middleware for cross-cutting concerns like logging and exception handling.

## 💉 Dependency Injection Examples

### Service Registration
```csharp
// Program.cs
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
```

### Constructor Injection
```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
}
```

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code (optional)

### Running the Application
1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ShopManagement
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the API**
   ```bash
   cd ShopManagement.API
   dotnet run
   ```

5. **Access the application**
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001` (root URL)

## 📚 API Endpoints

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product
- `GET /api/products/category/{categoryId}` - Get products by category
- `GET /api/products/in-stock` - Get in-stock products
- `GET /api/products/low-stock?threshold=10` - Get low-stock products
- `GET /api/products/search?searchTerm=laptop` - Search products
- `PATCH /api/products/{id}/stock` - Update product stock

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

## 🧪 Sample Data
The application automatically seeds sample data:
- **Categories**: Electronics, Clothing, Books
- **Products**: Laptop, Smartphone, T-Shirt, Programming Book

## 📝 Testing the API

### Using Swagger UI
1. Navigate to `https://localhost:5001`
2. Explore available endpoints
3. Test API calls directly from the browser

### Using curl
```bash
# Get all products
curl -X GET "https://localhost:5001/api/products"

# Create a new product
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "New Product",
    "description": "Product description",
    "price": 99.99,
    "stockQuantity": 50,
    "categoryId": 1
  }'
```

## 🎯 Key Learning Points

1. **Separation of Concerns**: Each layer has a specific responsibility
2. **Dependency Inversion**: High-level modules don't depend on low-level modules
3. **Interface Segregation**: Clients shouldn't depend on interfaces they don't use
4. **Single Responsibility**: Each class has one reason to change
5. **Open/Closed Principle**: Open for extension, closed for modification

## 🔍 Code Quality Features

- **Exception Handling**: Global exception middleware
- **Logging**: Comprehensive logging throughout the application
- **Validation**: Input validation and business rule enforcement
- **Documentation**: XML comments for API documentation
- **Consistent Naming**: Following C# naming conventions
- **Error Responses**: Standardized error response format

## 🚧 Future Enhancements

- [ ] Add authentication and authorization
- [ ] Implement caching strategies
- [ ] Add unit and integration tests
- [ ] Database migrations for production
- [ ] API versioning
- [ ] Rate limiting middleware
- [ ] Health checks
- [ ] Docker containerization

## 📖 Educational Value

This project demonstrates:
- **Real-world application structure**
- **Industry best practices**
- **Scalable architecture patterns**
- **Professional code organization**
- **Comprehensive error handling**
- **API design principles**

Perfect for students learning .NET Core 8 and modern web API development!

## 🤝 Contributing

This is an educational project. Feel free to:
- Report issues
- Suggest improvements
- Add new features
- Enhance documentation

## 📄 License

This project is for educational purposes. Feel free to use it for learning and teaching .NET Core concepts.