module FGram.Methods

open FGram.Bot
open FGram.Requests

let getMe (bot: Bot) =
    let request = GetMeRequest()
    bot.sendRequest request

let sendMessageGeneral (request: SendMessageRequest) (bot: Bot) = bot.sendRequest request

let sendMessage chatId text (bot: Bot) =
    let request =
        { emptySendMessageRequest with
              ChatId = chatId
              Text = text }

    bot.sendRequest request

let sendMessageReplyTo chatId text replyToMessageId (bot: Bot) =
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
