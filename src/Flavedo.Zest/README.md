# Flavedo.Zest

## Task module

`Task` は `C#` での `await` をサポートするための関数を提供しています。
また、`System.Threading.Tasks.Taskクラス` の一部のメソッドにも対応しています。

### Task.await

非同期処理を同期処理のように記述することが可能です。  
これは `C#` の `await` と対応しています。

```cs
// C#
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var path = "./sample.txt";
            using var reader = new StreamReader(path);
            var content = await reader.ReadToEndAsync();
        }
    }
}
```

```fsharp
// F#
open Flavedo.Zest
open System.IO

[<Literal>]
let path = "./sample.txt"
let reader = new StreamReader(path)
let content = Task.await reader.ReadToEndAsync ()
```

また、`ValueTask` については `Task.awaitv()` で待機可能です。

```fsharp
// F#
open Flavedo.Zest

let result = Task.awaitv SampleAsync ()
```

---

### Task.waitAll

複数の非同期タスクがすべて完了するまで待機することが可能です。
これは `C#` の `Task.WaitAll()` と対応しています。

```fsharp
// F#
open Flavedo.Zest

// 方法1
let task1 () = Task.delay 1000<millisec>
let task2 () = Task.delay 1500<millisec>
Task.waitAll (task1, task2) |> ignore
// 方法2
Task.waitAll (Task.delay 1000<millisec>, Task.delay 1500<millisec>) |> ignore
```

また `C#` の `Task.WhenAll()` についても `Task.waitAll()` で実現可能です。

```fsharp
// F#
open Flavedo.Zest
open System.IO

[<Literal>]
let path1 = "./sample1.txt"
[<Literal>]
let path2 = "./sample2.txt"
let reader1 = new StreamReader(path1)
let reader2 = new StreamReader(path2)

// 方法1
let task1 = reader1.ReadToEndAsync()
let task2 = reader2.ReadToEndAsync()
let (content1, content2) = Task.waitAll (task1, task2)
// 方法2
let (content1, content2) = Task.waitAll (reader1.ReadToEndAsync(), reader2.ReadToEndAsync())
```

---

### Task.waitAny

複数の非同期タスクのいずれかが完了するのを待機することが可能です。
これは `C#` の `Task.WaitAny()` と対応しています。

```fsharp
// F#
open Flavedo.Zest

// 方法1
let task1 () = Task.delay 1000<millisec>
let task2 () = Task.delay 1500<millisec>
Task.waitAny (task1, task2) |> ignore
// 方法2
Task.waitAny (Task.delay 1000<millisec>, Task.delay 1500<millisec>) |> ignore
```

---  

### Task.run

同期的処理を非同期処理として実行することが可能です。  
これは `C#` の `Task.Run()` と対応しています。

```cs
// C#
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                    Console.WriteLine($"i = {i}");
            });
        }
    }
}
```

```fsharp
// F#
open Flavedo.Zest

Task.await Task.run (fun () ->
    for i in 0..9 do
        printfn "i = %d" i ))
```

また、戻り値がある関数にも対応しています。

```cs
// C#
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = await Task.Run(() =>
            {
                // do something
                return "result";
            });
        }
    }
}
```

```fsharp
// F#
open Flavedo.Zest

let result = Task.await Task.run (fun () ->
    // do something
    "result" )
```

---  

### Task.delay

処理を一定時間遅延させたい場合に利用します。  
これは `C#` の `Task.Delay()` と対応しています。

```cs
// C#
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(1_000); // 1秒遅延
            Console.WriteLine("Hello, World!!");
        }
    }
}
```

```fsharp
// F#
open Flavedo.Zest

Task.await Task.delay 1_000<millisec> // 1秒遅延
printfn "Hello, World!!"
```
