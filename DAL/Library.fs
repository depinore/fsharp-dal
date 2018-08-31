
namespace DAL
open System.Threading.Tasks

module Products =
    open System.Data.SqlClient

    //type definitions
    type Product = { ProductID: int; Name: string; ProductNumber: string; Color: string; }

    //functions
    let getMostCommonProductNameLetter () =
        async {
            use connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb; Integrated Security=True;Database=adventureworkslt2016")

            let! products =
                connection
                |> Utils.dapperQuery<Product> "select productid, name, productnumber, color from saleslt.product"

            //We could do a lot of this work just in SQL, but I wanted to showcase the expressiveness of F#
            return products
            |> Seq.map (fun product -> product.Name.[0])                //get the first character in the name.
            |> Seq.groupBy (fun x -> x)                                 //group by this letter
            |> Seq.map (fun (key, items) -> (key, Seq.length items))    //convert each grouping into a tuple
            |> Seq.maxBy (fun (key, size) -> size)                      //find the tuple with the largest size of occurrences.
        }