open System.Threading
open FGram.TestBot.Token
open FGram.Bot
open FGram.Types
open FGram.Methods

let config =
    { Token = token
      Timeout = Some 5000
      AllowedUpdates = None
      Limit = None }

let bot = Bot(config)

[<Command("start")>]
let onStart (message: Message) =
    bot
    |> sendMessage (Id message.Chat.Id) "Hello"
    |> Async.RunSynchronously
    |> ignore

let photoUrl =
    "https://upload.wikimedia.org/wikipedia/en/a/a9/Example.jpg"

[<Command("sendPhoto")>]
let onSendPhoto (message: Message) =
    bot
    |> sendPhoto (Id message.Chat.Id) (Url photoUrl)
    |> Async.RunSynchronously
    |> ignore

[<Command("sendPhotoWithCaption")>]
let onSendPhotoWithCaption (message: Message) =
    bot
    |> sendPhotoWithCaption (Id message.Chat.Id) (Url photoUrl) "Caption"
    |> Async.RunSynchronously
    |> ignore

[<Command("replyToPhoto")>]
let onReplyPhoto (message: Message) =
    bot
    |> replyToPhoto (Id message.Chat.Id) (Url photoUrl) message.MessageId
    |> Async.RunSynchronously
    |> ignore

[<Command("getChat")>]
let onGetChat (message: Message) =
    bot
    |> sendMessage (Id message.Chat.Id) "Hello"
    |> Async.RunSynchronously
    |> ignore

[<Command("editMessage")>]
let onEditMessage (message: Message) =
    let result =
        bot
        |> sendMessage (Id message.Chat.Id) "Hello"
        |> Async.RunSynchronously

    Thread.Sleep(5000)

    match result.Result with
    | Some message ->
        bot
        |> editText (Id message.Chat.Id) message.MessageId "Edited"
        |> Async.RunSynchronously
        |> ignore
    | None -> None |> ignore

[<Command("deleteMessage")>]
let onDeleteMessage (message: Message) =
    let result =
        bot
        |> sendMessage (Id message.Chat.Id) "Hello"
        |> Async.RunSynchronously

    Thread.Sleep(5000)

    match result.Result with
    | Some message ->
        bot
        |> deleteMessage (Id message.Chat.Id) message.MessageId
        |> Async.RunSynchronously
        |> ignore
    | None -> None |> ignore

let onUpdateReceive (_: List<Update>) = true

let main () =
    bot.startAsync onUpdateReceive
    |> Async.RunSynchronously

[<EntryPoint>]
main ()
