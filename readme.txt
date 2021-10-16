Celem projektu jest zbudowanie systemu który:
1. Okresli ktore pozyczki nalezy zabezpieczyc ktorymi nieruchomosciami (w kolejnosci wczytania plikow). 
2. Okresli koszty pozyczek.

Zalozenia:
Dana nieruchomosc moze zabezpieczac wiele pozyczek o ile ich suma miesci sie w max kwocie obciazenia dopuszczonej przez nieruchomosc

Pliki (opis kolumn w kolejnosci):
bank.csv - informacje o banku (id, nazwa)
facilities.csv - informacje o nieruchomosci (kwota max obciazenia, oprocentowanie zabezpieczenia (koszt), identyfikator nieruchomosci, identyfikator banku)
loans.csv - informacje o pozyczkach (oprocentowanie pozyczli,kwota pozyczki,identyfikator pozyczki,prawdopodobienstwo niesplacenia pozyczki,stan w ktorym pozyczka jest udzielana)
convenance - informacje o ograniczeniach (id nieruchomosci ktorej dotyczy ogranicznie,maksymalne dopuszczalne prawdopodobienstwo niesplacenia pozyczki, identyfikator banku ktorego dotyczy, stany do ktorych ogranienia maja zastosowanie)
 
Katalog small zawiera dane testowe.
Plik assigments.csv - zawiera dopasowanie na podstawawie plikow z folderu

Katalog large zawiera docelowe dane.