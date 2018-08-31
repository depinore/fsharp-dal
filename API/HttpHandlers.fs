namespace API

module HttpHandlers =

    open Microsoft.AspNetCore.Http
    open Giraffe
    open DAL.Products
    open FSharp.Control.Tasks.V2.ContextInsensitive

    let commonProductLetter =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! mostCommonLetter = Async.StartAsTask(getMostCommonProductNameLetter ())
                let message = sprintf "The most common letter was %c with %d occurrences." (fst mostCommonLetter) (snd mostCommonLetter)
                return! text message next ctx
            }