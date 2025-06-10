🌐 InvoiceMaker (ASP.NET MVC) : invoicemakerjm.pl (link do strony)

Aplikacja webowa do wystawiania faktur z automatyzacją danych i generowaniem PDF

📄 Opis
InvoiceMaker to aplikacja webowa stworzona w technologii ASP.NET MVC jako rozwinięcie desktopowej wersji programu. Pozwala na wystawianie faktur sprzedaży, zarządzanie kontrahentami, automatyczne pobieranie danych z API oraz eksport dokumentów do PDF. Projekt stanowi część mojego portfolio jako .NET Developer.

🛠️ Technologie
- ASP.NET MVC (.NET 9)
- Entity Framework
- ASP.NET Identity – system rejestracji i logowania użytkowników
- CQRS + MediatR
- AutoMapper
- PDFsharp – generowanie faktur PDF
- API Ministerstwa Finansów (MRiF) – pobieranie danych kontrahenta po NIP
- Data Annotations – walidacja po stronie serwera i klienta
- Bootstrap 5 – responsywny interfejs
- biblioteka toastr
- Dependency Injection
- prosty javascript

🚀 Funkcjonalności
- Rejestracja i logowanie użytkowników z użyciem ASP.NET Identity
- Rejestracja sprzedawcy – dane automatycznie uzupełniane na fakturze
- Automatyczna numeracja faktur (np. FV/2025/06/001)
- Pobieranie danych nabywcy z MRiF po wpisaniu numeru NIP
- Formularz tworzenia faktury z dynamiczną listą pozycji
- Możliwość pobrania faktury jako pliku PDF (z logo sprzedawcy)
- Zarządzanie fakturami – edycja, podgląd, usuwanie
- Zarządzanie kontrahentami – dodawanie, edycja, usuwanie
- Możliwość dodania kontrahneta/sprzedawcy z lsity bez przeładowania strony

📂 Struktura
Projekt oparty o architekturę warstwową z wyraźnym podziałem na:
- Application (logika biznesowa + CQRS)
- Domain (klasy domenowe)
- Infrastructure (dostęp do bazy danych i API)
- Web (ASP.NET MVC)

🎯 Cel projektu
Projekt pokazuje moją znajomość nowoczesnych wzorców projektowych, pracy z API, zarządzania tożsamością użytkownika, tworzenia przejrzystych interfejsów oraz generowania dynamicznych dokumentów. Ma charakter praktyczny i stanowi element mojego portfolio w rekrutacjach na staż jako ASP.NET Developer.

📄 Strona i baza danych została umieszczona na hostingu webio.pl. Zachęcam do sprawdzenia jej funkcjonalności :)

![image](https://github.com/user-attachments/assets/6bf24807-debb-4b4f-b6c8-1b75a50885a1)
![image](https://github.com/user-attachments/assets/911a8466-a444-4617-b077-c2f7a9429792)
![image](https://github.com/user-attachments/assets/c8a9ccb2-b8d7-4516-a733-dad3fb3b5cd5)
![image](https://github.com/user-attachments/assets/2ee22f6d-fede-4c69-a237-285709878215)
![image](https://github.com/user-attachments/assets/4c620ca5-c75e-4d6a-8fcb-3533d101a38e)
![image](https://github.com/user-attachments/assets/10c57e81-5ee2-4c3d-9400-23417694caaa)


