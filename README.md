# Social Media Platform 

A scalable social media platform built with **ASP.NET Core (.NET 8)** following **Clean Architecture** principles.

---

## 🧱 Overview

This project is an ongoing effort to develop a modern social media platform with a focus on scalability, security, and maintainability.  
It is designed using **domain-driven design (DDD)** with a clear separation of concerns between the **Domain**, **Application**, **Infrastructure**, and **API** layers.

---

## ⚙️ Features

- **Authentication & Authorization**  
  - Secure JWT-based authentication  
  - Role-based access control  
  - Token lifecycle management  

- **Core Social Networking Features**  
  - Posts, comments, and likes  
  - Follow/unfollow users  
  - User profiles and activity tracking  

- **Personalized Feed Algorithm**  
  - Ranks content based on follow relationships  
  - Incorporates user engagement signals (likes, comments)  
  - Applies time-based decay for recent content  

- **Clean Architecture**  
  - Separate layers for Domain, Application, Infrastructure, and API  
  - Easy to extend and maintain  

---

## 🛠️ Technologies

- **Backend:** ASP.NET Core (.NET 8)  
- **Architecture:** Clean Architecture, Domain-Driven Design  
- **Authentication:** JWT, Role-Based Access Control  

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- SQL Server 

### Installation


```bash
git clone https://github.com/ezzeldine22/Social-Media.git
```
```bash
cd Social-Media
```
```bash
dotnet restore
```
```bash
dotnet ef database update
```
```bash
dotnet run
```
