namespace Flavedo.Zest

open System
open System.Threading
open System.Threading.Tasks

module Task =
    let unwrap (task:System.Threading.Tasks.Task<'T>) =
        task :> System.Threading.Tasks.Task
        
type internal NTask<'T> = System.Threading.Tasks.Task<'T>
type internal NTask = System.Threading.Tasks.Task
type internal VTask<'T> = System.Threading.Tasks.ValueTask<'T>
type internal VTask = System.Threading.Tasks.ValueTask

type Task () =
    static member waitAll ([<ParamArray>]tasks, token:CancellationToken option) =
        match token with
        | Some t -> Task.WaitAll(tasks, t)
        | None -> Task.WaitAll(tasks)

    static member waitAny ([<ParamArray>]tasks, token:CancellationToken option) =
        match token with
        | Some t -> Task.WaitAny(tasks, t)
        | None -> Task.WaitAny(tasks)
        
    static member await (task:NTask, ?token:CancellationToken) =
        Task.waitAll([| task |], token)
        
    static member await (task1:NTask, task2:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, task4:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, task8:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; task8; |], token)
        
    static member await (task1:NTask, task2:NTask, task3:NTask, task4:NTask, task5:NTask, task6:NTask, task7:NTask, task8:NTask, task9:NTask, ?token:CancellationToken) =
        Task.waitAll([| task1; task2; task3; task4; task5; task6; task7; task8; task9; |], token)
        
    static member await (task:VTask, ?token:CancellationToken) =
        Task.waitAll([| task.AsTask() |], token)
        
    static member await (task1:VTask, task2:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, task4:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, task8:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); |], token)
        
    static member await (task1:VTask, task2:VTask, task3:VTask, task4:VTask, task5:VTask, task6:VTask, task7:VTask, task8:VTask, task9:VTask, ?token:CancellationToken) =
        Task.waitAll([| task1.AsTask(); task2.AsTask(); task3.AsTask(); task4.AsTask(); task5.AsTask(); task6.AsTask(); task7.AsTask(); task8.AsTask(); task9.AsTask(); |], token)

    static member await<'T> (task:NTask<'T>, ?token:CancellationToken) =
        let task' = Task.unwrap task
        Task.waitAll([| task' |], token)
        task.Result

    static member await<'T1, 'T2> (task1:NTask<'T1>, task2:NTask<'T2>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result)
    
    static member await<'T1, 'T2, 'T3> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result)
        
    static member await<'T1, 'T2, 'T3, 'T4> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result)
        
    static member await<'T1, 'T2, 'T3, 'T4, 'T5> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result)
        
    static member await<'T1, 'T2, 'T3, 'T4, 'T5, 'T6> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result)
        
    static member await<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result)
        
    static member await<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, task8:NTask<'T8>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result)
        
    static member await<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> (task1:NTask<'T1>, task2:NTask<'T2>, task3:NTask<'T3>, task4:NTask<'T4>, task5:NTask<'T5>, task6:NTask<'T6>, task7:NTask<'T7>, task8:NTask<'T8>, task9:NTask<'T9>, ?token:CancellationToken) =
        let tasks = [| Task.unwrap task1; Task.unwrap task2; Task.unwrap task3; Task.unwrap task4; Task.unwrap task5; Task.unwrap task6; Task.unwrap task7; Task.unwrap task8; Task.unwrap task9; |]
        Task.waitAll(tasks, token)
        (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result)
