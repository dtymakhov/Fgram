module FGram.Methods

open FGram.Bot
open FGram.Requests

let getChat chatId (bot: Bot) =
    let request = { ChatId = chatId }
    bot.sendRequest request

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

let forwardMessage chatId fromChatId messageId (bot: Bot) =
    let request =
        { emptyForwardMessageRequest with
              ChatId = chatId
              FromChatId = fromChatId
              MessageId = messageId }

    bot.sendRequest request

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

let editText chatId messageId text (bot: Bot) =
    let request =
        { ChatId = chatId
          MessageId = Some messageId
          InlineMessageId = None
          Text = text
          ParseMode = None
          Entities = None
          DisableWebPagePreview = None
          ReplyMarkup = None }

    bot.sendRequest request

let editCaption chatId messageId caption (bot: Bot) =
    let request =
        { ChatId = chatId
          MessageId = Some messageId
          InlineMessageId = None
          Caption = caption
          ParseMode = None
          CaptionEntities = None
          DisableWebPagePreview = None
          ReplyMarkup = None }

    bot.sendRequest request
