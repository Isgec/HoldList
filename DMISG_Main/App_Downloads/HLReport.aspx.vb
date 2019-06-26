Imports System.Data
Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports System.Drawing

Partial Class HLReport
  Inherits System.Web.UI.Page
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim mLastScriptTimeout As Integer = HttpContext.Current.Server.ScriptTimeout
    HttpContext.Current.Server.ScriptTimeout = 1200
    'Dim FromDate As String = ""
    ' Dim ToDate As String = ""
    Dim Project As String = ""

    Try
      ' FromDate = Request.QueryString("fd")
      '  ToDate = Request.QueryString("td")
      Project = Request.QueryString("typ")
    Catch ex As Exception
      ' FromDate = ""
      '  ToDate = ""
      Project = ""
    End Try
    ' If FromDate = String.Empty Then Return
    Dim DWFile As String = "HOLD List Report"
    Dim FilePath As String = CreateFile(Project)
    HttpContext.Current.Server.ScriptTimeout = mLastScriptTimeout
    Response.ClearContent()
    Response.AppendHeader("content-disposition", "attachment; filename=" & DWFile & ".xlsx")
    Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(IO.Path.GetFileName(FilePath))
    Response.WriteFile(FilePath)
    Response.End()
  End Sub
  Private Function CreateFile(ByVal project As String) As String
    Dim FileName As String = Server.MapPath("~/..") & "App_Temp\" & Guid.NewGuid().ToString()
    IO.File.Copy(Server.MapPath("~/App_Templates") & "\HLTemplate.xlsx", FileName)
    Dim FileInfo As IO.FileInfo = New IO.FileInfo(FileName)
    Dim xlPk As ExcelPackage = New ExcelPackage(FileInfo)

    Dim xlWS As ExcelWorksheet = xlPk.Workbook.Worksheets("HOLD_LIST_DATA")
    Dim oDocs As List(Of HLReportClass) = HLReportClass.GetData(project)
    Dim r As Integer = 5
    Dim c As Integer = 2
    Dim s As Integer = 1
    Dim identifier As String = ""
    'xlWS.Cells(2, 2).Value = Now
    xlWS.Cells(3, 2).Value = "HOLD LIST Report for Project ID-" & project & " - Generated at - " & Now
    With xlWS
      For Each doc As HLReportClass In oDocs
        If r > 5 Then
          xlWS.InsertRow(r, 1, r + 1)
        End If
        ' c = 2

        'If identifier <> doc.Projectno Then
        .Cells(r, 2).Value = s

        'xlWS.Cells(r, 2).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
        'xlWS.Cells(r, 2).Style.Fill.BackgroundColor.SetColor(Color.Orange)
        s = s + 1
        'c = c + 1
        '  .Cells(r, 3).Value = doc.Description
        'xlWS.Cells(r, 3).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
        'xlWS.Cells(r, 3).Style.Fill.BackgroundColor.SetColor(Color.Orange)

        .Cells(r, 3).Value = doc.DocumentID

        .Cells(r, 4).Value = doc.SerialNo

        .Cells(r, 5).Value = doc.Description



        .Cells(r, 6).Value = doc.ProjectID

        .Cells(r, 7).Value = doc.Element

        .Cells(r, 8).Value = doc.RevisionAtHOLD



        .Cells(r, 9).Value = doc.DateOfIssue

        .Cells(r, 10).Value = doc.ResponsibleDept

        .Cells(r, 11).Value = doc.PartUnderHOLD

        .Cells(r, 12).Value = doc.ReasonForHOLD

        '.Cells(r, 14).Value = doc.MaterialStateID
        .Cells(r, 13).Value = doc.RevisionAtUnhold

        .Cells(r, 14).Value = doc.InputReceivedOn

        .Cells(r, 15).Value = doc.HoldRemovedIssued

        .Cells(r, 16).Value = doc.HoldStatus

        .Cells(r, 17).Value = doc.CreatedBy

        .Cells(r, 18).Value = doc.CreatedON



        ''.Cells(r, 21).Calculate = "=VLOOKUP(G5;ERPLN_GR_IR_DATA!B$2:G$9999;5;FALSE)"

        '.SelectedRange(r, 21).Value = "=VLOOKUP(G5;ERPLN_GR_IR_DATA!B$2:G$9999;5;FALSE)"
        identifier = doc.DocumentID
        r += 1
        xlPk.Workbook.Calculate
      Next
    End With




    'Dim xlWSB As ExcelWorksheet = xlPk.Workbook.Worksheets("ERPLN_GR_IR_DATA")
    'Dim oDocsb As List(Of MRNBReportClass) = MRNBReportClass.GetBaanData(FromDate, ToDate, project)
    'Dim rb As Integer = 2
    '' Dim cb As Integer = 2
    'Dim sb As Integer = 1
    'Dim identifierb As String = ""
    ''xlWS.Cells(2, 2).Value = Now
    'xlWSB.Cells(3, 2).Value = "MRN Report From " & FromDate & " TO " & ToDate
    'With xlWSB
    '  For Each docb As MRNBReportClass In oDocsb
    '    If rb > 2 Then
    '      xlWSB.InsertRow(rb, 1, rb + 1)
    '    End If
    '    'cb = 2

    '    'If identifier <> doc.Projectno Then
    '    .Cells(rb, 1).Value = sb

    '    'xlWS.Cells(r, 2).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
    '    'xlWS.Cells(r, 2).Style.Fill.BackgroundColor.SetColor(Color.Orange)
    '    sb = sb + 1
    '    'c = c + 1
    '    '.Cells(rb, 3).Value = docb.ProjectID
    '    'xlWS.Cells(r, 3).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
    '    'xlWS.Cells(r, 3).Style.Fill.BackgroundColor.SetColor(Color.Orange)

    '    .Cells(rb, 2).Value = docb.GRNO

    '    .Cells(rb, 3).Value = docb.GRDate

    '    .Cells(rb, 4).Value = docb.BPID

    '    .Cells(rb, 5).Value = docb.BPName

    '    .Cells(rb, 6).Value = docb.IRNO

    '    .Cells(rb, 7).Value = docb.IRDate

    '    identifierb = docb.GRNO
    '    rb += 1

    '  Next
    'End With

    xlPk.Save()
    xlPk.Dispose()
    Return FileName
  End Function
  Private Function RemoveChars(ByVal mstr As String) As String
    'Dim tstr As String = ""
    'For i As Integer = 0 To mstr.Length - 1
    '	If Asc(mstr.Chars(i)) Then

    '	End If
    'Next
    Return mstr.Replace(vbCr, "").Replace(vbLf, "").Replace(vbCrLf, "").Replace(vbNewLine, "")
  End Function
End Class
Public Class HLReportClass



  Private _ProjectID As String = ""
  Public Property ProjectID() As String
    Get
      Return _ProjectID
    End Get
    Set(ByVal value As String)
      _ProjectID = value
    End Set
  End Property
  Private _Description As String = ""
  Public Property Description() As String
    Get
      Return _Description
    End Get
    Set(ByVal value As String)
      _Description = value
    End Set
  End Property


  Private _DocumentID As String = ""
  Public Property DocumentID() As String
    Get
      Return _DocumentID
    End Get
    Set(ByVal value As String)
      _DocumentID = value
    End Set
  End Property


  Private _SerialNo As String = ""
  Public Property SerialNo() As String
    Get
      Return _SerialNo
    End Get
    Set(ByVal value As String)
      _SerialNo = value
    End Set
  End Property


  Private _Element As String = ""
  Public Property Element() As String
    Get
      Return _Element
    End Get
    Set(ByVal value As String)
      _Element = value
    End Set
  End Property

  Private _RevisionAtHOLD As String = ""
  Public Property RevisionAtHOLD() As String
    Get
      Return _RevisionAtHOLD
    End Get
    Set(ByVal value As String)
      _RevisionAtHOLD = value
    End Set
  End Property


  Private _RevisionAtUnhold As String = ""
  Public Property RevisionAtUnhold() As String
    Get
      Return _RevisionAtUnhold
    End Get
    Set(ByVal value As String)
      _RevisionAtUnhold = value
    End Set
  End Property


  Private _DateOfIssue As String = ""
  Public Property DateOfIssue() As String
    Get
      Return _DateOfIssue
    End Get
    Set(ByVal value As String)
      _DateOfIssue = value
    End Set
  End Property

  Private _ResponsibleDept As String = ""
  Public Property ResponsibleDept() As String
    Get
      Return _ResponsibleDept
    End Get
    Set(ByVal value As String)
      _ResponsibleDept = value
    End Set
  End Property


  Private _PartUnderHOLD As String = ""
  Public Property PartUnderHOLD() As String
    Get
      Return _PartUnderHOLD
    End Get
    Set(ByVal value As String)
      _PartUnderHOLD = value
    End Set
  End Property

  Private _ReasonForHOLD As String = ""
  Public Property ReasonForHOLD() As String
    Get
      Return _ReasonForHOLD
    End Get
    Set(ByVal value As String)
      _ReasonForHOLD = value
    End Set
  End Property


  Private _InputReceivedOn As String = ""
  Public Property InputReceivedOn() As String
    Get
      Return _InputReceivedOn
    End Get
    Set(ByVal value As String)
      _InputReceivedOn = value
    End Set
  End Property

  Private _HoldRemovedIssued As String = ""
  Public Property HoldRemovedIssued() As String
    Get
      Return _HoldRemovedIssued
    End Get
    Set(ByVal value As String)
      _HoldRemovedIssued = value
    End Set
  End Property


  Private _HoldStatus As String = ""
  Public Property HoldStatus() As String
    Get
      Return _HoldStatus
    End Get
    Set(ByVal value As String)
      _HoldStatus = value
    End Set
  End Property


  Private _CreatedON As String = ""
  Public Property CreatedON() As String
    Get
      Return _CreatedON
    End Get
    Set(ByVal value As String)
      _CreatedON = value
    End Set
  End Property

  Private _CreatedBy As String = ""
  Public Property CreatedBy() As String
    Get
      Return _CreatedBy
    End Get
    Set(ByVal value As String)
      _CreatedBy = value
    End Set
  End Property





  Public Sub New(ByVal Reader As SqlDataReader)
    Try
      For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
        If pi.MemberType = Reflection.MemberTypes.Property Then
          Try
            Dim Found As Boolean = False
            For I As Integer = 0 To Reader.FieldCount - 1
              If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                Found = True
                Exit For
              End If
            Next
            If Found Then
              If Convert.IsDBNull(Reader(pi.Name)) Then
                Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                  Case "decimal"
                    CallByName(Me, pi.Name, CallType.Let, "0.00")
                  Case "bit"
                    CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                  Case Else
                    CallByName(Me, pi.Name, CallType.Let, String.Empty)
                End Select
              Else
                CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
              End If
            End If
          Catch ex As Exception
          End Try
        End If
      Next
    Catch ex As Exception
    End Try
  End Sub
  Public Sub New()
  End Sub
  Public Shared Function GetData(ByVal project As String) As List(Of HLReportClass)

    Dim Sql As String = ""
    Sql = " SELECT [DocumentID],[SerialNo],[Description],[ProjectID],[Element],[RevisionAtHold],[RevisionAtUnhold],[DateOfIssue],[ResponsibleDept]  "
    Sql &= " ,[PartUnderHold],[ReasonForHold],[InputReceivedOn],[HoldRemovedIssued],(case HoldStatus when 0 then 'UNHOLD' "
    Sql &= " when 1 then 'HOLD'  end) as HoldStatus,[CreatedBy],[CreatedOn] "
    Sql &= "   FROM DWG_HoldList"
    Sql &= " where [ProjectID] = '" & project & "'"

    'And SerialNo = (select max(serialno) from DWG_HoldList as bb"
    'Sql &= " where(bb.DocumentID = aa.DocumentID))"


    Sql &= "  ORDER BY [SerialNo] Desc"

    Return GetHLReportClass(Sql)
  End Function

  Private Shared Function GetHLReportClass(ByVal Sql As String) As List(Of HLReportClass)
    Dim Results As List(Of HLReportClass) = Nothing
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Cmd.CommandTimeout = 1200
        Results = New List(Of HLReportClass)
        Con.Open()
        Dim Reader As SqlDataReader = Cmd.ExecuteReader()
        While (Reader.Read())
          Results.Add(New HLReportClass(Reader))
        End While
        Reader.Close()
      End Using
    End Using
    Return Results

  End Function



End Class



