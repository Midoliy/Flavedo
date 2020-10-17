namespace Flavedo.Zest

module String =
    let compare a b = 
        System.String.Compare(a, b)
    let compare' a b (ignoreCase:bool) = 
        System.String.Compare(a, b, ignoreCase)
    let compare'' a b (ignoreCase:bool) (culture:System.Globalization.CultureInfo) =
        System.String.Compare(a, b, ignoreCase, culture)

    let concat (s0:string) (s1:string) =
        System.String.Concat(s0, s1)
    let concat' (s0:string) (s1:string) (s2:string) =
        System.String.Concat(s0, s1, s2)
    let concat'' (args:string array) =
        System.String.Concat(args)

    let equals a b =
        System.String.Equals(a, b)
    let equals' a b (comparison:System.StringComparison) =
        System.String.Equals(a, b, comparison)

    let isEmpty = 
        System.String.IsNullOrEmpty
    let isWhitespace = 
        System.String.IsNullOrWhiteSpace


