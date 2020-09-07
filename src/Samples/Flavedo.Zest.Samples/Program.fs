open System
open System.Threading.Tasks
open Flavedo.Zest


type internal NTask = System.Threading.Tasks.Task

let task () =
    NTask.Run(fun () -> 
        printfn "start"
        let t = NTask.Delay(3000)
        Task.WaitAll(t)
        printfn "finish")

let t = task()
printfn "sample1"
printfn "sample2"
printfn "sample3"
Task.await t
