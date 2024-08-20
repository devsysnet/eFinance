<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="DaftarGajiNew.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.DaftarGajiNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }


        $(document).ready(function () {

            (function ($) {

                $.fn.tableHeadFixer = function (param) {

                    return this.each(function () {
                        table.call(this);
                    });

                    function table() {
                        /*
                         This function solver z-index problem in corner cell where fix row and column at the same time,
                         set corner cells z-index 1 more then other fixed cells
                         */
                        function setCorner() {
                            var table = $(settings.table);

                            if (settings.head) {
                                if (settings.left > 0) {
                                    var tr = table.find("> thead > tr");

                                    tr.each(function (k, row) {
                                        solverLeftColspan(row, function (cell) {
                                            $(cell).css("z-index", settings['z-index'] + 1);
                                        });
                                    });
                                }

                                if (settings.right > 0) {
                                    var tr = table.find("> thead > tr");

                                    tr.each(function (k, row) {
                                        solveRightColspan(row, function (cell) {
                                            $(cell).css("z-index", settings['z-index'] + 1);
                                        });
                                    });
                                }
                            }

                            if (settings.foot) {
                                if (settings.left > 0) {
                                    var tr = table.find("> tfoot > tr");

                                    tr.each(function (k, row) {
                                        solverLeftColspan(row, function (cell) {
                                            $(cell).css("z-index", settings['z-index']);
                                        });
                                    });
                                }

                                if (settings.right > 0) {
                                    var tr = table.find("> tfoot > tr");

                                    tr.each(function (k, row) {
                                        solveRightColspan(row, function (cell) {
                                            $(cell).css("z-index", settings['z-index']);
                                        });
                                    });
                                }
                            }
                        }

                        // Set style of table parent
                        function setParent() {
                            var parent = $(settings.parent);
                            var table = $(settings.table);

                            parent.append(table);
                            parent
                                .css({
                                    'overflow-x': 'auto',
                                    'overflow-y': 'auto'
                                });

                            parent.scroll(function () {
                                var scrollWidth = parent[0].scrollWidth;
                                var clientWidth = parent[0].clientWidth;
                                var scrollHeight = parent[0].scrollHeight;
                                var clientHeight = parent[0].clientHeight;
                                var top = parent.scrollTop();
                                var left = parent.scrollLeft();

                                if (settings.head)
                                    this.find("> thead > tr > *").css("top", top);

                                if (settings.foot)
                                    this.find("> tfoot > tr > *").css("bottom", scrollHeight - clientHeight - top);

                                if (settings.left > 0)
                                    settings.leftColumns.css("left", left);

                                if (settings.right > 0)
                                    settings.rightColumns.css("right", scrollWidth - clientWidth - left);
                            }.bind(table));
                        }

                        // Set table head fixed
                        function fixHead() {
                            var thead = $(settings.table).find("> thead");
                            var tr = thead.find("> tr");
                            var cells = thead.find("> tr > *");

                            setBackground(cells);
                            cells.css({
                                'position': 'relative'
                            });
                        }

                        // Set table foot fixed
                        function fixFoot() {
                            var tfoot = $(settings.table).find("> tfoot");
                            var tr = tfoot.find("> tr");
                            var cells = tfoot.find("> tr > *");

                            setBackground(cells);
                            cells.css({
                                'position': 'relative'
                            });
                        }

                        // Set table left column fixed
                        function fixLeft() {
                            var table = $(settings.table);

                            // var fixColumn = settings.left;

                            settings.leftColumns = $();

                            var tr = table.find("> thead > tr, > tbody > tr, > tfoot > tr");
                            tr.each(function (k, row) {

                                solverLeftColspan(row, function (cell) {
                                    settings.leftColumns = settings.leftColumns.add(cell);
                                });
                                // var inc = 1;

                                // for(var i = 1; i <= fixColumn; i = i + inc) {
                                // 	var nth = inc > 1 ? i - 1 : i;

                                // 	var cell = $(row).find("*:nth-child(" + nth + ")");
                                // 	var colspan = cell.prop("colspan");

                                // 	settings.leftColumns = settings.leftColumns.add(cell);

                                // 	inc = colspan;
                                // }
                            });

                            var column = settings.leftColumns;

                            column.each(function (k, cell) {
                                var cell = $(cell);

                                setBackground(cell);
                                cell.css({
                                    'position': 'relative'
                                });
                            });
                        }

                        // Set table right column fixed
                        function fixRight() {
                            var table = $(settings.table);

                            var fixColumn = settings.right;

                            settings.rightColumns = $();

                            var tr_head = table.find('> thead').find("> tr");
                            var tr_body = table.find('> tbody').find("> tr");
                            var fcell = null;
                            tr_head.each(function (k, row) {
                                solveRightColspanHead(row, function (cell) {
                                    if (k === 0) {
                                        fcell = cell;
                                    }
                                    settings.rightColumns = settings.rightColumns.add(fcell);
                                });
                            });

                            tr_body.each(function (k, row) {
                                solveRightColspanBody(row, function (cell) {
                                    settings.rightColumns = settings.rightColumns.add(cell);
                                });
                            });

                            var column = settings.rightColumns;

                            column.each(function (k, cell) {
                                var cell = $(cell);

                                setBackground(cell);
                                cell.css({
                                    'position': 'relative',
                                    'z-index': '9999'
                                });
                            });

                        }

                        // Set fixed cells backgrounds
                        function setBackground(elements) {
                            elements.each(function (k, element) {
                                var element = $(element);
                                var parent = $(element).parent();

                                var elementBackground = element.css("background-color");
                                elementBackground = (elementBackground == "transparent" || elementBackground == "rgba(0, 0, 0, 0)") ? null : elementBackground;

                                var parentBackground = parent.css("background-color");
                                parentBackground = (parentBackground == "transparent" || parentBackground == "rgba(0, 0, 0, 0)") ? null : parentBackground;

                                var background = parentBackground ? parentBackground : "white";
                                background = elementBackground ? elementBackground : background;

                                element.css("background-color", background);
                            });
                        }

                        function solverLeftColspan(row, action) {
                            var fixColumn = settings.left;
                            var inc = 1;

                            for (var i = 1; i <= fixColumn; i = i + inc) {
                                var nth = inc > 1 ? i - 1 : i;

                                var cell = $(row).find("> *:nth-child(" + nth + ")");
                                var colspan = cell.prop("colspan");

                                if (typeof cell.cellPos() != 'undefined' && cell.cellPos().left < fixColumn) {
                                    action(cell);
                                }

                                inc = colspan;
                            }
                        }

                        function solveRightColspanHead(row, action) {
                            var fixColumn = settings.right;
                            var inc = 1;
                            for (var i = 1; i <= fixColumn; i = i + inc) {
                                var nth = inc > 1 ? i - 1 : i;

                                var cell = $(row).find("> *:nth-last-child(" + nth + ")");
                                var colspan = cell.prop("colspan");

                                action(cell);

                                inc = colspan;
                            }
                        }

                        function solveRightColspanBody(row, action) {
                            var fixColumn = settings.right;
                            var inc = 1;
                            for (var i = 1; i <= fixColumn; i = i + inc) {
                                var nth = inc > 1 ? i - 1 : i;

                                var cell = $(row).find("> *:nth-last-child(" + nth + ")");
                                var colspan = cell.prop("colspan");
                                action(cell);
                                inc = colspan;
                            }
                        }

                        var defaults = {
                            head: true,
                            foot: false,
                            left: 0,
                            right: 0,
                            'z-index': 0
                        };

                        var settings = $.extend({}, defaults, param);

                        settings.table = this;
                        settings.parent = $(settings.table).parent();
                        setParent();

                        if (settings.head == true) {
                            fixHead();
                        }

                        if (settings.foot == true) {
                            fixFoot();
                        }

                        if (settings.left > 0) {
                            fixLeft();
                        }

                        if (settings.right > 0) {
                            fixRight();
                        }

                        setCorner();

                        $(settings.parent).trigger("scroll");

                        $(window).resize(function () {
                            $(settings.parent).trigger("scroll");
                        });
                    }
                };

            })(jQuery);

            /*  cellPos jQuery plugin
             ---------------------
             Get visual position of cell in HTML table (or its block like thead).
             Return value is object with "top" and "left" properties set to row and column index of top-left cell corner.
             Example of use:
             $("#myTable tbody td").each(function(){
             $(this).text( $(this).cellPos().top +", "+ $(this).cellPos().left );
             });
             */
            (function ($) {
                /* scan individual table and set "cellPos" data in the form { left: x-coord, top: y-coord } */
                function scanTable($table) {
                    var m = [];
                    $table.children("tr").each(function (y, row) {
                        $(row).children("td, th").each(function (x, cell) {
                            var $cell = $(cell),
                                cspan = $cell.attr("colspan") | 0,
                                rspan = $cell.attr("rowspan") | 0,
                                tx, ty;
                            cspan = cspan ? cspan : 1;
                            rspan = rspan ? rspan : 1;
                            for (; m[y] && m[y][x]; ++x);  //skip already occupied cells in current row
                            for (tx = x; tx < x + cspan; ++tx) {  //mark matrix elements occupied by current cell with true
                                for (ty = y; ty < y + rspan; ++ty) {
                                    if (!m[ty]) {  //fill missing rows
                                        m[ty] = [];
                                    }
                                    m[ty][tx] = true;
                                }
                            }
                            var pos = { top: y, left: x };
                            $cell.data("cellPos", pos);
                        });
                    });
                };

                /* plugin */
                $.fn.cellPos = function (rescan) {
                    var $cell = this.first(),
                        pos = $cell.data("cellPos");
                    if (!pos || rescan) {
                        var $table = $cell.closest("table, thead, tbody, tfoot");
                        scanTable($table);
                    }
                    pos = $cell.data("cellPos");
                    return pos;
                }
            })(jQuery);
            $("#<%=grdAccount.ClientID%>").tableHeadFixer({ 'left': 4 });
        });


    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div id="tabGrid" runat="server">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-inline">
                                            <div class="col-sm-10">
                                                <label>Periode :   </label>
                                                <asp:DropDownList ID="cboMonth" runat="server" Width="120">
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
                                                <asp:DropDownList ID="cboYear" runat="server" Width="100">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-7">
                                        <div class="form-inline">
                                            <div class="col-sm-15">
                                                <label>Cabang : </label>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" Width="250"></asp:DropDownList>
                                                <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Cari" OnClick="btnPosting_Click"></asp:Button>
                                               <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdAccount" DataKeyNames="Nama" SkinID="GridView" runat="server" AutoGenerateColumns="false" OnRowCommand="grdAccount_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnNoKaryawan" Value='<%#Bind("Nama") %>' />

                                                            <div class="text-center">
                                                                <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-success" Text="Print Slip Gaji" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                            </div>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama_Karyawan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Nama" runat="server" Text='<%# Eval("Nama").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sipil" SortExpression="ptd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Sipil" runat="server" Text='<%# Eval("Sipil") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kep." SortExpression="Kep." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Kep" runat="server" Text='<%# Eval("Kep") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Golongan" SortExpression="Golongan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Gol" runat="server" Text='<%# Eval("Gol") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gaji_Pokok" SortExpression="Gaji Pokok" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Gaji_Pokok" runat="server" Text='<%# Eval("Gaji_Pokok") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tunjangan_Keluarga" SortExpression="Kel" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Kel" runat="server" Text='<%# Eval("TunjanganKeluarga") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transport" SortExpression="Kehadiran" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="JF" runat="server" Text='<%# Eval("Kedatangan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pangan" SortExpression="Pangan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Pangan" runat="server" Text='<%# Eval("Pangan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subsidi_Yayasan" SortExpression="Subsidi Yayasan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubsidiYayasan" runat="server" Text='<%# Eval("SubsidiYayasan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lembur" SortExpression="Lembur" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lembur" runat="server" Text='<%# Eval("Lembur") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="THR" SortExpression="THR" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="THR" runat="server" Text='<%# Eval("THR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tunjangan_Jabatan" SortExpression="Tunjangan Jabatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TunjanganJabatan" runat="server" Text='<%# Eval("Tunjanganjabatan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tunjangan_Fungsional" SortExpression="Tunjangan Fungsional" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TunjanganFungsional" runat="server" Text='<%# Eval("Tunjanganfungsional") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kelebihan_Mengajar" SortExpression="Kelebihan Mengajar" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="honorlebihjam" runat="server" Text='<%# Eval("honorlebihjam") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inssentif_PPDB" SortExpression="Inssentif PPDB" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="insentifppdb" runat="server" Text='<%# Eval("insentifppdb") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Insentif_MID" SortExpression="Insentif MID" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="insentifmid" runat="server" Text='<%# Eval("insentifmid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Insentif_Ulangan" SortExpression="Insentif Ulangan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="insentifulangan" runat="server" Text='<%# Eval("insentifulangan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Insentif_UN" SortExpression="Insentif UN" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="InsentifUN" runat="server" Text='<%# Eval("insentifUN") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MPP" SortExpression="MPP" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="MPP" runat="server" Text='<%# Eval("MPP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subsidi_Indekos" SortExpression="Subsidi Indekos" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Indekos" runat="server" Text='<%# Eval("Indekos") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Honor" SortExpression="Honor" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Honor" runat="server" Text='<%# Eval("Honor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Honor Wali Kelas" SortExpression="Honor Wali Kelas" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HonorWaliKelas" runat="server" Text='<%# Eval("HonorWaliKelas") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Honor Subsidi" SortExpression="Honor Subsidi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HonorSubsidi" runat="server" Text='<%# Eval("HonorSubsidi") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Honor Exstrakulikuler" SortExpression="Honor Exstrakulikuler" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="KehadirandiluarKBM" runat="server" Text='<%# Eval("HonorEkstrakurikuler") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kehadiran di Luar KBM" SortExpression="Kehadiran di Luar KBM" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="KehadirandiluarKBM1" runat="server" Text='<%# Eval("KehadirandiluarKBM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Konsumsi" SortExpression="Konsumsi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Konsumsi" runat="server" Text='<%# Eval("Konsumsi") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Pokok Honor" SortExpression="Pokok Honor" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PokokHonor" runat="server" Text='<%# Eval("PokokHonor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total_Pendapatan" SortExpression="Total Pendapatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="totpendapatan" runat="server" Text='<%# Eval("totpendapatan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Iuran_Koperasi" SortExpression="Iuran Koperasi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="IuranKoperasi" runat="server" Text='<%# Eval("IuranKoperasi") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dana_Sosial" SortExpression="Dana Sosial" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DanaSosial" runat="server" Text='<%# Eval("DanaSosial") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pinjaman" SortExpression="Pinjaman" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Pinjaman" runat="server" Text='<%# Eval("Pinjaman") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pinjaman_Bulanan" SortExpression="Arisan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Arisan" runat="server" Text='<%# Eval("Arisan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Arisan" SortExpression="Arisan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Pinjamanbln" runat="server" Text='<%# Eval("PinjamanBulanan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Potongan Lain" SortExpression="Potonganlain" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="potonganlain" runat="server" Text='<%# Eval("potonganlain") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KWI" SortExpression="KWI" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="KWI" runat="server" Text='<%# Eval("KWI") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BPJS_Kesehatan" SortExpression="BPJS Kesehatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bpjskesehatan" runat="server" Text='<%# Eval("bpjskesehatan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BPJS_Ketenagakerjaan" SortExpression="BPJS Ketenagakerjaan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bpjsketenakerjaan" runat="server" Text='<%# Eval("bpjsketenakerjaan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PPH21" SortExpression="PPH21" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="pajak" runat="server" Text='<%# Eval("pajak") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total_Potongan" SortExpression="Total Potongan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="totpotongan" runat="server" Text='<%# Eval("totpotongan") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total_Terima" SortExpression="Total Terima" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="totalterima" runat="server" Text='<%# Eval("totalterima") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPosting" />
        </Triggers>
    </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

