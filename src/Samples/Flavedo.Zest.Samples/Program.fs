 open Flavedo.Zest

 let t1 () = Task.run(fun () -> 
     printfn "start1"
     Task.await Task.delay (1000<millisec>)
     printfn "### 1"
     Task.await Task.delay (1000<millisec>)
     printfn "### 1"
     Task.await Task.delay (1000<millisec>)
     printfn "### 1"
     Task.await Task.delay (1000<millisec>)
     printfn "finish1" )

 let t2 () = Task.run(fun () -> 
     printfn "start2"
     Task.await Task.delay (800<millisec>)
     printfn "### 2"
     Task.await Task.delay (800<millisec>)
     printfn "### 2"
     Task.await Task.delay (800<millisec>)
     printfn "### 2"
     Task.await Task.delay (800<millisec>)
     printfn "finish2" )

 Task.waitAll (t1(), t2())
 |> ignore

 let result = Task.await Task.run (fun () -> 
     printfn "start2"
     Task.await Task.delay (800<millisec>)
     printfn "### 2"
     Task.await Task.delay (800<millisec>)
     printfn "### 2"
     Task.await Task.delay (800<millisec>)
     printfn "### 2"
     Task.await Task.delay (800<millisec>)
     printfn "finish2"
     "f")

// open type Flavedo.Zest.Task

//open Flavedo.Zest

//let vt1 = Task.run(fun () -> 
//    printfn "start1"
//    Task.await Task.delay 1000<millisec>
//    printfn "finish1" 
//    500)
    
//printfn "## foo"
//Task.await Task.delay 100<millisec>

//let vt2 = Task.run(fun () -> 
//    printfn "start2"
//    Task.await Task.delay 800<millisec>
//    printfn "finish2"
//    "vtask2")
    
//let vt3 (x:int, y:int) = Task.run(fun () -> 
//    let time:int<millisec> = LanguagePrimitives.Int32WithMeasure (x + y)
//    printfn "start2"
//    Task.await Task.delay time
//    printfn "finish2"
//    "vtask2")
    
//let vt4 (x:int, y:int) = Task.run(fun () -> 
//    let time:int<millisec> = LanguagePrimitives.Int32WithMeasure (x + y)
//    printfn "start2"
//    Task.await Task.delay time
//    printfn "finish2" )
    
//printfn "## bar"

//let (v1, v2) = Task.waitAll(vt1, vt2)
//printfn "v1= %d, v2= %s" v1 v2
