<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="LGDefault" title="Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">

                <div id="abc" runat="server" visible="false">
         
              

          
          
  <div class="row" id="C10" runat="server" visible="false">
      <div class="col-sm-12 text-center ">
         <asp:label ID="HOLDDASHBOARD" runat="server" Text="" Font-Bold="true"  Font-Size="Large" Visible="true"></asp:label>
        <a class="chartDiv btn btn-outline-warning" id="HOLDCHART" runat="server">
         
<%--         <asp:Button ID="HOLDDATA" runat="server" CssClass="btn btn-outline-danger btn-sm btn-block font-weight-bold" ToolTip="Sorry !!! No Data in ERPLN" Text="" Font-Bold="true" Visible="true">  </asp:Button>--%>

          <asp:Chart
            ID="Chart10"
            Height="470px"
            Width="1270px"
            ClientIDMode="Predictable"
            runat="server">
            <Legends>
              <asp:Legend Name="Legend10" Docking="Bottom" IsDockedInsideChartArea="true">
                <Position Auto="True" />
              </asp:Legend>
            </Legends>
            <ChartAreas>
              <asp:ChartArea Name="ChartArea10" Area3DStyle-Enable3D="true" BackImageTransparentColor="WhiteSmoke" AlignmentOrientation="All" ShadowOffset="30">
              </asp:ChartArea>
            </ChartAreas>
          </asp:Chart>
          <div id="Div10" runat="server" class="container-fluid text-center"></div>
     
           </a>
      </div>
    </div>

   </div>
</asp:Content>

