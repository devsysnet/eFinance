<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mBarang.aspx.cs" Inherits="eFinance.Pages.Master.Input.mBarang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Barang <span class="mandatory">*</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" Cssclass="form-control " ID="namaBarang" type="text" placeholder="Nama Barang"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Barang <span class="mandatory">*</span> </label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cbojnsBarang" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged">
                                        <asp:ListItem Value="0">---Pilih Jenis Barang---</asp:ListItem>
                                        <asp:ListItem Value="1">Asset Tidak Bergerak</asp:ListItem>
                                        <asp:ListItem Value="2">Asset Bergerak</asp:ListItem>
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
                                     <asp:DropDownList ID="cboKategori" runat="server" CssClass="form-control">
                                      </asp:DropDownList>
                                </div>
                            </div>
                            
                            
                            <div class="form-group" id="showhideharga" runat="server" >
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Harga Jual</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control money" value="0.00" ID="hargajual" type="text" placeholder="Harga Jual" Width="100"></asp:TextBox>
                                 </div>
                             </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CSSclass="form-control " ID="keterangan" TextMode="MultiLine" type="text" placeholder="Uraian"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Cabang :</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true"></asp:DropDownList>
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
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
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
