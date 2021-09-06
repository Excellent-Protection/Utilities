using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public static class ConvertDocToPDF
    {
        public static string Convert(string FileText, string FileName)
        {
            string pdfName = "";
            return Convert(FileText, FileName, out pdfName, true);
        }
        public static string Convert(string FileText, string FileName, out string PdfName, bool Redirect)
        {
            PdfName = "";
            string filePath = @"C:\ISV\PDFFiles\" + DateTime.Now.Ticks.ToString() + FileName;
            System.IO.FileInfo fileasword = new System.IO.FileInfo(filePath);
            //
            //this important comment
            //to enable interop from asp.net
            //goto component Services=>DCOM Config => microsoft word =>right click properties >=Identity =>Inetractive user checkbox
            //and create desktop folder In this folders C:\Windows\SysWOW64\config\systemprofile
            //C:\Windows\System32\config\systemprofile
            try
            {


                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("~"));
                dir = dir.Parent;
                dir = new System.IO.DirectoryInfo(dir.FullName + "/PDFFiles/");

                string outPutFile = filePath.Replace(".doc", ".pdf").Replace(".xml", ".pdf");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                if (System.IO.File.Exists(outPutFile))
                {
                    System.IO.File.Delete(outPutFile);
                }
                System.IO.File.WriteAllText(filePath, FileText);
                // Create an instance of Word.exe
                Microsoft.Office.Interop.Word._Application oWord = new Microsoft.Office.Interop.Word.Application();

                // Make this instance of word invisible (Can still see it in the taskmgr).
                oWord.Visible = false;

                // Interop requires objects.
                object oMissing = System.Reflection.Missing.Value;
                object isVisible = true;
                object readOnly = false;
                object oInput = filePath;
                object oOutput = outPutFile;
                PdfName = outPutFile;
                object oFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;


                // Load a document into our instance of word.exe
                Microsoft.Office.Interop.Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                // Make this document the active document.
                oDoc.Activate();

                // Save this document in Word 2003 format.
                oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.ContentType = "application/pdf";
                //HttpContext.Current.Response.AddHeader("Content-Disposition",
                //    "attachment;filename=\"" + FileName + ".pdf\"");
                //HttpContext.Current.Response.TransmitFile(outPutFile);
                System.IO.FileInfo file = new System.IO.FileInfo(outPutFile);
                if (Redirect)
                {
                    // HttpContext.Current.Response.Redirect("~/PDFFiles/" + file.Name);
                    //HttpContext.Current.Response.Redirect(outPutFile);
                    return file.Name;

                }

            }
            catch (Exception ex)
            {
                ///LogError.Error(exc, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return fileasword.Name;
                //if (Redirect)
                //{

                //    HttpContext.Current.Response.Clear();
                //    HttpContext.Current.Response.ContentType = "application/ms-word";
                //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                //    HttpContext.Current.Response.Write(FileText);
                //    HttpContext.Current.Response.End();
                //}
            }
            return null;

        }

    }

}
