﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using ORS_RCM_BL;


namespace Yahoo_Point_Promotion_Console
{
    public class Change_Template
    {
        int start, end;
        string line = string.Empty;
        string replaceword = string.Empty;
        string html = string.Empty;
        string itemcode = string.Empty;
        DataTable dtvalue;

        Shop_Template_BL shopTemplate;
        Item_Master_BL master = new Item_Master_BL();
        Item_BL item = new Item_BL();
        Item_Image_BL imageBL = new Item_Image_BL();

        public DataTable ModifyTable(DataTable dt, int shopID)
        {
            if (dt.Rows.Count > 0)
            {
                DataTable dtFinal = new DataTable();
                if (dt.Columns.Contains("PC用商品説明文")) // Item_Description_PC (rakuten)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "PC用商品説明文"));
                }
                if (dt.Columns.Contains("PC用販売説明文")) // Sale_Description_PC (rakuten)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "PC用販売説明文"));
                }
                if (dt.Columns.Contains("スマートフォン用商品説明文")) // Item_Description_Phone (rakuten)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "スマートフォン用商品説明文"));
                }
                if (dt.Columns.Contains("explanation")) // Merchandise_Information (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "explanation"));
                }
                if (dt.Columns.Contains("additional1")) // Sale_Description_PC (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "additional1"));
                }
                if (dt.Columns.Contains("caption")) // Item_Description_PC (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "caption"));
                }
                if (dt.Columns.Contains("sp-additional")) // Smart_Template (yahoo)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "sp-additional"));
                }
                if (dt.Columns.Contains("商品説明(1)")) // Item_Description_PC (ponpare)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "商品説明(1)"));
                }
                if (dt.Columns.Contains("商品説明(2)")) // Sale_Description_PC (ponpare)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "商品説明(2)"));
                }
                if (dt.Columns.Contains("商品説明(スマートフォン用)")) // Item_Description_Phone (Jisha)
                {
                    dtFinal = MergeTable(ChangeTemplate(dt, shopID, "商品説明(スマートフォン用)"));
                }

                if (dtFinal.Rows.Count > 0)
                    return dtFinal;
                else
                    return dt;
            }
            else
            {
                return null;
            }
        }
        public DataTable ChangeTemplate(DataTable dt, int shopID, string columnName)
        {
            DataColumnCollection columns = dt.Columns;

            if (columns.Contains(columnName))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    html = dr[columnName].ToString();

                    #region itemcode
                    if (columns.Contains("Item_Code"))  //For yahoo
                    {
                        itemcode = dr["Item_Code"].ToString();
                    }
                    //else if (columns.Contains("商品番号"))  //For rakuten
                    //{
                    //    itemcode = dr["商品番号"].ToString();
                    //}
                    //else if (columns.Contains("商品ID"))  //For ponpare
                    //{
                    //    itemcode = dr["商品ID"].ToString();
                    //}
                    #endregion

                    int index = 0;

                    //@"\[([^]]*)\]"
                    while (Regex.IsMatch(html, @"\[\[([^]]*)\]\]"))
                    {
                        #region Break While Loop
                        int value = ++index;

                        if (value >= 10)
                        {
                            break;
                        }
                        #endregion

                        string[] templateID = GetTemplateID(html);  // Read [[ ]] words

                        if (templateID.Length > 0 && String.IsNullOrWhiteSpace(templateID[0].ToString())) // Remove unformat [[ ]] 
                        {
                            html = html.Replace("[", "").Replace("]", "");
                        }

                        #region ShopTemplate
                        dtvalue = new DataTable();
                        dtvalue = GetTemplateDescription(templateID, shopID);  //Get from ShopTemplate Table

                        if (dtvalue != null && dtvalue.Rows.Count > 0)
                        {
                            var replacements = new Dictionary<string, string>();

                            for (int i = 0; i < templateID.Count(); i++)
                            {
                                string Key = templateID[i].ToString();
                                string Value = "";

                                DataRow[] drtemplate = dtvalue.Select("Template_ID='" + Key + "'");
                                if (drtemplate.Count() > 0)  // exist replace value
                                {
                                    Value = dtvalue.Select("Template_ID='" + Key + "'").CopyToDataTable().Rows[0]["Template_Description"].ToString();

                                    if (!replacements.Keys.Contains(Key))
                                    {
                                        replacements.Add(Key, Value);
                                    }
                                }
                                else //  not exist replace value
                                {
                                    if (!replacements.Keys.Contains(Key) && !Key.Contains("IF") &&
                                        !Key.Contains("基本情報") && !Key.Contains("詳細情報") &&
                                        !Key.Contains("product.product_id") && !Key.Contains("商品名") &&
                                        !Key.Contains("商品番号") && !Key.Contains("ブランド名") &&
                                        !Key.Contains("サイズ正式名称") && !Key.Contains("カラー正式名称") &&
                                        !Key.Contains("ゼット用項目（PC商品説明文）") && !Key.Contains("ゼット用項目（PC販売説明文）") &&
                                        !Key.Contains("関連商品") && !Key.Contains("テクノロジー画像") &&
                                        !Key.Contains("キャンペーン画像") && !Key.Contains("商品ページURL") &&
                                        !Key.Contains("定価") && !Key.Contains("販売価格"))
                                    {
                                        replacements.Add(Key, " ");
                                    }
                                }
                            }

                            // Replace
                            foreach (var replacement in replacements)
                            {
                                html = html.SafeReplace(replacement.Key, replacement.Value, true);
                            }

                        }
                        else  // change value is not exists.
                        {
                            string format;
                            if (templateID.Length > 0)
                            {
                                for (int h = 0; h < templateID.Length; h++)
                                {
                                    if (!templateID[h].ToString().Contains("IF") &&
                                        !templateID[h].ToString().Contains("基本情報") && !templateID[h].ToString().Contains("詳細情報") &&
                                        !templateID[h].ToString().Contains("product.product_id") && !templateID[h].ToString().Contains("商品名") &&
                                        !templateID[h].ToString().Contains("商品番号") && !templateID[h].ToString().Contains("ブランド名") &&
                                        !templateID[h].ToString().Contains("サイズ正式名称") && !templateID[h].ToString().Contains("カラー正式名称") &&
                                        !templateID[h].ToString().Contains("ゼット用項目（PC商品説明文）") && !templateID[h].ToString().Contains("ゼット用項目（PC販売説明文）") &&
                                        !templateID[h].ToString().Contains("関連商品") && !templateID[h].ToString().Contains("テクノロジー画像") &&
                                        !templateID[h].ToString().Contains("キャンペーン画像") && !templateID[h].ToString().Contains("商品ページURL") &&
                                        !templateID[h].ToString().Contains("定価") && !templateID[h].ToString().Contains("販売価格"))
                                    {
                                        format = string.Format(@"\[\[{0}\]\]", templateID[h].ToString());
                                        if (Regex.IsMatch(html, format))
                                        {
                                            html = Regex.Replace(html, format, "");
                                        }
                                    }
                                }
                            }
                            //else
                            //{
                            //    html = html.Replace("[", "").Replace("]", "");
                            //}
                        }
                        #endregion

                        #region Change Template_Detail
                        if (!string.IsNullOrWhiteSpace(itemcode) && !string.IsNullOrWhiteSpace(html) && (html.Contains("基本情報") || html.Contains("詳細情報")))
                        {
                            Template_Detail_BL template = new Template_Detail_BL();
                            dtvalue = new DataTable();
                            dtvalue = template.SelectByItemCode(itemcode);

                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                if (html.Contains("基本情報"))
                                {
                                    #region 基本情報
                                    for (int t = 20; t >= 1; t--)
                                    {
                                        //html = html.SafeReplace("基本情報" + j, dtTemplateDetail.Rows[0]["Template" + j].ToString(), true).SafeReplace("基本情報内容" + j, dtTemplateDetail.Rows[0]["Template_Content" + j].ToString(), true);
                                        if (html.Contains("[[IF setVal=基本情報" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=基本情報" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(dtvalue.Rows[0]["Template" + t].ToString()))
                                            {
                                                html = html.SafeReplace("基本情報" + t, dtvalue.Rows[0]["Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                                  .SafeReplace("基本情報内容" + t, dtvalue.Rows[0]["Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                                html = html.Remove(start, ("[[IF setVal=基本情報" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                //html = html.Remove(start, ("[[IF setVal=基本情報" + j + "]]").Length);
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=基本情報" + t + "]]").Length);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("基本情報" + t, dtvalue.Rows[0]["Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                              .SafeReplace("基本情報内容" + t, dtvalue.Rows[0]["Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                        }
                                    }
                                    #endregion
                                }
                                else if (html.Contains("詳細情報"))
                                {
                                    #region 詳細情報
                                    for (int t = 4; t >= 1; t--)
                                    {
                                        if (html.Contains("[[IF setVal=詳細情報" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=詳細情報" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(dtvalue.Rows[0]["Detail_Template" + t].ToString()))
                                            {
                                                html = html.SafeReplace("詳細情報" + t, dtvalue.Rows[0]["Detail_Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                                  .SafeReplace("詳細情報内容" + t, dtvalue.Rows[0]["Detail_Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                                html = html.Remove(start, ("[[IF setVal=詳細情報" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=詳細情報" + t + "]]").Length);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("詳細情報" + t, dtvalue.Rows[0]["Detail_Template" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true)
                                                              .SafeReplace("詳細情報内容" + t, dtvalue.Rows[0]["Detail_Template_Content" + t].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), true);
                                        }
                                    }
                                    #endregion
                                }

                            }
                            else //dtTemplateDetail == null
                            {
                                if (html.Contains("基本情報"))
                                {
                                    #region 基本情報
                                    for (int k = 20; k >= 1; k--)
                                    {
                                        if (html.Contains("[[IF setVal=基本情報" + k + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=基本情報" + k + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=基本情報" + k + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("基本情報" + k, "", true).SafeReplace("基本情報内容" + k, "", true);
                                        }
                                    }
                                    #endregion
                                }
                                else if (html.Contains("詳細情報"))
                                {
                                    #region 詳細情報
                                    for (int k = 4; k >= 1; k--)
                                    {
                                        if (html.Contains("[[IF setVal=詳細情報" + k + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=詳細情報" + k + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=詳細情報" + k + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("詳細情報" + k, "", true).SafeReplace("詳細情報内容" + k, "", true);
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        #region product.product_id
                        if (html.Contains("product.product_id"))
                        {
                            //if (columns.Contains("code"))  //For yahoo
                            //{
                            //    html = html.SafeReplace("product.product_id", itemcode, true);
                            //}
                            //else if (columns.Contains("商品番号"))  //For rakuten
                            //{
                            //    html = html.SafeReplace("product.product_id", itemcode, true);
                            //}
                            //else if (columns.Contains("商品ID"))  //For ponpare
                            //{
                            //    html = html.SafeReplace("product.product_id", itemcode, true);
                            //}
                            html = html.SafeReplace("product.product_id", itemcode, true);
                        }
                        #endregion

                        #region 商品名
                        if (html.Contains("商品名"))
                        {
                            //if (columns.Contains("name"))  //For yahoo
                            //{
                            //    replaceword = dr["name"].ToString();
                            //    //html = html.SafeReplace("商品名", dr["name"].ToString(), true);
                            //}
                            //else if (columns.Contains("商品名")) // For rakuten , ponpare
                            //{
                            //    replaceword = dr["商品名"].ToString();
                            //    //html = html.SafeReplace("商品名", dr["商品名"].ToString(), true);
                            //}


                            if (columns.Contains("Y_Item_Name"))
                            {
                                string itemName = dr["Y_Item_Name"].ToString();
                                string[] arr = itemName.Split(new string[] { "倍】", "【ポイント" }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string itemname in arr)
                                {
                                    int i;
                                    if (!int.TryParse(itemname, out i))
                                    {
                                        replaceword = itemname;
                                    }
                                }
                                // replaceword = dr["Y_Item_Name"].ToString();
                            }

                            if (html.Contains("[[IF setVal=商品名]]"))
                            {
                                start = html.IndexOf("[[IF setVal=商品名]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("商品名", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=商品名]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=商品名]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("商品名", replaceword, true);
                            }
                        }
                        #endregion

                        #region 商品番号
                        if (html.Contains("商品番号"))
                        {
                            //html = html.SafeReplace("商品番号", itemcode, true);
                            replaceword = itemcode;  // select from DataBase
                            if (html.Contains("[[IF setVal=商品番号]]"))
                            {
                                start = html.IndexOf("[[IF setVal=商品番号]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("商品番号", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=商品番号]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=商品番号]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("商品番号", replaceword, true);
                            }
                        }
                        #endregion

                        #region ブランド名
                        if (html.Contains("ブランド名"))
                        {
                            replaceword = master.GetBrandName(itemcode);  // select from DataBase

                            if (html.Contains("[[IF setVal=ブランド名]]"))
                            {
                                start = html.IndexOf("[[IF setVal=ブランド名]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("ブランド名", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=ブランド名]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=ブランド名]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("ブランド名", replaceword, true);
                            }
                        }
                        #endregion

                        #region 定価
                        if (html.Contains("定価"))
                        {
                            replaceword = master.GetListPrice(itemcode);  // select from DataBase

                            if (html.Contains("[[IF setVal=定価]]"))
                            {
                                start = html.IndexOf("[[IF setVal=定価]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("定価", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=定価]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=定価]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("定価", replaceword, true);
                            }
                        }
                        #endregion

                        #region 販売価格
                        if (html.Contains("販売価格"))
                        {
                            replaceword = master.GetSalePrice(itemcode);  // select from DataBase

                            if (html.Contains("[[IF setVal=販売価格]]"))
                            {
                                start = html.IndexOf("[[IF setVal=販売価格]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("販売価格", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=販売価格]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=販売価格]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("販売価格", replaceword, true);
                            }
                        }
                        #endregion

                        #region サイズ正式名称
                        if (html.Contains("サイズ正式名称"))
                        {
                            replaceword = item.GetSKUSizeName(itemcode); // select from DataBase

                            if (html.Contains("[[IF setVal=サイズ正式名称]]"))
                            {
                                start = html.IndexOf("[[IF setVal=サイズ正式名称]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("サイズ正式名称", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=サイズ正式名称]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=サイズ正式名称]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("サイズ正式名称", replaceword, true);
                            }
                        }
                        #endregion

                        #region カラー正式名称
                        if (html.Contains("カラー正式名称"))
                        {
                            replaceword = item.GetSKUColorName(itemcode); // select from DataBase

                            if (html.Contains("[[IF setVal=カラー正式名称]]"))
                            {
                                start = html.IndexOf("[[IF setVal=カラー正式名称]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("カラー正式名称", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=カラー正式名称]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=カラー正式名称]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("カラー正式名称", replaceword, true);
                            }
                        }
                        #endregion

                        #region ゼット用項目（PC商品説明文）
                        if (html.Contains("ゼット用項目（PC商品説明文）"))
                        {
                            replaceword = master.GetZettItemDescription(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=ゼット用項目（PC商品説明文）]]"))
                            {
                                start = html.IndexOf("[[IF setVal=ゼット用項目（PC商品説明文）]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("ゼット用項目（PC商品説明文）", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC商品説明文）]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC商品説明文）]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("ゼット用項目（PC商品説明文）", replaceword, true);
                            }
                        }
                        #endregion

                        #region ゼット用項目（PC販売説明文）
                        if (html.Contains("ゼット用項目（PC販売説明文）"))
                        {
                            replaceword = master.GetZettSaleDescription(itemcode);  // select from DataBase
                            if (html.Contains("[[IF setVal=ゼット用項目（PC販売説明文）]]"))
                            {
                                start = html.IndexOf("[[IF setVal=ゼット用項目（PC販売説明文）]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("ゼット用項目（PC販売説明文）", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC販売説明文）]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=ゼット用項目（PC販売説明文）]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("ゼット用項目（PC販売説明文）", replaceword, true);
                            }
                        }
                        #endregion

                        #region 関連商品
                        if (html.Contains("関連商品"))
                        {
                            Item_Related_Item_BL relateditemBL = new Item_Related_Item_BL();
                            dtvalue = new DataTable();
                            dtvalue = relateditemBL.SelectRelatedCode(itemcode);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 5; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Related_ItemCode"].ToString();
                                        if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("関連商品_商品番号" + t, dtItem.Rows[0]["Related_ItemCode"].ToString(), true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
                                                html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("関連商品_商品番号" + t, dtItem.Rows[0]["Related_ItemCode"].ToString(), true).SafeReplace("関連商品_商品名" + t, dtItem.Rows[0]["Item_Name"].ToString(), true).SafeReplace("関連商品_商品画像" + t, dtItem.Rows[0]["Image_Name"].ToString(), true).SafeReplace("関連商品_販売価格" + t, dtItem.Rows[0]["Sale_Price"].ToString(), true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("関連商品_商品番号" + t, "", true).SafeReplace("関連商品_商品名" + t, "", true).SafeReplace("関連商品_商品画像" + t, "", true).SafeReplace("関連商品_販売価格" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 5; t++)
                                {
                                    if (html.Contains("[[IF setVal=関連商品" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=関連商品" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=関連商品" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("関連商品_商品番号" + t, "", true).SafeReplace("関連商品_商品名" + t, "", true).SafeReplace("関連商品_商品画像" + t, "", true).SafeReplace("関連商品_販売価格" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region 商品ページURL
                        if (html.Contains("商品ページURL"))
                        {
                            Shop_BL shop = new Shop_BL();
                            replaceword = shop.SelectProductPageURL(shopID);  // select from DataBase

                            if (html.Contains("[[IF setVal=商品ページURL]]"))
                            {
                                start = html.IndexOf("[[IF setVal=商品ページURL]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("商品ページURL", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=商品ページURL]]").Length);
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=商品ページURL]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("商品ページURL", replaceword, true);
                            }
                        }
                        #endregion

                        #region 商品画像
                        if (html.Contains("商品画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = imageBL.SelectImageName(itemcode, 0);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 6; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Image_Name"].ToString();
                                        if (html.Contains("[[IF setVal=商品画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=商品画像" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("商品画像" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("商品画像" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=商品画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=商品画像" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("商品画像" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 6; t++)
                                {
                                    if (html.Contains("[[IF setVal=商品画像" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=商品画像" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=商品画像" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("商品画像" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region テクノロジー画像
                        if (html.Contains("テクノロジー画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = imageBL.SelectImageName(itemcode, 1);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 6; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Image_Name"].ToString();
                                        if (html.Contains("[[IF setVal=テクノロジー画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=テクノロジー画像" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("テクノロジー画像" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("テクノロジー画像" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=テクノロジー画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=テクノロジー画像" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("テクノロジー画像" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 6; t++)
                                {
                                    if (html.Contains("[[IF setVal=テクノロジー画像" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=テクノロジー画像" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=テクノロジー画像" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("テクノロジー画像" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region キャンペーン画像
                        if (html.Contains("キャンペーン画像"))
                        {
                            dtvalue = new DataTable();
                            dtvalue = imageBL.SelectImageName(itemcode, 2);
                            if (dtvalue != null && dtvalue.Rows.Count > 0)
                            {
                                for (int t = 1; t <= 5; t++)
                                {
                                    DataRow[] drvalue = dtvalue.Select("SN=" + t + "");
                                    if (drvalue.Count() > 0)
                                    {
                                        DataTable dtItem = dtvalue.Select("SN=" + t + "").CopyToDataTable();
                                        replaceword = dtItem.Rows[0]["Image_Name"].ToString();
                                        if (html.Contains("[[IF setVal=キャンペーン画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=キャンペーン画像" + t + "]]");

                                            if (!string.IsNullOrWhiteSpace(replaceword))
                                            {
                                                html = html.SafeReplace("キャンペーン画像" + t, replaceword, true);
                                                html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                            }
                                            else
                                            {
                                                #region Delete Line
                                                end = html.IndexOf("[[/IF]]", start);
                                                if (end != -1) // If contain [[/IF]]
                                                {
                                                    line = html.Substring(start, end - start) + "[[/IF]]";
                                                    html = html.Remove(start, line.Length);
                                                }
                                                else
                                                {
                                                    html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("キャンペーン画像" + t, replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        #region Delete Line
                                        if (html.Contains("[[IF setVal=キャンペーン画像" + t + "]]"))
                                        {
                                            start = html.IndexOf("[[IF setVal=キャンペーン画像" + t + "]]");
                                            end = html.IndexOf("[[/IF]]", start);
                                            if (end != -1)
                                            {
                                                line = html.Substring(start, end - start) + "[[/IF]]";
                                                html = html.Remove(start, line.Length);
                                            }
                                            else
                                            {
                                                html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("キャンペーン画像" + t, "", true);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Delete All Line
                                for (int t = 1; t <= 5; t++)
                                {
                                    if (html.Contains("[[IF setVal=キャンペーン画像" + t + "]]"))
                                    {
                                        start = html.IndexOf("[[IF setVal=キャンペーン画像" + t + "]]");
                                        end = html.IndexOf("[[/IF]]", start);
                                        if (end != -1)
                                        {
                                            line = html.Substring(start, end - start) + "[[/IF]]";
                                            html = html.Remove(start, line.Length);
                                        }
                                        else
                                        {
                                            html = html.Remove(start, ("[[IF setVal=キャンペーン画像" + t + "]]").Length);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("キャンペーン画像" + t, "", true);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region Saleprice
                        if (html.Contains("[[IF saleprice"))
                        {
                            start = html.IndexOf("[[IF saleprice");
                            int index1 = html.IndexOf("]][[/IF]]");
                            string match2 = html.Substring(start, html.IndexOf("]][[/IF]]") - start);

                            match2 = match2.Replace("[[IF ", "");
                            int sale_price = Convert.ToInt32(dr["Sale_Price"].ToString());
                            if (match2.Contains(">="))
                            {
                                string[] str = match2.Split(new string[] { ">=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price >= tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains("<="))
                            {
                                string[] str = match2.Split(new string[] { "<=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price <= tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains(">"))
                            {
                                string[] str = match2.Split(new string[] { ">" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price > tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                string[] str = match2.Split(new string[] { "<" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (sale_price < tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region ListPrice
                        if (html.Contains("[[IF listprice"))
                        {
                            start = html.IndexOf("[[IF listprice");
                            int index1 = html.IndexOf("]][[/IF]]");
                            string match2 = html.Substring(start, html.IndexOf("]][[/IF]]") - start);
                            match2 = match2.Replace("[[IF ", "");
                            int list_price = Convert.ToInt32(dr["R_List_Price"].ToString());
                            if (match2.Contains(">="))
                            {
                                string[] str = match2.Split(new string[] { ">=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price >= tmp_Price)
                                {
                                    html = html.SafeReplace(str1, master.GetTemplateValue(match2, 1), true);
                                    // html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains("<="))
                            {
                                string[] str = match2.Split(new string[] { "<=" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price <= tmp_Price)
                                {
                                    html = html.SafeReplace(str1, master.GetTemplateValue(match2, 1), true);
                                    //html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else if (match2.Contains(">"))
                            {
                                string[] str = match2.Split(new string[] { ">" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price > tmp_Price)
                                {
                                    html = html.Replace(str1, master.GetTemplateValue(match2, 1));
                                    //html = html.Remove(start, str1.Length);
                                    //html = html.Replace(str1, "");
                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                string[] str = match2.Split(new string[] { "<" }, StringSplitOptions.None);
                                string str1 = "[[IF " + match2 + "]][[/IF]]";
                                int tmp_Price = Convert.ToInt32(str[1].ToString());
                                if (list_price < tmp_Price)
                                {
                                    html = html.SafeReplace(str1, master.GetTemplateValue(match2, 1), true);
                                    // html = html.Remove(start, str.Length);

                                }
                                else
                                {
                                    end = html.IndexOf("]][[/IF]]", start);
                                    if (end != -1)
                                    {
                                        line = html.Substring(start, end - start) + "]][[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF " + match2 + "]]").Length);
                                    }
                                }
                            }

                        }
                        #endregion

                        #region 販売単位
                        if (html.Contains("販売単位"))
                        {
                            replaceword = master.GetSalesUnit(itemcode, "Sales_unit");  // select from DataBase
                            if (html.Contains("[[IF setVal=販売単位]]"))
                            {
                                start = html.IndexOf("[[IF setVal=販売単位]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    replaceword = "1" + replaceword;
                                    html = html.SafeReplace("販売単位", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=販売単位]]").Length);
                                    //html = html.Remove(start, ("[[IF setVal=内容量数1]]").Length);
                                    if (html.Contains("内容量数1"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Content_quantity_number_1");

                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            string word = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                            if (!string.IsNullOrWhiteSpace(word))
                                            {
                                                replaceword = "(" + "1" + word + replaceword;
                                                html = html.SafeReplace("内容量数1", replaceword, true);
                                            }
                                            else
                                            {
                                                replaceword = "/" + replaceword;
                                                html = html.SafeReplace("内容量数1", replaceword, true);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("内容量数1", replaceword, true);

                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量数1", replaceword, true);
                                    }
                                    if (html.Contains("内容量単位1"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Contents_unit_1");
                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            string word = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                            if (!String.IsNullOrWhiteSpace(word))
                                            {
                                                replaceword = replaceword + ")";
                                                html = html.SafeReplace("内容量単位1", replaceword, true);
                                            }
                                            else
                                            {
                                                html = html.SafeReplace("内容量単位1", replaceword, true);
                                            }
                                        }
                                        else
                                        {
                                            html = html.SafeReplace("内容量単位1", replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量単位1", replaceword, true);
                                    }
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=販売単位]]</td></tr>").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("販売単位", replaceword, true);
                            }
                        }
                        #endregion

                        #region 内容量数2
                        if (html.Contains("内容量数2"))
                        {
                            //html = html.SafeReplace("商品番号", itemcode, true);
                            replaceword = master.GetSalesUnit(itemcode, "Content_quantity_number_2");  // select from DataBase 
                            if (html.Contains("[[IF setVal=内容量数2]]"))
                            {
                                start = html.IndexOf("[[IF setVal=内容量数2]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    replaceword = "/" + replaceword;
                                    html = html.SafeReplace("内容量数2", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    if (html.Contains("内容量単位2"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            replaceword = replaceword + "入り";
                                            html = html.SafeReplace("内容量単位2", replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量単位2", replaceword, true);
                                    }
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("内容量数2", replaceword, true);
                            }
                        }
                        #endregion

                        #region 2
                        if (html.Contains("その他商品説明"))
                        {
                            //html = html.SafeReplace("商品番号", itemcode, true);
                            replaceword = master.GetSalesUnit(itemcode, "Content_quantity_number_2");  // select from DataBase
                            if (html.Contains("[[IF setVal=内容量数2]]"))
                            {
                                start = html.IndexOf("[[IF setVal=内容量数2]]");

                                if (!string.IsNullOrWhiteSpace(replaceword))
                                {
                                    html = html.SafeReplace("内容量数2", replaceword, true);
                                    html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    if (html.Contains("内容量単位2"))
                                    {
                                        replaceword = master.GetSalesUnit(itemcode, "Contents_unit_2");
                                        if (!string.IsNullOrWhiteSpace(replaceword))
                                        {
                                            html = html.SafeReplace("内容量単位2", replaceword, true);
                                        }
                                    }
                                    else
                                    {
                                        html = html.SafeReplace("内容量単位2", replaceword, true);
                                    }
                                }
                                else
                                {
                                    end = html.IndexOf("[[/IF]]", start);
                                    if (end != -1) // If contain [[/IF]]
                                    {
                                        line = html.Substring(start, end - start) + "[[/IF]]";
                                        html = html.Remove(start, line.Length);
                                    }
                                    else
                                    {
                                        html = html.Remove(start, ("[[IF setVal=内容量数2]]").Length);
                                    }
                                }
                            }
                            else
                            {
                                html = html.SafeReplace("内容量数2", replaceword, true);
                            }
                        }
                        #endregion
                        html = html.Replace("[[/IF]]", "");
                        html = html.Replace("[[IF setVal=内容量数1]]", "");
                    }
                    dr[columnName] = html;
                }
            }
            return dt;
        }

        public DataTable GetTemplateDescription(string[] templateID, int shopID)
        {
            shopTemplate = new ORS_RCM_BL.Shop_Template_BL();
            DataTable dt = shopTemplate.GetTemplateDescription(templateID, shopID);
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public string[] GetTemplateID(string html)
        {
            //@"\[([^]]*)\]"
            ICollection<string> matches =
                Regex.Matches(html.Replace(Environment.NewLine, ""), @"\[\[([^]]*)\]\]")
                .Cast<Match>()
                .Select(x => x.Groups[1].Value)
                .ToList();

            string temp = "";

            if (matches.Count == 0)
            {
                html = html.Replace("[", "").Replace("]", "");
            }



            foreach (string match in matches)
            {
                temp += match + ',';
            }

            temp = temp.Replace("[", "");

            string[] templateID = temp.TrimEnd(',').TrimStart('[').Split(',');



            return templateID;
        }

        private static DataTable MergeTable(DataTable dt)
        {
            DataTable dtTmp = new DataTable();
            if (dt.Rows.Count > 0)
            {
                dtTmp.Merge(dt);
            }
            return dtTmp;
        }
    }
    public static class StringExtensions
    {
        public static string SafeReplace(this string input, string find, string replace, bool matchWholeWord)
        {

            string textToFind = matchWholeWord ? string.Format(@"\[\[({0})\]\]", find) : " ";
            if (Regex.IsMatch(input, textToFind))
            {
                return Regex.Replace(input, textToFind, replace);
            }
            else
            {
                return input;
            }


        }
    }
}
