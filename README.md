# Park IT Tech Test

See: https://github.com/holidayextras/recruitment-tasks

## API

This is a starter of a small API service, the project is broken down into API related functionality and core related functionality.  This defined boundaries around responsibilities and enables core to be utilised in a wider array of services.  It's running with the repository pattern separating the storage from the business entities.  Ideally all the data mapping logic would be outside the repository, so this could be unit tested whereas the repository is only covered by integration tests.

The project is using a SQLite database for portability of the example and any migrations would be managed using the CLI.