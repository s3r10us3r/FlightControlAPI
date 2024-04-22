# FilghtControlAPI

## Instrukcja instalacji

### Wymagania
  - Visual Studio 2022
  - .NET SDK 8.0
  - ASP.NET Core
  - Entity Framework Core 7
  - SQL Server 2022

Przed uruchomieniem należy wykonać migrację Entity Framework znajdującą się w pliku InitialFlightDB.cs.
(Za pomocą package manager'a w visual studio otworzyć katalog główny i wpisać komendę `Update-Database -TargetMigration:"InitialFlightDB"`)
Do uruchomienia programu wystarczy otworzyć plik source/FlightControl/FlightControl.sln i skompilować wykorzystując Visual Studio.

Testy wystarczy uruchomić z poziomu Visual Studio (nie trzeba robić migracji Entity Framework).

### Spełnienie wymagań projektowych
 - API umożliwa zarządzanie lotami oraz lotniskami (baza danych składa sie z trzech tabel, lotów, lotnisk oraz użytkowników) każdemu uwierzytelnionemu użytkownikowi.
 - Zaimplementowałem mechanizm autoryzacji za pomocą tokenów JWT.
 - Dane wejściowe są walidowane, login i hasło użytkownika muszą spełniać wymagania dot. długości, numery lotów muszą być zgodne z ogólnie przyjętą konwencją i wysyłane dane muszą być zgodne z projektem API.
 - Do interakcji z bazą danych wykorzystałem Entity Framework Core
 - Każdy projekt wchodzący w skład API posiada testy jednostkowe.
