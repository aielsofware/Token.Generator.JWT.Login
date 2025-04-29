**ESPAÑOL** 🇪🇸

¡Bienvenido a la  
**Guía Dinámica de la Librería de Generación de Tokens JWT** 🚀

---

## 🚦 Introducción

En el mundo del desarrollo web, la **seguridad** y la **autenticación** son esenciales. Con esta librería, podrás generar y gestionar tokens JWT de forma sencilla, segura y con un flujo totalmente integrado en tu proyecto .NET. ¡Descubre a continuación cómo aprovecharla al máximo!

---

## 🤔 ¿Qué es un Token JWT?

<details>
  <summary>▶️ Ver definición</summary>

  Un **JSON Web Token (JWT)** es un estándar abierto (RFC 7519) que define un formato compacto y auto-contenible para transmitir información de forma segura como un objeto JSON.

  - 🔐 **Firma digital**: Garantiza la integridad y autenticidad.  
  - 🕒 **Expiración**: Incluye un tiempo de vida para evitar usos indebidos.  
  - 📦 **Auto-contenible**: Transporta tanto los datos como la firma.  

</details>

---

## ⚙️ Instalación

<details>
  <summary>▶️ Mostrar comando</summary>

1. Abre la **Consola de Administrador de Paquetes** en Visual Studio.  
2. Ejecuta:

   ```powershell
   Install-Package Token.Generator.JWT.Login
   ```

3. ¡Listo! Ya tienes la librería disponible en tu proyecto.

</details>

---

## 🔧 Configuración

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Registra tu servicio de generación de tokens como singleton
    services.AddSingleton<ITokenService, TokenService>();

    // 🚀 Otros servicios de tu aplicación...
}
```

> 💡 **Tip**: Ajusta el ciclo de vida del servicio según tus necesidades (Scoped o Transient).

---

## 🏷️ Uso de la Librería

La librería expone tres métodos principales. Cada uno responde a un nivel de requerimiento distinto:

### 1. ✨ BasicToken

- **Duración**: 30 minutos  
- **Uso**: Sin parámetros extras  

```csharp
public string GenerateBasicToken()
{
    var tokenService = new TokenService();
    return tokenService.BasicToken();
}
```

---

### 2. ⚡ ProToken

- **Incluye**: issuer, audience y clientUsername  

```csharp
public string GenerateProToken(
    string issuer,
    string audience,
    string clientUsername)
{
    var tokenService = new TokenService();
    return tokenService.ProToken(
        issuer,
        audience,
        clientUsername);
}
```

---

### 3. 🌟 PremiumTokenAsync

- **Duración**: 30 días  
- **Requiere**: clave válida verificada desde repositorio  

```csharp
public async Task<string> GeneratePremiumTokenAsync(
    string key,
    string issuer,
    string audience,
    string clientUsername)
{
    var tokenService = new TokenService();
    return await tokenService.PremiumTokenAsync(
        key,
        issuer,
        audience,
        clientUsername);
}
```

---

## 🔄 Ejemplo Completo en ASP.NET Core

```csharp
[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    // 🎫 Token Básico
    [HttpGet("basic")]
    public IActionResult GetBasicToken()
    {
        var token = _tokenService.BasicToken();
        return Ok(new { Token = token });
    }

    // 🎫 Token Pro
    [HttpGet("pro")]
    public IActionResult GetProToken(
        string issuer,
        string audience,
        string clientUsername)
    {
        var token = _tokenService.ProToken(
            issuer,
            audience,
            clientUsername);
        return Ok(new { Token = token });
    }

    // 🎫 Token Premium
    [HttpGet("premium")]
    public async Task<IActionResult> GetPremiumTokenAsync(
        string key,
        string issuer,
        string audience,
        string clientUsername)
    {
        var token = await _tokenService.PremiumTokenAsync(
            key,
            issuer,
            audience,
            clientUsername);
        return Ok(new { Token = token });
    }
}
```

---

## 📞 Soporte y Contribución

- 🔗 **Repositorio & Portfolio**: Visita mi portfolio para más detalles y ejemplos.  
- 🐛 **Bugs / Requests**: Crea un _issue_ en GitHub o contáctame por email.  

---

**ENGLISH** 🇬🇧

Welcome to the 🚀 **Dynamic JWT Token Generation Library Guide** 🌟

---

## 🚦 Introduction

In the fast-paced world of web development, **security** and **authentication** are non-negotiable. This library makes it effortless to generate and manage JWTs in your .NET projects—securely and efficiently. Let’s dive in!

---

## 🤔 What Is a JWT?

<details>
  <summary>▶️ Show definition</summary>

  A **JSON Web Token (JWT)** is an open standard (RFC 7519) for transmitting claims between parties in a compact, self-contained JSON object.

  - 🔐 **Digital Signature**: Ensures integrity and authenticity.  
  - 🕒 **Expiration**: Includes a built-in expiry to mitigate misuse.  
  - 📦 **Self-Contained**: Carries both payload and signature.  

</details>

---

## ⚙️ Installation

<details>
  <summary>▶️ Show command</summary>

1. Open the **NuGet Package Manager Console** in Visual Studio.  
2. Run:

   ```powershell
   Install-Package Token.Generator.JWT.Login
   ```

3. You’re all set!

</details>

---

## 🔧 Configuration

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Register your token service as a singleton
    services.AddSingleton<ITokenService, TokenService>();

    // 🚀 Other application services...
}
```

> 💡 **Tip**: Adjust lifetime (Scoped/Transient) based on your needs.

---

## 🏷️ Library Usage

Three main methods cover basic through premium requirements:

### 1. ✨ BasicToken

- **TTL**: 30 minutes  
- **Parameters**: none  

```csharp
public string GenerateBasicToken()
{
    var tokenService = new TokenService();
    return tokenService.BasicToken();
}
```

---

### 2. ⚡ ProToken

- **Includes**: issuer, audience, clientUsername  

```csharp
public string GenerateProToken(
    string issuer,
    string audience,
    string clientUsername)
{
    var tokenService = new TokenService();
    return tokenService.ProToken(
        issuer,
        audience,
        clientUsername);
}
```

---

### 3. 🌟 PremiumTokenAsync

- **TTL**: 30 days  
- **Requires**: valid key checked against a GitHub-hosted list  

```csharp
public async Task<string> GeneratePremiumTokenAsync(
    string key,
    string issuer,
    string audience,
    string clientUsername)
{
    var tokenService = new TokenService();
    return await tokenService.PremiumTokenAsync(
        key,
        issuer,
        audience,
        clientUsername);
}
```

---

## 🔄 Full ASP.NET Core Example

```csharp
[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    // 🎫 Basic Token
    [HttpGet("basic")]
    public IActionResult GetBasicToken()
    {
        var token = _tokenService.BasicToken();
        return Ok(new { Token = token });
    }

    // 🎫 Pro Token
    [HttpGet("pro")]
    public IActionResult GetProToken(
        string issuer,
        string audience,
        string clientUsername)
    {
        var token = _tokenService.ProToken(
            issuer,
            audience,
            clientUsername);
        return Ok(new { Token = token });
    }

    // 🎫 Premium Token
    [HttpGet("premium")]
    public async Task<IActionResult> GetPremiumTokenAsync(
        string key,
        string issuer,
        string audience,
        string clientUsername)
    {
        var token = await _tokenService.PremiumTokenAsync(
            key,
            issuer,
            audience,
            clientUsername);
        return Ok(new { Token = token });
    }
}
```

---

## 📞 Support & Contributions

- 🔗 **Repo & Portfolio**: Check my portfolio for more details and examples.  
- 🐛 **Issues / Feature Requests**: Open an issue on GitHub or reach out via email.  

> Thanks for choosing the JWT Token Generation Library! 🙌 Happy coding, and keep your applications secure! 💻✨
