module FGram.HttpService

open System
open System.Net.Http
open System.Text

let httpClient = new HttpClient()
httpClient.BaseAddress <- Uri("https://api.telegram.org/")

let getAsync (url: string) =
    async {
        let! response = httpClient.GetAsync(url) |> Async.AwaitTask

        let! content =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        return content
    }

let postAsync (url: string, json: string) =
    async {
        let content =
            new StringContent(json, Encoding.UTF8, "application/json")

        let! response =
            httpClient.PostAsync(url, content)
            |> Async.AwaitTask

        let! content =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        return content
    }
