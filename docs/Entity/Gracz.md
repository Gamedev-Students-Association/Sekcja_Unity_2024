# Gracz

## Poruszanie się gracza

Gracz może poruszać się po poziomie biegając, skacząc i spadając z wysokości.

Możemy kontrolować wysokość skoku.

## Technikalia

Movement będzie rozwiązany ręcznie obliczając nowe wartości pozycji na bazie inputu gracza.

Aby poruszać obiektami w Unity możemy przypisać im nową wartość pozycji w komponencie `Transform`

```csharp
Vector3 exampleMove = new Vector3(1f, 0f, 0f);
// w układzie 2D
// X, Y - pozycja
// Z - "warstwa" - obiekty o niższym Z będą zasłaniać inne

transform.position += exampleMove;
// transform z małej litery, to specjalna zmienna do której zawsze mamy dostęp w komponencie
```

Jeśli zmieniamy pozycję gracza co klatkę musimy pamiętać o tym żeby umieścić kod w `FixedUpdate()` (stały czas pomiędzy wywołaniami metody) albo w `Update()` dodać mnożenie przez `Time.deltaTime`.
