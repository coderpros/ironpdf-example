Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Http

Public Class PdfController
    Inherits ApiController

    ''' <summary>
    ''' Simply renders static HTML as a PDF.
    ''' </summary>
    ''' <returns>A PDF</returns>
    Public Function GetStaticDocument() As HttpResponseMessage
        Dim resultado As Byte() = Nothing
        Dim texto_html As String = "<html><head><title>Kilroy was here</title></head><body><h1>Kilroy was here</h1></body></html>"
        Dim renderer = New IronPdf.HtmlToPdf()

        renderer.PrintOptions.MarginTop = 5
        renderer.PrintOptions.MarginLeft = 2
        renderer.PrintOptions.PaperSize = IronPdf.PdfPrintOptions.PdfPaperSize.A4
        renderer.PrintOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen

        Try
            resultado = renderer.RenderHtmlAsPdf(texto_html).BinaryData
        Catch ex As Exception
            ' Handle ex here.
            Throw ex
        End Try

        Dim response = Request.CreateResponse(HttpStatusCode.OK)
        response.Content = new StreamContent(new MemoryStream(resultado))
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf")
        response.Content.Headers.ContentLength = resultado.Length

        Return response
    End Function

End Class
