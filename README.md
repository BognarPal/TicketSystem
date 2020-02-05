# TicketSystem
Hibajegy kezelő rendszer szoftverfejlesztési projektekhez

### Az appsettings.json állományban található felhasználó létrehozása, és jogok megadása

CREATE USER 'ticket'@'localhost' IDENTIFIED BY 'hibajegy';

GRANT ALL PRIVILEGES ON * . * TO 'ticket'@'localhost';


## Kapcsolódó videók
1-Projekt létrehozása: https://youtu.be/IMBGA2uUJPA

2-Az induló projekt tisztítása: https://youtu.be/AX_Czrdoz_c

3-Felhasználókezeléshez szükséges modell osztályok és adatbázis létrehozása: https://youtu.be/_sFSxTqIA4U

4-Admin felhasználó létrehozása: https://youtu.be/AFVcMiwTfxk

5-Login controller létrehozása, tesztelése: https://youtu.be/XExvLDDuIxY

6-Authentication middleware a JWT-hez: https://youtu.be/1PQkX3rn68I

7-Login controller módosítása - token létrehozás: https://youtu.be/kuI5exdzyF0

8-Kliens oldalon login komponens létrehozása: https://youtu.be/LSH9DIDlAaY

9-Authorization védett függvény létrehozása: https://youtu.be/yCpODSyIT8k