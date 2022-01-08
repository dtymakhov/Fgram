module FGram.Requests

open FGram.Types

type RequestType =
    | Get
    | Post

type IRequest<'a> =
    abstract MethodName : string

type GetUpdatesRequest() =
    interface IRequest<Result> with
        member _.MethodName = "getUpdates"

type GetMeRequest() =
    interface IRequest<User> with
        member _.MethodName = "getMe"

type SendMessageRequest =
    { ChatId: int64
      Text: string
      ParseMode: ParseMode option
      DisableWebPagePreview: bool option
      DisableNotification: bool option
      ReplyToMessageId: int64 option
      ReplyMarkup: Markup option }
    interface IRequest<Message> with
        member _.MethodName = "sendMessage"

let emptySendMessageRequest =
    { ChatId = 0L
      Text = ""
      ParseMode = None
      DisableWebPagePreview = None
      DisableNotification = None
      ReplyToMessageId = None
      ReplyMarkup = None }

type ForwardMessageRequest =
    { ChatId: int64
      FromChatId: int64
      MessageId: int64
      ProtectContent: bool option
      DisableNotification: bool option }
    interface IRequest<Message> with
        member _.MethodName = "forwardMessage"

let emptyForwardMessageRequest =
    { ChatId = 0L
      FromChatId = 0L
      MessageId = 0L
      ProtectContent = None
      DisableNotification = None }
