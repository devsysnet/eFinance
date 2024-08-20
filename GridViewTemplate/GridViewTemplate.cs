using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance
{
    public class GridViewTemplate : ITemplate
    {
        private Systems ObjSys = new Systems();

        string _templateType;
        string _columnName;
        string _objectName;
        string _objectValue;
        public GridViewTemplate(string _type = "", string colname = "", string _object = "", string _value = "")
        {
            _templateType = _type;
            _columnName = colname;
            _objectName = _object;
            _objectValue = _value;
        }
        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (_templateType)
            {
                case "Header":
                    Label lbl = new Label();
                    lbl.Text = _columnName;
                    container.Controls.Add(lbl);
                    break;

                case "ItemTextBox":
                    TextBox tb1 = new TextBox();
                    if (_columnName != "")
                    {
                        tb1.DataBinding += new EventHandler(DataBindingTextBox);
                    }
                    tb1.ID = "TextBox" + _objectName;
                    tb1.Attributes.Add("class", "form-control width-120");
                    tb1.Columns = 4;
                    container.Controls.Add(tb1);
                    break;
                case "ItemTextBoxMoney":
                    TextBox tb2 = new TextBox();
                    if (_columnName != "")
                    {
                        tb2.DataBinding += new EventHandler(DataBindingTextBox);
                    }
                    else
                    {
                        tb2.Text = ObjSys.IsFormatNumber("0");
                    }
                    tb2.ID = "TextBox" + _objectName;
                    tb2.Attributes.Add("class", "form-control width-110 money");
                    tb2.Columns = 4;
                    container.Controls.Add(tb2);
                    break;
                case "ItemTextBoxNumeric":
                    TextBox tbN = new TextBox();
                    if (_columnName != "")
                    {
                        tbN.DataBinding += new EventHandler(DataBindingTextBoxNumeric);
                    }
                    else
                    {
                        tbN.Text = "0";
                    }
                    tbN.ID = "TextBox" + _objectName;
                    tbN.Attributes.Add("class", "form-control width-80 numeric");
                    tbN.Columns = 4;
                    container.Controls.Add(tbN);
                    break;
                case "ItemTextBoxNumericInsurance":
                    TextBox tb9 = new TextBox();
                    if (_columnName != "")
                    {
                        tb9.DataBinding += new EventHandler(DataBindingTextBoxNumeric);
                    }
                    else
                    {
                        tb9.Text = "0";
                    }
                    tb9.ID = "TextBox" + _objectName;
                    tb9.Attributes.Add("class", "form-control width-110 numeric margin-auto");
                    tb9.Columns = 4;
                    container.Controls.Add(tb9);
                    break;
                case "ItemTextHeaderNumeric":
                    Label lblHeader = new Label();
                    lblHeader.Text = _columnName;
                    container.Controls.Add(lblHeader);

                    TextBox tb3 = new TextBox();
                    tb3.ID = "TextBox" + _objectName;
                    tb3.Attributes.Add("class", "form-control width-65 numeric margin-auto");
                    tb3.Attributes.Add("placeholder", "Prosen");
                    tb3.Text = "0";
                    tb3.Columns = 4;
                    container.Controls.Add(tb3);
                    break;
                case "ItemTextHeaderNumeric100":
                    Label lblHeader100 = new Label();
                    lblHeader100.Text = _columnName;
                    container.Controls.Add(lblHeader100);

                    TextBox tb4 = new TextBox();
                    tb4.ID = "TextBox" + _objectName;
                    tb4.Attributes.Add("class", "form-control width-65 numeric margin-auto");
                    tb4.Attributes.Add("placeholder", "Prosen");
                    if(_columnName == "M")
                        tb4.Text = "100";
                    else
                        tb4.Text = "0";
                    tb4.Columns = 4;
                    container.Controls.Add(tb4);
                    break;
                case "ItemTextHeaderNumeric100Edit":
                    Label lblHeader100Edit = new Label();
                    lblHeader100Edit.Text = _columnName;
                    container.Controls.Add(lblHeader100Edit);

                    TextBox tb5 = new TextBox();
                    tb5.ID = "TextBox" + _objectName;
                    tb5.Attributes.Add("class", "form-control width-65 numeric margin-auto");
                    tb5.Attributes.Add("placeholder", "Prosen");
                    if (_objectValue == "")
                        tb5.Text = "0";
                    else
                        tb5.Text = _objectValue;
                    tb5.Columns = 4;
                    container.Controls.Add(tb5);
                    break;
                case "ItemLabelHeaderNumeric100Edit":
                    Label lblHeader100Edit2 = new Label();
                    lblHeader100Edit2.Text = _columnName;
                    container.Controls.Add(lblHeader100Edit2);

                    TextBox tb6 = new TextBox();
                    tb6.ID = "TextBox" + _objectName;
                    tb6.Attributes.Add("class", "form-control width-65 numeric margin-auto disabled");
                    tb6.Attributes.Add("placeholder", "Prosen");
                    tb6.Attributes.Add("disabled", "disabled");
                    if (_objectValue == "")
                        tb6.Text = "0";
                    else
                        tb6.Text = _objectValue;
                    tb6.Columns = 4;
                    container.Controls.Add(tb6);
                    break;
                case "ItemLabel":
                    Label lbl1 = new Label();
                    if (_columnName != "")
                    {
                        lbl1.DataBinding += new EventHandler(DataBindingLabel);
                    }
                    lbl1.ID = "Label" + _objectName;
                    lbl1.Attributes.Add("class", "width-90");
                    //lbl1.Width = 100;
                    container.Controls.Add(lbl1);
                    break;
                case "ItemLabelFixedWidth":
                    Label lbl2 = new Label();
                    if (_columnName != "")
                    {
                        lbl2.DataBinding += new EventHandler(DataBindingLabel);
                    }
                    lbl2.ID = "Label" + _objectName;
                    lbl2.Attributes.Add("class", "width-90");
                    lbl2.Width = 200;
                    container.Controls.Add(lbl2);
                    break;
                case "ItemLabelFixedWidthModel":
                    Label lbl3 = new Label();
                    if (_columnName != "")
                    {
                        lbl3.DataBinding += new EventHandler(DataBindingLabel);
                    }
                    lbl3.ID = "Label" + _objectName;
                    lbl3.Attributes.Add("class", "width-230");
                    lbl3.Width = 270;
                    container.Controls.Add(lbl3);
                    break;
                case "ItemLabelFixedWidthMoney":
                    Label lbl4 = new Label();
                    if (_columnName != "")
                    {
                        lbl4.DataBinding += new EventHandler(DataBindingLabelMoney);
                    }
                    lbl4.ID = "Label" + _objectName;
                    lbl4.Attributes.Add("class", "width-90 text-right");
                    lbl4.Width = 200;
                    container.Controls.Add(lbl4);
                    break;
                case "ItemLabelFixedWidthNumeric":
                    Label lbl5 = new Label();
                    if (_columnName != "")
                    {
                        lbl5.DataBinding += new EventHandler(DataBindingLabel);
                    }
                    lbl5.ID = "Label" + _objectName;
                    lbl5.Attributes.Add("class", "width-90 text-right");
                    lbl5.Width = 80;
                    container.Controls.Add(lbl5);
                    break;
                case "ItemCheckBox":
                    CheckBox chk1 = new CheckBox();
                    chk1.ID = "CheckBox" + _objectName;
                    chk1.Checked = true;
                    container.Controls.Add(chk1);
                    break;
                case "ItemCheckBoxHeader":
                    CheckBox chk2 = new CheckBox();
                    chk2.ID = "chkCheck";
                    chk2.Checked = true;
                    chk2.Attributes.Add("onClick", _objectName + "(this)");
                    container.Controls.Add(chk2);
                    break;
                case "EditItem":
                    break;

                case "Footer":
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + _columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }
        void DataBindingTextBox(object sender, EventArgs e)
        {
            TextBox txtdata = (TextBox)sender;
            GridViewRow container = (GridViewRow)txtdata.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                txtdata.Text = ObjSys.IsFormatNumber(dataValue.ToString());
            }
        }
        void DataBindingTextBoxNumeric(object sender, EventArgs e)
        {
            TextBox txtdata = (TextBox)sender;
            GridViewRow container = (GridViewRow)txtdata.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                txtdata.Text = dataValue.ToString();
            }
        }
        void DataBindingLabel(object sender, EventArgs e)
        {
            Label lblData = (Label)sender;
            GridViewRow container = (GridViewRow)lblData.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                lblData.Text = dataValue.ToString();
            }
        }
        void DataBindingLabelMoney(object sender, EventArgs e)
        {
            Label lblData = (Label)sender;
            GridViewRow container = (GridViewRow)lblData.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                lblData.Text = ObjSys.IsFormatNumber(dataValue.ToString());
            }
        }
    }
}