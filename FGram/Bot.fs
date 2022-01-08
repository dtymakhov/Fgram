module FGram.Bot

open System
open System.Collections.Generic
open System.Net.Http
open System.Reflection
open System.Text
open System.Threading
open FSharp.Json
open Microsoft.FSharp.Control
open FGram.HttpService
open FGram.Requests
open FGram.Types

[<AttributeUsage(AttributeTargets.Method, AllowMultiple = false)>]
type CommandAttribute(_name: string) =
    inherit Attribute()

let isAttributeExist (attributes: IList<CustomAttributeTypedArgument>, name: string) =
    attributes |> Seq.exists (fun a -> a.Value = name)

let getMethod name =
    let types = Assembly.GetEntryAssembly().GetTypes()

    let command =
        types
        |> Array.collect (fun typ -> typ.GetMethods())
        |> Array.tryFind
            (fun mi ->
                mi.CustomAttributes
                |> Seq.exists
                    (fun attr ->
                        attr.AttributeType = typeof<CommandAttribute>
                        && isAttributeExist (attr.ConstructorArguments, name)))

    command

let isCommand (message: Message option) =
    match message with
    | Some m -> m.Text.Value.StartsWith("/")
    | None -> false

let getCommands (updates: list<Update>) =
    updates
    |> List.filter (fun x -> isCommand x.Message)

let invokeCommand (message: Message) =
    let charLocation =
        message.Text.Value.IndexOf(" ", StringComparison.Ordinal)

    let command =
        if charLocation > 0 then
            message.Text.Value.Substring(0, charLocation)
        else
            message.Text.Value

    match getMethod command with
    | Some method -> method.Invoke(null, [| message |]) |> ignore
    | None -> None |> ignore

let invokeCommands (updates: list<Update>) =
    updates
    |> List.iter (fun x -> invokeCommand x.Message.Value)

let config =
    JsonConfig.create (jsonFieldNaming = Json.snakeCase, serializeNone = SerializeNone.Omit)

type Bot(token: string) =
    member this.Token = token

    member this.startAsync(onUpdate: list<Update> -> bool) =
        async { return! this.getUpdates onUpdate 0L }

    member this.sendRequest(request: IRequest<'a>, ?requestType: RequestType) =
        async {
            let postRequest =
                lazy
                    (let json = Json.serializeEx config request
                     let url = $"/bot{this.Token}/{request.MethodName}"
                     postAsync (url, json))

            let! result =
                match requestType with
                | Some requestType ->
                    match requestType with
                    | Get ->
                        let url = $"/bot{this.Token}/{request.MethodName}"
                        getAsync url
                    | Post -> postRequest.Force()
                | None -> postRequest.Force()

            return Json.deserializeEx<Result<'a>> config result
        }

    member this.getUpdates (onUpdate: list<Update> -> bool) offset =
        let request =
            { Offset = offset
              Limit = None
              Timeout = None
              AllowedUpdates = None }

        let result =
            this.sendRequest request
            |> Async.RunSynchronously

        if result.Ok && result.Result.IsSome then
            getCommands result.Result.Value |> invokeCommands
            onUpdate result.Result.Value |> ignore

        Thread.Sleep(10000)

        let offset =
            match result.Result with
            | Some updates -> updates.Head.UpdateId + 1L
            | None -> 0L

        this.getUpdates onUpdate offset

    member this.getMe =
        let request = GetMeRequest()
        this.sendRequest request
