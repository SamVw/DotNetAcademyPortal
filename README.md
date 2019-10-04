# DotNetAcademyPortal - Gebruiksaanwijzingen

## Installeren en opstarten programma:
- Ctrl + F5 in Visual Studio 
- Aanmaken en migraties van database gebeuren vanzelf


## Admin account (automatisch gegenereerd):
- username: Admin
- passwoord: @Admin123


## Customer account ( via admin aangemaakt):
- username: ingegeven email
- passwoord: @Test123 (hetzelfde voor alle customers)


## Features:
- Login ( Customer en Administrators )
- Overzicht van klanten ( Admin ):
    - Aanmaken 
    - Aanpassen
- Overzicht van deelnemers voor klant ( Admin ):
    - Aanmaken
    - Aanpassen
    - Limiet kan niet overschreden worden
- Overzicht van deelnemers voor klant ( Customer):
    - Enkel naam kan aangepast worden
    
    
## Non-functional requirements:
- exception handling
- Dependency injection
- Logging ( zie /logs )
- Unit tests
- Database setup

## Mediator
Voor de communicatie tussen de service en business laag is gebruik gemaakt van het Mediator Design Pattern. Hierdoor zijn beide lagen loosely coupled en gescheiden van elkaar.
