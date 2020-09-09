// open Flavedo.Zest

// let t1 () = Task.run(fun () -> 
//     printfn "start1"
//     Task.await (Task.delay(1000<millisec>))
//     printfn "### 1"
//     Task.await (Task.delay(1000<millisec>))
//     printfn "### 1"
//     Task.await (Task.delay(1000<millisec>))
//     printfn "### 1"
//     Task.await (Task.delay(1000<millisec>))
//     printfn "finish1" )

// let t2 () = Task.run(fun () -> 
//     printfn "start2"
//     Task.await (Task.delay(800<millisec>))
//     printfn "### 2"
//     Task.await (Task.delay(800<millisec>))
//     printfn "### 2"
//     Task.await (Task.delay(800<millisec>))
//     printfn "### 2"
//     Task.await (Task.delay(800<millisec>))
//     printfn "finish2" )

// Task.await(t1(), t2())


// open type Flavedo.Zest.Task

open Flavedo.Zest

let vt1 () = Task.run(fun () -> 
    printfn "start1"
    Task.await (Task.delay(1000<millisec>))
    printfn "finish1" 
    500)

let vt2 () = Task.run(fun () -> 
    printfn "start2"
    Task.await (Task.delay(800<millisec>))
    printfn "finish2"
    "vtask2")

let (v1, v2) = Task.await(vt1(), vt2())
printfn "v1= %d, v2= %s" v1 v2

