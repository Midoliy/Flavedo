 open System.Net.Http
 open type Flavedo.Zest.Task

 async {
    use client = new HttpClient()
    let! response = await (client.GetAsync("https://midoliy.com"))
    return! await (response.Content.ReadAsStringAsync())
 }
 |> Async.RunSynchronously
 |> printfn "%s"

