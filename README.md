# Что такое DTO
`DTO (Data Transfer Object)` - объект, предназначенный для передачи данных между слоями приложения.  
Пусть у нас есть какая `data`
```
data = {
    "username" : "Arlan",
    "password" : "12345"
}
```
**DTO** позволяет сокрыть определенные данные или видоизменять их для лучшего понимания.  
То есть:
```
// data -> DTO -> view
view = {
    "username" : "Arlan"
} 
```

# Repository Pattern and Dependency Injection
`Repository pattern` - шаблон проектирования, который разделяет бизнес-логику приложения от доступа к данным. Он предоставляет абстракцию над уровнем доступа к данным, позволяя бизнес-логике взаимодействовать с данными через простой и унифицированный интерфейс, не заботясь о конкретной реализации доступа к данным (будь то база данных, файл или веб-сервис).
> До
```c#
public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
```

> После
```c#
public interface IUserRepository
{
    User GetUserById(int id);
    void AddUser(User user);
    // ... другие методы
}

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}


public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }


    public void AddUser(User user)
    {
        _userRepository.AddUser(user);
    }
}
```
>В этом примере UserService зависит от интерфейса IUserRepository, а не от конкретной реализации UserRepository. Это позволяет легко подменять UserRepository на mock-объект при тестировании.  

`Dependency Injection, Внедрение зависимостей` - шаблон проектирования, который позволяет создавать слабосвязанные и легко тестируемые приложения. Основная идея DI заключается в том, чтобы передавать зависимости объекта извне, вместо того, чтобы объект создавал их самостоятельно. 
> Представьте, что у вас есть класс UserService, который использует EmailService для отправки email:
```c#
public class UserService
{
    private readonly EmailService _emailService = new EmailService(); // Плохо: жесткая зависимость

    public void RegisterUser(string email)
    {
        // ...
        _emailService.SendEmail(email, "Welcome!");
    }
}
```
> В этом примере UserService создает экземпляр EmailService внутри себя. Это создает жесткую зависимость между UserService и EmailService. Такой подход имеет несколько недостатков:  
> - Тестирование UserService затрудняется, так как для этого нужно настроить реальный EmailService, что может быть сложно или невозможно в тестовой среде.  
> - UserService жестко связан с конкретной реализацией EmailService. Если потребуется использовать другой сервис для отправки email, придется изменить код UserService
> - UserService нельзя использовать в других контекстах, где требуется другой EmailService.

```c#
public class UserService
{
    private readonly IEmailService _emailService; // Интерфейс

    public UserService(IEmailService emailService) // Внедрение через конструктор
    {
        _emailService = emailService;
    }

    public void RegisterUser(string email)
    {
        // ...
        _emailService.SendEmail(email, "Welcome!");
    }
}
```
> Теперь UserService зависит от интерфейса IEmailService, а не от конкретной реализации. Экземпляр IEmailService передается через конструктор.

