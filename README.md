# Что такое DTO
DTO (Data Transfer Object) - объект, предназначенный для передачи данных между слоями приложения.  
Пусть у нас есть какая `data`
```json
data = {
    "username" : "Arlan",
    "password" : "12345"
}
```
**DTO** позволяет сокрыть определенные данные или видоизменять их для лучшего понимания.  
То есть:
```json
// data -> DTO -> view
view = {
    "username" : "Arlan"
} 
```
