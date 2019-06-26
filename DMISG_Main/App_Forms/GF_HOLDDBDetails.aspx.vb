Partial Class GF_HOLDDBDetails
  Inherits System.Web.UI.Page



  Private Sub ShowHoldData()
    Dim Data As List(Of SIS.HOLDDetails) = SIS.HOLDDetails.GetHOLDData()
    Dim tbl As New Table

    With tbl

      .GridLines = GridLines.Both
      .BorderWidth = 2
      .CellSpacing = 2
      .Width = Unit.Percentage(100)

    End With

    Dim tr As TableRow = Nothing
    Dim td As TableCell = Nothing

    'Header
    tr = New TableRow

    td = New TableCell
    td.Text = "S.NO."
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "DOCUMENT ID"
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "SERIAL NO"
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "DESCRIPTION"
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "DEPT."
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "HOLD STATUS"
    tr.Cells.Add(td)


    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "PartUnderHold"
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "ReasonforHold"
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "Rev.@Hold"
    tr.Cells.Add(td)

    'td = New TableCell
    'With td
    '  .Font.Bold = True
    '  .Font.Size = FontUnit.Point(8)
    'End With
    'td.Text = "Rev.@Unhold"
    'tr.Cells.Add(td)


    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "CreatedBy"
    tr.Cells.Add(td)

    td = New TableCell
    With td
      .Font.Bold = True
      .Font.Size = FontUnit.Point(8)
    End With
    td.Text = "CreatedOn"
    tr.Cells.Add(td)




    tbl.Rows.Add(tr)

    Dim I As Integer = 0
    '================
    For Each tmp As SIS.HOLDDetails In Data
      I += 1
      tr = New TableRow

      td = New TableCell
      td.Text = I
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.DocumentID
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.SerialNo
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.Description
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.ResponsibleDept
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.HoldStatus
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.PartUnderHold
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.reasonforHold
      tr.Cells.Add(td)


      td = New TableCell
      td.Text = tmp.RevisionAtHold
      tr.Cells.Add(td)

      'td = New TableCell
      'td.Text = tmp.RevisionAtUnhold
      'tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.CreatedBy
      tr.Cells.Add(td)

      td = New TableCell
      td.Text = tmp.CreatedOn
      tr.Cells.Add(td)



      tbl.Rows.Add(tr)

    Next
    '================

    pnlDetails.Controls.Add(tbl)

  End Sub


  Private Sub GF_HOLDDBDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
    'Dim Det As String = Request.QueryString("detail")
    'Dim PrjID As String = Request.QueryString("LgnID")


    'If (Det = "HOLD_CHART") Then
    '  ' PPSheading.Text = "Hold Status -Details"
    '  ShowHoldData()
    'End If
    Dim a As SIS.HOLDDetails = SIS.HOLDDetails.GetHCTChart()
    ShowHoldData()

    PPSheading.Text = " HOLD Document List for " & a.UserD & " Division "

  End Sub
End Class
