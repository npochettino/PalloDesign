using Modelos;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;

namespace UI.Web.Util
{
    public class FuncionesComunes
    {
        public static string GenerateExcel(DataTable dtReferencias, string nombreArchivo, string excelSheetName)
        {
            string fileName = nombreArchivo;
            //string currentDirectorypath = Environment.CurrentDirectory;       
            //string currentDirectorypath = System.Configuration.ConfigurationManager.AppSettings["CarpetaDownload"];
            //string serverPath = 
            string finalFileNameWithPath = string.Empty;
            fileName = string.Format("{0}_{1}", fileName, DateTime.Now.ToString("dd-MM-yyyy"));
            string currentDirectorypath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/"), fileName);
            finalFileNameWithPath = string.Format("{0}.xlsx", currentDirectorypath);
            //finalFileNameWithPath = string.Format("{0}\\{1}.xlsx", currentDirectorypath, fileName);

            //Delete existing file with same file name.
            if (File.Exists(finalFileNameWithPath))
                File.Delete(finalFileNameWithPath);

            var newFile = new FileInfo(finalFileNameWithPath);

            using (var package = new ExcelPackage(newFile))
            {
                //Add a new table from the given datatable and start loading table form A1 cell.
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(excelSheetName);

                //Add a new table from the given datatable

                //TODO (Agregar sucursales dinámicamente)
                //Encabezado
                worksheet.Cells["A1"].Value = "Nombre Articulo";
                worksheet.Cells["B1"].Value = "Precio Compra";
                worksheet.Cells["C1"].Value = "Precio Venta";
                worksheet.Cells["D1"].Value = "ID Rubro";
                worksheet.Cells["E1"].Value = "Stock Maximo";
                worksheet.Cells["F1"].Value = "Stock Minimo";
                worksheet.Cells["G1"].Value = "Stock Actual";
                worksheet.Cells["H1"].Value = "Stock Suc. 1";
                worksheet.Cells["I1"].Value = "Stock Suc. 2";
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["B1"].Style.Font.Bold = true;
                worksheet.Cells["C1"].Style.Font.Bold = true;
                worksheet.Cells["D1"].Style.Font.Bold = true;
                worksheet.Cells["E1"].Style.Font.Bold = true;
                worksheet.Cells["F1"].Style.Font.Bold = true;
                worksheet.Cells["G1"].Style.Font.Bold = true;
                worksheet.Cells["H1"].Style.Font.Bold = true;
                worksheet.Cells["I1"].Style.Font.Bold = true;


                //Referencias
                worksheet.Cells["K1"].Value = "Referencias Rubros: ";
                worksheet.Cells["K2"].LoadFromDataTable(dtReferencias, true, TableStyles.Light3);//Light3

                worksheet.Cells.AutoFitColumns();          
          
                package.Workbook.Properties.Title = @"Importar Articulos";
                package.Workbook.Properties.Author = "Las Chulas";
                package.Workbook.Properties.Subject = @"Importar Articulos";


                //Save all changes to excel sheet
                package.Save();
                return finalFileNameWithPath;


            }
        }

        public static string GenerateExcelReporteArticulos(DataTable dtReferencias, string nombreArchivo, string cabecera)
        {
            string fileName = nombreArchivo;
            //string currentDirectorypath = Environment.CurrentDirectory;       
            //string currentDirectorypath = System.Configuration.ConfigurationManager.AppSettings["CarpetaDownload"];
            //string serverPath = 
            string finalFileNameWithPath = string.Empty;
            fileName = string.Format("{0}_{1}", fileName, DateTime.Now.ToString("dd-MM-yyyy"));
            string currentDirectorypath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/"), fileName);
            finalFileNameWithPath = string.Format("{0}.xlsx", currentDirectorypath);
            //finalFileNameWithPath = string.Format("{0}\\{1}.xlsx", currentDirectorypath, fileName);

            //Delete existing file with same file name.
            if (File.Exists(finalFileNameWithPath))
                File.Delete(finalFileNameWithPath);

            var newFile = new FileInfo(finalFileNameWithPath);

            using (var package = new ExcelPackage(newFile))
            {
                //Add a new table from the given datatable and start loading table form A1 cell.
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("StockArticulos");

                //Encabezado
                worksheet.Cells["A1"].Value = "Stock Articulos x Sucursal";
                worksheet.Cells["A1"].Style.Font.Bold = true;

                worksheet.Cells["A3"].Value = cabecera;
                worksheet.Cells["A3"].Style.Font.Bold = true;

                //Referencias          
                worksheet.Cells["A5"].LoadFromDataTable(dtReferencias, true, TableStyles.Light3);//Light3

                worksheet.Cells.AutoFitColumns();

                package.Workbook.Properties.Title = @"Exportar Stock Articulos";
                package.Workbook.Properties.Author = "Las Chulas";
                package.Workbook.Properties.Subject = @"Exportar Stock Articulos";


                //Save all changes to excel sheet
                package.Save();
                return finalFileNameWithPath;


            }
        }

        public static void DownloadFile(string nombreArchivo)
        {           
            nombreArchivo = nombreArchivo + ".xlsx";
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            Response.Clear();
            Response.ContentType = @"application\octet-stream";
            System.IO.FileInfo file = new System.IO.FileInfo(nombreArchivo);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.Flush();
        }

        public static DataTable ListToDataTable<T>(List<Rubro> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(Rubro).GetProperties())
            {
               
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (Rubro t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(Rubro).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            dt.Columns.Remove("Descripcion");
            dt.Columns.Remove("Habilitado");
            dt.Columns.Remove("Articulos");
            return dt;
        }

        public static DataTable ListToDataTable<T>(List<StockArticuloSucursal> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(StockArticuloSucursal).GetProperties())
            {

                if (info.Name == "Articulo")
                {
                    dt.Columns.Add(new DataColumn(info.Name, System.Type.GetType("System.String")));
                }
                else if (info.Name == "Sucursal")
                {
                    dt.Columns.Add(new DataColumn(info.Name, System.Type.GetType("System.String")));
                }
                else
                {
                    dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
                }
               
            }
            foreach (StockArticuloSucursal t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(StockArticuloSucursal).GetProperties())
                {
                    if (info.Name == "Articulo")
                    {
                        row[info.Name] = t.Articulo.Nombre;
                    }
                    else if (info.Name == "Sucursal")
                    {
                        row[info.Name] = t.Sucursal.Nombre;
                    }
                    else
                    {
                        row[info.Name] = info.GetValue(t, null);
                    }
                   
                }
                dt.Rows.Add(row);
            }
            dt.Columns.Remove("ArticuloID");
            dt.Columns.Remove("SucursalID");
            dt.Columns.Remove("Id");
            return dt;
        }

        public static void ShowFile(string path, HttpResponse response)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    /* FileInfo fi = new FileInfo(path);
                     if (fi.Exists)
                     {
                         System.Diagnostics.Process.Start(@path);
                     }*/

                    TransferFile(path, "attachment", response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void TransferFile(string direccionFile, string mode, HttpResponse Response)
        {
            //Descarga el documento en el cliente.  
            //Creamos una instancia de la clase FileInfo para obtener las propiedades del archivo siendo descargado
            if (string.IsNullOrEmpty(direccionFile)) return;
            FileInfo fileDown = new FileInfo(direccionFile);
            direccionFile = null;
            // Checkear si el archivo existe
            if (fileDown.Exists)
            {
                Response.Clear();

                // Limpiar el contenido del response
                Response.Buffer = true;

                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", mode + "; filename=" + fileDown.Name);

                // Agregamos el tamaño del archivo                
                Response.AddHeader("content-length", fileDown.Length.ToString());

                // Seteamos el tipo del contenido
                Response.ContentType = "application/pdf";// FuncionesUtiles.ReturnContentType(fileDown.Extension.ToLower());

                // Escribimos el archivo en el response
                if (fileDown.Exists)
                {
                    Response.TransmitFile(fileDown.FullName);
                    Response.Flush();
                }
            }
        }
    }
}