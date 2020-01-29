# SuperMed WSEI

Projekt zaliczeniowy do przedmiotu Programowanie aplikacji w ASP.NET

## Sposób uruchomienia
Wymagana baza Microsoft SQL Server (w dowolnej wersji) z zainstalowaną LocalDB.

Upewnić się, że baza o nazwie **SuperMedLocalDB_ASPNET** nie istnieje!
oraz, że narzędzia interfejsu wiersza polecenia (CLI) dla Entity Framework Core są zainstalowane, jeśli nie proszę w wierszu poleceń wykonać polecenie

```
dotnet tool install --global dotnet-ef
```

Po pobraniu źródeł aplikacji, uruchomić można na dwa sposoby:

-- Sposób 1:
- otworzyć plik solucji (.sln).
- zbudować aplikację.
- z nuget Package Console w Visual Studio proszę wpisać: **update-database**.
- uruchomić aplikację.

-- Sposób 2:
- wypakować źródła do folderu
- otworzyć wiersz poleceń jako administrator
- w wierszu poleceń przejść do folderu z plikiem **.sln** aplikacji
- wykonać po kolei komendy:
```
dotnet build
dotnet test --filter TestCategory=UnitTest
dotnet ef migrations add InitialCreate --project SuperMed/SuperMed.csproj
dotnet ef database update --project SuperMed/SuperMed.csproj
dotnet run --project SuperMed/SuperMed.csproj
```
otworzyć w dowolnej przeglądarce adres podany w linijce zaczynającej się od: Now listening on... (prawdopodobnie: https://localhost:5001)

## Krótki opis funkcjonalny

Prosta aplikacja w ASP.NET Core 2.2 korzystająca ze wzorca MVC oraz bazy danych SQL Server.
Aplikacja umożliwia w prosty sposób rejestrowanie wizyt do lekarzy.
W systemie istnieją 2 dostępne dla użytkowników rodzaje postaci: pacjent i doktor. Pacjent może zarejestrować wizytę u doktora: na konkretny dzień i godzinę wraz z krótkim opisem dolegliwości. W tym samym czasie doktor widzi w swoim panelu nadchodzące wizyty na dzisiaj w kolejności od najwcześniejszej godziny. 

Doktor po otworzeniu widoku konkretnej wizyty dodaje opis, zalecenia lub jakiekolwiek inne wnioski z wizyty po czym kończy wizytę. Użytkownik pacjent może odwołać wizytę. Wizyty mogą być realizowane w godzinach od 8 co 15 minut do 15:45 z wyłączeniem godzinnej przerwy o 12-tej. 
Dodatkowo, jeśli pacjent chce zarejestrować wizytę dzisiaj, godziny wcześniejsze niż aktualna nie są dla niego widoczne (np. jeśli pacjent o godzinie 14:00 chce zarejestrować wizytę na dzisiaj, nie zobaczy godzin przed godziną 14-tą).

Doktor może dodać dzień wolny w którym nie będą mogły być umawiane wizyty. Nie może jednak ustanowić dnia wolnego jeśli już tego dnia ma jakieś wizyty. 
Pacjent może edytować swoje dane: nazwisko oraz numer telefonu.

## Wymagania do projektu rozszerzonego
- Przygotowanie testów automatycznych:
	- do projektu dodane są testy jednostkowe klas pomocniczych, atrybutów, *extension methods*, repozytoriów i kontrolerów oraz testy E2E w selenium

- Wdrożenie na publiczny serwer
	- aplikacja jest *zeployowana* w Azure za pomocą AppService (wraz z bazą Azure SQL)

- Opracowanie prostego CI/CD
	- aplikacja jest budowana w Azure Pipelines. Automatyczny *build* i *release* następuje po dodaniu nowego kodu do brancha *master* w repozytorium Azure Repos

- Zastosowanie wzorców projektowych
	- zastosowano wzorce: MVC, Repository pattern, Facade pattern, Dependency Injection

- Czytelny kod, zrozumiała architektura
	- dokonano starań aby kod był czytelny,
		- repozytoria realizują jedynie proste metody CRUD
  - prosta architektura:

[Widoki] <->
[Kontrolery] <->
[Warstwa serwisu] <->
[Warstwa repozytorium] <->
[EntityFramework] <->
[SQL Server]
