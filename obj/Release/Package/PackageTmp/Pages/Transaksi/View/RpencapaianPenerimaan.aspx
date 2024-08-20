<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RpencapaianPenerimaan.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RpencapaianPenerimaan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <script type="text/javascript">
       
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
            $("#<%=grdAccount.ClientID%>").tableHeadFixer({ 'left': 1 });
        });
  
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

        function getAmount() {

            var julpend = document.querySelectorAll(".julpend");;
            for (let i = 0; i < julpend.length; i++) {

                julpend[i].innerHTML = money(julpend[i].textContent);

            }
            console.log(julpend)
        };
    </script>
    <style>
        td.text-center {
			position: -webkit-sticky;
			position: sticky;
			left: 0;
            margin-top:20px;
			background: #ccc;
            z-index:1;
		}
         .tableFixHead {
			position: relative;
			width:100%;
			z-index: 100;
			margin: auto;
			overflow: scroll;
			height: 80vh;
		}
		.tableFixHead table {
			width: 100%;
			min-width: 100px;
			margin: auto;
			border-collapse: separate;
			border-spacing: 0;
		}
		.table-wrap {
			position: relative;
		}
		.tableFixHead th,
		.tableFixHead td {
			padding: 5px 10px;
			border: 1px solid #000;
			#background: #fff;
			vertical-align: top;
			text-align: center;
		}
		.tableFixHead  th {
			background: #f6bf71;
			position: -webkit-sticky;
			position: sticky;
			top: 0;
            z-index:9;

		}
		td{
			z-index: -4;

		}
       
		

.tableFixHead   tr:nth-child(2) th {
    top: 25px;
}
    </style>
    <asp:HiddenField ID="hdnYayasan" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label text-right">
                                        Filter Cabang
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" ></asp:DropDownList>
                                    </div>
                                     <div class="col-sm-3">
                                            <asp:DropDownList Visible="false" ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                     <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="Click"></asp:Button>
                                      <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>


                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                        
                            <div class="col-sm-12">
                                   <div>

                           </div>
                            <br />

                                <div class="tableFixHead table-responsive overflow-x-table">
                                    <asp:GridView ID="grdAccount" SkinID="GridView" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt" OnDataBound="grdAccount_dataBound" >
                                        <Columns>
                                              <asp:TemplateField HeaderText="Nama Cabang" SortExpression="Ket" ItemStyle-CssClass="text-center" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("namaCabang").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Jul-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Jul-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Jul-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Ags-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Ags-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ags-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Sept-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Sept-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Sept-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            
                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Okt-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Okt-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Okt-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Nov-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Nov-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Nov-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            
                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Des-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Des-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Des-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Jan-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Jan-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Jan-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Feb-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Feb-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Feb-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Mar-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Mar-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Mar-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Apr-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Apr-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Apr-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Mei-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Mei-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Mei-persen").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Rencana Pendapatan" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ket" CssClass="julpend" runat="server" Text='<%# Eval("Jun-Pendapatan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Penerimaan" SortExpression="Ket" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" CssClass="julpend" Text='<%# Eval("Jun-Penerimaan").ToString()  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Persen" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ket" runat="server" Text='<%# Eval("Jun-persen").ToString()  %>'></asp:Label>
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
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
       
    <script type="text/javascript">

        
        var julpend = document.querySelectorAll(".julpend");;
        for (let i = 0; i < julpend.length; i++) {

            julpend[i].innerHTML = money(julpend[i].textContent);

        }
        function getAmount() {

            var julpend = document.querySelectorAll(".julpend");;
            for (let i = 0; i < julpend.length; i++) {

                julpend[i].innerHTML = money(julpend[i].textContent);

            }
            console.log(julpend)
        };
    </script>
        <script type="text/javascript">
       
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>




