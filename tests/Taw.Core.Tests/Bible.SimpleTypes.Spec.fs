module Taw.Core.Tests.Bible.SimpleTypes.Spec

open Expecto
open Taw.Core.Bible.SimpleTypes

[<Tests>]
let tests =
    testList
        "Bible.SimpleTypes"
        [
            testList
                "BookName"
                [
                    testCase "create should return an error when null"
                    <| fun () ->
                        let bibleName = null
                        let expected = bibleName |> Bible.InvalidBook |> Error

                        let result = Bible.Book.create bibleName

                        Expect.isError result "Null is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "create should return an error when empty"
                    <| fun () ->
                        let bibleName = ""
                        let expected = bibleName |> Bible.InvalidBook |> Error

                        let result = Bible.Book.create bibleName

                        Expect.isError result "Empty is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "create should return an error when name is not in known book names"
                    <| fun () ->
                        let bibleName = "mark" // Not an OT Bible Book
                        let expected = bibleName |> Bible.InvalidBook |> Error

                        let result = Bible.Book.create bibleName

                        Expect.isError result "Mark is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "create should return valid names for OT books"
                    <| fun () ->
                        let bibleNames = Bible.Book.all_books |> Set.toList // Not an OT Bible Book

                        let valueIsLower =
                            function
                            | Ok n ->
                                let str = n |> Bible.Book.value
                                str = str.ToLower ()
                            | Error _ -> false

                        let result = bibleNames |> List.map Bible.Book.create

                        Expect.all result Result.isOk "All OT Bible names should return ok"
                        Expect.all result valueIsLower "Value should be lower"
                ]
            testList
                "Chapter"
                [
                    testCase "create should return an error if below minimum"
                    <| fun () ->
                        let chapter = Bible.Chapter.min_chapter - 1
                        let expected = chapter |> Bible.InvalidChapter |> Error

                        let actual = Bible.Chapter.create chapter

                        Expect.isError actual "Should be an invalid chapter."
                        Expect.equal actual expected "Should be an invalid chapter."

                    testCase "create should return an error if above the maximum"
                    <| fun () ->
                        let chapter = Bible.Chapter.max_chapter + 1
                        let expected = chapter |> Bible.InvalidChapter |> Error

                        let actual = Bible.Chapter.create chapter

                        Expect.isError actual "Should be an invalid chapter."
                        Expect.equal actual expected "Should be an invalid chapter."

                    testCase "create should return a valid chapter when between or equal to min or max"
                    <| fun () ->
                        let chapters = [ Bible.Chapter.min_chapter ; Bible.Chapter.max_chapter ; 75 ]

                        let actual = chapters |> List.map Bible.Chapter.create

                        Expect.all actual Result.isOk "Valid chapters should return ok"
                ]
            testList
                "VerseNumber"
                [
                    testCase "create should return an error if below minimum"
                    <| fun () ->
                        let verse_number = Bible.VerseNumber.min_verse_number - 1
                        let expected = verse_number |> Bible.InvalidVerseNumber |> Error

                        let actual = Bible.VerseNumber.create verse_number

                        Expect.isError actual "Should be an invalid verse number."
                        Expect.equal actual expected "Should be an invalid verse number."

                    testCase "create should return an error if above the maximum"
                    <| fun () ->
                        let verse_number = Bible.VerseNumber.max_verse_number + 1
                        let expected = verse_number |> Bible.InvalidVerseNumber |> Error

                        let actual = Bible.VerseNumber.create verse_number

                        Expect.isError actual "Should be an invalid verse number."
                        Expect.equal actual expected "Should be an invalid verse number."

                    testCase "create should return a valid chapter when between or equal to min or max"
                    <| fun () ->
                        let verse_numbers =
                            [ Bible.VerseNumber.min_verse_number ; Bible.VerseNumber.min_verse_number ; 26 ]

                        let actual = verse_numbers |> List.map Bible.VerseNumber.create

                        Expect.all actual Result.isOk "Valid verse numbers should return ok"
                ]
        ]
