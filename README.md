# Case Assignment

## NEWS Calculator

The [**N**ational **E**arly **W**arning **S**core](https://www.mdcalc.com/calc/1873/national-early-warning-score-news) determines the degree of illness of a patient and prompts critical care intervention.

## Starting the NEWS Calculator locally

1. To start the NEWS calculator locally, run the following command:
    ```bash
    docker compose up --build -d
    ```

2. Visit: [http://localhost:3000](http://localhost:3000)

3. To stop everything, run:
    ```bash
    docker compose down
    ```

## Additional Considerations

- Should the UI provide recommendations depending on the NEWS score?
- How about limitations (e.g., pregnancy, age less than 16, etc.)? Should those somehow be visible in the UI?
- Should the UI include shortcuts to telephone numbers (or other communication channels) if the score is too high?
