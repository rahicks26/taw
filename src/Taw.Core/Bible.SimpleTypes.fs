module Taw.Core.Bible.SimpleTypes

open System

[<RequireQualifiedAccess>]
module Bible =

    type Error =
        | InvalidBook of string
        | InvalidChapter of int
        | InvalidVerseNumber of int

    [<Struct>]
    type Book = private | Book of string

    [<Struct>]
    type Chapter = private | Chapter of int

    [<Struct>]
    type VerseNumber = private | VerseNumber of int

    [<RequireQualifiedAccess>]
    module Book =
        let all_books =
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

        let (|IsValidBookName|_|) str =
            let bookWasFound s = all_books |> Set.contains s

            str
            |> Option.ofObj
            |> Option.filter (String.IsNullOrWhiteSpace >> not)
            |> Option.map (fun str -> str.ToLower ())
            |> Option.filter bookWasFound

        let create =
            function
            | IsValidBookName str -> str |> Book |> Ok
            | str -> str |> InvalidBook |> Error

        let value (Book str) = str

    [<RequireQualifiedAccess>]
    module Chapter =
        let max_chapter = 150
        let min_chapter = 0

        let (|IsValidChapter|_|) num =
            if min_chapter <= num && num <= max_chapter then
                Some num
            else
                None

        let create =
            function
            | IsValidChapter num -> num |> Chapter |> Ok
            | num -> num |> InvalidChapter |> Error

        let value (Chapter num) = num

    [<RequireQualifiedAccess>]
    module VerseNumber =
        let max_verse_number = 176
        let min_verse_number = 1

        let (|IsValidVerseNumber|_|) num =
            if min_verse_number <= num && num <= max_verse_number then
                Some num
            else
                None

        let create =
            function
            | IsValidVerseNumber num -> num |> VerseNumber |> Ok
            | num -> num |> InvalidVerseNumber |> Error
