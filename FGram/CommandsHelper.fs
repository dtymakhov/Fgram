module internal FGram.CommandsHelper

open System
open System.Collections.Generic
open System.Reflection
open FGram.Types

let isAttributeExist (attributes: IList<CustomAttributeTypedArgument>, name: string) =
    attributes |> Seq.exists (fun a -> a.Value = name)

let internal getMethod name =
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
    | Some m ->
        match m.Text with
        | Some text -> text.StartsWith("/")
        | None -> false
    | None -> false

let getCommands (updates: list<Update>) =
    updates
    |> List.filter (fun x -> isCommand x.Message)

let invokeCommand (message: Message) =
    let charLocation =
        message.Text.Value.IndexOf(" ", StringComparison.Ordinal)

    let command =
        if charLocation > 0 then
            message.Text.Value.Substring(1, charLocation)
        else
            message.Text.Value.Substring(1)

    match getMethod command with
    | Some method -> method.Invoke(null, [| message |]) |> ignore
    | None -> None |> ignore

let invokeCommands (updates: list<Update>) =
    updates
    |> List.iter (fun x -> invokeCommand x.Message.Value)

let processUpdates updates (onUpdate: list<Update> -> bool) =
    getCommands updates |> invokeCommands

    updates
    |> List.filter (fun x -> not (isCommand x.Message))
    |> onUpdate
    |> ignore
