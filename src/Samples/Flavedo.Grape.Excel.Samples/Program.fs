open System
open Flavedo.Grape
open Flavedo.Grape.ExcelBuilder

"sample.xlsx"
|> Excel.create 
|> Excel.add_sheet ""
|> Excel.close


let doc = ExcelDocument.create "sample2.xlsx"
let doc2 = doc.save_as "sample3.xlsx"
doc.dispose()
doc2.append_sheet()
doc2.dispose()







excel {
  create_blank "sample.xlsx"
  add_sheet "Sample"

  // do something

  save
  close
}
