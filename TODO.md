Folosind API-ul https://pomber.github.io/covid19/timeseries.json?fbclid=IwAR2FznKc4nXVzWdyZMKc7X58psda0y3DzTMet9u_FU8BtEfkB6n3H9uxhDA
Eventual (https://covid19.hackout.ro/api/getPersoane/)


Aplicatie cu mai multe tab-uri.
    Tab 1: Harta lumii cu culori in functie de numarul de infectati
    Tab 2: Grafice cu evolutia in timp a numarului de persoane infectate / decedate / vindecate in lume.
    Tab 3: Grafice specifice unei tari + algoritm de cautare.

Unit testing folosind (Unit Test Project in Visual Studio)
Documentatie folosind Doxygen.

1) Preluat date de la API (https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.8)
2) Introducere in baza de date sqlite.
2) Afisat date cu numarul de infectati pe harta. (folosind LiveCharts C#)
3) Afisat statistici (posibil folosind LiveCharts)
4) Posibilitate de alegere tara apasand pe harta.