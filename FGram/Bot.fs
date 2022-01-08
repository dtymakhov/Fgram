module FGram.Bot

open System.Net.Http
open System.Text
open FSharp.Json
open FGram.HttpService
open FGram.Requests
open FGram.Types
open FGram.CommandsHelper

let jsonConfig =
    JsonConfig.create (jsonFieldNaming = Json.snakeCase, serializeNone = SerializeNone.Omit)

type Bot(config: Config) =
    member this.startAsync(onUpdate: list<Update> -> bool) =
        async { return! this.getUpdates onUpdate 0L }

    member this.getMe =
        async {
            let! result = getAsync $"/bot{config.Token}/getMe"
            return Json.deserializeEx<Result<User>> jsonConfig result
        }

    member internal this.sendRequest(request: IRequest<'a>) =
        async {
            let json = Json.serializeEx jsonConfig request

            let url =
                $"/bot{config.Token}/{request.MethodName}"

            let! result = postAsync (url, json)
            return Json.deserializeEx<Result<'a>> jsonConfig result
        }

    member internal this.getUpdates (onUpdate: list<Update> -> bool) offset =
        async {
            let request =
                { Offset = offset
                  Limit = config.Limit
                  Timeout = config.Timeout
                  AllowedUpdates = config.AllowedUpdates }

            let! result = this.sendRequest request

            match result.Result with
            | Some updates -> processUpdates updates onUpdate
            | None -> None |> ignore

            let offset =
                match result.Result with
                | Some result when result.IsEmpty -> offset
                | Some updates ->
                    updates
                    |> List.map (fun x -> x.UpdateId)
                    |> List.max
                    |> (+) 1L
                | None -> offset

            this.getUpdates onUpdate offset
            |> Async.RunSynchronously
        }