# Park IT Tech Test

See: https://github.com/holidayextras/recruitment-tasks

## API

This is a starter of a small API service, the project is broken down into API related functionality and core related functionality.  This defined boundaries around responsibilities and enables core to be utilised in a wider array of services.  It's running with the repository pattern separating the storage from the business entities.  Ideally all the data mapping logic would be outside the repository, so this could be unit tested whereas the repository is only covered by integration tests.

The project is using a SQLite database for portability of the example and any migrations would be managed using the CLI.

## SPA

I simple React app build in Type Script with Redux state management.  Particular issues were that the Flickr API is not intended to be used directly from clients using things like fetch as there's no CORS and the json format is actually jsonp.  It doesn't actually work in Internet Explorer but I'm sure that it just needs a compatibility plugin to get it up to speed.  I'm sure if I just consulted someone more experienced with this they would be able to point me in the right direction.

Considerations:

- Use of CSS variables
- Use of CSS media colour scheme detection
- Use of CSS object-fit
- Use of debounce for the search form
- I'm familiar with SASS but didn't get round to setting it up here