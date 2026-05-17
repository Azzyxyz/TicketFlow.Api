# TicketFlow API

Backendowy projekt zaliczeniowy w C# i ASP.NET Core Web API. System obsługuje zgłoszenia helpdesk: klientów, pracowników wsparcia, statusy zgłoszeń, raporty oraz metadane domeny generowane przez refleksję.

## Funkcje

- tworzenie i pobieranie klientów,
- tworzenie i pobieranie pracowników wsparcia,
- tworzenie zgłoszeń typu `Incident` oraz `ServiceRequest`,
- przypisywanie zgłoszenia do pracownika,
- zmiana statusu zgłoszenia,
- dodawanie komentarzy,
- lista zgłoszeń przeterminowanych względem SLA,
- eksport raportu do `txt` albo `csv`,
- endpoint refleksji pokazujący klasy domenowe i ich właściwości.

## Uruchomienie

Wymagania: .NET 8 SDK.

```bash
dotnet restore
dotnet run --project src/TicketFlow.Api
```

Po uruchomieniu w trybie developerskim dostępny jest Swagger:

```text
https://localhost:xxxx/swagger
```

Można też użyć pliku:

```text
src/TicketFlow.Api/TicketFlow.Api.http
```

## Najważniejsze endpointy

```http
GET    /api/tickets
GET    /api/tickets/{id}
POST   /api/tickets
PUT    /api/tickets/{id}/assign
PUT    /api/tickets/{id}/status
POST   /api/tickets/{id}/comments
GET    /api/tickets/overdue
GET    /api/customers
POST   /api/customers
GET    /api/agents
POST   /api/agents
GET    /api/metadata/domain
GET    /api/reports/tickets.txt
GET    /api/reports/tickets.csv
```

## Elementy obiektowości

Projekt zawiera wszystkie wymagane elementy:

1. Klasy
2. Konstruktory
3. Właściwości i indeksatory
4. Składowe statyczne
5. Dziedziczenie
6. Polimorfizm
7. Interfejsy i abstrakcję
8. Typy ogólne i kolekcje
9. Delegacje i zdarzenia
10. Przeciążanie operatorów
11. Programowanie asynchroniczne
12. Refleksję

Dokładne omówienie znajduje się w dokumentacji PDF w katalogu `docs`.
