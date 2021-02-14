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
    static member await (async:Async<'T>) =
        async

    static member await (task:System.Threading.Tasks.Task) =
        Async.AwaitTask task

    static member await (task:System.Threading.Tasks.Task<'T>) =
        Async.AwaitTask task
        
    static member await (task:System.Threading.Tasks.ValueTask) =
        Async.AwaitTask (task.AsTask())

    static member await (task:System.Threading.Tasks.ValueTask<'T>) =
        Async.AwaitTask (task.AsTask())

    static member internal unwrap (task:System.Threading.Tasks.Task<'T>) =
        task :> System.Threading.Tasks.Task

    static member internal unwrap (task:System.Threading.Tasks.ValueTask<'T>) =
        task.AsTask() :> System.Threading.Tasks.Task
        
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
        
    static member waitAll (task:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, task7:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, task7:System.Threading.Tasks.Task, task8:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; task8; |], token)
        
    static member waitAll (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, task7:System.Threading.Tasks.Task, task8:System.Threading.Tasks.Task, task9:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; task8; task9; |], token)
        
    static member waitAll (task:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task.AsTask() |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, task7:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, task7:System.Threading.Tasks.ValueTask, task8:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); |], token)
        
    static member waitAll (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, task7:System.Threading.Tasks.ValueTask, task8:System.Threading.Tasks.ValueTask, task9:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); task9.AsTask(); |], token)

    static member waitAll<'T> (task:System.Threading.Tasks.Task<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAll([| task' |], token)
        task.Result

    static member waitAll<'T1, 'T2> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result)
    
    static member waitAll<'T1, 'T2, 'T3> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, task7:System.Threading.Tasks.Task<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, task7:System.Threading.Tasks.Task<'T7>, task8:System.Threading.Tasks.Task<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result)
        
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, task7:System.Threading.Tasks.Task<'T7>, task8:System.Threading.Tasks.Task<'T8>, task9:System.Threading.Tasks.Task<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result)
        
    static member waitAll<'T> (task:System.Threading.Tasks.ValueTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAll([| task' |], token)
        task.Result

    static member waitAll<'T1, 'T2> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result)
        
    static member waitAll<'T1, 'T2, 'T3> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, task7:System.Threading.Tasks.ValueTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, task7:System.Threading.Tasks.ValueTask<'T7>, task8:System.Threading.Tasks.ValueTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result)
            
    static member waitAll<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, task7:System.Threading.Tasks.ValueTask<'T7>, task8:System.Threading.Tasks.ValueTask<'T8>, task9:System.Threading.Tasks.ValueTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result)

    static member waitAny (task:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, task7:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; task7; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, task7:System.Threading.Tasks.Task, task8:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; task7; task8; |], token)
        
    static member waitAny (task1:System.Threading.Tasks.Task, task2:System.Threading.Tasks.Task, task3:System.Threading.Tasks.Task, task4:System.Threading.Tasks.Task, task5:System.Threading.Tasks.Task, task6:System.Threading.Tasks.Task, task7:System.Threading.Tasks.Task, task8:System.Threading.Tasks.Task, task9:System.Threading.Tasks.Task, ?token:CancellationToken) =
        Task.waitAny([| task1; task2; task3; task4; task5; task6; task7; task8; task9; |], token)
        
    static member waitAny (task:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task.AsTask() |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, task7:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, task7:System.Threading.Tasks.ValueTask, task8:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); |], token)
        
    static member waitAny (task1:System.Threading.Tasks.ValueTask, task2:System.Threading.Tasks.ValueTask, task3:System.Threading.Tasks.ValueTask, task4:System.Threading.Tasks.ValueTask, task5:System.Threading.Tasks.ValueTask, task6:System.Threading.Tasks.ValueTask, task7:System.Threading.Tasks.ValueTask, task8:System.Threading.Tasks.ValueTask, task9:System.Threading.Tasks.ValueTask, ?token:CancellationToken) =
        Task.waitAny([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); task9.AsTask(); |], token)

    static member waitAny<'T> (task:System.Threading.Tasks.Task<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAny([| task' |], token)

    static member waitAny<'T1, 'T2> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAny(tasks, token)
        
    static member waitAny<'T1, 'T2, 'T3> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, task7:System.Threading.Tasks.Task<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, task7:System.Threading.Tasks.Task<'T7>, task8:System.Threading.Tasks.Task<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:System.Threading.Tasks.Task<'T1>, task2:System.Threading.Tasks.Task<'T2>, task3:System.Threading.Tasks.Task<'T3>, task4:System.Threading.Tasks.Task<'T4>, task5:System.Threading.Tasks.Task<'T5>, task6:System.Threading.Tasks.Task<'T6>, task7:System.Threading.Tasks.Task<'T7>, task8:System.Threading.Tasks.Task<'T8>, task9:System.Threading.Tasks.Task<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T> (task:VTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAny([| task' |], token)

    static member waitAny<'T1, 'T2> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAny(tasks, token)
            
    static member waitAny<'T1, 'T2, 'T3> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, task7:System.Threading.Tasks.ValueTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, task7:System.Threading.Tasks.ValueTask<'T7>, task8:System.Threading.Tasks.ValueTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAny(tasks, token)
                
    static member waitAny<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:System.Threading.Tasks.ValueTask<'T1>, task2:System.Threading.Tasks.ValueTask<'T2>, task3:System.Threading.Tasks.ValueTask<'T3>, task4:System.Threading.Tasks.ValueTask<'T4>, task5:System.Threading.Tasks.ValueTask<'T5>, task6:System.Threading.Tasks.ValueTask<'T6>, task7:System.Threading.Tasks.ValueTask<'T7>, task8:System.Threading.Tasks.ValueTask<'T8>, task9:System.Threading.Tasks.ValueTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAny(tasks, token)
