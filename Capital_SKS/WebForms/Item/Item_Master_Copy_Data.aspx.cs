using ORS_RCM;
using ORS_RCM_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Capital_SKS.WebForms.Item
{
    public partial class Item_Master_Copy_Data : System.Web.UI.Page
    {
        public string Item_Code
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Item_Name
        {
            get
            {
                if (Request.QueryString["Item_Name"] != null)
                {
                    return Request.QueryString["Item_Name"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        Item_Master_BL imeBL = new Item_Master_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtItemCode.Text = Item_Code;
                    txtItem_Name.Text = Item_Name;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                String itemcode = txtItemCode.Text;
                if (itemcode != null)
                {
                    int ItemID = imeBL.SelectItemID(itemcode);
                    if (ItemID == 0) {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Item_Code", typeof(string));
                        dt.Columns.Add("Item_Name", typeof(string));
                        DataRow dr = dt.NewRow();
                        dr["Item_Code"] = txtItemCode.Text;
                        dr["Item_Name"] = txtItem_Name.Text;
                        dt.Rows.Add(dr);

                        Session["Item_Code_Copy"] = dt;
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
                    }
                    else
                    {
                        GlobalUI.MessageBox("Item code is already exists!!");
                    }
                }
           }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}