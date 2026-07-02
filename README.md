# Block4
### Keuze voor een in-memory database

Voor dit project is gekozen voor een in-memory database in plaats van een persistente database of SQLite. De focus lag op snel ontwikkelen, testen en valideren van functionaliteit. Een in-memory database is hiervoor beter geschikt omdat wijzigingen direct zichtbaar zijn en er geen setup nodig is voor een database, schema’s of migraties. Dit maakt de ontwikkelcyclus sneller en eenvoudiger.

Daarnaast was het belangrijk om de applicatie makkelijk te kunnen testen zonder afhankelijk te zijn van externe bestanden of databases, waardoor fouten sneller opgespoord en opgelost konden worden.

**Let op:**

* Data is niet persistent en gaat verloren bij herstart van de applicatie.
