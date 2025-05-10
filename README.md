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
- **Requerimientos**:
  - `secretKey` (string): Clave secreta para firmar el token
- **Seguridad**:
  - Firma adaptativa basada en el tamaño de la clave:
    - < 256 bits: HMAC-SHA256
    - 256-511 bits: HMAC-SHA384
    - ≥ 512 bits: HMAC-SHA512
- **Respuesta**: Token JWT en formato string

---

### 2. ⚡ ProToken

- **Duración**: 30 minutos
- **Requerimientos**:
  - `issuer` (string): Emisor del token
  - `audience` (string): Audiencia objetivo del token
  - `clientUsername` (string): Nombre de usuario
  - `secretKey` (string): Clave secreta para firmar el token
- **Seguridad**: Mismo sistema adaptativo de firma que BasicToken
- **Respuesta**: JSON:
  ```json
  {
    "Token": "eyJhbGciOiJS...",
    "Expiration": "2025-06-09T10:30:00Z"
  }
  ```

---

### 3. 🌟 PremiumTokenAsync

- **Duración**: 30 días
- **Requerimientos**:
  - `key` (string): Clave de licencia premium (verificada contra repositorio GitHub)
  - `issuer` (string): Emisor del token
  - `audience` (string): Audiencia objetivo del token
  - `clientUsername` (string): Nombre de usuario
  - `secretKey` (string): Clave secreta para firmar el token
- **Características Premium**:
  - Validación de licencia en tiempo real
  - Seguimiento de uso de tokens
  - Claim adicional de usuario premium
- **Respuesta**: JSON:
  ```json
  {
    "Token": "eyJhbGciOiJS...",
    "Expiration": "2025-06-09T10:30:00Z",
    "UsageReport": {
        "TokensGenerated": 1,
        "LastUsage": "2025-05-10T10:30:00Z"
    }
  }
  ```

---

## 🔄 Ejemplo Completo en ASP.NET Core

```csharp
var builder = WebApplication.CreateBuilder(args);

// Registra tu servicio de generación de tokens como singleton
builder.Services.AddSingleton<ITokenService, TokenService>();

var app = builder.Build();

// 🎫 Token Básico
app.MapGet("/token/basic", () =>
{
    string secretKey = "TuClaveSecreta"; // Reemplaza con tu clave secreta
    var tokenService = new TokenService();
    var token = tokenService.BasicToken(secretKey);
    return Results.Ok(new { Token = token });
})
.WithName("GetBasicToken")
.WithOpenApi()
.WithTags("Tokens");

// 🎫 Token Pro
app.MapGet("/token/pro", () =>
{
    string secretKey = "TuClaveSecreta"; // Reemplaza con tu clave secreta
    string issuer = "TuEmisor"; // Reemplaza con tu emisor
    string audience = "TuAudiencia"; // Reemplaza con tu audiencia
    string clientUsername = "NombreDelUsuario"; // Reemplaza con tu nombre de tu usuario

    var tokenService = new TokenService();

    var response = tokenService.ProToken(
        issuer,
        audience,
        clientUsername,
        secretKey);
    
    // El response ya viene en formato JSON
    return Results.Ok(response);
})
.WithName("GetProToken")
.WithOpenApi()
.WithTags("Tokens");

// 🎫 Token Premium
app.MapGet("/token/premium", async () =>
{
    string key = "TuClavePremium"; // Reemplaza con tu clave premium
    string secretKey = "TuClaveSecreta"; // Reemplaza con tu clave secreta
    string issuer = "TuEmisor"; // Reemplaza con tu emisor
    string audience = "TuAudiencia"; // Reemplaza con tu audiencia
    string clientUsername = "NombreDelUsuario"; // Reemplaza con tu nombre de tu usuario

    var response = await tokenService.PremiumTokenAsync(
        key,
        issuer,
        audience,
        clientUsername,
        secretKey);
    
    // El response ya viene en formato JSON
    return Results.Ok(response);
})
.WithName("GetPremiumToken")
.WithOpenApi()
.WithTags("Tokens");

app.Run();
```

---

## 📞 Soporte y Contribución

- 🔗 **Repositorio & Portfolio**: Visita mi portfolio para más detalles.  
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

- **Duration**: 30 minutes
- **Requirements**:
  - `secretKey` (string): Secret key for token signing
- **Security**:
  - Adaptive signing based on key size:
    - < 256 bits: HMAC-SHA256
    - 256-511 bits: HMAC-SHA384
    - ≥ 512 bits: HMAC-SHA512
- **Response**: JWT token as string

---

### 2. ⚡ ProToken

- **Duration**: 30 minutes
- **Requirements**:
  - `issuer` (string): Token issuer
  - `audience` (string): Target audience
  - `clientUsername` (string): Username
  - `secretKey` (string): Secret key for token signing
- **Security**: Same adaptive signing system as BasicToken
- **Response**: JSON with:
  ```json
  {
    "Token": "eyJhbGciOiJS...",
    "Expiration": "2025-06-09T10:30:00Z"
  }
  ```

---

### 3. 🌟 PremiumTokenAsync

- **Duration**: 30 days
- **Requirements**:
  - `key` (string): Premium license key (verified against GitHub repository)
  - `issuer` (string): Token issuer
  - `audience` (string): Target audience
  - `clientUsername` (string): Username
  - `secretKey` (string): Secret key for token signing
- **Premium Features**:
  - Real-time license validation
  - Token usage tracking
  - Additional premium user claim
- **Response**: JSON with:
  ```json
  {
    "Token": "eyJhbGciOiJS...",
    "Expiration": "2025-06-09T10:30:00Z",
    "UsageReport": {
        "TokensGenerated": 1,
        "LastUsage": "2025-05-10T10:30:00Z"
    }
  }
  ```

---

## 🔄 Full ASP.NET Core Example

```csharp
var builder = WebApplication.CreateBuilder(args);

// Register token service as singleton
builder.Services.AddSingleton<ITokenService, TokenService>();

var app = builder.Build();

// 🎫 Basic Token
app.MapGet("/token/basic", () =>
{
    string secretKey = "YourSecretKey"; // Replace with your secret key
    var tokenService = new TokenService();
    var token = tokenService.BasicToken(secretKey);
    return Results.Ok(new { Token = token });
})
.WithName("GetBasicToken")
.WithOpenApi()
.WithTags("Tokens");

// 🎫 Pro Token
app.MapGet("/token/pro", () =>
{
    string secretKey = "YourSecretKey"; // Replace with your secret key
    string issuer = "YourIssuer"; // Replace with your issuer
    string audience = "YourAudience"; // Replace with your audience
    string clientUsername = "UserName"; // Replace with your username

    var tokenService = new TokenService();

    var response = tokenService.ProToken(
        issuer,
        audience,
        clientUsername,
        secretKey);
    
    // Response already comes in JSON format
    return Results.Ok(response);
})
.WithName("GetProToken")
.WithOpenApi()
.WithTags("Tokens");

// 🎫 Premium Token
app.MapGet("/token/premium", async () =>
{
    string key = "YourPremiumKey"; // Replace with your premium key
    string secretKey = "YourSecretKey"; // Replace with your secret key
    string issuer = "YourIssuer"; // Replace with your issuer
    string audience = "YourAudience"; // Replace with your audience
    string clientUsername = "UserName"; // Replace with your username

    var response = await tokenService.PremiumTokenAsync(
        key,
        issuer,
        audience,
        clientUsername,
        secretKey);
    
    // Response already comes in JSON format
    return Results.Ok(response);
})
.WithName("GetPremiumToken")
.WithOpenApi()
.WithTags("Tokens");

app.Run();

```

---

## 📞 Support & Contributions

- 🔗 **Repo & Portfolio**: Check my portfolio for more details.  
- 🐛 **Issues / Feature Requests**: Open an issue on GitHub or reach out via email.  

> Thanks for choosing the JWT Token Generation Library! 🙌 Happy coding, and keep your applications secure! 💻✨
