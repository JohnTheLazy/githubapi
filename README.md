# githubapi (c#)

Sample Endpoint:
<http://localhost:2685/v1/users/retrieveUsers>

Sample Usage:
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