This README file will explain (in Danish) how to use the system.

1. Programmet opretter selv en MongoDb database med de nødvendige collections ved kørsel.
2. Første gang programmet køres, skal der seedes statisk data ned i databasen. Dette gøres ved at taste "s" i hovedmenuen. 
3. Menuen indeholder også følgende muligheder: 
	1. Vis planlagte fødsler.
		- Viser planlagte fødsler de kommende 3 dage - inklusiv BirthID, planlagt starttidspunkt og navn på barnet.
	3. Aktuelt igangværende fødsler
		- Viser igangværende fødsler. Fødsler der er påbegyndt inden for de sidste 5 timer antages at være igangværende  
	5. Vis reserverede rum og associeret klinik personale til en specifik fødsel. 
		- Efter indtasting af BirthID (fødsels ID), vil alle associerede rum og klinisk personale blive vist.
		- Bruges til at se detaljer for fødlser evt. efter brug af UC 1 og 3.
   Derudover kan man vælge en af følgende funktionaliteter: 
	B: Lav en reservation til fødsel
		- Man kan oprette en ny fødsels reservation, hvilket kræver barnets (midlertidige) navn, navn på moderen og tidspunktet for fødslen.
		- Ved valg af personale og rum SKAL man vælge en af de viste muligheder.
		- Indtastning af dato og tidspunkt skal overholde de skrevne anvisninger. 
4. Til sidst kan man afslutte programmet ved at trykke "x".

************************** OBS ******************************
For yderligere information vedrørende beslutninger foretaget i systemet, se vedlagde PDF "DAB_Assignment3_Gruppe19".