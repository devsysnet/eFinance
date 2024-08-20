<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mBarang.aspx.cs" Inherits="eFinance.Pages.Master.Update.mBarang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Cari :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdBarang" DataKeyNames="noBarang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnRowCommand="grdBarang_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kodeBarang" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namaBarang" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="jnsBrg" HeaderText="Jenis" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Ubah" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Barang </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" Enabled="false" Cssclass="form-control " ID="txtKode"  placeholder="Kode Barang"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Barang <span class="mandatory">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" Cssclass="form-control " ID="txtnamaBarang"  placeholder="Nama Barang"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Barang <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cbojnsBarang" Cssclass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbojnsBarang_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">---Pilih Jenis Barang---</asp:ListItem>
                                                    <asp:ListItem Value="1">Asset</asp:ListItem>
                                                    <asp:ListItem Value="2">Non Asset</asp:ListItem>
                                                    <asp:ListItem Value="3">Jasa</asp:ListItem>
                                                    <asp:ListItem Value="4">Inventaris</asp:ListItem>
                                                    <asp:ListItem Value="5">Sales</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                             <div class="col-sm-2" id="showhidekode" runat="server">
                                                <asp:TextBox runat="server" Cssclass="form-control " ID="txtKodeAsset" type="text" placeholder="Kode Asset"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori Barang <span class="mandatory">*</span></label>
                                            <div class="col-sm-4">
                                                 <asp:DropDownList ID="cboKategori" Cssclass="form-control" runat="server">
                                                  </asp:DropDownList>
                                            </div>
                                        </div>
                                     
                                        <div class="form-group" id="showhideharga" runat="server">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Harga Jual</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control money" value="0.00" ID="harga" type="text" placeholder="Harga Jual" Width="100"></asp:TextBox>
                                             </div>
                                         </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" Cssclass="form-control " ID="keterangan" TextMode="MultiLine"  placeholder="Uraian"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="row form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Dari Satuan Terkecil</label>
                    <div class="col-sm-5 " >
                        <table class="table"  style="width:100px">  
                            <thead>
                                 <tr >  
                                 <th style="text-align:center;width:100px;">  
                                     Satuan  
                                 </th>  
                                 <th style="text-align:center;width:100px;">  
                                     Konfersi 
                                 </th>  
                             </tr>  
                            </thead>
                             <tbody>
                                 <tr>  
                                 <td style="width:100px;padding:0px"> 
                                     <asp:TextBox runat="server" CSSclass="form-control " ID="satuan1" type="text" placeholder="Satuan Terkecil" Width="100"></asp:TextBox>   
                                 </td>  
                                 <td style="width:100px">  
                                     <asp:TextBox runat="server" CSSclass="form-control money" value="0.00" ID="konfersi1" type="text" placeholder="konfersi Terkecil" Width="100"></asp:TextBox>   
                                 
                                 </td>  
                                 
                             </tr>
                                  <tr>  
                                 <td style="width:100px;padding:0px"> 
                                     <asp:TextBox runat="server" CSSclass="form-control " ID="satuan2" type="text" placeholder="Satuan" Width="100"></asp:TextBox>   
                                 </td>  
                                 <td style="width:100px">  
                                     <asp:TextBox runat="server" CSSclass="form-control money" value="0.00" ID="konfersi2" type="text" placeholder="konfersi" Width="100"></asp:TextBox>   
                                 
                                 </td>  
                                 
                             </tr>
                                  <tr>  
                                 <td style="width:100px;padding:0px"> 
                                     <asp:TextBox runat="server" CSSclass="form-control " ID="satuan3" type="text" placeholder="Satuan" Width="100"></asp:TextBox>   
                                 </td>  
                                 <td style="width:100px">  
                                     <asp:TextBox runat="server" CSSclass="form-control money" value="0.00" ID="konfersi3" type="text" placeholder="konfersi" Width="100"></asp:TextBox>   
                                 
                                 </td>  
                                 
                             </tr>
                                 <tr>  
                                 <td style="width:100px;padding:0px"> 
                                     <asp:TextBox runat="server" CSSclass="form-control " ID="satuan4" type="text" placeholder="Satuan" Width="100"></asp:TextBox>   
                                 </td>  
                                 <td style="width:100px">  
                                     <asp:TextBox runat="server" CSSclass="form-control money" value="0.00" ID="konfersi4" type="text" placeholder="konfersi" Width="100"></asp:TextBox>   
                                 
                                 </td>  
                                 
                             </tr>
                                 <tr>  
                                 <td style="width:100px;padding:0px"> 
                                     <asp:TextBox runat="server" CSSclass="form-control " ID="satuan5" type="text" placeholder="Satuan" Width="100"></asp:TextBox>   
                                 </td>  
                                 <td style="width:100px">  
                                     <asp:TextBox runat="server" CSSclass="form-control money" value="0.00" ID="konfersi5" type="text" placeholder="konfersi" Width="100"></asp:TextBox>   
                                 
                                 </td>  
                                 
                             </tr>
                             </tbody>  
                         </table> 
                    </div>
                </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Batal" OnClick="btnCancel_Click"></asp:Button>
                                                </div>
                                            </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
