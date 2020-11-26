namespace Flavedo.Grape

open Flavedo.Grape.Excel

module ExcelBuilder =

  type ExcelDocument = { document: Excel.T }

  type ExcelBuilder () =
    member __.Yield(_) =
      { document = Excel null }

    member __.Return(x) =
      x

    [<CustomOperation("create_blank")>]
    member __.Blank(doc, file_name) =
      { doc with document = Excel.create file_name }
        
    [<CustomOperation("add_sheet")>]
    member __.AddSheet(doc, sheet_name) =
      { doc with document = Excel.add_sheet sheet_name doc.document  }
        
    [<CustomOperation("save")>]
    member __.Save(doc) =
      { doc with document = Excel.save doc.document  }
        
    [<CustomOperation("close")>]
    member __.Close(doc) =
      Excel.close doc.document
      ()

  let excel = ExcelBuilder()
