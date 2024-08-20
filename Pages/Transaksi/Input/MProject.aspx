<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MProject.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.MProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Project</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNama" type="text" placeholder="Enter Project" Width="300"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Project</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nomor Kontrak</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="Textnokontrak" type="text" placeholder="Nomor Kontrak" Width="200"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai Project</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtValue" Text="0.00" Width="200"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Uraian Project</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" class="form-control " ID="txtUraian" TextMode="MultiLine" type="text" placeholder="Uraian Project"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
