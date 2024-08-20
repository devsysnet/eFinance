<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstApprovalInput.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstApprovalInput" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function money(money) {
            var num = money;
            if (num != "") {
                var string = String(num);
                var string = string.replace('.', ' ');

                var array2 = string.toString().split(' ');
                num = parseInt(num).toFixed(0);

                var array = num.toString().split('');

                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }
                if (array2.length == 1) {
                    money = array.join('') + '.00';
                } else {
                    money = array.join('') + '.' + array2[1];
                    if (array2.length == 1) {
                        money = array.join('') + '.00';
                    } else {
                        if (array2[1].length == 1) {
                            money = array.join('') + '.' + array2[1].substring(0, 2) + '0';
                        } else {
                            money = array.join('') + '.' + array2[1].substring(0, 2);
                        }
                    }
                }
            } else {
                money = '0.00';
            }
            return money;
        }
        function removeMoney(money) {
            var minus = money.substring(0, 1);
            if (minus == "-") {
                var number = '-' + Number(money.replace(/[^0-9\.]+/g, ""));
            } else {
                var number = Number(money.replace(/[^0-9\.]+/g, ""));
            }
            return number;
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Approval</label>
                                <div class="controls col-sm-4">
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="">---Pilih Status---</asp:ListItem>
                                        <asp:ListItem Value="perUnit">Per Unit</asp:ListItem>
                                        <asp:ListItem Value="perPerwakilan">Per Perwakilan</asp:ListItem>
                                        <asp:ListItem Value="semuaUnitPerwakilan">Semua Unit dan Perwakilan</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="showhide">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Unit / Perwakilan</label>
                                <div class="controls col-sm-4">
                                    <asp:DropDownList ID="cboUnitperwakilan" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Peruntukan</label>
                                <div class="controls col-sm-4">
                                    <asp:DropDownList ID="cboUntuk" runat="server">
                                        <asp:ListItem Value="-">--Pilih Peruntukan--</asp:ListItem>
                                        <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                        <asp:ListItem Value="Perwakilan">Perwakilan</asp:ListItem>
                                        <asp:ListItem Value="Yayasan">Yayasan</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Kategori</label>
                                <div class="controls col-sm-4">
                                    <asp:DropDownList ID="cboCategory" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="showhideSemua">
                        <div class="text-center">
                            <div class="table-responsive">
                                <asp:GridView ID="grdApproval" SkinID="GridView" runat="server" Width="60%" HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jabatan" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="form-inline">
                                                        <asp:DropDownList ID="cboJabatan" runat="server">
                                                            <asp:ListItem Value="0">--Pilih Jabatan--</asp:ListItem>
                                                            <asp:ListItem Value="Kepala Yayasan">Kepala Yayasan</asp:ListItem>
                                                            <asp:ListItem Value="Kepala Perwakilan">Kepala Perwakilan</asp:ListItem>
                                                            <asp:ListItem Value="Kepala Sekolah">Kepala Sekolah</asp:ListItem>
                                                            <asp:ListItem Value="Bendahara">Bendahara</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approval" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboLevel" runat="server">
                                                        <asp:ListItem Value="1">Approval 1</asp:ListItem>
                                                        <asp:ListItem Value="2">Approval 2</asp:ListItem>
                                                        <asp:ListItem Value="3">Approval 3</asp:ListItem>
                                                        <asp:ListItem Value="4">Approval 4</asp:ListItem>
                                                        <asp:ListItem Value="5">Approval 5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="showhideUnitPerwakilan">
                        <div class="text-center">
                            <div class="table-responsive">
                                <asp:GridView ID="grdApprovalUnit" SkinID="GridView" runat="server" Width="60%" HorizontalAlign="Center" OnSelectedIndexChanged="grdApprovalUnit_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="User ID" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" ID="txtIDUser" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnnoUserD" runat="server" />
                                                            <asp:ImageButton ID="btnImgUser" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="select" CausesValidation="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approval" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboLevel" runat="server">
                                                        <asp:ListItem Value="1">Approval 1</asp:ListItem>
                                                        <asp:ListItem Value="2">Approval 2</asp:ListItem>
                                                        <asp:ListItem Value="3">Approval 3</asp:ListItem>
                                                        <asp:ListItem Value="4">Approval 4</asp:ListItem>
                                                        <asp:ListItem Value="5">Approval 5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="showhidebutton">
                        <div class="col-sm-12">
                            <div class="text-center">
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAddRow_Click"/>
                                <asp:Button ID="btnAddRowDetil" runat="server" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAddRowDetil_Click"/>
                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click"/>
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgUser" runat="server" PopupControlID="panelUser" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelUser" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data User</h4>
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" placeholder="Search..."></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                <asp:HiddenField runat="server" ID="hdnParameterDetil" />
                    <asp:GridView ID="grdUser" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdUser_PageIndexChanging" OnSelectedIndexChanged="grdUser_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="User ID" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblUserID" Text='<%#Eval("userid")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoUser" runat="server" Value='<%#Eval("noUser")%>'/>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Lengkap" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblnamaUser" Text='<%#Eval("namaUser")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Lokasi" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lbllokasi" Text='<%#Eval("namaCabang")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Select" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatal" runat="server" Text="Close" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
