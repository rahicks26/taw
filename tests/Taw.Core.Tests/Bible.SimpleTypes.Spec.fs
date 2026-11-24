module Taw.Core.Tests.Bible.SimpleTypes.Spec

open Expecto
open Taw.Core.Bible.SimpleTypes

[<Tests>]
let tests =
    testList
        "Bible.SimpleTypes"
        [
            testList
                "Bible.Book"
                [
                    testCase "parse should return an error when null"
                    <| fun () ->
                        let bibleName = null
                        let expected = bibleName |> Bible.BookIsNotKnown |> Error

                        let result = Bible.Book.parse bibleName

                        Expect.isError result "Null is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "parse should return an error when empty"
                    <| fun () ->
                        let bibleName = ""
                        let expected = bibleName |> Bible.BookIsNotKnown |> Error

                        let result = Bible.Book.parse bibleName

                        Expect.isError result "Empty is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "parse should return an error when name is not in known book names"
                    <| fun () ->
                        let bibleName = "mark" // Not an OT Bible Book
                        let expected = bibleName |> Bible.BookIsNotKnown |> Error

                        let result = Bible.Book.parse bibleName

                        Expect.isError result "Mark is not valid."
                        Expect.equal result expected "Should be an invalid book name."

                    testCase "parse should return valid names for OT books"
                    <| fun () ->
                        let bibleNames = Bible.Book.canonical_books |> Set.toList // Not an OT Bible Book

                        let valueIsLower =
                            function
                            | Ok n ->
                                let str = n |> Bible.Book.value
                                str = str.ToLower ()
                            | Error _ -> false

                        let result = bibleNames |> List.map Bible.Book.parse

                        Expect.all result Result.isOk "All OT Bible names should return ok"
                        Expect.all result valueIsLower "Value should be lower"
                ]
            testList
                "Bible.Chapter"
                [
                    testCase "parse should return an error if below minimum"
                    <| fun () ->
                        let chapter = Bible.Chapter.min - 1
                        let expected = chapter |> Bible.ChapterIsOutOfRange |> Error

                        let actual = Bible.Chapter.parse chapter

                        Expect.isError actual "Should be an invalid chapter."
                        Expect.equal actual expected "Should be an invalid chapter."

                    testCase "parse should return an error if above the maximum"
                    <| fun () ->
                        let chapter = Bible.Chapter.max + 1
                        let expected = chapter |> Bible.ChapterIsOutOfRange |> Error

                        let actual = Bible.Chapter.parse chapter

                        Expect.isError actual "Should be an invalid chapter."
                        Expect.equal actual expected "Should be an invalid chapter."

                    testCase "parse should return a valid chapter when between or equal to min or max"
                    <| fun () ->
                        let chapters = [ Bible.Chapter.min ; Bible.Chapter.max ; 75 ]

                        let actual = chapters |> List.map Bible.Chapter.parse

                        Expect.all actual Result.isOk "Valid chapters should return ok"
                ]
            testList
                "Bible.Text"
                [
                    testCase "parse should return an error if null"
                    <| fun () ->
                        let text = null
                        let expected = Bible.TextIsEmptyOrNull |> Error

                        let actual = Bible.Text.parse text

                        Expect.isError actual "Should be an invalid text."
                        Expect.equal actual expected "Should be an invalid text."

                    testCase "parse should return an error if empty"
                    <| fun () ->
                        let text = ""
                        let expected = Bible.TextIsEmptyOrNull |> Error

                        let actual = Bible.Text.parse text

                        Expect.isError actual "Should be an invalid text."
                        Expect.equal actual expected "Should be an invalid text."

                    testCase "parse should return a valid chapter when not empty or null"
                    <| fun () ->
                        let text = "not empty ;)"

                        let actual = Bible.Text.parse text

                        Expect.isOk actual "Should be an valid text."
                ]
            testList
                "Bible.VerseNumber"
                [
                    testCase "parse should return an error if below minimum"
                    <| fun () ->
                        let verse_number = Bible.VerseNumber.min - 1
                        let expected = verse_number |> Bible.VerseNumberIsOutOfRange |> Error

                        let actual = Bible.VerseNumber.parse verse_number

                        Expect.isError actual "Should be an invalid verse number."
                        Expect.equal actual expected "Should be an invalid verse number."

                    testCase "parse should return an error if above the maximum"
                    <| fun () ->
                        let verse_number = Bible.VerseNumber.max + 1
                        let expected = verse_number |> Bible.VerseNumberIsOutOfRange |> Error

                        let actual = Bible.VerseNumber.parse verse_number

                        Expect.isError actual "Should be an invalid verse number."
                        Expect.equal actual expected "Should be an invalid verse number."

                    testCase "parse should return a valid chapter when between or equal to min or max"
                    <| fun () ->
                        let verse_numbers = [ Bible.VerseNumber.min ; Bible.VerseNumber.min ; 26 ]

                        let actual = verse_numbers |> List.map Bible.VerseNumber.parse

                        Expect.all actual Result.isOk "Valid verse numbers should return ok"
                ]
            testList
                "Bible.Version"
                [
                    testCase "parse should return an error if null"
                    <| fun () ->
                        let version = null
                        let expected = Bible.VersionIsEmptyOrNull |> Error

                        let actual = Bible.Version.parse version

                        Expect.isError actual "Should be an invalid version."
                        Expect.equal actual expected "Should be an invalid version."

                    testCase "parse should return an error if empty"
                    <| fun () ->
                        let version = ""
                        let expected = Bible.VersionIsEmptyOrNull |> Error

                        let actual = Bible.Version.parse version

                        Expect.isError actual "Should be an invalid version."
                        Expect.equal actual expected "Should be an invalid version."

                    testCase "parse should return an error if more then 100 characters long"
                    <| fun () ->
                        // A simple string that is more than long 100 ;)
                        let version = [ 1..101 ] |> List.map (fun n -> n.ToString ()) |> String.concat ""
                        let expected = version |> Bible.VersionIsTooLong |> Error

                        let actual = Bible.Version.parse version

                        Expect.isError actual "Should be an invalid version."
                        Expect.equal actual expected "Should be an invalid version."

                    testCase "parse should return a valid chapter when not empty or null"
                    <| fun () ->
                        let version = "CSB"

                        let actual = Bible.Version.parse version

                        Expect.isOk actual "Should be an valid version."
                ]
        ]
