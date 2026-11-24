module Taw.Core.Bible.SimpleTypes

open System
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module Bible =

    type Error =
        | InvalidBook of string
        | InvalidChapter of int
        | InvalidText of string
        | InvalidVerseNumber of int

    [<Struct>]
    type Book =
        private
        | Book of string

        static member canonical_books =
            set
                [
                    "genesis"
                    "exodus"
                    "leviticus"
                    "numbers"
                    "deuteronomy"
                    "joshua"
                    "judges"
                    "ruth"
                    "1 samuel"
                    "2 samuel"
                    "1 kings"
                    "2 kings"
                    "1 chronicles"
                    "2 chronicles"
                    "ezra"
                    "nehemiah"
                    "esther"
                    "job"
                    "psalms"
                    "proverbs"
                    "ecclesiastes"
                    "song of solomon"
                    "isaiah"
                    "jeremiah"
                    "lamentations"
                    "ezekiel"
                    "daniel"
                    "hosea"
                    "joel"
                    "amos"
                    "obadiah"
                    "jonah"
                    "micah"
                    "nahum"
                    "habakkuk"
                    "zephaniah"
                    "haggai"
                    "zechariah"
                    "malach"
                ]

        static member parse str =
            let bookWasFound s = Book.canonical_books |> Set.contains s

            str
            |> Option.ofObj
            |> Option.filter (String.IsNullOrWhiteSpace >> not)
            |> Option.map (fun str -> str.ToLower ())
            |> Option.filter bookWasFound
            |> Result.requireSomeWith (fun () -> str |> InvalidBook)
            |> Result.map Book

        static member value (Book str) = str

    [<Struct>]
    type Chapter =
        private
        | Chapter of int

        static member max = 150
        static member min = 0

        static member parse num =
            if Chapter.min <= num && num <= Chapter.max then
                num |> Chapter |> Ok
            else
                num |> InvalidChapter |> Error

        static member value (Chapter num) = num

    [<Struct>]
    type Text =
        private
        | Text of string

        static member parse str =
            match str with
            | str when String.IsNullOrWhiteSpace str -> str |> InvalidText |> Error
            | str -> str |> Text |> Ok

        static member value (Text str) = str

    [<Struct>]
    type VerseNumber =
        private
        | VerseNumber of int

        static member max = 176
        static member min = 1

        static member parse num =
            if VerseNumber.min <= num && num <= VerseNumber.max then
                num |> VerseNumber |> Ok
            else
                num |> InvalidVerseNumber |> Error
