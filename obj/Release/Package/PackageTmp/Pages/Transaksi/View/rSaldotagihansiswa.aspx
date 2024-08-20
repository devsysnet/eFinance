<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="rSaldotagihansiswa.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.rSaldotagihansiswa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdAccount.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
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
            $("#<%=grdAccount.ClientID%>").tableHeadFixer({ 'left': 6 });
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
                            <div class="form-horizontal">
                                <div class="form-group">
                                       <label>  Unit:   </label>
                                        <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control"  Width="250" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="140"></asp:DropDownList>
                                        <%--<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>--%>
                                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <%--<div class="table-responsive overflow-x-table">--%>
                                    <asp:GridView ID="grdAccount" SkinID="GridView" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                           
                                        </Columns>
                                    </asp:GridView>
                                <%--</div>--%>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>




