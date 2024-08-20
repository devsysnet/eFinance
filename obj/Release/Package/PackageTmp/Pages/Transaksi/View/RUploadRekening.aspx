<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RUploadRekening.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RUploadRekening" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdHarianGL.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                             <div class="form-group">
                                <div class="form-inline">
                                        <div class="col-sm-10">Cabang    
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" width="250"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboYear" Enabled="true" width="250">
                                                  <asp:ListItem Value="0">--Pilih Tahun--</asp:ListItem>
                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                                    <asp:ListItem Value="2022">2022</asp:ListItem>
                                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                    <asp:ListItem Value="2025">2025</asp:ListItem>
                              
                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                         <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="notrekening" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="namaCabang" SortExpression="kdrek" HeaderText="Nama Cabang" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                         
                                        <asp:BoundField DataField="tahun" SortExpression="ket" HeaderText="Tahun" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="TGL_TRAN" SortExpression="ket" HeaderText="DATE" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd MMM yyyy}"/>
                                        <asp:BoundField DataField="TRREMK" SortExpression="ket" HeaderText="REMARK" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="MUTASI_DEBET" SortExpression="ket" HeaderText="DEBET" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="MUTASI_KREDIT" SortExpression="ket" HeaderText="KREDIT" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="SALDO_AWAL_MUTASI" SortExpression="ket" HeaderText="LEDGER" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="KODE_TRAN_TELLER" SortExpression="ket" HeaderText="TELLER ID" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
 
                           
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <%--<asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click"></asp:Button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
