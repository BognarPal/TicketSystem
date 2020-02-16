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

10-Authentication service létrehozása: https://youtu.be/qHRAXMVYQ5Y

11-A JWT authentikáció mégsem volt jó ötlet ebben az esetben: https://youtu.be/_6JaUzElVp8

12-Virtuális mezők hozzáadása a modellhez: https://youtu.be/I740jxXgrwE

13-Angular routing, kliens oldal authentikáció és authorizáció igazítása: http://youtu.be/_Tn98heGjrY