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
    |> sendMessage message.Chat.Id "Hello"
    |> Async.RunSynchronously
    |> ignore

let onUpdateReceive (_: List<Update>) = true

let main () =
    bot.startAsync onUpdateReceive
    |> Async.RunSynchronously

[<EntryPoint>]
main ()
