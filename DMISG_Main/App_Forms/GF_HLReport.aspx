<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GF_HLReport.aspx.vb" Inherits="GF_HLReport" title="HOLD LIST Report" %>
<asp:Content ID="CPHlgDMisg" ContentPlaceHolderID="cph1" Runat="Server">
  <div id="div1" class="page">
<asp:UpdatePanel ID="UPNLlgDMisg" runat="server">
  <ContentTemplate>
   <hr/>
    <h6> <asp:Label ID="LabellgDMisg" runat="server" Text="&nbsp;HOLD LIST Report"   Font-Underline="true" Font-Bold="true" Width="100%" CssClass="sis_formheading"></asp:Label>
  </h6>
    <asp:UpdateProgress ID="UPGSlgDMisg" runat="server" AssociatedUpdatePanelID="UPNLlgDMisg" DisplayAfter="100">
      <ProgressTemplate>
        <span style="color: #ff0033">Loading...</span>
      </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
      var pcnt = 0;
      function print_report(o) {
        pcnt = pcnt + 1;
        var nam = 'wTask' + pcnt;
        var url = self.location.href.replace('App_Forms/GF_','App_Print/RP_');
        url = url + '?pk=' + o.alt;
        window.open(url, nam, 'left=20,top=20,width=1000,height=600,toolbar=1,resizable=1,scrollbars=1');
        return false;
       }
       function script_download(typ) {
       	pcnt = pcnt + 1;
       	var nam = 'wdwd' + pcnt;
       	var url = self.location.href.replace('App_Forms/GF_HLReport.aspx', 'App_Downloads/HLReport.aspx');
         typ.toUpperCase=true
       	url = url + '?typ=' + $get(typ).value;
       	window.open(url, nam, 'left=20,top=20,width=100,height=100,toolbar=1,resizable=1,scrollbars=1');
       	return false;
       }
    </script>
     <br/>
     <div class="row mt-3">
      
       <div class="col-3">

          <div class="form-group">

              <div class="input-group">
       
      <asp:Label ID="lbl_Project" CssClass="btn btn-outline-dark" runat="server" Text="Project:" Font-Bold="True"></asp:Label>
     
       
     <asp:TextBox ID="Txt_ProjectID" type="text" class="form-control btn-light ml-1" runat="server" BackColor="#99CCFF" Width="77px"></asp:TextBox>
   
        </div>
            </div>
         </div>
         <hr/>
       </div>
    <div class="row">

      </div>
  
     
					<input type="button"  class="btn btn-dark" onclick="return script_download('<%= Txt_ProjectID.ClientID %>');" value=" Download " />
				
		


 <hr/>
  </ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphMain">
  <style type="text/css">
  .auto-style1 {
    margin-left: 40px;
  }
</style>
</asp:Content>

