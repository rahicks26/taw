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
                        let expected = bibleName |> InvalidBookName |> Error

                        let result = BookName.create bibleName

                        Expect.isError result "Null is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "create should return an error when empty"
                    <| fun () ->
                        let bibleName = ""
                        let expected = bibleName |> InvalidBookName |> Error

                        let result = BookName.create bibleName

                        Expect.isError result "Empty is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "create should return an error when name is not in known book names"
                    <| fun () ->
                        let bibleName = "mark" // Not an OT Bible Book
                        let expected = bibleName |> InvalidBookName |> Error

                        let result = BookName.create bibleName

                        Expect.isError result "Mark is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "create should return valid names for OT books"
                    <| fun () ->
                        let bibleNames = BookName.books |> Set.toList // Not an OT Bible Book

                        let valueIsLower =
                            function
                            | Ok n ->
                                let str = n |> BookName.value
                                str = str.ToLower ()
                            | Error _ -> false

                        let result = bibleNames |> List.map BookName.create

                        Expect.all result Result.isOk "All OT Bible names should return ok"
                        Expect.all result valueIsLower "Value should be lower"
                ]
            testList
                "Chapter"
                [
                    testCase "create should return an error if below minimum"
                    <| fun () ->
                        let chapter = Chapter.min_chapter - 1
                        let expected = chapter |> InvalidChapter |> Error

                        let actual = Chapter.create chapter

                        Expect.isError actual "Should be an invalid chapter."
                        Expect.equal actual expected "Should be an invalid chapter."

                    testCase "create should return an error if above the maximum"
                    <| fun () ->
                        let chapter = Chapter.max_chapter + 1
                        let expected = chapter |> InvalidChapter |> Error

                        let actual = Chapter.create chapter

                        Expect.isError actual "Should be an invalid chapter."
                        Expect.equal actual expected "Should be an invalid chapter."

                    testCase "create should return a valid chapter when between or equal to min or max"
                    <| fun () ->
                        let chapters = [ Chapter.min_chapter ; Chapter.max_chapter ; 75 ]

                        let actual = chapters |> List.map Chapter.create

                        Expect.all actual Result.isOk "Valid chapters should return ok"
                ]
            testList
                "VerseNumber"
                [
                    testCase "create should return an error if below minimum"
                    <| fun () ->
                        let verse_number = VerseNumber.min_verse_number - 1
                        let expected = verse_number |> InvalidVerseNumber |> Error

                        let actual = VerseNumber.create verse_number

                        Expect.isError actual "Should be an invalid verse number."
                        Expect.equal actual expected "Should be an invalid verse number."

                    testCase "create should return an error if above the maximum"
                    <| fun () ->
                        let verse_number = VerseNumber.max_verse_number + 1
                        let expected = verse_number |> InvalidVerseNumber |> Error

                        let actual = VerseNumber.create verse_number

                        Expect.isError actual "Should be an invalid verse number."
                        Expect.equal actual expected "Should be an invalid verse number."

                    testCase "create should return a valid chapter when between or equal to min or max"
                    <| fun () ->
                        let verse_numbers =
                            [ VerseNumber.min_verse_number ; VerseNumber.min_verse_number ; 26 ]

                        let actual = verse_numbers |> List.map VerseNumber.create

                        Expect.all actual Result.isOk "Valid verse numbers should return ok"
                ]
        ]
