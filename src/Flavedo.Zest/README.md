# Flavedo.Zest

## Task module

`Task` は `C#` での `await` をサポートするための関数を提供しています。
また、`System.Threading.Tasks.Taskクラス` の一部のメソッドにも対応しています。

### Task.await

以下のような C# コードをほぼそのままの形で移植することが可能です。
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
let content = Task.await (reader.ReadToEndAsync())
```

また、複数の非同期タスクを待機する場合は以下のように記述可能です。

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
let (content1, content2) = Task.await (task1, task2)
// 方法2
let (content1, content2) = Task.await (reader1.ReadToEndAsync(), reader2.ReadToEndAsync())
```
