﻿using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Eval;
using System.Collections.Generic;

namespace Netnr.Core
{
    public class NPOITo
    {
        /// <summary>
        /// DataTable生成Excel
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="strExcelFileName">物理路径 + 文件名称 + 格式</param>
        /// <returns>返回生成状态</returns>
        public static bool DataTableToExcel(DataTable dt, string strExcelFileName)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");

                //标题样式
                ICellStyle HeadercellStyle = workbook.CreateCellStyle();
                HeadercellStyle.BorderBottom = BorderStyle.Thin;
                HeadercellStyle.BorderLeft = BorderStyle.Thin;
                HeadercellStyle.BorderRight = BorderStyle.Thin;
                HeadercellStyle.BorderTop = BorderStyle.Thin;
                HeadercellStyle.Alignment = HorizontalAlignment.Center;
                HeadercellStyle.VerticalAlignment = VerticalAlignment.Center;
                //字体
                IFont headerfont = workbook.CreateFont();
                headerfont.Boldweight = (short)FontBoldWeight.Bold;
                HeadercellStyle.SetFont(headerfont);

                //用column name 作为列名
                int icolIndex = 0;
                IRow headerRow = sheet.CreateRow(0);
                headerRow.Height = 20 * 22;
                foreach (DataColumn item in dt.Columns)
                {
                    ICell cell = headerRow.CreateCell(icolIndex);
                    cell.SetCellValue(item.ColumnName);
                    cell.CellStyle = HeadercellStyle;
                    icolIndex++;
                }

                //单元格样式
                ICellStyle cellStyle = workbook.CreateCellStyle();

                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
                cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                cellStyle.BorderBottom = BorderStyle.Thin;
                cellStyle.BorderLeft = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
                cellStyle.BorderTop = BorderStyle.Thin;
                cellStyle.VerticalAlignment = VerticalAlignment.Center;

                IFont cellfont = workbook.CreateFont();
                cellfont.Boldweight = (short)FontBoldWeight.Normal;
                cellStyle.SetFont(cellfont);

                //建立内容行
                int iRowIndex = 1;
                int iCellIndex = 0;
                foreach (DataRow Rowitem in dt.Rows)
                {
                    IRow DataRow = sheet.CreateRow(iRowIndex);
                    DataRow.Height = 20 * 16;
                    foreach (DataColumn Colitem in dt.Columns)
                    {
                        ICell cell = DataRow.CreateCell(iCellIndex);
                        cell.SetCellValue(Rowitem[Colitem].ToString());
                        cell.CellStyle = cellStyle;
                        iCellIndex++;
                    }
                    iCellIndex = 0;
                    iRowIndex++;
                }

                //自适应列宽度
                for (int i = 0; i < icolIndex; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                //写Excel
                FileStream file = new FileStream(strExcelFileName, FileMode.OpenOrCreate);
                workbook.Write(file);
                file.Flush();
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 导出多个工作簿
        /// </summary>
        /// <param name="dicSheet">工作簿名：数据表</param>
        /// <param name="strExcelFileName">物理路径 + 文件名称 + 格式</param>
        /// <returns></returns>
        public static bool DataTableToExcel(Dictionary<string, DataTable> dicSheet, string strExcelFileName)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                foreach (var sheetitem in dicSheet.Keys)
                {
                    var dt = dicSheet[sheetitem];

                    ISheet sheet = workbook.CreateSheet(sheetitem);

                    //标题样式
                    ICellStyle HeadercellStyle = workbook.CreateCellStyle();
                    HeadercellStyle.BorderBottom = BorderStyle.Thin;
                    HeadercellStyle.BorderLeft = BorderStyle.Thin;
                    HeadercellStyle.BorderRight = BorderStyle.Thin;
                    HeadercellStyle.BorderTop = BorderStyle.Thin;
                    HeadercellStyle.Alignment = HorizontalAlignment.Center;
                    HeadercellStyle.VerticalAlignment = VerticalAlignment.Center;
                    //字体
                    IFont headerfont = workbook.CreateFont();
                    headerfont.Boldweight = (short)FontBoldWeight.Bold;
                    HeadercellStyle.SetFont(headerfont);

                    //用column name 作为列名
                    int icolIndex = 0;
                    IRow headerRow = sheet.CreateRow(0);
                    headerRow.Height = 20 * 22;
                    foreach (DataColumn item in dt.Columns)
                    {
                        ICell cell = headerRow.CreateCell(icolIndex);
                        cell.SetCellValue(item.ColumnName);
                        cell.CellStyle = HeadercellStyle;
                        icolIndex++;
                    }

                    //单元格样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();

                    //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                    cellStyle.BorderBottom = BorderStyle.Thin;
                    cellStyle.BorderLeft = BorderStyle.Thin;
                    cellStyle.BorderRight = BorderStyle.Thin;
                    cellStyle.BorderTop = BorderStyle.Thin;
                    cellStyle.VerticalAlignment = VerticalAlignment.Center;

                    IFont cellfont = workbook.CreateFont();
                    cellfont.Boldweight = (short)FontBoldWeight.Normal;
                    cellStyle.SetFont(cellfont);

                    //建立内容行
                    int iRowIndex = 1;
                    int iCellIndex = 0;
                    foreach (DataRow Rowitem in dt.Rows)
                    {
                        IRow DataRow = sheet.CreateRow(iRowIndex);
                        DataRow.Height = 20 * 16;
                        foreach (DataColumn Colitem in dt.Columns)
                        {
                            ICell cell = DataRow.CreateCell(iCellIndex);
                            cell.SetCellValue(Rowitem[Colitem].ToString());
                            cell.CellStyle = cellStyle;
                            iCellIndex++;
                        }
                        iCellIndex = 0;
                        iRowIndex++;
                    }

                    //自适应列宽度
                    for (int i = 0; i < icolIndex; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                }

                //写Excel
                FileStream file = new FileStream(strExcelFileName, FileMode.OpenOrCreate);
                workbook.Write(file);
                file.Flush();
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable ExcelToDataTable(HSSFWorkbook workbook, int iSheetIndex)
        {
            DataTable dt = new DataTable();

            ISheet sheet = workbook.GetSheetAt(iSheetIndex);

            //列头
            foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
            {
                dt.Columns.Add(item.ToString(), typeof(string));
            }

            //写入内容
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;
                if (row.RowNum == sheet.FirstRowNum)
                {
                    continue;
                }

                DataRow dr = dt.NewRow();
                foreach (ICell item in row.Cells)
                {
                    switch (item.CellType)
                    {
                        case CellType.Boolean:
                            dr[item.ColumnIndex] = item.BooleanCellValue;
                            break;
                        case CellType.Error:
                            dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                            break;
                        case CellType.Formula:
                            switch (item.CachedFormulaResultType)
                            {
                                case CellType.Boolean:
                                    dr[item.ColumnIndex] = item.BooleanCellValue;
                                    break;
                                case CellType.Error:
                                    dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                    break;
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(item))
                                    {
                                        dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                    }
                                    else
                                    {
                                        dr[item.ColumnIndex] = item.NumericCellValue;
                                    }
                                    break;
                                case CellType.String:
                                    string str = item.StringCellValue;
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        dr[item.ColumnIndex] = str.ToString();
                                    }
                                    else
                                    {
                                        dr[item.ColumnIndex] = null;
                                    }
                                    break;
                                case CellType.Unknown:
                                case CellType.Blank:
                                default:
                                    dr[item.ColumnIndex] = string.Empty;
                                    break;
                            }
                            break;
                        case CellType.Numeric:
                            if (DateUtil.IsCellDateFormatted(item))
                            {
                                dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                            }
                            else
                            {
                                dr[item.ColumnIndex] = item.NumericCellValue;
                            }
                            break;
                        case CellType.String:
                            string strValue = item.StringCellValue;
                            if (!string.IsNullOrEmpty(strValue))
                            {
                                dr[item.ColumnIndex] = strValue.ToString();
                            }
                            else
                            {
                                dr[item.ColumnIndex] = null;
                            }
                            break;
                        case CellType.Unknown:
                        case CellType.Blank:
                        default:
                            dr[item.ColumnIndex] = string.Empty;
                            break;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }


        /// <summary>
        /// Excel文件导成Datatable
        /// </summary>
        /// <param name="strFilePath">Excel文件目录地址</param>
        /// <param name="iSheetIndex">Excel sheet index</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string strFilePath, int iSheetIndex)
        {
            string strExtName = Path.GetExtension(strFilePath);
            DataTable dt = new DataTable();
            if (strExtName.Equals(".xls") || strExtName.Equals(".xlsx"))
            {
                using (FileStream file = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(file);
                    dt = ExcelToDataTable(workbook, iSheetIndex);
                }
            }

            return dt;
        }

        public static DataTable ExcelToDataTable(Stream s, int iSheetIndex)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook workbook = new HSSFWorkbook(s);
            dt = ExcelToDataTable(workbook, iSheetIndex);
            return dt;
        }

    }
}