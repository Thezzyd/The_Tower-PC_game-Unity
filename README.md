# The Tower (Wieża) - Projekt gry PC

## Cel projektu
Celem jest zaprojektowanie i implementacja gry komputerowej 2D z kategorii gier zręcznościowych, hack and slash i endless runner, przy wykorzystaniu silnika Unity. Wykonana gra komputerowa będzie miała na celu przede wszystkim dostarczyć jego użytkownikowi rozrywkę oraz możliwość poprawiania czasu reakcji gracza, jak i zręczności w tego typu grach w miarę liczby rozegranych rozgrywek. Gra będzie w szczególności charakteryzowała się elementem nieskończonej i losowej generacji nowych obiektów w grze w miarę pokonywanej wysokości, aby nowa rozgrywka nigdy nie wyglądała, tak jak żadna z poprzednich. Informacje o koncie użytkownika oraz o wynikach uzyskanych po zakończeniu rozgrywki danego gracza będą zbierane i przechowywane w bazie danych, aby później móc porównywać swoje wyniki z wynikami innych graczy.

## Organizacja pracy
Praca nad projektem, który wymaga wykonania wielu różnych czynności w drodze do otrzymania produktu finalnego tak, aby aplikacja działała poprawnie i była spójna z całością, potrzebuje zaplanowania, jakie elementy pracy i w jakiej kolejności będą realizowane po sobie. W celu zachowania porządku i optymalnego wykorzystania czasu został zaplanowany schemat obrazujący organizację pracy nad projektem

![workflow-diagram](/diagrams/workflow.PNG)

## Założenia funkcjonalne
Przy projektowaniu aplikacji bardzo ważnym elementem jest określenie potrzeb funkcjonalnych, jakie tworzone oprogramowanie ma realizować. Ułatwia to zobrazowanie usług, jakie są widoczne z zewnątrz systemu

![use_case-diagram](/diagrams/use_case.PNG)

### Funkcjonalności i mechaniki podczas rozgrywki:
* sterowanie i interakcja z aplikacją za pomocą klawiatury i myszy,
* automatyczne i dynamiczne generowanie elementów mapy, takich jak platformy, dekoracje otoczenia, przeciwnicy, czy obiekty zbieralne,
* wprowadzenie elementu losowości i nieskończoności w procesie generowania mapy, tak, że prowadzona rozgrywka nigdy nie będzie taka sama, jak żadna z poprzednich. Zostanie nam zapewniona możliwość gry, gdzie zawsze jest szansa ustanowienia nowego rekordu i prowadzenia rozgrywki przez tyle czasu, ile chcemy lub do skucia,
* różnorodni coraz trudniejsi i coraz liczebni przeciwnicy w zależności od osiągniętej wysokości podczas prowadzonej rozgrywki, w celu zapewnienia zwiększającego się poziomu trudności,
* ograniczone pole dokładnej widoczności wokoło naszej postaci, w celu podniesienia poziomu trudności poruszania się w świecie gry,
* możliwość dynamicznej walki pomiędzy naszą postacią, a napotykanymi przeciwnikami,
* różnorodność umiejętności przy walce z przeciwnikami, gdzie w danym momencie dostępna jest tylko jedna z całej puli,
* wprowadzenie obiektów „gwiazd”, których zbieranie jest naszym głównym celem, aby utrzymać postać przy życiu. Po zebraniu dokonuje się rotacja umiejętności walki z przeciwnikami,
* licznik czasowy, który trzeba stale resetować, zbierając obiekty „gwiazd”. Po zejściu licznika do zera, nasza aktywna umiejętność walki będzie stopniowo słabnąć, a w chwili krytycznej dojdzie do skucia naszej postaci,
* wprowadzenie obiektu „esencji”, której zbieranie zapewnia ulepszanie siły aktywnej w danym czasie umiejętności. Obiekty te są pozostawiane przez pokonywanych przeciwników,
* wprowadzenie obiektu „skrzyń”, po którego zebraniu otrzymujemy losową liczbę dodatkowych obiektów „esencji”,
* informacje na ekranie o aktualnej zdobytej wysokości, zdobytych obiektach „gwiazd”, o poziomie siły aktywnej obecnie umiejętności i jej rodzaju, o wartości licznika czasowego, posiadanych punktach doświadczenia i aktualnym poziomie zalogowanego gracza,
* wzór przeliczający osiągnięte wyniki w rozgrywce na łączną liczbę punktów,
* przesyłanie danych z rozegranej rozgrywki do bazy danych,
* możliwość ulepszania pasywnych umiejętności w grze za pomocą dostępnych punktów uzyskanych po każdorazowym podniesieniu poziomu gracza,
* możliwość wstrzymania i zakończenia rozgrywki w dowolnym momencie, jak i zmiana poziomu głośności, a także włączenia lub wyłączenia efektu „screen shake ”.

### Funkcjonalności poza rozgrywką:
* tworzenie indywidualnego konta użytkownika, gwarantującego bezpieczne przechowywanie danych w bazie, korzystając z rozwiązań Firebase,
* konieczność zalogowania się na swoje konto użytkownika wraz z automatyzacją tego procesu przy następnym uruchomieniu aplikacji, jeśli pozostaliśmy zalogowani przy zamknięciu gry,
* opcja wylogowania się z obecnie zalogowanego konta z poziomu sekcji „Options”,
* komunikacja z aplikacją za pomocą myszy,
* opcja dostosowania poziomu głośności efektów dźwiękowych z poziomu sekcji „Options”,
* możliwość aktywacji lub dezaktywacji efektu „shake screen” z poziomu sekcji „Options”,
* możliwość podglądu swoich osiągniętych wyników z rozegranych gier w przeszłości za pomocą wykresu z wieloma opcjami dostosowania,
* możliwość dokonania podglądu prognozy spodziewanych wyników w przyszłych dniach na podstawie wyników z przeszłości (zastosowanie modelu regresji liniowej),
* możliwość podglądu wyliczonej korelacji pomiędzy wiekiem gracza, a osiąganymi wynikami (zastosowanie metody liniowej Pearsona),
* zdobywanie osiągnięć po zdobyciu konkretnych wyników podczas prowadzenia rozgrywki,
* możliwość podglądu w ilu procentach gracz uzyskał dane osiągnięcie szybciej w stosunku do całej reszty graczy, co również uzyskała to osiągnięcie,
* opcja wyświetlenia tablicy najlepszych wyników, gdzie będziemy mieli możliwość podglądu, którzy gracze zajmują jakie miejsca w globalnym rankingu, wraz z informacją, jakie miejsce zajmuje zalogowany gracz,
* możliwość wyboru mapy do prowadzenia rozgrywki (na potrzeby projektu zostanie zrealizowana tylko jedna).

## Założenia funkcjonalne
* działanie aplikacji na urządzeniach z systemem Windows 7 lub nowszym,
* nomenklatura stosowana przy deklaracji nazw klas, funkcji, zmiennych itd. wyrażana jest w języku angielskim, jak i sama aplikacja będzie w angielskiej wersji językowej,
* aplikacja budowana na silniku Unity 2D z wykorzystaniem wewnętrznych narzędzi do animacji oraz tworzenia efektów wizualnych,
* stosowanie standaryzacji przy pisaniu kodu aplikacji,
* maksymalizacja płynności prowadzonej rozgrywki,
* minimalizacja błędów i niepożądanych zachowań w grze,
* dostosowanie aplikacji tak, aby poprawnie była skalowana na różnych rozdzielczościach,
* wymóg połączenia internetowego ze względu na konieczność zalogowania się przed rozpoczęciem gry i pobrania danych o użytkowniku,
* opatrzenie danych w bazie ograniczeniami dostępu w celu maksymalizacji ich bezpieczeństwa.

## Struktura danych
Bardzo ważnym elementem każdego projektu, który korzysta z rozwiązań bazodanowych jest dobrze przemyślana struktura danych tak, aby wszystkie tworzone zapytania były maksymalnie zoptymalizowane, biorąc pod uwagę nasze zasoby, potrzeby i cel. W tworzonym projekcie baza danych opiera się na obiektowym formacie przechowywania informacji, jest to rodzaj bazy danych określanej, jako NoSQL. Tworząc bazę danych, korzystając z rozwiązań dostawcy Firebase, najważniejszymi aspektami jest, aby cechowała się ona jak najmniejszą liczbą potrzebnych do wykonania zapytań oraz projektowanie struktury w taki sposób, żeby nie obciążać zapytań złożonymi funkcjami agregującymi. 

![data_structure-diagram](/diagrams/data_structure.PNG)

W tworzonej aplikacji zostały wdrożone cztery główne kolekcje danych – kolekcja danych użytkownika, kolekcja danych z przeprowadzonych rozgrywek, kolekcja najlepszych wyników graczy i ostatnia z odblokowanymi osiągnięciami przez poszczególnych graczy.

## Przepływ aktywności
Kolejną ważną czynnością, jaką możemy wykonać przy projektowaniu pracy jest przemyślenie i zamodelowanie przepływu czynności naszej aplikacji. Możemy go przedstawić za pomocą graficznego diagramu. Służy on głównie do modelowania dynamicznych aspektów systemu oraz do przedstawienia sekwencji kroków-czynności, które są wykonywane przez opisywany fragment systemu.

![activity-diagram](/diagrams/activity_diagram.PNG)

Po uruchomieniu aplikacji pierwszą czynnością, aby móc przejść do następnego kroku w drodze do rozegrania gry jest konieczność zalogowania się do systemu na istniejące konto lub jeżeli go nie posiadamy musimy je utworzyć. Poza naszym wzrokiem system dokona procesu autentykacji oraz autoryzacji i pobierze z bazy danych wszystkie potrzebne informacje o zalogowanym graczu. Po sukcesywnym przejściu procesu logowania będziemy mogli przejść do wyboru mapy rozgrywki, po zatwierdzeniu wyboru rozgrywka się rozpocznie. System w momencie zmiany wartości zebranego doświadczenia będzie przesyłał zaktualizowane dane do bazy danych, gdzie przy każdej próbie wysyłania i odbierania danych, system sprawdzi reguły dostępu do konkretnych zasobów i zostaniemy przepuszczeni albo w razie jakichś nieprawidłowości system nie zrealizuje danego żądania. W momencie, gdy kończymy rozgrywkę nasz ostateczny wynik ląduje w bazie danych, a użytkownik decyduje, czy chce ponowić rozgrywkę lub opuszcza aplikację.

## Sposób wdrożenia
Za pomocą diagramu wdrożenia można zaplanować, z jakich komponentów będzie się składał tworzony system. W przypadku tworzonej pracy głównymi komponentami będą:
* aplikacja – wykonywalna desktopowa aplikacja będąca grą wideo, zbudowana w silniku Unity,
* Firebase Realtime DB – baza danych typu NoSQL, która będzie odpowiedzialna za bezpieczne przechowywanie wszystkich danych,
* Firebase Authentication – zapewnia zestaw usług back-endowych, skupiając się na procesie autentykacji użytkowników logujących się do systemu.

![deployment-diagram](/diagrams/deployment.PNG)

## Interfejs użytkownika
Aplikacje budowane z wykorzystaniem silnika Unity złożone są ze scen. Sceny składają się ze środowiska gry oraz interfejsu użytkownika. Są one zbiorem ułożonych w hierarchii obiektów gry, które reprezentują poszczególne elementy tworzonego projektu np. pole tekstowe, grafika drzewa z nałożoną animacją, czy efekt wizualny imitujący świetliki na niebie. Tworzona gra wideo w obecnej formie składa się z dwóch scen.

### Scena startowa menu
Pierwsza sceną uruchamiającą się automatycznie po załadowaniu aplikacji jest menu. Scena ta składa się na widok logowania, rejestracji, głównego menu, opcji ustawień, statystyk gracza oraz wyboru mapy do przeprowadzenia rozgrywki.

#### Widok logowania
W tym widoku mamy takie dostępne możliwości, jak:
* wprowadzenie danych logowania istniejącego konta, a następnie kliknięcie w przycisk „Login”. Sukcesywne zalogowanie się użytkownika spowoduje przejście do widoku głównego menu aplikacji, a w przeciwnym przypadku zostanie ukazany odpowiedni komunikat, co poszło nie tak,
* przejście do widoku rejestracji, klikając w przycisk „Registration screen”, 
* wyłączenie aplikacji za pomocą kliknięcia w przycisk „Quit”.

![UI_screen - 1](/UI_screens/menu_login.PNG)

#### Widok rejestracji
Widok rejestracji udostępnia takie możliwości, jak:
* wprowadzenie danych tworzonego konta, a następnie kliknięcie w przycisk „Register”. Sukcesywne przejście procesu spowoduje utworzenie konta, przesyłając wszystkie wprowadzone dane do bazy oraz nastąpi przejście do widoku logowania, w przeciwnym przypadku zostanie pokazany odpowiedni komunikat, co poszło nie tak,
* przejście do widoku logowania, klikając w przycisk „Login screen”,
* wyłączenie aplikacji za pomocą przycisku „Quit”.

![UI_screen - 2](/UI_screens/menu_register.PNG)

#### Widok menu
Z poziomu widoku głównego menu aplikacji mamy takie możliwości, jak:
* przejście do widoku wyboru mapy, klikając w przycisk „Play”,
* przejście do widoku opcji, klikając w przycisk „Options”,
* przejście do widoku statystyk zalogowanego użytkownika za pomocą kliknięcia w przycisk „Statistics”,
* wyłączenie aplikacji za pomocą przycisku „Quit”.

![UI_screen - 3](/UI_screens/menu_main.PNG)

#### Widok opcji ustawień
Z poziomu widoku opcji mamy takie możliwości, jak:
* dostosowanie poziomu głośności muzyki, jak i efektów dźwiękowych, które będą zaaplikowane w obrębie wszystkich scen w aplikacji,
* wylogowanie się z obecnie zalogowanego konta użytkownika za pomocą przycisku „Logout”. Po kliknięciu zostaniemy przeniesieni do widoku logowania,
* przejście z powrotem do widoku głównego menu aplikacji za pomocą przycisku „Back”.

![UI_screen - 4](/UI_screens/menu_options.PNG)

#### Widok statystyk
* podgląd wyników zalogowanego gracza, osiągniętych w rozegranych grach w przeszłości z poziomu zakładki „User Progression”,
* podgląd przyszłych spodziewanych wyników na podstawie danych z przeszłości, wykorzystując do prognozowania metodę liniowej regresji,
* podgląd wyniku obliczeń oraz interpretacja korelacji pomiędzy wiekiem użytkowników, a osiąganymi przez nich wynikami z rozegranych gier wraz z wizualizacją danych na wykresie z poziomu zakładki „Corelation”,
* podgląd zdobytych osiągnięć przez zalogowanego użytkownika z poziomu zakładki „Compare Achievements”,
* podgląd tablicy najlepszych wyników z poziomu zakładki „Scoreboard”. W tablicy przedstawione są najlepsze gry wszystkich z graczy pod kątem zebranych punktów podczas rozgrywki, uszeregowane od najlepszych do najgorszych.

![UI_screen - 5](/UI_screens/statistics_1.PNG)

#### Widok wyboru mapy
Z poziomu widoku wyboru mapy mamy takie możliwości, jak:
* wybór mapy do przeprowadzenia rozgrywki z pośród wszystkich dostępnych. Aby dokonać wyboru należy przeciągnąć za pomocą myszki bloczek reprezentujący wybraną mapę na środek, a następnie w niego kliknąć,
* przejście z powrotem do widoku głównego menu aplikacji za pomocą przycisku „Return”.

![UI_screen - 6](/UI_screens/menu_map_select.PNG)

### Scena rozgrywki
Druga z dostępnych scen uruchamia się po dokonaniu wyboru mapy do przeprowadzenia rozgrywki, klikając w odpowiedni przycisk na poprzedniej scenie. Składa się ona na widok rozgrywki, wstrzymania rozgrywki, ulepszeń, dostępnych umiejętności oraz podsumowania przeprowadzonej rozgrywki.

#### Widok gry
Z poziomu widoku prowadzonej rozgrywki mamy takie możliwości, jak:
* podgląd aktualnego poziomu i liczby punktów doświadczenia, zdobytych przez obecnie zalogowanego gracza,
* podgląd siły obecnie dostępnej umiejętności walki wraz z liczbą zebranych obiektów „esencji”, odpowiadających za podniesienie jej poziomu,
* podgląd wartości spadającego licznika czasu wraz z informacją o liczbie zdobytych obiektów „gwiazd” w rozgrywce,
* podgląd osiągniętej maksymalnej wysokości podczas trwającej rozgrywki,
* opcja zatrzymania lub wstrzymania trwającej gry, klikając w przycisk z ikoną zębatki. Po kliknięciu ukaże się widok wstrzymania rozgrywki,
* opcja przejścia do widoku ulepszeń pasywnych umiejętności postaci w grze po kliknięciu przycisku z ikoną umiejętności,
* opcja przejścia do widoku dostępnych w grze umiejętności walki po kliknięciu w przycisk z ikoną postaci.

![UI_screen - 7](/UI_screens/gameplay.PNG)

#### Widok wstrzymania rozgrywki
Z poziomu widoku wstrzymania rozgrywki mamy takie możliwości, jak:
* wznowienie rozgrywki w dowolnym momencie, klikając przycisk „Resume”,
* przejście do sekcji z opcjami po kliknięciu w przycisk „Options” z możliwością dostosowania poziomu głośności muzyki i efektów dźwiękowych oraz z możliwością aktywowania lub dezaktywacji efektu „screen shake”,
* przejście do głównego menu aplikacji po kliknięciu w przycisk „Menu”, co oznacza porzucenie aktualnie prowadzonej rozgrywki bez możliwości późniejszej jej kontynuacji,
* opcja całkowitego opuszczenia aplikacji po kliknięciu w przycisk „Quit”.

![UI_screen - 8](/UI_screens/gameplay_pause.PNG)

#### Widok ulepszeń
Z poziomu widoku ulepszeń mamy takie możliwości, jak:
* podniesienie wartości jednej z pięciu dostępnych pasywnych umiejętności (dodatkowe obrażenia zadawane przeciwnikom, szybkość poruszania się postaci, liczba obiektów „essencjii” uzyskanych po zebraniu obiektu „gwiazdy”, maksymalną wartość licznika czasowego oraz wartość zadawanej kary z każdą sekundą po upływie licznika czasowego) za pomocą punktów uzyskiwanych każdorazowo po podniesieniu poziomu zalogowanego gracza,
* podgląd liczby przypisanych punktów dla każdej z dostępnych pasywnych umiejętności wraz z informacją, jaką wartość procentową zmiany danego parametru w grze oferują,
* opcja zresetowania wszystkich aktualnie przypisanych punktów, w celu późniejszej ich alokacji w innych proporcjach,
* opcja powrotu do widoku rozgrywki, klikając w przycisk z ikoną krzyżyka.

![UI_screen - 9](/UI_screens/gameplay_evolve.PNG)

#### Widok umiejętności
Z poziomu widoku umiejętności mamy takie możliwości, jak:
* dokonanie podglądu informacji o poszczególnych wartościach bitewnych każdej z dostępnych umiejętności walki w grze, które są zależne od aktualnego ich poziomu. Aby zmienić widoczną umiejętność należy kliknąć w jeden z przycisków z ikoną strzałki,
* opcja powrotu do widoku rozgrywki, klikając w przycisk z ikoną krzyżyka.

![UI_screen - 10](/UI_screens/gameplay_skills.PNG)

#### Widok podsumowania rozgrywki
Z poziomu widoku podsumowania rozgrywki mamy takie możliwości, jak:
* podgląd podsumowujących informacji o zakończonej rozgrywce wraz z porównaniem do najlepszej rozegranej przez nas gry z przeszłości,
* podgląd uzyskanych dodatkowych punktów doświadczenia po zakończeniu rozgrywki wraz z informacją o aktualnym poziomie użytkownika,
* opcja ponowienia rozgrywki poprzez kliknięcie w przycisk z ikoną zapętlonej strzałki,
* opcja przejścia do widoku głównego menu aplikacji po kliknięciu przycisku z ikoną domku.

![UI_screen - 11](/UI_screens/gameplay_game_over.PNG)

## Wykorzystane technologie
* Silnik - Unity,
* Język programowania - C#,
* Tworzenie i przetwarzanie grafik, tworzenie animacji poklatkowych - Adobe Photoshop CC 2019,
* Środowisko programistyczne - Microsoft Visual Studio 2019,
* Nagrywanie i przetwarzanie plików audio - Audacity,
* Rozwiązania bazodanowe - Firebase (Realtime DB & Authentication),
* Efekty wizualne - System cząstek Unity, Shader Graph Unity, Oświetlenie Unity,
* Tworzenie animacji opartych o kość - Skinning Editor Unity.


