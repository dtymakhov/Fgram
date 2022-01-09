module FGram.Types

open System
open System.IO
open FSharp.Json

[<AttributeUsage(AttributeTargets.Method, AllowMultiple = false)>]
type CommandAttribute(_name: string) =
    inherit Attribute()

type ChatType =
    | Private
    | Group
    | SuperGroup
    | Channel
    | Unknown

type MaskPoint =
    | Forehead
    | Eyes
    | Mouth
    | Chin

type ParseMode =
    | Markdown
    | HTML

type ChatAction =
    | Typing
    | UploadPhoto
    | RecordVideo
    | UploadVideo
    | RecordAudio
    | UploadAudio
    | UploadDocument
    | FindLocation
    | RecordVideoNote
    | UploadVideoNote

type ChatMemberStatus =
    | Creator
    | Administrator
    | Member
    | Restricted
    | Left
    | Kicked
    | Unknown

[<JsonUnion(Mode = UnionMode.AsValue)>]
type ChatId =
    | Id of int64
    | Username of string

[<JsonUnion(Mode = UnionMode.AsValue)>]
type InputFile =
    | Url of string 
    | InputFile of string * Stream

type Location = { Longitude: double; Latitude: double }

type User =
    { Id: int64
      IsBot: bool
      FirstName: string
      LastName: string option
      Username: string option
      LanguageCode: string option }

type ChatPhoto =
    { SmallFileId: string
      BigFileId: string }

type ChatPermissions =
    { CanSendMessages: bool option
      CanSendMediaMessages: bool option
      CanSendPools: bool option
      CanSendOtherMessages: bool option
      CanAddWebPagePreviews: bool option
      CanChangeInfo: bool option
      CanInviteUsers: bool option
      CanPinMessages: bool option }

type PollOption = { Text: string; VoterCount: int }

type Poll =
    { Id: string
      Question: string
      Options: PollOption []
      IsClosed: bool }

type LoginUrl =
    { Url: string
      ForwardText: string option
      BotUsername: string option
      RequestWriteAccess: bool option }

type InlineKeyboardButton =
    { Text: string
      Url: string option
      LoginUrl: LoginUrl option
      CallbackData: string option
      SwitchInlineQuery: string option
      SwitchInlineQueryCurrentChat: string option
      Pay: bool option }

type KeyboardButton =
    { Text: string
      RequestContact: bool option
      RequestLocation: bool option }

type InlineKeyboardMarkup =
    { InlineKeyboard: InlineKeyboardButton list list }

type ReplyKeyboardMarkup =
    { Keyboard: KeyboardButton list list
      ResizeKeyboard: bool option
      OneTimeKeyboard: bool option
      Selective: bool option }

type ReplyKeyboardRemove =
    { RemoveKeyboard: bool
      Selective: bool option }

type ForceReply =
    { ForceReply: bool
      Selective: bool option }

type Chat =
    { Id: int64
      Type: ChatType
      Title: string option
      Username: string option
      FirstName: string option
      LastName: string option
      AllMembersAreAdministrators: bool option
      Photo: ChatPhoto option
      Description: string option
      InviteLink: string option
      PinnedMessage: Message option
      Permissions: ChatPermissions option
      StickerSetName: string option
      CanSetStickerSet: bool option }

and MessageEntity =
    { Type: string
      Offset: int64
      Length: int64
      Url: string option
      User: User option }

and Audio =
    { FileId: string
      Duration: int
      Performer: string option
      Title: string option
      MimeType: string option
      FileSize: int option
      Thumb: PhotoSize option }

and PhotoSize =
    { FileId: string
      Width: int
      Height: int
      FileSize: int option }

and Document =
    { FileId: string
      Thumb: PhotoSize option
      FileName: string option
      MimeType: string option
      FileSize: int option }

and MaskPosition =
    { Point: MaskPoint
      XShift: float
      YShift: float
      Scale: float }

and Sticker =
    { FileId: string
      Width: int
      Height: int
      IsAnimated: bool
      Thumb: PhotoSize option
      Emoji: string option
      SetName: string option
      MaskPosition: MaskPosition option
      FileSize: int option }

and StickerSet =
    { Name: string
      Title: string
      IsAnimated: bool
      ContainsMasks: bool
      Stickers: Sticker list }

and Video =
    { FileId: string
      Width: int
      Height: int
      Duration: int
      Thumb: PhotoSize option
      MimeType: string option
      FileSize: int option }

and Voice =
    { FileId: string
      Duration: int
      MimeType: string option
      FileSize: int option }

and Contact =
    { PhoneNumber: string
      FirstName: string
      LastName: string option
      UserId: int option
      VCard: string option }

and Venue =
    { Location: Location
      Title: string
      Address: string
      FoursquareId: string option
      FoursquareType: string option }

and UserProfilePhotos =
    { TotalCount: int
      Photos: list<list<PhotoSize>> }

and File =
    { FileId: string
      FileSize: int option
      FilePath: string option }

and Animation =
    { FileId: string
      Width: int
      Height: int
      Duration: int
      Thumb: PhotoSize option
      FileName: string option
      MimeType: string option
      FileSize: int option }

and VideoNote =
    { FileId: string
      Length: int
      Duration: int
      Thumb: PhotoSize option
      FileSize: int option }

and Message =
    { MessageId: int64
      From: User option

      [<JsonField(Transform = typeof<Transforms.DateTimeEpoch>)>]
      Date: DateTime
      Chat: Chat
      ForwardFrom: User option
      ForwardFromChat: Chat option
      ForwardFromMessageId: int64 option
      ForwardSignature: string option
      ForwardSenderName: string option

      [<JsonField(Transform = typeof<Transforms.DateTimeEpoch>)>]
      ForwardDate: DateTime option
      ReplyToMessage: Message option

      [<JsonField(Transform = typeof<Transforms.DateTimeEpoch>)>]
      EditDate: DateTime option

      AuthorSignature: string option
      Text: string option
      Entities: MessageEntity List option
      CaptionEntities: MessageEntity list option
      Audio: Audio option
      Document: Document option
      Animation: Animation option
      Photo: PhotoSize list option
      Sticker: Sticker option
      Video: Video option
      Voice: Voice option
      VideoNote: VideoNote option
      Caption: string option
      Contact: Contact option
      Location: Location option
      Venue: Venue option
      Poll: Poll option
      NewChatMembers: User list option
      LeftChatMember: User option
      NewChatTitle: string option
      NewChatPhoto: PhotoSize list option
      DeleteChatPhoto: bool option
      GroupChatCreated: bool option
      SupergroupChatCreated: bool option
      ChannelChatCreated: bool option
      MigrateToChatId: int64 option
      MigrateFromChatId: int64 option
      PinnedMessage: Message option
      ConnectedWebsite: string option
      ReplyMarkup: InlineKeyboardMarkup option }

type InlineQuery =
    { Id: string
      From: User
      Location: Location option
      Query: string
      Offset: string }

type ChosenInlineResult =
    { ResultId: string
      From: User
      Location: Location option
      InlineMessageId: string option
      Query: string }

type CallbackQuery =
    { Id: string
      From: User
      Message: Message option
      InlineMessageId: string option
      ChatInstance: string
      Data: string option
      GameShortName: string option }

type Update =
    { UpdateId: int64
      Message: Message option
      EditedMessage: Message option
      ChannelPost: Message option
      EditedChannelPost: Message option
      InlineQuery: InlineQuery option
      ChosenInlineResult: ChosenInlineResult option
      CallbackQuery: CallbackQuery option
      Poll: Poll option }

type ChatMember =
    { User: User
      Status: ChatMemberStatus

      [<JsonField(Transform = typeof<Transforms.DateTimeEpoch>)>]
      UntilDate: DateTime option

      CanBeEdited: bool option
      CanPostMessages: bool option
      CanEditMessages: bool option
      CanDeleteMessages: bool option
      CanInviteUsers: bool option
      CanRestrictMembers: bool option
      CanPinMessages: bool option
      CanPromoteMembers: bool option
      CanChangeInfo: bool option
      CanSendMessages: bool option
      CanSendMediaMessages: bool option
      CanSendPolls: bool option
      CanSendOtherMessages: bool option
      CanAddWebPagePreviews: bool option }

type ResponseParameters =
    { MigrateToChatId: int option
      RetryAfter: int option }

type Markup =
    | InlineKeyboardMarkup of InlineKeyboardMarkup
    | ReplyKeyboardMarkup of ReplyKeyboardMarkup
    | ReplyKeyboardRemove of ReplyKeyboardRemove
    | ForceReply of ForceReply

type EditMessageResult =
    | Message of Message
    | Success of bool

type InputTextMessageContent =
    { MessageText: string
      ParseMode: ParseMode option
      DisableWebPagePreview: bool option }

type InputLocationMessageContent =
    { Latitude: float
      Longitude: float
      LivePeriod: int option }

type InputVenueMessageContent =
    { Latitude: float
      Longitude: float
      Title: string
      Address: string
      FoursquareId: string option
      FoursquareType: string option }

type InputContactMessageContent =
    { PhoneNumber: string
      FirstName: string
      LastName: string option
      VCard: string option }

type InputMessageContent =
    | TextMessage of InputTextMessageContent
    | LocationMessage of InputLocationMessageContent
    | VenueMessage of InputVenueMessageContent
    | ContactMessage of InputContactMessageContent

type Config =
    { Token: string
      Limit: int option
      Timeout: int option
      AllowedUpdates: string option }

type Result<'a> =
    { Ok: bool
      Result: 'a option
      ErrorCode: int option
      Description: string option }
