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
