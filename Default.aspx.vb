Imports System.IO
Imports OfficeOpenXml
Imports System.Web.Script.Serialization
Partial Class LGDefault
  Inherits System.Web.UI.Page

  Private Sub Chart10_PreRender(sender As Object, e As EventArgs) Handles Me.Load

    If HttpContext.Current.User.Identity.IsAuthenticated Then

      Dim a As SIS.HOLDDetails = SIS.HOLDDetails.GetHCTChart()

      HOLDDASHBOARD.Text = a.UserD & " - DOCUMENTS HOLD STATUS"

      abc.Visible = True
      C10.Visible = True
      Dim Dt As SIS.HOLDDetails = SIS.HOLDDetails.GetHCTChart()

      Chart10 = SIS.HOLDDetails.RenderChart10(Chart10, Dt)


      Try
        'OverallDataTable.InnerHtml = Dt.GetDataTable
      Catch ex As Exception
      End Try
    End If

  End Sub


  Private Sub HOLDCHART_Load(sender As Object, e As EventArgs) Handles HOLDCHART.Load

    HOLDCHART.HRef = "~/DMISG_Main/App_Forms/GF_HOLDDBDetails.aspx"

  End Sub
End Class
