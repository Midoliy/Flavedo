namespace Flavedo.Zest

open System
open System.Threading
open System.Threading.Tasks

type internal NTask<'T> = Task<'T>
type internal NTask = Task
type internal VTask<'T> = ValueTask<'T>
type internal VTask = ValueTask

[<AbstractClass; Sealed>]
type Task private() =
    static member private as_task (value_task: ValueTask<'U>) =
        value_task.AsTask()

    static member await async =
        Async.AwaitTask async
        
    static member await (task:ValueTask) =
        Async.AwaitTask (task.AsTask())

    static member await (task:ValueTask<'T>) =
        Async.AwaitTask (task.AsTask())

    static member internal unwrap (task:NTask<'T>) =
        task :> NTask

    static member internal unwrap (task:VTask<'T>) =
        task.AsTask() :> NTask
        
    static member run<'T> (f:unit->'T, ?token:CancellationToken) =
        match token with
        | Some t -> 
            let task = async { 
                return! NTask.Run(f, t) |> Async.AwaitTask }
            Async.StartAsTask (task, cancellationToken=t)
        | None -> 
            async { 
                return! NTask.Run(f) |> Async.AwaitTask }
            |> Async.StartAsTask
        
    static member delay (time:int<millisec>, ?token:CancellationToken) =
        match token with
        | Some t -> Async.StartAsTask (Async.Sleep (int time), cancellationToken=t)
        | None -> Async.StartAsTask (Async.Sleep (int time))

    static member waitAll ([<ParamArray>]tasks, token:CancellationToken option) =
        match token with
        | Some t -> Task.WaitAll(tasks, t)
        | None -> Task.WaitAll(tasks)

    static member waitAny ([<ParamArray>]tasks, token:CancellationToken option) =
        match token with
        | Some t -> Task.WaitAny(tasks, t)
        | None -> Task.WaitAny(tasks)
        
    static member waitAll (task:NTask, ?token:CancellationToken) =
        Task.waitAll([| task |], token)
        
    static member waitAll (task1:NTask, task2:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, task4:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, task8:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; task8; |], token)
        
    static member waitAll (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, task8:NTask, task9:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; task8; task9; |], token)
        
    static member waitAll (task:VTask, ?token:CancellationToken) =
        Task.waitAll([| task.AsTask() |], token)
        
    static member waitAll (task1:VTask, task2:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, task4:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, task8:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); |], token)
        
    static member waitAll (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, task8:VTask, task9:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); task9.AsTask(); |], token)

    static member waitAll<'T> (task:NTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAll([| task' |], token)
        task.Result

    static member waitAll<'T1, 'T2> (task1:NTask<'T1>, task2:NTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result)
    
    static member waitAll<'T1, 'T2, 'T3> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, task8:NTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, task8:NTask<'T8>, task9:NTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result)
        
    static member waitAll<'T> (task:VTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAll([| task' |], token)
        task.Result

    static member waitAll<'T1, 'T2> (task1:VTask<'T1>, task2:VTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result)
        
    static member waitAll<'T1, 'T2, 'T3> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, task7:VTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, task7:VTask<'T7>, task8:VTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, task7:VTask<'T7>, task8:VTask<'T8>, task9:VTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result)

    static member waitAny (task:NTask, ?token:CancellationToken) =
        Task.waitAny([| task |], token)
        
    static member waitAny (task1:NTask, task2:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, task4:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; task7; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, task8:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; task7; task8; |], token)
        
    static member waitAny (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, task8:NTask, task9:NTask, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; task7; task8; task9; |], token)
        
    static member waitAny (task:VTask, ?token:CancellationToken) =
        Task.waitAny([| task.AsTask() |], token)
        
    static member waitAny (task1:VTask, task2:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, task4:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, task8:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); |], token)
        
    static member waitAny (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, task8:VTask, task9:VTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); task9.AsTask(); |], token)

    static member waitAny<'T> (task:NTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAny([| task' |], token)

    static member waitAny<'T1, 'T2> (task1:NTask<'T1>, task2:NTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAny(tasks, token)
        
    static member waitAny<'T1, 'T2, 'T3> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, task8:NTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, task8:NTask<'T8>, task9:NTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T> (task:VTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAny([| task' |], token)

    static member waitAny<'T1, 'T2> (task1:VTask<'T1>, task2:VTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, task7:VTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, task7:VTask<'T7>, task8:VTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:VTask<'T1>, task2:VTask<'T2>, task3:VTask<'T3>, task4:VTask<'T4>, task5:VTask<'T5>, task6:VTask<'T6>, task7:VTask<'T7>, task8:VTask<'T8>, task9:VTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAny(tasks, token)
