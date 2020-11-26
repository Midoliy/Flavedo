namespace Flavedo.Grape

open System
open System.IO
open System.Linq
open System.Collections.Generic
open DocumentFormat.OpenXml
open DocumentFormat.OpenXml.Packaging
open DocumentFormat.OpenXml.Spreadsheet

type ExcelDocument (document: SpreadsheetDocument) as self =
  [<Literal>]
  let default_sheetname = "Sheet"
  let mutable selected_sheet : WorksheetPart = null
  /// <summary>
  /// シート名生成関数
  /// </summary>
  let generate_sheet_name(name: string option) = 
    // TODO: 同一シート名が存在している場合の処理を入れる
    let number = document.WorkbookPart.WorksheetParts |> Seq.length
    match name with
    | Some sheet_name -> 
      if String.IsNullOrWhiteSpace sheet_name 
        then $"%s{default_sheetname}%d{number}"
        else sheet_name
    | None -> $"%s{default_sheetname}%d{number}"
    
  /// <summary>
  /// Rowオブジェクトを取得する
  /// </summary>
  let find_row (sheet_data:SheetData) row_index =
    let row = sheet_data.Elements<Row>().FirstOrDefault(fun r -> r.RowIndex = row_index)
    if row = null
      then 
        let new_row = Row()
        new_row.RowIndex <- row_index
        sheet_data.Append new_row
        new_row
      else 
        row

  /// <summary>
  /// セル新規作成関数
  /// </summary>
  let insert_cell(row_index, column_index) =
    let worksheet = selected_sheet.Worksheet
    let sheet_data = worksheet.GetFirstChild<SheetData>()

    let row = find_row sheet_data row_index
    let ref_cell = row.Elements<Cell>().FirstOrDefault(fun c -> c. = )
    

    ()

  do
    if document.WorkbookPart = null
      then 
        document.AddWorkbookPart() |> ignore
        document.WorkbookPart.Workbook <- Workbook()
      else ()
    if document.WorkbookPart.Workbook.Sheets = null
      then document.WorkbookPart.Workbook.AppendChild(Sheets()) |> ignore
      else ()
    selected_sheet <- document.WorkbookPart.WorksheetParts.FirstOrDefault()

  interface IDisposable with
    member __.Dispose() = document.Dispose()
  member __.dispose() = (self:>IDisposable).Dispose()
  member __.close() = document.Close()
  member __.save() = document.Save()
  member __.save_as(file_path) = 
    let doc = document.SaveAs(file_path) :?> SpreadsheetDocument
    new ExcelDocument(doc)

  static member create(file_path: string) =
    let doc = SpreadsheetDocument.Create(file_path, SpreadsheetDocumentType.Workbook)
    let excel = new ExcelDocument(doc)
    excel.append_sheet()
    excel

  member __.append_sheet(?sheet_name) =
    let worksheet_part = document.WorkbookPart.AddNewPart<WorksheetPart>()
    worksheet_part.Worksheet <- Worksheet(SheetData() :> OpenXmlElement)
    let sheet = document.WorkbookPart.Workbook.Sheets.AppendChild(Sheet())
    sheet.Id <- document.WorkbookPart.GetIdOfPart worksheet_part |> StringValue
    sheet.SheetId <- document.WorkbookPart.WorksheetParts |> Seq.length |> (uint >> UInt32Value)
    sheet.Name <- generate_sheet_name(sheet_name) |> StringValue
    selected_sheet <- if selected_sheet = null 
                        then document.WorkbookPart.WorksheetParts.FirstOrDefault()
                        else selected_sheet
