Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions

Class MainWindow


    Private Sub Scrape()

        Dim text_formatted As String = ""

        Try
            Do Until text_formatted.Contains("price:")  'gets past Amazon captcha
                Dim url_string As String = "https://www.amazon.com/Mushkin-120GB-Enhanced-External-MKNSSDEC120GB/dp/B00UW7UYE8"

                Dim url_str_output As String = ""

                Dim response As WebResponse
                Dim request As WebRequest = HttpWebRequest.Create(url_string)

                response = request.GetResponse()

                Using sr As New StreamReader(response.GetResponseStream())
                    url_str_output = sr.ReadToEnd()
                    ' Close and clean up the StreamReader
                    sr.Close()
                End Using

                'Formatting Techniques

                ' Remove Doctype ( HTML 5 )
                url_str_output = Regex.Replace(url_str_output, "<!(.|\s)*?>", "")

                ' Remove HTML Tags
                url_str_output = Regex.Replace(url_str_output, "</?[a-z][a-z0-9]*[^<>]*>", "")

                ' Remove HTML Comments
                url_str_output = Regex.Replace(url_str_output, "<!--(.|\s)*?-->", "")

                ' Remove Script Tags
                url_str_output = Regex.Replace(url_str_output, "<script.*?</script>", "", RegexOptions.Singleline Or RegexOptions.IgnoreCase)

                ' Remove Stylesheets
                url_str_output = Regex.Replace(url_str_output, "<style.*?</style>", "", RegexOptions.Singleline Or RegexOptions.IgnoreCase)

                url_str_output.Replace("<style.*?</style>", "")
                url_str_output.Replace("<script.*?</script>", "")
                url_str_output.Replace("<!--(.|\s)*?-->", "")
                url_str_output.Replace("|", "")


                'this is the var that the url text is saved to
                text_formatted = url_str_output.Trim
            Loop




        Catch ex As Exception

            Console.WriteLine(ex.Message, "Error")

        End Try

    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Scrape()
    End Sub
End Class
