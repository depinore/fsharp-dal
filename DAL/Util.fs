namespace DAL

//These are just utility functions that makes it easier to use functional-style programming
//on top of Dapper.
module Utils =
    open System.Data.SqlClient
    open Dapper

    let dapperQuery<'Result> (query:string) (connection:SqlConnection) =
            Async.AwaitTask(connection.QueryAsync<'Result>(query))
    let dapperParametrizedQuery<'Result> (query:string) (param:obj) (connection:SqlConnection) : Async<'Result seq> =
        Async.AwaitTask(connection.QueryAsync<'Result>(query, param))