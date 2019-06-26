Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Web.UI.DataVisualization.Charting
Namespace SIS
  <DataObject()>
  Public Class HOLDDetails

    Public Property ValX As String = ""
    Public Property ValY As Integer = 0
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

    Public Property CountOfXValuesToBeShown As Integer = 30
    Public Property IntervalX As Integer = 1
    Public Property MinimumX As DateTime = Nothing
    Public Property MaximumX As DateTime = Nothing
    Public Property ProjectID As String = ""

    Public Property ProjectDesc As String = ""
    Public Property ActivityType As String = ""
    Public Property PlannedX As String()
    Public Property PlannedY As Integer()
    Public Property ActualX As DateTime()
    Public Property ActualY As Decimal()
    Public Property OutlookX As DateTime()
    Public Property OutlookY As Decimal()
    Public Property LastUpdatedOn As DateTime = Nothing
    Public Property LastUpdatedIndex As Integer = 0
    Public Property LastProcessed As DateTime
    Public Property UserID As String = ""
    Public Property UserD As String = ""

    Public Property DocumentID As String = ""
    Public Property SerialNo As String = ""
    Public Property Description As String = ""
    Public Property ResponsibleDept As String = ""
    Public Property PartUnderHold As String = ""
    Public Property RevisionAtHold As String = ""
    Public Property RevisionAtUnhold As String = ""
    Public Property reasonforHold As String = ""
    Public Property CreatedBy As String = ""
    Public Property CreatedOn As String = ""
    Public Property HoldStatus As String = ""



    Public Shared Function RenderChart10(ByVal Chart10 As Chart, ByVal dt As HOLDDetails, Optional ByVal IntervalY As Integer = 10, Optional ByVal Border As Integer = 3) As Chart
      Dim ca As ChartArea = Chart10.ChartAreas(0)
      With ca
        With .AxisX
          '.Interval = dt.IntervalX
          '.Minimum = dt.MinimumX.ToOADate
          '.Maximum = dt.MaximumX.ToOADate
          '.LabelStyle.Format = "dd-MMM"
          .MajorGrid.LineColor = Drawing.Color.LightGoldenrodYellow
          .MajorGrid.LineWidth = 1
          .LabelStyle.Font = New Font("Comic Sans MS", 15)
        End With
        With .AxisY

          .MajorGrid.LineColor = Drawing.Color.LightBlue
          .MajorGrid.LineWidth = 1
          .LabelStyle.Font = New Font("Comic Sans MS", 15)
        End With

      End With


      'Add Series to the Chart.
      Dim s As New Series("HOLD")
      Chart10.Series.Add(s)
      With s
        .ChartType = SeriesChartType.Column
        .Points.DataBindXY(dt.PlannedX, dt.PlannedY)

        .ChartArea = "ChartArea10"
        .BorderWidth = Border
        .Color = Drawing.Color.Gray

        .ToolTip = "#VALY"


        Chart10.Series(0)("PieLabelStyle") = "inside"
        Chart10.ChartAreas(0).Area3DStyle.Enable3D = False
        ' Chart10.ChartAreas(0).AxisX.Title = "DEPARTMENTS"
        Chart10.ChartAreas(0).AxisY.Title = "NUMBER OF DOCUMENTS"
        'Chart10.ChartAreas(0).AxisX.TitleFont = New Font("Comic Sans MS", 13, FontStyle.Underline)
        Chart10.ChartAreas(0).AxisY.TitleFont = New Font("Comic Sans MS", 15, FontStyle.Underline)
        Chart10.ChartAreas(0).AxisY.TitleForeColor = Drawing.Color.Gray
        Chart10.ChartAreas(0).AxisX.TitleForeColor = Drawing.Color.Gray


        Chart10.Series(0).Font = New Font("Comic Sans MS", 15)

        Chart10.Legends(0).Font = New Font("Comic Sans MS", 10, FontStyle.Bold)
        Chart10.ChartAreas(0).Area3DStyle.Inclination = 70

        Chart10.Series(0).IsValueShownAsLabel = True
        Chart10.Series(0).IsVisibleInLegend = True
        Chart10.Series(0).BorderWidth = 10
        Chart10.Font.Bold = True
      End With
      Return Chart10
    End Function

    Public Shared Function GetHCTChart() As HOLDDetails

      Dim mRet As New HOLDDetails
      Dim UserID As String = HttpContext.Current.Session("LoginID")
      Dim UserIDT As Integer = 0
      Try
        UserIDT = Convert.ToInt32(UserID)
      Catch ex As Exception

      End Try

      Dim Sql As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString)
        Con.Open()
        '2. Get Planned
        Sql = " select distinct (case t_eunt "
        Sql &= " when 'EU200' then 'BOILER' "
        Sql &= " when 'EU210' then 'EPC' "
        Sql &= " when 'EU220' then 'PC' "
        Sql &= " when 'EU230' then 'SMD' "
        Sql &= " when 'EU240' then 'APC' "
        Sql &= " when 'EU250' then 'ISGEC Wet FGD Business' "
        Sql &= " When 'EU290' then 'ISGEC R&D' "
        Sql &= " when 'EU400' then 'ISGEC HO' "
        Sql &= " When 'EU650' then 'Isgec Covema Ltd.' "
        Sql &= " end)  "
        Sql &= " from tdmisg101200 where t_emno in ('" & UserID & "','" & UserIDT & "') "

        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          mRet.UserD = Cmd.ExecuteScalar

        End Using

      End Using


      Dim Divtext As String = ""
      If mRet.UserD = "BOILER" Then Divtext = "'CA', 'IP', 'JA', 'JB','JE', 'JG'"
      If mRet.UserD = "ESP" Then Divtext = "'AG', 'AS'"
      If mRet.UserD = "EPC" Then Divtext = "'EC', 'EE', 'EG', 'EM', 'ES', 'JP'"
      If mRet.UserD = "SMD" Then Divtext = "'JS', 'SE', 'SG', 'SS', 'XP'"
      If mRet.UserD = "PC" Then Divtext = "PS"
      If mRet.UserD = "ISGEC Wet FGD Business" Then Divtext = "'FS', 'FG'"


      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString)
        Con.Open()

        Sql = ""
        Sql &= " SELECT        ResponsibleDept AS ValX, ValY"
        Sql &= " FROM            (SELECT        ResponsibleDept, COUNT(*) AS ValY "
        Sql &= " FROM            [IJTPerks].[dbo].[DWG_HoldList] as aa"
        Sql &= " WHERE        (HoldStatus = 1) AND aa.SerialNo =(SELECT MAX(SerialNo) FROM [IJTPerks].[dbo].[DWG_HoldList] AS bb WHERE (bb.DocumentID = aa.DocumentID) ) and substring(PROJECTID,1,2) in (" & Divtext & ")"
        Sql &= " GROUP BY ResponsibleDept) AS tmp "
        Dim aData As New List(Of HOLDDetails)
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            aData.Add(New HOLDDetails(Reader))
          End While
          Reader.Close()
        End Using
        mRet.PlannedX = aData.Select(Function(x) x.ValX).ToArray
        mRet.PlannedY = aData.Select(Function(x) x.ValY).ToArray
      End Using


      Return mRet
    End Function

    Public Shared Function GetHOLDData() As List(Of HOLDDetails)

      Dim mRet As New HOLDDetails
      Dim mRetd As New List(Of SIS.HOLDDetails)
      Dim UserID As String = HttpContext.Current.Session("LoginID")
      Dim UserIDT As Integer = 0
      Try
        UserIDT = Convert.ToInt32(UserID)
      Catch ex As Exception

      End Try

      Dim Sql As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString)
        Con.Open()
        '2. Get Planned
        Sql = " select distinct (case t_eunt "
        Sql &= " when 'EU200' then 'BOILER' "
        Sql &= " when 'EU210' then 'EPC' "
        Sql &= " when 'EU220' then 'PC' "
        Sql &= " when 'EU230' then 'SMD' "
        Sql &= " when 'EU240' then 'APC' "
        Sql &= " when 'EU250' then 'ISGEC Wet FGD Business' "
        Sql &= " When 'EU290' then 'ISGEC R&D' "
        Sql &= " when 'EU400' then 'ISGEC HO' "
        Sql &= " When 'EU650' then 'Isgec Covema Ltd.' "
        Sql &= " end)  "
        Sql &= " from tdmisg101200 where t_emno in ('" & UserID & "','" & UserIDT & "') "

        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          mRet.UserD = Cmd.ExecuteScalar

        End Using

      End Using


      Dim Divtext As String = ""
      If mRet.UserD = "BOILER" Then Divtext = "'CA', 'IP', 'JA', 'JB','JE', 'JG'"
      If mRet.UserD = "ESP" Then Divtext = "'AG', 'AS'"
      If mRet.UserD = "EPC" Then Divtext = "'EC', 'EE', 'EG', 'EM', 'ES', 'JP'"
      If mRet.UserD = "SMD" Then Divtext = "'JS', 'SE', 'SG', 'SS', 'XP'"
      If mRet.UserD = "PC" Then Divtext = "PS"
      If mRet.UserD = "ISGEC Wet FGD Business" Then Divtext = "'FS', 'FG'"


      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Con.Open()


        Sql = "  "
        Sql &= "    SELECT       DocumentID,SerialNo,Description,ResponsibleDept,(case HoldStatus  when 0 then 'HOLD LIFTED' when 1 then 'HOLD ACTIVE' end) as HoldStatus,PartUnderHold,reasonforHold,RevisionAtHold,RevisionAtUnhold,CreatedBy,CreatedOn "
        Sql &= " FROM            [IJTPerks].[dbo].[DWG_HoldList] as aa "
        Sql &= " WHERE        (HoldStatus = 1) AND aa.SerialNo =(SELECT MAX(SerialNo) FROM [IJTPerks].[dbo].[DWG_HoldList] AS bb WHERE (bb.DocumentID = aa.DocumentID) ) and substring(PROJECTID,1,2) in (" & Divtext & ") "



        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Dim rd As SqlDataReader = Cmd.ExecuteReader
          While (rd.Read)
            mRetd.Add(New HOLDDetails(rd))
          End While
        End Using
      End Using
      Return mRetd
    End Function


  End Class
End Namespace
