# Programowanie Obiektowe - Projekt backendowy w C#

**Projekt:** TicketFlow API  
**Autor:** Michał Matusiewicz 82957  
**Technologia:** C# 12, .NET 8, ASP.NET Core Web API

## 1. Cel i opis programu

TicketFlow API jest backendowym systemem do obsługi zgłoszeń helpdesk. Program przechowuje klientów, pracowników wsparcia i zgłoszenia oraz pozwala zarządzać nimi przez endpointy HTTP.

## 2. Struktura projektu

Projekt jest podzielony na warstwy: Api, Application, Domain i Infrastructure. Kontrolery odpowiadają za HTTP, Application za przypadki użycia, a Domain za najważniejsze klasy obiektowe.

## 3. API i zakres funkcjonalny

Najważniejsze endpointy: GET /api/tickets, POST /api/tickets, PUT /api/tickets/{id}/assign, PUT /api/tickets/{id}/status, POST /api/tickets/{id}/comments, GET /api/tickets/overdue, GET /api/reports/tickets.csv, GET /api/metadata/domain.

## 4. Elementy obiektowości

Projekt wykorzystuje: klasy, konstruktory, właściwości, indeksatory, elementy statyczne, dziedziczenie, polimorfizm, interfejsy, abstrakcję, typy ogólne, kolekcje, delegacje, zdarzenia, przeciążanie operatorów, async oraz refleksję.

## 5. Spełnienie kryteriów projektu

Projekt spełnia wymagania z listy zajęciowej. Zakres backendowy obejmuje modele domenowe, kontrolery HTTP, usługi aplikacyjne, repozytoria, raport CSV, powiadomienia, obsługę SLA oraz metadane klas.

## 6. Kluczowe fragmenty kodu

Dokumentacja PDF zawiera najważniejsze fragmenty kodu i krótkie uzasadnienia projektowe.
