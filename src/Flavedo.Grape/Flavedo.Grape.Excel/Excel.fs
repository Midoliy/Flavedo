namespace Flavedo.Grape

open System
open System.IO
open System.Linq
open DocumentFormat.OpenXml
open DocumentFormat.OpenXml.Packaging
open DocumentFormat.OpenXml.Spreadsheet



module Excel =
  type T = 
    Excel of SpreadsheetDocument

  [<Literal>]
  let private extension = 
    ".xlsx"

  [<Literal>]
  let private default_sheet_name =
    "Sheet"

  /// <summary>
  /// 1シート追加する
  /// </summary>
  let add_sheet (sheet_name: string) (Excel document) =
    let workbook_part =
      if document.WorkbookPart = null 
        then 
          let part = document.AddWorkbookPart()
          part.Workbook <- Workbook()
          part
        else 
          document.WorkbookPart
    let worksheet_part = 
      workbook_part.AddNewPart<WorksheetPart>()
    worksheet_part.Worksheet <- Worksheet(SheetData() :> OpenXmlElement)
    let sheets = 
      if workbook_part.Workbook.Sheets = null
        then workbook_part.Workbook.AppendChild(Sheets())
        else workbook_part.Workbook.Sheets
    let sheet =
      sheets.AppendChild(Sheet())
    let count =
      sheets.Count() |> uint
    let name = 
      if String.IsNullOrWhiteSpace sheet_name 
        // TODO
        then $"%s{default_sheet_name}%d{count}"
        else sheet_name
    sheet.Id <- StringValue(document.WorkbookPart.GetIdOfPart worksheet_part)
    sheet.SheetId <- UInt32Value(count)
    sheet.Name <- StringValue(name)
    document.Save()
    Excel document
        
  /// <summary>
  /// Excelファイルを保存する
  /// </summary>
  let save (Excel document) =
    document.Save()
    Excel document

  /// <summary>
  /// 空のExcelファイルを作成する
  /// </summary>
  let create (file_path: string) =
    let document = 
      SpreadsheetDocument.Create(file_path, SpreadsheetDocumentType.Workbook)
      |> Excel
    document
    |> (add_sheet "" >> save)

  /// <summary>
  /// Excelファイルを閉じる
  /// </summary>
  let close (Excel document) =
    document.Save()
    document.Close()
