Imports System.Xml
Imports System.Xml.XPath
Imports System.IO


Public Class WebServiceClient

    Private Shared singleton As WebServiceClient = Nothing

    Dim imageCacheDirectory As String = Directory.GetCurrentDirectory + "\cache\images\"
    Dim resultsCacheDirectory As String = Directory.GetCurrentDirectory + "\cache\resultXMLs\"
    Dim cacheKeyXPath As String = "//CacheKey"
    Dim cachedResultsLookup As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Dim timeOfPreviousCall As DateTime = Nothing
    Const minimumTimeBetweenCallsInMilliseconds As UInteger = 1000

    Public Shared Function GetClient() As WebServiceClient
        If (singleton Is Nothing) Then
            singleton = New WebServiceClient()
        End If

        Return singleton
    End Function

    Public Function RetrieveResult(ByVal url As String)
        If (cachedResultsLookup.ContainsKey(url)) Then
            Dim myCachedResultXML As XmlDocument = New XmlDocument()
            Dim path As String = cachedResultsLookup.Item(url)
            Dim successfulLoad As Boolean = True

            If (File.Exists(path)) Then
                Try
                    myCachedResultXML.Load(path)
                Catch ex As Exception
                    successfulLoad = False
                End Try
            Else
                successfulLoad = False
            End If

            If (successfulLoad) Then
                Return myCachedResultXML
            End If
        End If

        Dim myCallResultXML As XmlDocument = New XmlDocument()

        If (timeOfPreviousCall = Nothing) Then
            timeOfPreviousCall = DateTime.Now
        ElseIf (DateTime.Now.Subtract(timeOfPreviousCall).Milliseconds < minimumTimeBetweenCallsInMilliseconds) Then
            System.Threading.Thread.Sleep(minimumTimeBetweenCallsInMilliseconds - DateTime.Now.Subtract(timeOfPreviousCall).Milliseconds)
        End If

        Try
            myCallResultXML.Load(url)
        Catch ex As Exception
            myCallResultXML = Nothing
        End Try

        If (Not myCallResultXML Is Nothing) Then
            CommitResultFileToCache(url, myCallResultXML)
        End If

        timeOfPreviousCall = DateTime.Now

        Return myCallResultXML
    End Function

    Public Function RetrieveImageLocationToUse(ByVal url As String)
        Dim cachedImagePath As String = GenerateFilePathFromImageURL(url)

        Dim success As Boolean = True

        If (Not File.Exists(cachedImagePath)) Then
            success = CacheThisImage(url)
        End If

        If (success) Then
            Return cachedImagePath
        Else
            Return ""
        End If
    End Function

    Private Function CacheThisImage(ByVal url As String) As Boolean
        Dim cachedImagedPath As String = GenerateFilePathFromImageURL(url)
        Dim imageClient As System.Net.WebClient = New System.Net.WebClient

        Try
            imageClient.DownloadFile(url, cachedImagedPath)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Sub New()
        InitializeCache()
    End Sub

    Private Sub InitializeCache()
        If (Not Directory.Exists(resultsCacheDirectory)) Then
            Directory.CreateDirectory(resultsCacheDirectory)
        End If

        If (Not Directory.Exists(imageCacheDirectory)) Then
            Directory.CreateDirectory(imageCacheDirectory)
        End If

        Dim cachedResultsFilePaths As String() = Directory.GetFiles(resultsCacheDirectory, "*.xml", SearchOption.AllDirectories)

        PopulateCachedResultsLookup(cachedResultsFilePaths)
    End Sub

    Private Sub PopulateCachedResultsLookup(ByVal filePaths As String())
        For Each path As String In filePaths
            Dim key As String = RetrieveCacheKey(path)

            If (Not cachedResultsLookup.ContainsKey(key)) Then
                cachedResultsLookup.Add(key, path)
            End If
        Next
    End Sub

    Private Function RetrieveCacheKey(ByVal path As String)
        Dim resultsFile As XmlDocument = New XmlDocument
        resultsFile.Load(path)
        Return XMLGetValueAt(resultsFile, cacheKeyXPath, 0)
    End Function

    Private Function GenerateFilePathFromImageURL(ByVal url As String)
        Dim fileName As String = url.Replace(":", "").Replace("/", "").Replace("\", "").Replace("=", "").Replace("&", "")
        Dim path As String = imageCacheDirectory + fileName
        Return path
    End Function



    Private Sub CommitResultFileToCache(ByVal url As String, ByRef myCallResultXML As XmlDocument)
        Dim filePath As String = resultsCacheDirectory + Path.GetRandomFileName() + ".xml"
        Dim cacheKeyElement As XmlElement = myCallResultXML.CreateElement("CacheKey")
        cacheKeyElement.InnerText = url
        myCallResultXML.DocumentElement.AppendChild(cacheKeyElement)

        If (Not cachedResultsLookup.ContainsKey(url)) Then
            cachedResultsLookup.Add(url, filePath)
            myCallResultXML.Save(filePath)
        End If
    End Sub

    Public Function XMLGetValueAt(ByVal node As XmlNode, ByVal pathQuery As String, ByVal index As UInteger) As String
        Dim valueElement As Xml.XmlElement = Nothing

        Dim nodes As XmlNodeList = node.SelectNodes(pathQuery)

        If (nodes.Count >= index + 1) Then
            valueElement = nodes.Item(index)
            Return valueElement.InnerText
        Else
            Return Nothing
        End If
    End Function

    Public Function XMLGetValueAt(ByRef node As XmlNode, ByVal pathQuery As String, ByVal attributeName As String, ByVal attributeValue As String) As String
        Dim valueElement As Xml.XmlElement = Nothing

        Dim nodes As XmlNodeList = node.SelectNodes(pathQuery)

        For Each element As XmlElement In nodes
            If (element.GetAttribute(attributeName) = attributeValue) Then
                valueElement = element
                Exit For
            End If
        Next

        If (Not valueElement Is Nothing) Then
            Dim value As String = valueElement.InnerText
            Return value
        End If

        Return Nothing
    End Function
End Class
