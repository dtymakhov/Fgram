module FGram.Methods

open FGram.Bot
open FGram.Requests

let sendMessageGeneral (request: SendMessageRequest) (bot: Bot) = bot.sendRequest request

let sendMessage chatId text (bot: Bot) =
    let request =
        { emptySendMessageRequest with
              ChatId = chatId
              Text = text }

    bot.sendRequest request

let replyTo chatId text replyToMessageId (bot: Bot) =
    let request =
        { emptySendMessageRequest with
              ChatId = chatId
              Text = text
              ReplyToMessageId = replyToMessageId }

    bot.sendRequest request

let forwardMessageGeneral (request: ForwardMessageRequest) (bot: Bot) = bot.sendRequest request

let forwardMessage chatId fromChatId messageId (bot: Bot) =
    let request =
        { emptyForwardMessageRequest with
              ChatId = chatId
              FromChatId = fromChatId
              MessageId = messageId }

    bot.sendRequest request

let sendPhotoGeneral (request: SendPhotoRequest) (bot: Bot) = bot.sendRequest request

// TODO: Fix InputFile of string * Stream
let sendPhoto chatId photo (bot: Bot) =
    let request =
        { emptySendPhotoRequest with
              ChatId = chatId
              Photo = photo }

    bot.sendRequest request

let sendPhotoWithCaption chatId photo caption (bot: Bot) =
    let request =
        { emptySendPhotoRequest with
              ChatId = chatId
              Photo = photo
              Caption = Some caption }

    bot.sendRequest request

let replyToPhoto chatId photo replyToMessageId (bot: Bot) =
    let request =
        { emptySendPhotoRequest with
              ChatId = chatId
              Photo = photo
              ReplyToMessageId = Some replyToMessageId }

    bot.sendRequest request

let replyToPhotoWithCaption chatId photo replyToMessageId caption (bot: Bot) =
    let request =
        { emptySendPhotoRequest with
              ChatId = chatId
              Photo = photo
              ReplyToMessageId = replyToMessageId
              Caption = caption }

    bot.sendRequest request
