# GitHub API using C# and .NET Core

**C# Task:**
* Create a net core Webapi project that has an API endpoint called retrieveUsers. This endpoint takes:
    * List<string> usernames: This is an list of usernames that will be used to look up basic information from GitHub's public API. Only users in this list should be retrieved from Github.
* This API call returns to the user a list of basic information for those users including:
    * name
    * login
    * company
    * number of followers
    * number of public repositories
    * The average number of followers per public repository (ie. number of followers divided by the number of public repositories)
* The returned users should be sorted alphabetically by name
* If some usernames cannot be found, this should not fail the other usernames that were requested
* Implement a caching layer (eg. memory cache, redis, etc) that will store a user that has been retrieved from GitHub for 2 minutes.
    * A user should be cached for 2 minutes
    * Each user should be cached individually. For example, if I request users A and B, then do another request inside 2 minutes for users B, C and D, user B should come from the cache and users C and D should come from GitHub
    * If a user is returned from the cache, it should not hit GitHub for that user
* Treat this endpoint like it was going into production. Include error and exception handling and the appropriate frameworks to make the project more extensible. For example, if the app can’t connect to redis and it throws an exception, that user should be retrieved from GitHub instead
* Use regular http calls to hit GitHub's API, don’t use any pre made GitHub net core libraries to integrate with GitHub’s API
* The API endpoint needed to get Github user information is https://api.github.com/users/{username}
* Include unit tests and integration tests in the project to test the above logic. Any testing frameworks can be used for this (for example, XUnit and Moq)
    * Integration tests should be used to test your code against the live endpoints such as Redis and gitHub
    * Unit tests should be used to test your code’s logic without the external endpoint dependancies.

* Sample API endpoint
<http://localhost:2685/v1/users/retrieveUsers>

* Sample Code:
```csharp
using (HttpClient client = new ClientProvider().Client)
{
    string requestUri = "/v1/users/retrieveUsers";
    HttpResponseMessage response = await client.PostAsync(requestUri,
        new StringContent(JsonConvert.SerializeObject(userNames),
        Encoding.UTF8,
        "application/json"));

    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
    byte[] byteArray = buffer.ToArray();
    string content = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
    List<GitHubUser> users = new List<GitHubUser>();
    try
    {
        users = JsonConvert.DeserializeObject<List<GitHubUser>>(content);
    }
    catch
    {
    }
    ViewBag.Result = users;
}
```
