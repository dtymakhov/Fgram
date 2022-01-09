module FGram.Requests

open FGram.Types

type IRequest<'a> =
    abstract MethodName : string

type GetUpdatesRequest =
    { Offset: int64
      Limit: int option
      Timeout: int option
      AllowedUpdates: string option }
    interface IRequest<list<Update>> with
        member _.MethodName = "getUpdates"

type SendMessageRequest =
    { ChatId: ChatId
      Text: string
      ParseMode: ParseMode option
      DisableWebPagePreview: bool option
      DisableNotification: bool option
      ReplyToMessageId: int64 option
      ReplyMarkup: Markup option }
    interface IRequest<Message> with
        member _.MethodName = "sendMessage"

let emptySendMessageRequest =
    { ChatId = Id 0L
      Text = ""
      ParseMode = None
      DisableWebPagePreview = None
      DisableNotification = None
      ReplyToMessageId = None
      ReplyMarkup = None }

type ForwardMessageRequest =
    { ChatId: ChatId
      FromChatId: int64
      MessageId: int64
      ProtectContent: bool option
      DisableNotification: bool option }
    interface IRequest<Message> with
        member _.MethodName = "forwardMessage"

let emptyForwardMessageRequest =
    { ChatId = Id 0L
      FromChatId = 0L
      MessageId = 0L
      ProtectContent = None
      DisableNotification = None }
