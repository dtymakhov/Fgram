module FGram.Requests

open System
open System.IO
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

type GetChatRequest =
    { ChatId: ChatId }
    interface IRequest<Chat> with
        member _.MethodName = "getCHat"

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

let internal emptySendMessageRequest =
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

let internal emptyForwardMessageRequest =
    { ChatId = Id 0L
      FromChatId = 0L
      MessageId = 0L
      ProtectContent = None
      DisableNotification = None }

type SendPhotoRequest =
    { ChatId: ChatId
      Photo: InputFile
      Caption: string option
      ParseMode: ParseMode option
      DisableNotification: bool option
      ReplyToMessageId: int64 option
      ReplyMarkup: Markup option }
    interface IRequest<Message> with
        member _.MethodName = "sendPhoto"

// TODO: Add ability to serialize Uri to Json library. Replace InputFile.Url of string with InputFile.Url of Uri
let emptySendPhotoRequest =
    { ChatId = Id 0L
      Photo = InputFile("", Stream.Null)
      Caption = None
      ParseMode = None
      DisableNotification = None
      ReplyToMessageId = None
      ReplyMarkup = None }

type EditMessageTextRequest =
    { ChatId: ChatId
      MessageId: int64 option
      InlineMessageId: int64 option
      Text: string
      ParseMode: ParseMode option
      Entities: list<MessageEntity> option
      DisableWebPagePreview: bool option
      ReplyMarkup: InlineKeyboardMarkup option }
    interface IRequest<Message> with
        member _.MethodName = "editMessageText"

type EditMessageCaptionRequest =
    { ChatId: ChatId
      MessageId: int64 option
      InlineMessageId: int64 option
      Caption: string option
      ParseMode: ParseMode option
      CaptionEntities: list<MessageEntity> option
      DisableWebPagePreview: bool option
      ReplyMarkup: InlineKeyboardMarkup option }
    interface IRequest<Message> with
        member _.MethodName = "editMessageCaption"

type DeleteMessageRequest =
    { ChatId: ChatId
      MessageId: int64 }
    interface IRequest<bool> with
        member _.MethodName = "deleteMessage"

type GetChatAdministratorsRequest =
    { ChatId: ChatId }
    interface IRequest<list<ChatMember>> with
        member _.MethodName = "getChatAdministrators"