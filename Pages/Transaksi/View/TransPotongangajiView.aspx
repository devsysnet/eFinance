<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPotongangajiView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransPotongangajiView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdKelasSiswa.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
         function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete all selected data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
    </script>
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
        <asp:Button ID="cmdMode" runat="server" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                                <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged" Width="200"></asp:DropDownList>
                                                <label>Jenis :</label>
                                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"  Width="170" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                         
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Tgl :</label>
                                             <asp:DropDownList ID="dtPO" runat="server" Width="120">
                        <asp:ListItem Value="1">Januari</asp:ListItem>
                        <asp:ListItem Value="2">Februari</asp:ListItem>
                        <asp:ListItem Value="3">Maret</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">Mei</asp:ListItem>
                        <asp:ListItem Value="6">Juni</asp:ListItem>
                        <asp:ListItem Value="7">Juli</asp:ListItem>
                        <asp:ListItem Value="8">Agustus</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">Oktober</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">Desember</asp:ListItem>
                        </asp:DropDownList>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClick="cmdMode_Click" Text="Delete all selected" />
                            </div>
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdKelasSiswa" DataKeyNames="nogajibln" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdKelasSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnnokaryawan" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                                        <asp:HiddenField ID="hdnnogajibln" runat="server" Value='<%# Bind("nogajibln") %>' />
                                                        <asp:HiddenField ID="hdnnocabang" runat="server" Value='<%# Bind("nocabang") %>' />
                                         
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px"   onclick="return CheckAllData(this);"/>
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck"   />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="namacabang" HeaderText="Nama Cabang" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="bln" HeaderText="Bulan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="komponengaji" HeaderText="Jenis" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right" />

                                             <asp:TemplateField Visible="false" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Nilai">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="nilai" runat="server" CssClass="form-control money" Text='<%# Bind("nilai","{0:n2}") %>' Width="200" ></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="button" runat="server">
                            <div class="col-sm-12 ">
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Ubah" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

