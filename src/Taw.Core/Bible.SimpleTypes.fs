module Taw.Bible.SimpleTypes

open System

type BibleError =
    | InvalidBookName of string
    | InvalidChapter of int
    | InvalidVerseNumber of int

[<Struct>]
type BookName = private | BookName of string

[<Struct>]
type Chapter = private | Chapter of int

[<Struct>]
type VerseNumber = private | VerseNumber of int

module BookName =
    let books =
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
        let bookWasFound s = books |> Set.contains s

        str
        |> Option.ofObj
        |> Option.filter (String.IsNullOrWhiteSpace >> not)
        |> Option.map (fun str -> str.ToLower ())
        |> Option.filter bookWasFound

    let create =
        function
        | IsValidBookName str -> str |> BookName |> Ok
        | str -> str |> InvalidBookName |> Error

    let value (BookName str) = str

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
