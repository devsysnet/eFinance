<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rrekapdetailbiaya.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rrekapdetailbiaya" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="TabName" runat="server" />
    <style>
        #BodyContent_grdSiswaCopyFreeze {
            height: auto !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            SetTabs();
        });
        Sys.Application.add_init(appl_init);
        function appl_init() {
            var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
            pgRegMgr.add_beginRequest(SetTabs);
            pgRegMgr.add_endRequest(SetTabs);
        }
        function SetTabs() {
            var tabName = $("#<%=TabName.ClientID%>").val();
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("#<%=TabName.ClientID%>").val($(this).attr("href").replace("#", ""));
            });

        };
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="col-sm-1 control-label">
                                        <span>Filter Cari</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Width="100"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                         <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="kdrek" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdSiswa_PageIndexChanging"
                                        OnSelectedIndexChanged="grdSiswa_SelectedIndexChanged">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:BoundField DataField="kdrek" HeaderText="COA" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />--%>
                                            <asp:BoundField DataField="ket" HeaderText="Keterangan" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="Jan" HeaderText="Jan" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="feb" HeaderText="Feb" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="mar" HeaderText="Mar" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="apr" HeaderText="Apr" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Mei" HeaderText="Mei" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Jun" HeaderText="Jun" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Jul" HeaderText="Jul" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Ags" HeaderText="Ags" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Sept" HeaderText="Sept" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Okt" HeaderText="Okt" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Nov" HeaderText="Nov" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="des" HeaderText="Des" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="" HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right" />                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
     </div>
 </div>
 
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $('#BodyContent_grdSiswa').gridviewScroll({
                width: '99%',
                height: 450,
                freezesize: 1, // Freeze Number of Columns. 
            });
        });
    </script>
    
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>

 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
